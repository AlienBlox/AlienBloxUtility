using AlienBloxUtility.Utilities.Abstracts;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities
{
    public class ShowUtilityMenuButton : UIState
    {
        private TemplateUI _UI;

        public override void OnInitialize()
        {
            _UI = new TemplateUI(ModContent.Request<Texture2D>("AlienBloxUtility/Common/Assets/UIGrid").Value);
            _UI.Width.Set(200f, 0f);
            _UI.Height.Set(50f, 0f);
            _UI.Left.Set(-25f, 0f);
            _UI.Top.Set(-25f, 0f);
            _UI.HAlign = 0f;  // Align to the right (1 = right edge of the screen)
            _UI.VAlign = 1f;  // Align to the bottom (1 = bottom edge of the screen)

            var buttonText = new UIText("Test");
            buttonText.HAlign = 0.5f;  // Center the text horizontally
            buttonText.VAlign = 0.5f;  // Center the text vertically
            _UI.Append(buttonText);

            Append(_UI);
        }
    }
}