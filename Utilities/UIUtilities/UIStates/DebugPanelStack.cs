using AlienBloxUtility.Utilities.Core;
using AlienBloxUtility.Utilities.UIUtilities.UIElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIStates
{
    public class DebugPanelStack : UIState
    {
        private UIElement _myPanel;

        private List<ButtonIcon> _buttons;

        private bool SetData = false;

        public override void OnInitialize()
        {
            _myPanel = new UIElement();

            _buttons = CreateButtonRow(_myPanel, 6, 50, 50, 10, "Mods.AlienBloxUtility.Buttons.ConsoleButton", ItemID.IronPickaxe, Color.White);

            Append(_myPanel);
        }

        private List<ButtonIcon> CreateButtonRow(UIElement parent, int buttonCount, int buttonWidth, int buttonHeight, int spacing, string key, int itemID, Color selectorColor)
        {
            List<ButtonIcon> buttonList = new List<ButtonIcon>();

            parent.Width.Set(buttonWidth * buttonCount + (spacing * buttonCount), 0f);
            parent.Height.Set(buttonHeight, 0f);
            parent.Left.Set((_myPanel.Width.Pixels / 4) - (spacing * buttonCount) / 2 - buttonWidth + spacing / 2, 0);
            parent.HAlign = .5f;
            parent.VAlign = 0.05f;

            for (int i = 0; i < buttonCount; i++)
            {
                // Create a new button
                ButtonIcon button = new(key, itemID, selectorColor);
                button.Width.Set(buttonWidth, 0f);
                button.Height.Set(buttonHeight, 0f);
                button.Left.Set(i * (buttonWidth + spacing), 0f); // Space buttons horizontally
                button.Top.Set(0, 0f); // Keep the buttons aligned at the top

                button.OnMouseOver += button.OnHover;
                button.OnLeftClick += button.OnToggle;

                // Add click event for the button
                int index = i; // Capture index for the click event

                // Add the button to the parent panel
                parent.Append(button);

                // Add the button to the list to return later
                buttonList.Add(button);
            }

            // Return the list of created buttons
            return buttonList;
        }

        public override void Update(GameTime gameTime)
        {
            if (!SetData)
            {
                _buttons[0].SetStats(ItemID.GravediggerShovel, "Mods.AlienBloxUtility.Buttons.ConsoleButton", Color.Green);
                _buttons[0].OnLeftClick += ToggleConsole;

                _buttons[1].SetStats(ItemID.Zenith, "Mods.AlienBloxUtility.Buttons.StatsButton", Color.Purple);
                _buttons[1].OnLeftClick += ToggleStatsButton;

                _buttons[2].SetStats(ItemID.DirtBlock, "Mods.AlienBloxUtility.Buttons.DecompilerButton", Color.Brown);
                _buttons[2].OnLeftClick += ToggleDecompilerButton;

                _buttons[3].SetStats(ItemID.PaperAirplaneA, "Mods.AlienBloxUtility.Buttons.PacketSpyButton", Color.MintCream);
                _buttons[3].OnLeftClick += TogglePacketSpyButton;

                _buttons[4].SetStats(ItemID.FlaskofNanites, "Mods.AlienBloxUtility.Buttons.MilkerButton", Color.LightBlue);
                _buttons[4].OnLeftClick += ToggleStatsButton;

                _buttons[5].SetStats(ItemID.AngelStatue, "Mods.AlienBloxUtility.Buttons.ExtrasButton", Color.DarkSlateGray);
                _buttons[5].OnLeftClick += ToggleExtrasMenuButton;

                SetData = true;
            }
            

            base.Update(gameTime);
        }

        public static void ToggleConsole(UIMouseEvent Evt, UIElement Element)
        {
            DebugUtilityList.ConsoleWindowEnabled = !DebugUtilityList.ConsoleWindowEnabled;
        }

        public static void ToggleStatsButton(UIMouseEvent Evt, UIElement Element)
        {
            DebugUtilityList.StatsMenuEnabled = !DebugUtilityList.StatsMenuEnabled;
        }

        public static void ToggleDecompilerButton(UIMouseEvent Evt, UIElement Element)
        {
            DebugUtilityList.DecompilerMenuEnabled = !DebugUtilityList.DecompilerMenuEnabled;
        }

        public static void TogglePacketSpyButton(UIMouseEvent Evt, UIElement Element)
        {
            DebugUtilityList.PacketSpyEnabled = !DebugUtilityList.PacketSpyEnabled;
        }

        public static void ToggleSystemStatsButton(UIMouseEvent Evt, UIElement Element)
        {
            DebugUtilityList.MilkerEnabled = !DebugUtilityList.MilkerEnabled;
        }

        public static void ToggleExtrasMenuButton(UIMouseEvent Evt, UIElement Element)
        {
            DebugUtilityList.ExtrasMenuEnabled = !DebugUtilityList.ExtrasMenuEnabled;
        }
    }
}