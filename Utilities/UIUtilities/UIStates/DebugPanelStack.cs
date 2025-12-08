using AlienBloxUtility.Utilities.UIUtilities.UIElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
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

        public override void OnInitialize()
        {
            _myPanel = new UIElement();
            _myPanel.Width.Set(50 * 5f * 2, 0f); // Set width
            _myPanel.Height.Set(50f, 0f); // Set height
            _myPanel.Left.Set(_myPanel.Width.Pixels / 4, 0);
            _myPanel.HAlign = .5f;
            _myPanel.VAlign = 0.05f;

            _buttons = CreateButtonRow(_myPanel, 5, 50, 50, 0, "Mods.AlienBloxUtility.Buttons.ConsoleButton", ItemID.IronPickaxe);

            Append(_myPanel);
        }

        private List<ButtonIcon> CreateButtonRow(UIElement parent, int buttonCount, int buttonWidth, int buttonHeight, int spacing, string key, int itemID)
        {
            List<ButtonIcon> buttonList = new List<ButtonIcon>();

            //parent.Width.Set(buttonWidth * buttonCount + (spacing * buttonCount), 0f);
            //parent.Height.Set(buttonHeight, 0f);
            //parent.Left.Set(_myPanel.Width.Pixels / 4, 0);

            for (int i = 0; i < buttonCount; i++)
            {
                // Create a new button
                ButtonIcon button = new(key, itemID);
                button.Width.Set(buttonWidth, 0f);
                button.Height.Set(buttonHeight, 0f);
                button.Left.Set(i * (buttonWidth + spacing), 0f); // Space buttons horizontally
                button.Top.Set(0, 0f); // Keep the buttons aligned at the top

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
            base.Update(gameTime);
        }
    }
}