using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIElements
{
    public class AlienBloxUtilityButton : UIElement
    {
        private readonly Texture2D PowerButtonIcon;
        private readonly Texture2D ActivateButtonIcon;
        private readonly Texture2D ActivateButtonOutline;

        public AlienBloxUtilityButton()
        {
            ActivateButtonIcon = ModContent.Request<Texture2D>("AlienBloxUtility/Common/Assets/ActivateButton").Value;
            ActivateButtonOutline = ModContent.Request<Texture2D>("AlienBloxUtility/Common/Assets/ActivateButtonOutline").Value;
            PowerButtonIcon = ModContent.Request<Texture2D>("AlienBloxUtility/Common/Assets/PowerButtonIcon").Value;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 Position = new(GetDimensions().X, GetDimensions().Y);

            spriteBatch.Draw(ActivateButtonIcon, Position, new Color(255, 255, 255, 128));
            spriteBatch.Draw(ActivateButtonOutline, Position, new Color(255, 255, 255, 128));
            spriteBatch.Draw(PowerButtonIcon, Position, new Color(255, 255, 255, 128));

            base.Draw(spriteBatch);
        }
    }
}