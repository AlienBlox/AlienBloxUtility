using AlienBloxUtility.Utilities.Helpers;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIElements
{
    public class HorrificScream : UIElement
    {
        public Texture2D texture;

        public HorrificScream()
        {
            texture = ModContent.Request<Texture2D>("AlienBloxUtility/Dont_click_and_see_this_please.").Value;

            Width.Set(1430, 0);
            Height.Set(1080, 0);
            VAlign = HAlign = .5f;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, GetDimensions().Position(), new(255, 255, 255));

            base.DrawSelf(spriteBatch);
        }

        public override void LeftClick(UIMouseEvent evt)
        {
            ScreamJumpscare.ScreamVisual = false;

            base.LeftClick(evt);
        }
    }
}