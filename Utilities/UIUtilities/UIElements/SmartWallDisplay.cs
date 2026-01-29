using AlienBloxUtility.Utilities.Core;
using AlienBloxUtility.Utilities.Helpers;
using Microsoft.Xna.Framework.Graphics;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIElements
{
    public class SmartWallDisplay : UIPanel
    {
        public readonly int WallID;

        public UIItemIcon WallIcon;

        public SmartWallDisplay(int wallID)
        {
            WallID = wallID;

            Width.Set(40, 0);
            Height.Set(40, 0);

            WallIcon = new(new(WallCache.ItemFromWall(wallID)), false);
            WallIcon.Width.Set(0, 1);
            WallIcon.Height.Set(0, 1);

            if (WallIcon != null)
            {
                Append(WallIcon);
            }
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            this.SetUIBase(ContentIDToString.WallToString(WallID));

            base.DrawSelf(spriteBatch);
        }

        public override void RightClick(UIMouseEvent evt)
        {
            SoundEngine.PlaySound(SoundID.MenuTick);

            Main.LocalPlayer.AlienBloxUtility().ForcePlaceWall = WallID;

            base.LeftClick(evt);
        }
    }
}