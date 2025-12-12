using AlienBloxUtility.Utilities.Core;
using AlienBloxUtility.Utilities.UIUtilities.UIElements;
using AlienBloxUtility.Utilities.UIUtilities.UIRenderers;
using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIStates
{
    public class ConHostSystem : UIState
    {
        public DraggableUIWrapper Conhost;

        public UIButton<string> ClearConsole, ExportConsole;

        public UIText ClearConsoleText, ExportConsoleText;

        public UIPanel MainPanel, SidePanel, CommandPanel;

        public UIScrollbar PanelScroll;
        public UIList BackingList;

        public UITextBox CommandBox;

        public bool Fix;

        public override void OnInitialize()
        {
            PanelScroll = new();
            BackingList = [];

            ClearConsole = new(Language.GetText("Mods.AlienBloxUtility.UI.ClearConsole").Value);
            ExportConsole = new(Language.GetText("Mods.AlienBloxUtility.UI.SaveLogs").Value);

            ClearConsoleText = new(Language.GetText("Mods.AlienBloxUtility.UI.ClearConsole"));
            ExportConsoleText = new(Language.GetText("Mods.AlienBloxUtility.UI.SaveLogs").Value);

            ExportConsoleText.Width.Set(0, 1);
            ExportConsoleText.Height.Set(30, 0);

            ClearConsoleText.Width = ExportConsoleText.Width;
            ClearConsoleText.Height = ExportConsoleText.Height;

            Conhost = new(new(650, 450), Vector2.Zero, new(0, 128, 0, 128), new(0, 0, 0), Language.GetText("Mods.AlienBloxUtility.UI.Conhost").Value, true, false);

            BackingList.Width.Percent = BackingList.Height.Percent = 1f;
            BackingList.SetScrollbar(PanelScroll);
            BackingList.Append(PanelScroll);
            BackingList.VAlign = .5f;

            MainPanel = new();
            SidePanel = new();
            CommandPanel = new();
            CommandBox = new("");

            Conhost.SetPadding(15);

            CommandPanel.Width.Set(0, .75f);
            CommandPanel.Height.Set(0, .9f);
            CommandPanel.BackgroundColor = new(0, 0, 0);
            CommandPanel.BorderColor = new(255, 255, 255);

            CommandBox.SetTextMaxLength(40);
            CommandBox.Width.Set(0, .75f);
            CommandBox.Height.Set(-5, .05f);
            CommandBox.BackgroundColor = new(0, 128, 0, 128);
            CommandBox.VAlign = 1;
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

            ClearConsole.Append(ClearConsoleText);
            ExportConsole.Append(ExportConsoleText);

            BackingList.Add(ClearConsole);
            BackingList.Add(ExportConsole);

            BackingList.RecalculateChildren();

            SidePanel.Append(BackingList);

            Conhost.Append(MainPanel);
            MainPanel.Append(SidePanel);
            MainPanel.Append(CommandBox);
            MainPanel.Append(CommandPanel);

            Append(Conhost);
        }

        public override void Update(GameTime gameTime)
        {
            if (!Fix)
            {
                ClearConsole.SetText(Language.GetText("Mods.AlienBloxUtility.UI.ClearConsole").Value);
                ExportConsole.SetText(Language.GetText("Mods.AlienBloxUtility.UI.SaveLogs").Value);
                Conhost.Close.OnLeftClick += LeftClick;

                Fix = true;
            }

            base.Update(gameTime);
        }

        public static void LeftClick(UIMouseEvent evt, UIElement element)
        {
            DebugUtilityList.ConsoleWindowEnabled = false;

            ModContent.GetInstance<DebugPanelStackRender>().Element.buttons[0].Toggle = false;
        }
    }
}