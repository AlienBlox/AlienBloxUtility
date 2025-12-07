using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.Abstracts
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
            base.Draw(spriteBatch);

            // Ensure the texture is loaded
            if (texture == null)
                return;

            // Get the current position and size of the element
            var dimensions = GetDimensions();
            Vector2 position = dimensions.Position();
            float width = dimensions.Width;
            float height = dimensions.Height;

            // Draw the top-left corner (fixed)
            spriteBatch.Draw(texture, position, new Rectangle(0, 0, sliceWidth, sliceHeight), Color.White);

            // Draw the top edge (stretched horizontally)
            spriteBatch.Draw(texture, new Vector2(position.X + sliceWidth, position.Y),
                new Rectangle(sliceWidth, 0, sliceWidth, sliceHeight), Color.White,
                0f, Vector2.Zero, new Vector2((width - 2 * sliceWidth) / sliceWidth, 1f), SpriteEffects.None, 0f);

            // Draw the top-right corner (fixed)
            spriteBatch.Draw(texture, new Vector2(position.X + width - sliceWidth, position.Y),
                new Rectangle(2 * sliceWidth, 0, sliceWidth, sliceHeight), Color.White);

            // Draw the left edge (stretched vertically)
            spriteBatch.Draw(texture, new Vector2(position.X, position.Y + sliceHeight),
                new Rectangle(0, sliceHeight, sliceWidth, sliceHeight), Color.White,
                0f, Vector2.Zero, new Vector2(1f, (height - 2 * sliceHeight) / sliceHeight), SpriteEffects.None, 0f);

            // Draw the center (stretched to fit)
            spriteBatch.Draw(texture, new Vector2(position.X + sliceWidth, position.Y + sliceHeight),
                new Rectangle(sliceWidth, sliceHeight, sliceWidth, sliceHeight), Color.White,
                0f, Vector2.Zero, new Vector2((width - 2 * sliceWidth) / sliceWidth, (height - 2 * sliceHeight) / sliceHeight),
                SpriteEffects.None, 0f);

            // Draw the right edge (stretched vertically)
            spriteBatch.Draw(texture, new Vector2(position.X + width - sliceWidth, position.Y + sliceHeight),
                new Rectangle(2 * sliceWidth, sliceHeight, sliceWidth, sliceHeight), Color.White,
                0f, Vector2.Zero, new Vector2(1f, (height - 2 * sliceHeight) / sliceHeight), SpriteEffects.None, 0f);

            // Draw the bottom-left corner (fixed)
            spriteBatch.Draw(texture, new Vector2(position.X, position.Y + height - sliceHeight),
                new Rectangle(0, 2 * sliceHeight, sliceWidth, sliceHeight), Color.White);

            // Draw the bottom edge (stretched horizontally)
            spriteBatch.Draw(texture, new Vector2(position.X + sliceWidth, position.Y + height - sliceHeight),
                new Rectangle(sliceWidth, 2 * sliceHeight, sliceWidth, sliceHeight), Color.White,
                0f, Vector2.Zero, new Vector2((width - 2 * sliceWidth) / sliceWidth, 1f), SpriteEffects.None, 0f);

            // Draw the bottom-right corner (fixed)
            spriteBatch.Draw(texture, new Vector2(position.X + width - sliceWidth, position.Y + height - sliceHeight),
                new Rectangle(2 * sliceWidth, 2 * sliceHeight, sliceWidth, sliceHeight), Color.White);
        }
    }
}
