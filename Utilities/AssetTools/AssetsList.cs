using AlienBloxUtility.Utilities.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AlienBloxUtility.Utilities.AssetTools
{
    public static class AssetsList
    {
        public static readonly CommonAsset<Texture2D> TELabel = ModContent.Request<Texture2D>("AlienBloxUtility/Common/Assets/TELabel");

        public static readonly CommonAsset<Texture2D> IrelandFlag = ModContent.Request<Texture2D>("AlienBloxUtility/Common/Assets/IrelandFlag");
    }
}