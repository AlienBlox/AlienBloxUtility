using AlienBloxUtility.Utilities.Helpers;
using AlienBloxUtility.Utilities.LuaHelpers;
using AlienBloxUtility.Utilities.UIUtilities.UIElements;
using AlienBloxUtility.Utilities.UIUtilities.UIRenderers;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIStates
{
    public class LuaManager : UIState
    {
        public PanelV2 BackingPanel, ScriptInstaller;

        public UIPanel ScriptManagerBacking, InfoPanel, RefreshScripts, DownloadScript, OpenLuaFolder;

        public UIElement BackerElement, InstallScript;

        public UIScrollbar Scrollbar, ButtonBarScroll;

        public UIList BackingList, ButtonBar;

        public UITextBoxImproved TextBox;

        public bool Fix;

        public bool LuaInstallerMenu = false;

        public override void OnInitialize()
        {
            ScriptInstaller = new(new(550, 120), Vector2.Zero, new(0, 150, 0), Color.Black, "Mods.AlienBloxUtility.UI.DownloadLua", true, minWidth: 400, minHeight: 120);
            BackingPanel = new(new(500, 400), Vector2.Zero, new(0, 150, 0), Color.Black, "Mods.AlienBloxUtility.UI.LuaDisplay", true, minWidth: 500, minHeight: 400);
            BackingList = [];
            ScriptManagerBacking = new();
            InfoPanel = new();
            RefreshScripts = new();
            Scrollbar = new();
            BackerElement = new();
            DownloadScript = new();
            ButtonBar = [];
            ButtonBarScroll = new();
            OpenLuaFolder = new();

            DownloadScript.Width.Set(0, 1f);
            DownloadScript.Height.Set(30, 0f);
            DownloadScript.InsertText(Language.GetText("Mods.AlienBloxUtility.UI.DownloadLua"));
            DownloadScript.OnMouseOver += HoverOn;
            DownloadScript.OnMouseOut += HoverOff;
            DownloadScript.OnLeftClick += ShowInstallMenu;

            RefreshScripts.Width.Set(0, 1f);
            RefreshScripts.Height.Set(30, 0f);
            RefreshScripts.InsertText(Language.GetText("Mods.AlienBloxUtility.UI.RefreshLua"));
            RefreshScripts.OnMouseOver += HoverOn;
            RefreshScripts.OnMouseOut += HoverOff;
            RefreshScripts.OnLeftClick += ReloadScripts;

            OpenLuaFolder.Width.Set(0, 1f);
            OpenLuaFolder.Height.Set(30, 0f);
            OpenLuaFolder.InsertText(Language.GetText("Mods.AlienBloxUtility.UI.OpenLuaFolder"));
            OpenLuaFolder.OnLeftClick += Lua;
            OpenLuaFolder.OnMouseOver += HoverOn;
            OpenLuaFolder.OnMouseOut += HoverOff;

            ButtonBarScroll.VAlign = .5f;

            ButtonBar.ManualSortMethod = (_) => { };
            ButtonBar.Add(DownloadScript);
            ButtonBar.Add(RefreshScripts);
            ButtonBar.Add(OpenLuaFolder);

            BackerElement.Width.Set(0, 1);
            BackerElement.Height.Set(-30, 1);
            BackerElement.HAlign = .5f;
            BackerElement.VAlign = 1;
            BackerElement.SetPadding(15);

            ScriptManagerBacking.Width.Set(0, .6f);
            ScriptManagerBacking.Height.Set(0, 1f);
            ScriptManagerBacking.VAlign = 0.5f;

            Scrollbar.OnScrollWheel += HotbarScrollFix;
            Scrollbar.VAlign = .5f;

            BackingList.VAlign = 0.5f;
            BackingList.HAlign = 0.5f;
            BackingList.Height.Set(0, 1);
            BackingList.Width.Set(0, 1);
            BackingList.ManualSortMethod = (_) => { };
            BackingList.Append(Scrollbar);
            BackingList.SetScrollbar(Scrollbar);

            InfoPanel.Width.Set(0, .4f);
            InfoPanel.Height.Set(0, 1f);
            InfoPanel.VAlign = 0.5f;
            InfoPanel.HAlign = 1f;
            InfoPanel.BackgroundColor = new(0, 255, 0);

            ButtonBar.Width.Set(0, 1);
            ButtonBar.Height.Set(0, 1);
            ButtonBar.Append(ButtonBarScroll);
            ButtonBar.SetScrollbar(ButtonBarScroll);

            ScriptManagerBacking.Append(BackingList);
            InfoPanel.Append(ButtonBar);
            BackerElement.Append(ScriptManagerBacking);
            BackerElement.Append(InfoPanel);
            BackingPanel.Append(BackerElement); 

            Append(BackingPanel);
            Append(ScriptInstaller);
        }

        public override void Update(GameTime gameTime)
        {
            if (!Fix)
            {
                ReloadScripts(null, null);

                BackingPanel.Close.OnLeftClick += (_, _) =>
                {
                    LuaManagerRender.LuaManagerEnabled = !LuaManagerRender.LuaManagerEnabled;
                };

                InstallScript = new()
                {
                    HAlign = 1,
                    VAlign = .5f
                };

                InstallScript.Width.Set(0, 0.1f);
                InstallScript.Height.Set(0, 1f);
                InstallScript.MaxWidth.Set(30, 0);
                InstallScript.InsertText($"[i:{ItemID.PaperAirplaneA}]");
                InstallScript.OnLeftClick += InstallerForScript;

                ScriptInstaller.Topbar.Height.Set(35, 0);
                ScriptInstaller.Close.OnLeftClick += ShowInstallMenu;

                TextBox = new(Language.GetText("Mods.AlienBloxUtility.UI.DownloadingScript").Value)
                {
                    TextScale = 0.7f
                };
                TextBox.Width.Set(0, .9f);
                TextBox.Height.Set(0, .25f);
                TextBox.VAlign = .6f;
                TextBox.HAlign = .5f;
                TextBox.SetTextMaxLength(120);

                TextBox.Append(InstallScript);
                ScriptInstaller.Append(TextBox);

                Fix = true;
            }

            if (!LuaInstallerMenu)
            {
                RemoveChild(ScriptInstaller);
            }
            else
            {
                Append(ScriptInstaller);
            }

            base.Update(gameTime);
        }

        public void ReloadScripts(UIEvent evt, UIElement element)
        {
            BackingList.Clear();

            try
            {
                foreach (string s in Directory.GetFiles(AlienBloxUtility.LuaStorageLocation))
                {
                    LuaPanel Panel = new(s);

                    Panel.Width.Set(0, 1f);
                    Panel.Height.Set(30, 0);
                    Panel.OnLeftClick += Panel.DoLua;
                    Panel.OnRightClick += Panel.LuaInfo;
                    BackingList.Add(Panel);
                }
            }
            catch (Exception e)
            {
                ConHostRender.Write($"{e.GetType().Name}: {e.Message}");
            }
        }

        public void ShowInstallMenu(UIEvent evt, UIElement element)
        {
            LuaInstallerMenu = !LuaInstallerMenu;
        }

        public void InstallerForScript(UIEvent evt, UIElement element)
        {
            SoundEngine.PlaySound(SoundID.MenuTick);

            try
            {
                Task.Run( async () => LuaFileManagement.Request(TextBox.Text, AlienBloxUtility.LuaStorageLocation, $"LuaFile-{new Guid()}") );

                //ConHostRender.Write("Lua file installed!");
            }
            catch (Exception E)
            {
                ConHostRender.Write($"{E.GetType().Name}: {E.Message}");
            }

            TextBox.SetText(string.Empty);
        }

        public static void Lua(UIEvent evt, UIElement element)
        {
            UrlEngine.OpenURL(AlienBloxUtility.LuaStorageLocation + "\\");
        }

        public static void HotbarScrollFix(UIScrollWheelEvent evt, UIElement listeningElement) => Main.LocalPlayer.ScrollHotbar(PlayerInput.ScrollWheelDelta / 120);

        public static void HoverOn(UIEvent evt, UIElement ListeningElement)
        {
            SoundEngine.PlaySound(SoundID.MenuTick);

            if (ListeningElement is UIPanel panel)
            {
                panel.BorderColor = Color.White;
            }
        }

        public static void HoverOff(UIEvent evt, UIElement ListeningElement)
        {
            SoundEngine.PlaySound(SoundID.MenuTick);

            if (ListeningElement is UIPanel panel)
            {
                panel.BorderColor = Color.Black;
            }
        }
    }
}