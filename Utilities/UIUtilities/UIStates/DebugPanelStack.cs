using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIStates
{
    public class DebugPanelStack : UIState
    {
        private UIElement _myPanel;
        private List<UIElement> Buttons;

        public override void OnInitialize()
        {
            _myPanel = new UIElement();
            _myPanel.Width.Set(200f, 0f); // Set width
            _myPanel.Height.Set(50f, 0f); // Set height
            _myPanel.HAlign = .5f;
            _myPanel.VAlign = 0.05f;
            Append(_myPanel);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}