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

        public AlienBloxUtilityUIBase(string texturePath = "AlienBloxUtility/Common/Assets/UIGrid")
        {
            // Load the texture for the 9-slice UI dynamically
            texture = ModContent.Request<Texture2D>(texturePath).Value;

            // Make sure the texture was loaded
            if (texture == null)
            {
                Main.NewText("ERROR: Texture not found!");
            }

            // Calculate slice size based on texture dimensions (assuming 3x3 grid)
            sliceWidth = texture.Width / 3;
            sliceHeight = texture.Height / 3;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Ensure the texture is loaded and available
            if (texture == null)
                return;

            // Get the current position and size of the element
            var dimensions = GetDimensions();
            Vector2 position = dimensions.Position();
            float width = dimensions.Width;
            float height = dimensions.Height;

            // Calculate how much the slices will stretch
            float horizontalStretch = (width - 2 * sliceWidth) / sliceWidth;
            float verticalStretch = (height - 2 * sliceHeight) / sliceHeight;

            // Draw the top-left corner
            spriteBatch.Draw(texture, position, new Rectangle(0, 0, sliceWidth, sliceHeight), Color.White);

            // Draw the top edge (stretch horizontally)
            spriteBatch.Draw(texture, new Vector2(position.X + sliceWidth, position.Y),
                new Rectangle(sliceWidth, 0, sliceWidth, sliceHeight), Color.White, 0f, Vector2.Zero,
                new Vector2(horizontalStretch, 1f), SpriteEffects.None, 0f);

            // Draw the top-right corner
            spriteBatch.Draw(texture, new Vector2(position.X + width - sliceWidth, position.Y),
                new Rectangle(2 * sliceWidth, 0, sliceWidth, sliceHeight), Color.White);

            // Draw the left edge (stretch vertically)
            spriteBatch.Draw(texture, new Vector2(position.X, position.Y + sliceHeight),
                new Rectangle(0, sliceHeight, sliceWidth, sliceHeight), Color.White, 0f, Vector2.Zero,
                new Vector2(1f, verticalStretch), SpriteEffects.None, 0f);

            // Draw the center (stretch both horizontally and vertically)
            spriteBatch.Draw(texture, new Vector2(position.X + sliceWidth, position.Y + sliceHeight),
                new Rectangle(sliceWidth, sliceHeight, sliceWidth, sliceHeight), Color.White, 0f, Vector2.Zero,
                new Vector2(horizontalStretch, verticalStretch), SpriteEffects.None, 0f);

            // Draw the right edge (stretch vertically)
            spriteBatch.Draw(texture, new Vector2(position.X + width - sliceWidth, position.Y + sliceHeight),
                new Rectangle(2 * sliceWidth, sliceHeight, sliceWidth, sliceHeight), Color.White, 0f, Vector2.Zero,
                new Vector2(1f, verticalStretch), SpriteEffects.None, 0f);

            // Draw the bottom-left corner
            spriteBatch.Draw(texture, new Vector2(position.X, position.Y + height - sliceHeight),
                new Rectangle(0, 2 * sliceHeight, sliceWidth, sliceHeight), Color.White);

            // Draw the bottom edge (stretch horizontally)
            spriteBatch.Draw(texture, new Vector2(position.X + sliceWidth, position.Y + height - sliceHeight),
                new Rectangle(sliceWidth, 2 * sliceHeight, sliceWidth, sliceHeight), Color.White, 0f, Vector2.Zero,
                new Vector2(horizontalStretch, 1f), SpriteEffects.None, 0f);

            // Draw the bottom-right corner
            spriteBatch.Draw(texture, new Vector2(position.X + width - sliceWidth, position.Y + height - sliceHeight),
                new Rectangle(2 * sliceWidth, 2 * sliceHeight, sliceWidth, sliceHeight), Color.White);

            base.Draw(spriteBatch);
        }
    }
}
