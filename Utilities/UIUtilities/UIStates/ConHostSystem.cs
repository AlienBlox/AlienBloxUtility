using AlienBloxUtility.Utilities.Core;
using AlienBloxUtility.Utilities.UIUtilities.UIElements;
using AlienBloxUtility.Utilities.UIUtilities.UIRenderers;
using Microsoft.Xna.Framework;
using Terraria;
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

        public UIPanel MainPanel, SidePanel, CommandPanel, ClearConsole, ExportConsole;

        public SpriteButton SendCommand;

        public UIScrollbar PanelScroll, ConSysScroll;
        public UIList BackingList, BackingConSysUI;

        public UITextBoxImproved CommandBox;

        public bool Fix;

        public override void OnInitialize()
        {
            SendCommand = new($"Terraria/Images/Item_{ItemID.PaperAirplaneA}", Language.GetText("Mods.AlienBloxUtility.UI.SendCmd"));
            ConSysScroll = new();
            PanelScroll = new();

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

            PanelScroll.HAlign = 1;
            PanelScroll.VAlign = .5f;

            Conhost = new(new(650, 450), Vector2.Zero, new(0, 128, 0, 128), new(0, 0, 0), Language.GetText("Mods.AlienBloxUtility.UI.Conhost").Value, true, false);

            BackingConSysUI.VAlign = 0;
            BackingConSysUI.HAlign = .5f;
            BackingConSysUI.Height.Set(0, .95f);
            BackingConSysUI.Width.Set(0, 1f);
            BackingConSysUI.SetScrollbar(ConSysScroll);
            BackingConSysUI.Append(ConSysScroll);

            BackingList.Width.Percent = BackingList.Height.Percent = 1f;
            BackingList.SetScrollbar(PanelScroll);
            BackingList.Append(PanelScroll);
            BackingList.VAlign = .5f;

            MainPanel = new();
            SidePanel = new();
            CommandPanel = new();
            CommandBox = new("");

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
                ClearConsoleText.SetText(Language.GetText("Mods.AlienBloxUtility.UI.ClearConsole"));
                ExportConsoleText.SetText(Language.GetText("Mods.AlienBloxUtility.UI.SaveLogs"));
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