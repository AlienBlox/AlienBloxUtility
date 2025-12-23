using AlienBloxUtility.Utilities.Core;
using AlienBloxUtility.Utilities.Helpers;
using AlienBloxUtility.Utilities.UIUtilities.UIElements;
using AlienBloxUtility.Utilities.UIUtilities.UIRenderers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIStates
{
    public class ConHostSystem : UIState
    {
        public DraggableUIWrapper Conhost;

        public UIText ClearConsoleText, ExportConsoleText;

        public UIPanel MainPanel, SidePanel, CommandPanel, ClearConsole, ExportConsole, StopLuaExecution;

        public SpriteButton SendCommand;

        public UIScrollbar PanelScroll, ConSysScroll;
        public UIList BackingList, BackingConSysUI;

        public UITextBoxImproved CommandBox;

        public List<UIText> ConsoleText;

        public List<string> BackingString;

        public bool Fix;

        public override void OnInitialize()
        {
            ConsoleText = [];
            BackingString = [];

            SendCommand = new($"Terraria/Images/Item_{ItemID.PaperAirplaneA}", Language.GetText("Mods.AlienBloxUtility.UI.SendCmd"));
            ConSysScroll = new();
            PanelScroll = new();

            PanelScroll.OnScrollWheel += LuaManager.HotbarScrollFix;
            ConSysScroll.OnScrollWheel += LuaManager.HotbarScrollFix;

            BackingConSysUI = [];
            BackingList = [];

            ClearConsole = new();
            ExportConsole = new();

            ConSysScroll.HAlign = 1f;
            ConSysScroll.VAlign = .5f;
            ConSysScroll.Height.Set(0, 1);

            ClearConsole.Width.Set(0, 1f);
            //ClearConsole.Height.Set(0, .25f);
            ClearConsole.Height.Set(30, 0);
            ExportConsole.Width.Set(0, 1f);
            //ExportConsole.Height.Set(0, .25f);
            ExportConsole.Height.Set(30, 0);

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

            Conhost = new(new(650, 450), Vector2.Zero, new(0, 128, 0, 128), new(0, 0, 0), Language.GetText("Mods.AlienBloxUtility.UI.Conhost").Value, true, false);

            BackingConSysUI.VAlign = 0;
            BackingConSysUI.HAlign = .5f;
            BackingConSysUI.Height.Set(0, .95f);
            BackingConSysUI.Width.Set(0, 1f);
            BackingConSysUI.ManualSortMethod = (e) => { };
            BackingConSysUI.SetScrollbar(ConSysScroll);
            BackingConSysUI.Append(ConSysScroll);

            BackingList.Width.Percent = BackingList.Height.Percent = 1f;
            BackingList.SetScrollbar(PanelScroll);
            BackingList.Append(PanelScroll);
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
            //SendCommand.OnMouseOver += LockCommandBox;
            //SendCommand.OnMouseOut += UnlockCommandBox;
            SendCommand.OnLeftClick += RunCommand;

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
            SidePanel.Height.Set(0, 1);

            SidePanel.HAlign = 1f;

            MainPanel.Width.Set(0, 1f);
            MainPanel.Height.Set(0, 0.9f);
            MainPanel.HAlign = 0.5f;
            MainPanel.VAlign = 1f;

            MainPanel.SetPadding(5);

            SidePanel.Append(BackingList);

            Conhost.Append(MainPanel);
            MainPanel.Append(SidePanel);
            
            MainPanel.Append(CommandPanel);
            CommandPanel.Append(CommandBox);
            CommandBox.Append(SendCommand);
            CommandPanel.Append(BackingConSysUI);

            BackingList.AddRange([ClearConsole, ExportConsole]);

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

                Fix = true;
            }

            base.Update(gameTime);
        }

        public void AddConsoleText(string text)
        {
            SoundEngine.PlaySound(SoundID.MenuTick);

            UIText ConsoleTextVal = new(text, 0.7f);

            ConsoleTextVal.Width.Set(0, 1f);
            ConsoleTextVal.Height.Set(10f, 0f);
            ConsoleTextVal.TextOriginX = 0f;

            ConsoleText.Add(ConsoleTextVal);
            BackingConSysUI.Add(ConsoleTextVal);
            BackingString.Add(text);
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

        public void LockCommandBox(UIMouseEvent evt, UIElement element)
        {
            CommandBox.IgnoresMouseInteraction = true;
        }

        public void UnlockCommandBox(UIMouseEvent evt, UIElement element)
        {
            CommandBox.IgnoresMouseInteraction = false;
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