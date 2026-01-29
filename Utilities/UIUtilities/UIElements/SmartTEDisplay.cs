using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI.Elements;

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

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            //this.SetUIBase(content)

            base.DrawSelf(spriteBatch);
        }
    }
}