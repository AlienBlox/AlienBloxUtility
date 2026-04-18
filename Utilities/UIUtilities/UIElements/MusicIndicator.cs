using AlienBloxUtility.Utilities.Helpers;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIElements
{
    public class MusicIndicator : UIElement
    {
        private static Texture2D MusicIcon;

        public MusicIndicator()
        {
            if (MusicIcon == null)
            {
                MusicIcon = ModContent.Request<Texture2D>("Terraria/Images/Item_" + ItemID.MusicBox).Value;
            }

            Width.Set(0, .5f);
            Height.Set(0, .5f);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            this.SetUIBase(Language.GetText("Mods.AlienBloxUtility.UI.MusicPlaying").Format(Path.GetFileName(MusicHelper.GetCurPath())));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            spriteBatch.Draw(MusicIcon, GetDimensions().Position(), Colors.CoinCopper);
        }

        public override void RightClick(UIMouseEvent evt)
        {
            MusicHelper.DownloadCurrentMusic(AlienBloxUtility.CacheLocation);
        }
    }
}