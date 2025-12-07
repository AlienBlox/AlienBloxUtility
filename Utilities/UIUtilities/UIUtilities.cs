using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities
{
    public static class UIUtilities
    {
        /// <summary>
        /// Quickly draws a 9-slice UI system
        /// </summary>
        /// <param name="uiState"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="texture"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="color"></param>
        public static void DrawNineSlice(this UIState uiState, SpriteBatch spriteBatch, Texture2D texture, float x, float y, float width, float height, Color color)
        {
            // The texture is expected to be a 3x3 grid (total 9 slices)
            int sliceWidth = texture.Width / 3;   // Slice width (assuming 3x3 grid)
            int sliceHeight = texture.Height / 3;  // Slice height (assuming 3x3 grid)

            // Draw corners (top-left, top-right, bottom-left, bottom-right)
            spriteBatch.Draw(texture, new Rectangle((int)x, (int)y, sliceWidth, sliceHeight), new Rectangle(0, 0, sliceWidth, sliceHeight), color); // top-left
            spriteBatch.Draw(texture, new Rectangle((int)(x + width - sliceWidth), (int)y, sliceWidth, sliceHeight), new Rectangle(2 * sliceWidth, 0, sliceWidth, sliceHeight), color); // top-right
            spriteBatch.Draw(texture, new Rectangle((int)x, (int)(y + height - sliceHeight), sliceWidth, sliceHeight), new Rectangle(0, 2 * sliceHeight, sliceWidth, sliceHeight), color); // bottom-left
            spriteBatch.Draw(texture, new Rectangle((int)(x + width - sliceWidth), (int)(y + height - sliceHeight), sliceWidth, sliceHeight), new Rectangle(2 * sliceWidth, 2 * sliceHeight, sliceWidth, sliceHeight), color); // bottom-right

            // Draw top and bottom edges (horizontal stretching)
            if (width > 2 * sliceWidth)
            {
                spriteBatch.Draw(texture, new Rectangle((int)(x + sliceWidth), (int)y, (int)(width - 2 * sliceWidth), sliceHeight), new Rectangle(sliceWidth, 0, sliceWidth, sliceHeight), color); // top
                spriteBatch.Draw(texture, new Rectangle((int)(x + sliceWidth), (int)(y + height - sliceHeight), (int)(width - 2 * sliceWidth), sliceHeight), new Rectangle(sliceWidth, 2 * sliceHeight, sliceWidth, sliceHeight), color); // bottom
            }

            // Draw left and right edges (vertical stretching)
            if (height > 2 * sliceHeight)
            {
                spriteBatch.Draw(texture, new Rectangle((int)x, (int)(y + sliceHeight), sliceWidth, (int)(height - 2 * sliceHeight)), new Rectangle(0, sliceHeight, sliceWidth, sliceHeight), color); // left
                spriteBatch.Draw(texture, new Rectangle((int)(x + width - sliceWidth), (int)(y + sliceHeight), sliceWidth, (int)(height - 2 * sliceHeight)), new Rectangle(2 * sliceWidth, sliceHeight, sliceWidth, sliceHeight), color); // right
            }

            // Draw the center (the part that stretches in both directions)
            if (width > 2 * sliceWidth && height > 2 * sliceHeight)
            {
                spriteBatch.Draw(texture, new Rectangle((int)(x + sliceWidth), (int)(y + sliceHeight), (int)(width - 2 * sliceWidth), (int)(height - 2 * sliceHeight)), new Rectangle(sliceWidth, sliceHeight, sliceWidth, sliceHeight), color); // center
            }
        }

        public static Vector2 Size(this CalculatedStyle style)
        {
            return new(style.Width, style.Height);
        }
    }
}