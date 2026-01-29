using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AlienBloxUtility.Utilities.Helpers
{
    public static class TextureHelpers
    {
        /// <summary>
        /// Gets the icon texture for a buff, whether vanilla or modded.
        /// </summary>
        /// <param name="buffType">The buff ID.</param>
        /// <returns>The Texture2D of the buff icon, or null if not found.</returns>
        public static Asset<Texture2D> GetBuffTexture(int buffType)
        {
            // Check if it's a modded buff
            ModBuff modBuff = ModContent.GetModBuff(buffType);

            if (modBuff != null)
            {
                // Load and return the modded buff texture
                return ModContent.Request<Texture2D>(modBuff.Texture); //.Value;
            }

            // Otherwise, assume it's a vanilla buff
            if (buffType >= 0 && buffType < TextureAssets.Buff.Length)
            {
                return TextureAssets.Buff[buffType];
            }

            // Invalid buff type
            return TextureAssets.MagicPixel;//null;
        }
    }
}