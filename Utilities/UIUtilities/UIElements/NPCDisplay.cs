using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
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

        public int NPCType;

        public SmartNPCDisplay(int npcID)
        {
            NPCType = npcID;

            if (ModContent.GetModNPC(npcID) == null)
                Main.instance.LoadNPC(npcID);

             NPCTexture = TextureAssets.Npc[npcID];

            Width.Set(40, 0);
            Height.Set(40, 0);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            DrawNpc(spriteBatch);

            base.DrawSelf(spriteBatch);
        }

        private void DrawNpc(SpriteBatch spriteBatch)
        {
            // Calculate frame height based on total sprite sheet height / frame count
            int frameCount = Main.npcFrameCount[NPCType];
            int frameHeight = Texture.Height / frameCount;

            // Choose which frame to show (0 is usually idle)
            Rectangle sourceRect = new Rectangle(0, 0, Texture.Width, frameHeight);

            // Get the position of your UI element
            Vector2 drawPos = GetDimensions().Center();

            // Draw the NPC centered in the element
            spriteBatch.Draw(Texture, drawPos, sourceRect, Color.White, 0f, sourceRect.Size() / 2f, 1f, SpriteEffects.None, 0f);
        }

    }
}