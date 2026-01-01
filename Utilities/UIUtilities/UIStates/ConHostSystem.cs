using AlienBloxUtility.Utilities.Core;
using AlienBloxUtility.Utilities.Helpers;
using AlienBloxUtility.Utilities.UIUtilities.UIElements;
using AlienBloxUtility.Utilities.UIUtilities.UIRenderers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIStates
{
    public class ConHostSystem : UIState
    {
        public PanelV2 Conhost;

        public UIText ClearConsoleText, ExportConsoleText;

        public UIElement BackingElement;

        public UIPanel AssetInspectorMenu, MainPanel, SidePanel, CommandPanel, ClearConsole, ExportConsole, StopLuaExecution, CommandListPanel, CommandScrollBacking, ModalMask, AssetInspector;

        public SpriteButton SendCommand, CommandList, SearchButton;

        public UIScrollbar PanelScroll, ConSysScroll, CommandsMenuScroll;
        public UIList BackingList, BackingConSysUI, CommandsScroll, AssetInspectorBacker, AssetInspectorMenuThingy;

        public UITextBoxImproved CommandBox, SearchBar;

        public List<UIText> ConsoleText;

        public List<string> BackingString;

        public List<UIPanel> ExtraButtons;

        public bool Fix;

        private bool _canDoubleClickDisable;

        private bool _hasModal;

        public override void OnInitialize()
        {
            SetAssetTool();
            CommandsScroll = [];
            ConsoleText = [];
            BackingString = [];
            CommandsScroll = [];
            ExtraButtons = [];

            SearchButton = new($"Terraria/Images/Item_{ItemID.PaperAirplaneA}", Language.GetText("Mods.AlienBloxUtility.UI.SearchBar"));
            SendCommand = new($"Terraria/Images/Item_{ItemID.PaperAirplaneA}", Language.GetText("Mods.AlienBloxUtility.UI.SendCmd"));
            CommandList = new($"Terraria/Images/Item_{ItemID.Book}", Language.GetText("Mods.AlienBloxUtility.UI.CommandList"));
            ConSysScroll = new();
            PanelScroll = new();
            CommandsMenuScroll = new();
            StopLuaExecution = new();
            CommandListPanel = new();
            BackingElement = new();
            CommandScrollBacking = new();
            SearchBar = new("Search Command");
            ModalMask = new();
            
            AssetInspector = AddConhostPanel(Language.GetText("Mods.AlienBloxUtility.UI.AssetInspector"));

            AssetInspector.OnLeftClick += (_, _) =>
            {
                if (!_canDoubleClickDisable)
                {
                    SetModal(true, false, AssetInspectorMenu);
                }
            };

            ModalMask.Width.Set(0, 1);
            ModalMask.Height.Set(0, 1);
            ModalMask.BackgroundColor = new(0, 0, 0, 128);
            ModalMask.OnLeftDoubleClick += (_, _) =>
            {
                SetModal(false);
            };

            var text = ModalMask.InsertText(Language.GetText("Mods.AlienBloxUtility.UI.ModalNotice"));
            text.Height.Set(0, .1f);
            text.VAlign = 1;
            text.TextOriginX = 0;

            BackingElement.Width.Set(0, 1f);
            BackingElement.Height.Set(0, 1f);
            BackingElement.VAlign = 1;
            BackingElement.HAlign = .5f;

            PanelScroll.OnScrollWheel += LuaManager.HotbarScrollFix;
            ConSysScroll.OnScrollWheel += LuaManager.HotbarScrollFix;

            BackingConSysUI = [];
            BackingList = [];

            ClearConsole = new();
            ExportConsole = new();

            ConSysScroll.HAlign = 1f;
            ConSysScroll.VAlign = .5f;
            //ConSysScroll.Height.Set(0, 1);

            ClearConsole.Width.Set(0, 1f);
            ClearConsole.Height.Set(30, 0);
            ExportConsole.Width.Set(0, 1f);
            ExportConsole.Height.Set(30, 0);
            StopLuaExecution.Width.Set(0, 1f);
            StopLuaExecution.Height.Set(30, 0);
            StopLuaExecution.BackgroundColor = new(255, 0, 0);
            StopLuaExecution.InsertText(Language.GetText("Mods.AlienBloxUtility.UI.EmergencyStop"), .7f);
            StopLuaExecution.OnLeftClick += (_, _) =>
            {
                AddConsoleText($"Task Count: {AlienBloxUtility.CentralTokenStorage.Count}");
                AlienBloxUtility.CancelAll();
                AlienBloxUtility.CancelGlobal();
                AddConsoleText("Obliterated all tasks.");
            };

            ClearConsoleText = new(Language.GetText("Mods.AlienBloxUtility.UI.ClearConsole"), .7f);
            ExportConsoleText = new(Language.GetText("Mods.AlienBloxUtility.UI.SaveLogs"), .7f);

            ExportConsoleText.Width.Set(0, 1);
            ExportConsoleText.Height.Set(0, 1f);

            ClearConsoleText.IgnoresMouseInteraction = ExportConsoleText.IgnoresMouseInteraction = true;
            ClearConsoleText.Width.Percent = ExportConsoleText.Width.Percent;
            ClearConsoleText.Height.Percent = ExportConsoleText.Height.Percent;
            ClearConsoleText.VAlign = ExportConsoleText.VAlign = .5f;
            ClearConsoleText.HAlign = ExportConsoleText.HAlign = .5f;

            ClearConsole.OnMouseOver += HoverTick;
            ClearConsole.OnMouseOut += Unhover;
            ClearConsole.OnLeftClick += ClearConSysText;
            ExportConsole.OnMouseOver += HoverTick;
            ExportConsole.OnMouseOut += Unhover;
            ExportConsole.OnLeftClick += ExportConSysText;

            PanelScroll.HAlign = 1;
            PanelScroll.VAlign = .5f;

            Conhost = new(new(650, 450), Vector2.Zero, new(0, 128, 0, 128), new(0, 0, 0), Language.GetText("Mods.AlienBloxUtility.UI.Conhost").Value, true, false, 650, 450);

            BackingConSysUI.VAlign = 0;
            BackingConSysUI.HAlign = .5f;
            BackingConSysUI.Height.Set(0, .95f);
            BackingConSysUI.Width.Set(0, 1f);
            BackingConSysUI.ManualSortMethod = (e) => { };
            BackingConSysUI.SetScrollbar(ConSysScroll);
            BackingConSysUI.Append(ConSysScroll);

            BackingList.ManualSortMethod = (_) => { };
            
            BackingList.Width.Percent = BackingList.Height.Percent = 1f;
            BackingList.Append(PanelScroll);
            BackingList.SetScrollbar(PanelScroll);
            BackingList.VAlign = .5f;

            MainPanel = new();
            SidePanel = new();
            CommandPanel = new();
            CommandBox = new("Run Command...");

            Conhost.SetPadding(15);

            CommandPanel.VAlign = .5f;
            CommandPanel.HAlign = 0f;
            CommandPanel.Width.Set(0, .75f);
            CommandPanel.Height.Set(-10, 1f);
            CommandPanel.BackgroundColor = new(0, 0, 0);
            CommandPanel.BorderColor = new(255, 255, 255);

            SendCommand.Width.Set(0, .1f);
            SendCommand.Height.Set(0, 1f);
            SendCommand.VAlign = SendCommand.HAlign = 1f;
            SendCommand.OnLeftClick += RunCommand;

            CommandList.Width.Set(0, .1f);
            CommandList.Height.Set(0, 1f);
            CommandList.VAlign = CommandList.HAlign = .9f;
            CommandList.OnLeftClick += OpenCommandList;

            CommandListPanel.VAlign = .85f;
            CommandListPanel.HAlign = .5f;
            CommandListPanel.Width.Set(0, 1);
            CommandListPanel.Height.Set(0, .3f);
            CommandListPanel.SetPadding(0);

            SearchBar.Width.Set(0, 1);
            SearchBar.Height.Set(0, .2f);
            SearchBar.VAlign = 0;
            SearchBar.HAlign = .5f;

            CommandScrollBacking.Width.Set(0, 1f);
            CommandScrollBacking.Height.Set(0, .8f);
            CommandScrollBacking.VAlign = 1;
            CommandScrollBacking.HAlign = .5f;
            CommandScrollBacking.BackgroundColor = CommandScrollBacking.BorderColor = new(0, 0, 0, 0);

            CommandsMenuScroll.HAlign = 1f;
            CommandsMenuScroll.VAlign = .5f;

            CommandsScroll.Append(CommandsMenuScroll);
            CommandsScroll.SetScrollbar(CommandsMenuScroll);
            CommandScrollBacking.Append(CommandsScroll);

            CommandsScroll.Width.Set(0, 1f);
            CommandsScroll.Height.Set(0, 1f);

            SearchButton.Width.Set(0, .1f);
            SearchButton.Height.Set(0, 1);
            SearchButton.VAlign = .5f;
            SearchButton.HAlign = 1;
            SearchButton.OnLeftClick += Search;

            SearchBar.Append(SearchButton);

            CommandListPanel.BackgroundColor = new(0, 128, 0, 128);
            CommandListPanel.Append(SearchBar);
            CommandListPanel.Append(CommandScrollBacking);
            CommandListPanel.SetPadding(15);

            CommandBox.SetTextMaxLength(100);
            CommandBox.Width.Set(0, 1f);
            CommandBox.Height.Set(0, .05f);
            CommandBox.BackgroundColor = new(0, 128, 0, 128);
            CommandBox.VAlign = 1;
            CommandBox.HAlign = 0;
            CommandBox.IgnoresMouseInteraction = false;

            MainPanel.BackgroundColor = new(0, 128, 0);
            SidePanel.BackgroundColor = new(0, 175, 0);

            SidePanel.Width.Set(-5, .25f);
            SidePanel.Height.Set(0, 1f);

            SidePanel.HAlign = 1f;

            MainPanel.Width.Set(0, 1f);
            MainPanel.Height.Set(0, 1f);
            MainPanel.HAlign = 0.5f;
            MainPanel.VAlign = .5f;

            MainPanel.SetPadding(5);

            SidePanel.Append(BackingList);

            BackingElement.Append(MainPanel);
            Conhost.Append(BackingElement);
            MainPanel.Append(SidePanel);

            CommandBox.Append(SendCommand);
            CommandBox.Append(CommandList);
            MainPanel.Append(CommandPanel);
            CommandPanel.Append(CommandBox);
            CommandPanel.Append(BackingConSysUI);

            ExtraButtons.RemoveRange(0, 1);

            BackingList.AddRange(ExtraButtons);
            BackingList.AddRange([AssetInspector, ClearConsole, ExportConsole, StopLuaExecution]);

            ClearConsole.Append(ClearConsoleText);
            ExportConsole.Append(ExportConsoleText);

            Append(Conhost);
        }

        public override void Update(GameTime gameTime)
        {
            if (!Fix)
            {
                AddConsoleText(Language.GetText("Mods.AlienBloxUtility.UI.Console.ConWelcome").Format(AlienBloxUtility.Instance.Version));
                AddConsoleText(Language.GetTextValue("Mods.AlienBloxUtility.UI.Console.ConHelp"));

                ClearConsoleText.SetText(Language.GetText("Mods.AlienBloxUtility.UI.ClearConsole"));
                ExportConsoleText.SetText(Language.GetText("Mods.AlienBloxUtility.UI.SaveLogs"));
                Conhost.Close.OnLeftClick += LeftClick;
                BackingElement.MaxHeight.Set(-Conhost.Topbar.GetDimensions().Height, 1f);

                Search(null, null);
                //SetModal(true);

                SetupAssetThing();

                Fix = true;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Sets a new modal UI to the console.
        /// </summary>
        /// <param name="modalSet">Should the modal be enabled</param>
        /// <param name="elem">The element to add as the modal's main item</param>
        public void SetModal(bool modalSet, bool doubleClickDisable = false, UIElement elem = null)
        {
            _hasModal = modalSet;

            if (_hasModal)
            {
                BackingElement.Append(ModalMask);

                if (elem != null)
                {
                    ModalMask.Append(elem);
                }

                if (!doubleClickDisable)
                {
                    var text = ModalMask.InsertText(Language.GetText("Mods.AlienBloxUtility.UI.ModalNotice"));
                    text.Height.Set(0, .1f);
                    text.VAlign = 1;
                    text.TextOriginX = 0;
                }
            }
            else
            {
                ModalMask.RemoveAllChildren();
                ModalMask.Remove();
            }
        }

        public UIText AddConsoleText(string text)
        {
            try
            {
                SoundEngine.PlaySound(SoundID.MenuTick);

                UIText ConsoleTextVal = new(text, 0.7f);

                ConsoleTextVal.Width.Set(0, 1f);
                ConsoleTextVal.Height.Set(10f, 0f);
                ConsoleTextVal.TextOriginX = 0f;

                ConsoleText.Add(ConsoleTextVal);
                BackingConSysUI.Add(ConsoleTextVal);
                BackingString.Add(text);

                AlienBloxUtility.Instance.Logger.Debug(text);

                return ConsoleTextVal;
            }
            catch
            {
                return new("");
            }
        }

        public void ClearConSysText(UIMouseEvent evt, UIElement element)
        {
            for (int i = 0; i < ConsoleText.Count; i++)
            {
                UIElement T = ConsoleText[i];

                if (T is UIText text)
                {
                    BackingConSysUI.Remove(text);
                }
            }

            BackingString.Clear();
        }

        public void SetAssetTool()
        {
            AssetInspectorMenu = new();
            AssetInspectorMenu.Width.Set(0, .75f);
            AssetInspectorMenu.Height.Set(0, .75f);
            AssetInspectorMenu.VAlign = AssetInspectorMenu.HAlign = .5f;
            AssetInspectorMenu.BackgroundColor.A = 255;
            AssetInspectorMenu.SetPadding(0);  

            var AssetInspectorTopBar = new UIPanel
            {
                VAlign = 0,
                HAlign = .5f
            };

            AssetInspectorTopBar.Width.Set(0, 1);
            AssetInspectorTopBar.Height.Set(0, .1f);
            AssetInspectorTopBar.BackgroundColor.A = 255;
            AssetInspectorTopBar.BackgroundColor.R -= 30;
            AssetInspectorTopBar.BackgroundColor.G -= 30;
            AssetInspectorTopBar.BackgroundColor.B -= 30;
            AssetInspectorTopBar.InsertText(Language.GetText("Mods.AlienBloxUtility.UI.AssetTool"));

            AssetInspectorMenu.Append(AssetInspectorTopBar);

            var SideBar = new UIPanel()
            {
                VAlign = 1,
                HAlign = 0
            };

            var SidePanel = new UIPanel()
            {
                VAlign = 1,
                HAlign = 1
            };

            SideBar.Width.Set(0, .3f);
            SideBar.Height.Set(0, .9f);
            SideBar.SetPadding(0);

            UIList backer = [];
            FixedUIScrollbar scroll = new(UserInterface.ActiveInstance)
            {
                VAlign = .5f
            };

            backer.Width.Set(0, 1);
            backer.Height.Set(0, 1);
            backer.Append(scroll);
            backer.SetScrollbar(scroll);
            AssetInspectorBacker = backer;
            SideBar.Append(backer);

            SidePanel.Width.Set(0, .7f);
            SidePanel.Height.Set(0, .9f);

            var PanelBacking = new UIList();

            PanelBacking.VAlign = PanelBacking.HAlign = .5f;
            PanelBacking.Width.Set(0, 1);
            PanelBacking.Height.Set(0, 1);

            FixedUIScrollbar BackerScroll = new(UserInterface.ActiveInstance)
            {
                VAlign = .5f
            };

            BackerScroll.Height.Set(0, 1);
            PanelBacking.Append(BackerScroll);
            PanelBacking.SetScrollbar(BackerScroll);
            SidePanel.Append(PanelBacking);
            SidePanel.Append(BackerScroll);
            SidePanel.SetPadding(10);

            AssetInspectorMenuThingy = PanelBacking;
            //AssetInspectorMenuThingy.ManualSortMethod = (_) => { };

            SetupAssetThing();

            AssetInspectorMenu.Append(SideBar);
            AssetInspectorMenu.Append(SidePanel);
        }

        public void SetupAssetThing()
        {
            try
            {
                if (AlienBloxUtility.AlienBloxLogger == null)
                {
                    return;
                }

                var mods = ExternalTModInspection.GetAllModsLoaded();
                var UIE = mods.EnumerateToMenu();
                var UI = UIE.EnumerateToMenu();

                Dictionary<UIPanel, TmodFile> E = [];

                AlienBloxUtility.AlienBloxLogger?.Info(UI.Length.ToString());

                for (int i = 0; i < mods.Length; i++)
                {
                    E.Add(UI[i], mods[i]);

                    var txt = UI[i].InsertText(mods[i].Name + $" (v{mods[i].Version})");

                    UI[i].OnMiddleClick += (_, menu) =>
                    {
                        if (E.TryGetValue((UIPanel)menu, out var file))
                        {
                            AddConsoleText(file.DeriveProperty());
                        }
                    };

                    UI[i].OnRightClick += (_, menu) =>
                    {
                        if (E.TryGetValue((UIPanel)menu, out var file))
                        {
                            ExternalTModInspection.ExportToLocation(AlienBloxUtility.DecompLocation + "\\", file);
                        }
                    };

                    UI[i].OnLeftClick += (_, menu) =>
                    {
                        try
                        {   
                            AssetInspectorMenuThingy.Clear();

                            if (E.TryGetValue((UIPanel)menu, out var file))
                            {
                                var UIs = file.EnumerateToMenu();
                                var FESet = TModInspector.GetFESet(file);

                                for (int i = 0; i < UIs.Length; i++)
                                {
                                    UIs[i].InsertText(FESet[i].Name);
                                }

                                AlienBloxUtility.AlienBloxLogger.Info(UIs.Length.ToString());
                                AssetInspectorMenuThingy.AddRange(UIs);
                            } 
                        }
                        catch
                        {
                            menu.Remove();

                            AssetInspectorBacker.Recalculate();
                        }
                    };
                }

                AssetInspectorBacker.AddRange(UI);
            }
            catch
            {

            }
        }

        public void ExportConSysText(UIMouseEvent evt, UIElement element)
        {
            if (!Directory.Exists(AlienBloxUtility.LogLocation))
            {
                Directory.CreateDirectory(AlienBloxUtility.LogLocation);
            }

            Task.Run(async () => File.WriteAllTextAsync($"{AlienBloxUtility.LogLocation}\\Logs-{Guid.NewGuid()}.txt", BackingString.ToArray().MakeString()));
        }

        public void RunCommand(UIMouseEvent evt, UIElement element)
        {
            CmdHelperSystem.Call(CommandBox.Text, this);

            CommandBox.SetText(string.Empty);
        }

        public void OpenCommandList(UIMouseEvent evt, UIElement element)
        {
            if (CommandListPanel.Parent == null)
            {
                CommandPanel.Append(CommandListPanel);
            }
            else
            {
                CommandPanel.RemoveChild(CommandListPanel);
            }
        }

        public void Search(UIMouseEvent evt, UIElement element)
        {
            CommandsScroll.Clear();

            foreach (var item in CmdHelperSystem.GetCmdNames())
            {
                if (item.Contains(SearchBar.Text))
                {
                    var panel = new UIPanel();

                    panel.Width.Set(0, 1f);
                    panel.Height.Set(30, 0);
                    panel.InsertText(item);
                    panel.OnLeftClick += (_, _) =>
                    {
                        SoundEngine.PlaySound(SoundID.MenuTick);

                        CommandBox.SetText(item);
                    };

                    CommandsScroll.Add(panel);
                }
            }

            SearchBar.SetText(string.Empty);
        }

        /// <summary>
        /// Adds a new panel to the console's easy selection area
        /// </summary>
        /// <param name="text">The text to add.</param>
        /// <param name="bgColor">The background color of the panel</param>
        /// <returns>The panel for you to edit.</returns>
        public UIPanel AddConhostPanel(string text, Color? bgColor = default)
        {
            UIPanel panel = new();

            panel.InsertText(text, .7f);

            if (bgColor != null)
            {
                panel.BackgroundColor = bgColor.Value;
            }
            
            panel.OnMouseOver += HoverTick;
            panel.OnMouseOut += Unhover;
            panel.Width.Set(0, 1);
            panel.Height.Set(30, 0);

            ExtraButtons.Add(panel);

            return panel;
        }

        /// <summary>
        /// Adds a new panel to the console's easy selection area
        /// </summary>
        /// <param name="text">The text to add.</param>
        /// <param name="bgColor">The background color of the panel</param>
        /// <returns>The panel for you to edit.</returns>
        public UIPanel AddConhostPanel(LocalizedText text, Color? bgColor = default)
        {
            UIPanel panel = new();

            panel.InsertText(text, .7f);

            if (bgColor != null)
            {
                panel.BackgroundColor = bgColor.Value;
            }

            panel.OnMouseOver += HoverTick;
            panel.OnMouseOut += Unhover;
            panel.Width.Set(0, 1);
            panel.Height.Set(30, 0);

            ExtraButtons.Add(panel);

            return panel;
        }

        /// <summary>
        /// Adds a new panel to the console's easy selection area
        /// </summary>
        /// <param name="text">The text to add.</param>
        /// <param name="bgColor">The background color of the panel</param>
        /// <returns>The panel for you to edit.</returns>
        public static UIPanel StaticAddConhostPanel(string text, Color bgColor = default)
        {
            UIPanel panel = new();

            panel.InsertText(text, .7f);
            panel.BackgroundColor = bgColor;
            panel.OnMouseOver += HoverTick;
            panel.OnMouseOut += Unhover;
            panel.Width.Set(0, 1);
            panel.Height.Set(30, 0);

            ConHostRender.Instance.Element.ExtraButtons.Add(panel);

            return panel;
        }

        public static void LeftClick(UIMouseEvent evt, UIElement element)
        {
            DebugUtilityList.ConsoleWindowEnabled = false;

            ModContent.GetInstance<DebugPanelStackRender>().Element.buttons[0].Toggle = false;
        }

        public static void HoverTick(UIMouseEvent evt, UIElement element)
        {
            SoundEngine.PlaySound(SoundID.MenuTick);

            if (element is UIPanel panel)
            {
                panel.BorderColor = Color.White;
            }
        }

        public static void Unhover(UIMouseEvent evt, UIElement element)
        {
            if (element is UIPanel panel)
            {
                panel.BorderColor = Color.Black;
            }
        }
    }
}