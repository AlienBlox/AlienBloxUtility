using AlienBloxUtility.Utilities.Core;
using AlienBloxUtility.Utilities.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIElements
{
    public class SmartBuffDisplay : UIPanel
    {
        public readonly int buffID;

        private readonly Asset<Texture2D> _tex;

        public SmartBuffDisplay(int buffID)
        {
            this.buffID = buffID;

            _tex = TextureHelpers.GetBuffTexture(buffID);

            Width.Set(40, 0);
            Height.Set(40, 0);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            this.SetUIBase(ContentIDToString.BuffIdToString(buffID));

            base.DrawSelf(spriteBatch);

            spriteBatch.Draw(_tex.Value, GetDimensions().Center() - new Vector2(16, 16), Color.White);
        }

        public void GenCard(UIElement cardInsertTo)
        {
            UIText txt = cardInsertTo.InsertText(UIUtilities.LoadCard(buffID), .45f);

            txt.IsWrapped = true;
        }

        public override void LeftClick(UIMouseEvent evt)
        {
            Main.LocalPlayer.AddBuff(buffID, 60);

            base.LeftClick(evt);
        }

        public override void RightClick(UIMouseEvent evt)
        {
            Main.LocalPlayer.ClearBuff(buffID);

            base.RightClick(evt);
        }
    }
}