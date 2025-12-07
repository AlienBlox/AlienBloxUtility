using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIElements
{
    public class AlienBloxUtilityUIBase : UIElement
    {
        private Texture2D texture;

        private int sliceWidth;
        private int sliceHeight;

        public AlienBloxUtilityUIBase(string Location = "AlienBloxUtility/Common/Assets/UIGrid")
        {
            // Load the 9-slice texture from the mod's assets
            texture = ModContent.Request<Texture2D>(Location).Value;

            // Define the 9 slices, assuming the image is 3x3 grid of equal size
            if (texture == null)
            {
                Main.NewText("ERROR: Texture not found!");
            }

            // Assuming the texture is a 3x3 grid of slices
            sliceWidth = texture.Width / 3;
            sliceHeight = texture.Height / 3;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(Main.screenWidth / 2, Main.screenHeight / 2), Color.White);

            base.Draw(spriteBatch);

            // Ensure the texture is loaded
            if (texture == null)
                return;

            var dimensions = GetDimensions();
            Vector2 position = dimensions.Position();
            float width = dimensions.Width;
            float height = dimensions.Height;

            //Main.NewText($"Position: {position} Width: {width} Height: {height}");

            spriteBatch.Draw(texture, position, new Rectangle(0, 0, sliceWidth, sliceHeight), Color.White);

            // Draw top edge (stretch horizontally)
            spriteBatch.Draw(texture, new Vector2(position.X + sliceWidth, position.Y),
                new Rectangle(sliceWidth, 0, sliceWidth, sliceHeight), Color.White, 0f, Vector2.Zero,
                new Vector2((width - 2 * sliceWidth) / sliceWidth, 1f), SpriteEffects.None, 0f);

            // Draw top-right corner
            spriteBatch.Draw(texture, new Vector2(position.X + width - sliceWidth, position.Y),
                new Rectangle(2 * sliceWidth, 0, sliceWidth, sliceHeight), Color.White);

            // Draw left edge (stretch vertically)
            spriteBatch.Draw(texture, new Vector2(position.X, position.Y + sliceHeight),
                new Rectangle(0, sliceHeight, sliceWidth, sliceHeight), Color.White, 0f, Vector2.Zero,
                new Vector2(1f, (height - 2 * sliceHeight) / sliceHeight), SpriteEffects.None, 0f);

            // Draw center (stretch both horizontally and vertically)
            spriteBatch.Draw(texture, new Vector2(position.X + sliceWidth, position.Y + sliceHeight),
                new Rectangle(sliceWidth, sliceHeight, sliceWidth, sliceHeight), Color.White, 0f, Vector2.Zero,
                new Vector2((width - 2 * sliceWidth) / sliceWidth, (height - 2 * sliceHeight) / sliceHeight),
                SpriteEffects.None, 0f);

            // Draw right edge (stretch vertically)
            spriteBatch.Draw(texture, new Vector2(position.X + width - sliceWidth, position.Y + sliceHeight),
                new Rectangle(2 * sliceWidth, sliceHeight, sliceWidth, sliceHeight), Color.White, 0f, Vector2.Zero,
                new Vector2(1f, (height - 2 * sliceHeight) / sliceHeight), SpriteEffects.None, 0f);

            // Draw bottom-left corner
            spriteBatch.Draw(texture, new Vector2(position.X, position.Y + height - sliceHeight),
                new Rectangle(0, 2 * sliceHeight, sliceWidth, sliceHeight), Color.White);

            // Draw bottom edge (stretch horizontally)
            spriteBatch.Draw(texture, new Vector2(position.X + sliceWidth, position.Y + height - sliceHeight),
                new Rectangle(sliceWidth, 2 * sliceHeight, sliceWidth, sliceHeight), Color.White, 0f, Vector2.Zero,
                new Vector2((width - 2 * sliceWidth) / sliceWidth, 1f), SpriteEffects.None, 0f);

            // Draw bottom-right corner
            spriteBatch.Draw(texture, new Vector2(position.X + width - sliceWidth, position.Y + height - sliceHeight),
                new Rectangle(2 * sliceWidth, 2 * sliceHeight, sliceWidth, sliceHeight), Color.White);
        }
    }
}
