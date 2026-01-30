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

        public override void LeftClick(UIMouseEvent evt)
        {
            Main.LocalPlayer.AlienBloxUtility().ForcePlaceTE = TEID;

            base.LeftClick(evt);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            this.SetUIBase(ContentIDToString.TEToString(TEID));

            base.DrawSelf(spriteBatch);

            spriteBatch.Draw(AssetsList.TELabel.Value, GetDimensions().Center() - new Vector2(14, 13), Color.White);
        }
    }
}