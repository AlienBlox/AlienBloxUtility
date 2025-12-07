using AlienBloxUtility.Utilities.UIUtilities.UIElements;
using Microsoft.Xna.Framework;
using System.Linq;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIStates
{
    public class DebugPanelStack : UIState
    {
        private UIElement _myPanel;
        private ButtonIcon _consoleButton;
        private ButtonIcon _extrasButton;

        public override void OnInitialize()
        {
            _myPanel = new UIElement();
            _myPanel.Width.Set(200f * 2, 0f); // Set width
            _myPanel.Height.Set(40f, 0f); // Set height
            _myPanel.HAlign = .5f;
            _myPanel.VAlign = 0.05f;

            _consoleButton = CreateChild("Mods.AlienBloxUtility.Buttons.ConsoleButton");
            _extrasButton = CreateChild("Mods.AlienBloxUtility.Buttons.ExtrasButton");
            _myPanel.Append(_consoleButton);
            _myPanel.Append(_extrasButton);

            Append(_myPanel);
        }

        private ButtonIcon CreateChild(string LocalizationKey)
        {
            ButtonIcon child = new ButtonIcon(LocalizationKey);
            child.Height.Set(40f, 0f); // Set a fixed height for the children
            child.Width.Set(0f, 1f); // Allow the text to auto-size based on its content

            // Calculate the positioning for each child element
            int childCount = _myPanel.Children.Count();
            float spacing = 10f; // Space between children

            // Calculate total width used by children and the remaining space
            float totalChildrenWidth = childCount * child.Width.Pixels;
            float totalSpacing = (childCount - 1) * spacing;
            float remainingWidth = _myPanel.Width.Pixels - totalChildrenWidth - totalSpacing;

            // Calculate the starting position for the first child (centered)
            float startX = spacing + (remainingWidth / (childCount + 1));

            // Set the Left position based on the calculated start position
            child.Left.Set(startX + (child.Width.Pixels + spacing) * (childCount - 1), 0f);
            child.Top.Set(25f, 0f); // Set the Y position to center it vertically within the parent

            // Add the child to the parent panel
            _myPanel.Append(child);

            return child;         
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}