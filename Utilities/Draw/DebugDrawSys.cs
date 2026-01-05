using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AlienBloxUtility.Utilities.Draw
{
    public class DebugDrawNPC : GlobalNPC
    {
        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (Main.LocalPlayer.AlienBloxUtility().HitboxTool)
            {
                spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle((int)(npc.position.X - screenPos.X), (int)(npc.position.Y - screenPos.Y), npc.width, npc.height), Color.Red * .6f);
            }
        }
    }

    public class DebugDrawItem : GlobalItem
    {
        public override void PostDrawInWorld(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            if (Main.LocalPlayer.AlienBloxUtility().HitboxTool)
            {
                Vector2 screenPos = Main.screenPosition;

                spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle((int)(item.position.X - screenPos.X), (int)(item.position.Y - screenPos.Y), item.width, item.height), Color.Green * .6f);
            }
        }
    }

    public class DebugDrawProjectile : GlobalProjectile
    {
        public override void PostDraw(Projectile projectile, Color lightColor)
        {
            if (Main.LocalPlayer.AlienBloxUtility().HitboxTool)
            {
                Vector2 screenPos = Main.screenPosition;

                Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle((int)(projectile.position.X - screenPos.X), (int)(projectile.position.Y - screenPos.Y), projectile.width, projectile.height), Color.Blue * .6f);
            }
        }
    }

    public class DrawDebugTE : GlobalTile
    {
        public override void PostDraw(int i, int j, int type, SpriteBatch spriteBatch)
        {
            if (Main.LocalPlayer.AlienBloxUtility().HitboxTool)
            {
                foreach (var pair in TileEntity.ByPosition)
                {
                    Rectangle locationRectangle = new(pair.Key.X * 16, pair.Key.Y * 16, 16, 16);
                    locationRectangle.Offset((int)-Main.screenPosition.X, (int)-Main.screenPosition.Y);
                    locationRectangle = Main.ReverseGravitySupport(locationRectangle);
                    Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, locationRectangle, Color.Green * 0.6f);
                }
            }
        }
    }
}