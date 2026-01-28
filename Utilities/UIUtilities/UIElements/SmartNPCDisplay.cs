using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;

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

            //if (ModContent.GetModNPC(npcID) == null)
                //Main.instance.LoadNPC(npcID);


            if (TextureAssets.Npc[npcID].State == AssetState.NotLoaded)
            {
                Main.Assets.Request<Texture2D>(TextureAssets.Npc[npcID].Name);
            }

            NPCTexture = TextureAssets.Npc[npcID];

            Width.Set(40, 0);
            Height.Set(40, 0);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
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