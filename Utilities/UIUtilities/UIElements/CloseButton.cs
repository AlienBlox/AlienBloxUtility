using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIElements
{
    public class CloseButton : UIElement
    {
        public override void OnInitialize()
        {
            Width.Set(50, 0);
            Height.Set(50, 0);
            
            Left.Set(-25, 0);
            Top.Set(-25, 0);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 Pos = GetDimensions().Position();
            Texture2D ButtonTexture = ModContent.Request<Texture2D>("AlienBloxUtility/Common/Assets/CloseButton").Value;

            spriteBatch.Draw(ButtonTexture, Pos, Color.Red);

            this.SetUIBase(Language.GetText("Mods.AlienBloxUtility.Buttons.CloseButton").Value, true);
        }
    }
}