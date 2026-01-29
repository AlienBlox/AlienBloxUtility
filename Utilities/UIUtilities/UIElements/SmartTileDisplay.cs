using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.UI;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using AlienBloxUtility.Utilities.Core;

namespace AlienBloxUtility.Utilities.UIUtilities.UIElements
{
    public class SmartTileDisplay : UIPanel
    {
        public readonly int TileID;

        public UIItemIcon TileIcon;

        public SmartTileDisplay(int tileID)
        {
            TileID = tileID;

            Width.Set(40, 0);
            Height.Set(40, 0);

            int dropID = TileLoader.GetItemDropFromTypeAndStyle(tileID);

            TileIcon = new(new(dropID), false);

            TileIcon.Width.Set(0, 1);
            TileIcon.Height.Set(0, 1);

            Append(TileIcon);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            this.SetUIBase(ContentIDToString.TileToString(TileID));

            base.DrawSelf(spriteBatch);
        }

        public override void RightClick(UIMouseEvent evt)
        {
            SoundEngine.PlaySound(SoundID.MenuTick);

            Main.LocalPlayer.AlienBloxUtility().ForcePlaceTile = TileID;

            base.LeftClick(evt);
        }
    }
}