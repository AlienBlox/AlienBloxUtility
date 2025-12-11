using AlienBloxUtility.Utilities.Core;
using AlienBloxUtility.Utilities.UIUtilities.UIElements;
using AlienBloxUtility.Utilities.UIUtilities.UIRenderers;
using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIStates
{
    public class ConHostSystem : UIState
    {
        public DraggableUIWrapper Conhost;

        public UIPanel MainPanel;
        public UIPanel SidePanel;
        public UIPanel CommandPanel;
        public UITextBox CommandBox;

        public bool Fix;

        public override void OnInitialize()
        {
            Conhost = new(new(650, 450), Vector2.Zero, new(0, 128, 0, 128), new(0, 0, 0), Language.GetText("Mods.AlienBloxUtility.UI.Conhost").Value, true, false);

            MainPanel = new();
            SidePanel = new();
            CommandPanel = new();
            CommandBox = new(Language.GetText("Mods.AlienBloxUtility.UI.ConTypeLine").Value);

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
            CommandBox.SetText(Language.GetText("Mods.AlienBloxUtility.UI.ConTypeLine").Value);

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