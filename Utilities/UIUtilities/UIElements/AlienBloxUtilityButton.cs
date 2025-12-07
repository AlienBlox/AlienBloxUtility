using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
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
            Vector2 position = GetDimensions().Position();

            spriteBatch.Draw(ModContent.Request<Texture2D>("Terraria/Images/UI/ButtonPlay").Value, position, new Color(255, 255, 255, 128));

            spriteBatch.Draw(ActivateButtonIcon, position, new Color(255, 255, 255, 128));
            spriteBatch.Draw(ActivateButtonOutline, position, Color.White);
            spriteBatch.Draw(PowerButtonIcon, position, Color.White);

            base.Draw(spriteBatch);
        }
    }
}