using AlienBloxUtility.Utilities.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIElements
{
    public class SmartNPCDisplay : UIPanel
    {
        public Asset<Texture2D> NPCTexture;

        public Texture2D Texture => NPCTexture.Value;

        public readonly int NPCType;

        public SmartNPCDisplay(int npcID)
        {
            NPCType = npcID;

            if (ModContent.GetModNPC(npcID) == null)
                Main.Assets.Request<Texture2D>(TextureAssets.Npc[npcID].Name);

            NPCTexture = TextureAssets.Npc[npcID];

            Width.Set(40, 0);
            Height.Set(40, 0);
        }

        public void GenCard(UIElement cardInsertTo)
        {
            NPC npc = new();

            npc.SetDefaults(NPCType);

            cardInsertTo.RemoveAllChildren();

            UIText txt = cardInsertTo.InsertText(UIUtilities.LoadCard(npc), .45f);

            txt.IsWrapped = true;
        }

        public override void LeftClick(UIMouseEvent evt)
        {
            AlienBloxUtility.SpawnNPCClient(NPCType, (int)Main.LocalPlayer.position.X - 300, (int)Main.LocalPlayer.position.Y);

            base.LeftClick(evt);
        }

        public override void RightClick(UIMouseEvent evt)
        {
            AlienBloxUtility.ButcherNPCType(NPCType);

            base.RightClick(evt);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            this.SetUIBase(ContentIDToString.NPCIdToString(NPCType));

            base.DrawSelf(spriteBatch);

            NPC drawTemplate = new();

            drawTemplate.SetDefaults(NPCType);

            DrawNpcInUI(spriteBatch, drawTemplate, new((int)GetDimensions().X, (int)GetDimensions().Y, (int)Width.Pixels, (int)Height.Pixels));
        }

        private static void DrawNpcInUI(SpriteBatch spriteBatch, NPC npc, Rectangle drawBox)
        {
            Texture2D texture = TextureAssets.Npc[npc.type].Value;
            Rectangle frame = npc.frame;
            Vector2 origin = frame.Size() / 2f;
            Vector2 drawPos = drawBox.Center.ToVector2();

            // Bestiary-style scaling
            float scaleX = (float)drawBox.Width / frame.Width;
            float scaleY = (float)drawBox.Height / frame.Height;
            float scale = Math.Min(scaleX, scaleY) * 0.85f; // padding

            spriteBatch.Draw(
                texture,
                drawPos,
                frame,
                npc.GetAlpha(Color.White),
                0f,
                origin,
                scale,
                SpriteEffects.None,
                0f
            );
        }
    }
}