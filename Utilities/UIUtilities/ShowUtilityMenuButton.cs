using AlienBloxUtility.Utilities.Abstracts;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities
{
    public class ShowUtilityMenuButton : UIState
    {
        private AlienBloxUtilityUIBase _UI;

        public override void OnInitialize()
        {
            _UI = new();
            _UI.Width.Set(50f, 0f);
            _UI.Height.Set(50f, 0f);
            _UI.Left.Set(-10f, 0f);
            _UI.Top.Set(-10f, 0f);
            _UI.HAlign = 0f;  // Align to the right (1 = right edge of the screen)
            _UI.VAlign = 1f;  // Align to the bottom (1 = bottom edge of the screen)

            var buttonText = new UIText("Test")
            {
                HAlign = 0.5f,  // Center the text horizontally
                VAlign = 0.5f  // Center the text vertically
            };
            _UI.Append(buttonText);

            Append(_UI);
        }
    }
}