using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace AlienBloxUtility.Utilities.DataStructures
{
    public class CommonAsset<T> where T : class
    {
        public Asset<T> ConnectedAsset { get; private set; }

        public T Value => ConnectedAsset.Value;

        public Rectangle Size { get; private set; }

        public CommonAsset(Asset<T> asset)
        {
            ConnectedAsset = asset;

            CacheSize();
        }

        public void CacheSize()
        {
            if (ConnectedAsset.Value is Texture2D tex)
            {
                Size = new(0, 0, tex.Width, tex.Height);
            }
        }

        public static CommonAsset<T> Update(ref CommonAsset<T> asset, Asset<T> AssetToUpdate)
        {
            return asset = new(AssetToUpdate);
        }

        public static implicit operator Asset<T>(CommonAsset<T> asset)
        {
            return asset.ConnectedAsset;
        }

        public static implicit operator CommonAsset<T>(Asset<T> asset)
        {
            return new(asset);
        }
    }
}
