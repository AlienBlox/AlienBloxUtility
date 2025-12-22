using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIStates
{
    public class DVDLogo : UIState
    {
        private Texture2D logoTexture;
        private Vector2 position;
        private Vector2 velocity;

        public override void OnInitialize()
        {
            logoTexture = ModContent.Request<Texture2D>("AlienBloxUtility/Common/Assets/DVD").Value;
            position = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2); // starting position
            velocity = new Vector2(3, 3); // pixels per frame
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            position += velocity;

            // Check screen bounds
            int screenWidth = Main.screenWidth;
            int screenHeight = Main.screenHeight;

            if (position.X <= 0 || position.X + logoTexture.Width >= screenWidth)
            {
                velocity.X *= -1; // bounce horizontally
            }

            if (position.Y <= 0 || position.Y + logoTexture.Height >= screenHeight)
            {
                velocity.Y *= -1; // bounce vertically
            }

            spriteBatch.Draw(logoTexture, position, Color.White);
        }
    }
}