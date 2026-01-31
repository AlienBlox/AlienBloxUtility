using AlienBloxUtility.Utilities.AssetTools;
using AlienBloxUtility.Utilities.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIElements
{
    public class SmartTEDisplay : UIPanel
    {
        public readonly int TEID;

        public SmartTEDisplay(int TEID)
        {
            this.TEID = TEID;

            Width.Set(40, 0);
            Height.Set(40, 0);
        }

        public override void RightClick(UIMouseEvent evt)
        {
            if (TEID != -1)
            {
                Main.LocalPlayer.AlienBloxUtility().ForcePlaceTE = TEID;
            }
            else
            {
                Main.LocalPlayer.AlienBloxUtility().ForceDelTE = true;
            }

            base.RightClick(evt);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            if (TEID != -1)
                this.SetUIBase(ContentIDToString.TEToString(TEID));
            else
                this.SetUIBase("Delete Tile Entity");

            base.DrawSelf(spriteBatch);

            spriteBatch.Draw(AssetsList.TELabel.Value, GetDimensions().Center() - new Vector2(14, 13), Color.White);
        }
    }
}