using AlienBloxUtility.Utilities.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities.UIElements
{
    public class SmartProjectileDisplay : UIPanel
    {
        public UIItemIcon ServerButton;

        public readonly int ProjType;

        public SmartProjectileDisplay(int projID)
        {
            ProjType = projID;

            if (ModContent.GetModProjectile(projID) == null)
                Main.Assets.Request<Texture2D>(TextureAssets.Projectile[projID].Name);

            Width.Set(40, 0);
            Height.Set(40, 0);

            ServerButton = new(new(ItemID.Nanites), false);
            ServerButton.Width.Set(0, .25f);
            ServerButton.Height.Set(0, .25f);
            ServerButton.VAlign = 1;
            ServerButton.OnLeftClick += (_, _) =>
            {
                AlienBloxUtility.RequestServerProjectile(ProjType);
            };

            Append(ServerButton);
        }

        public void GenCard(UIElement cardInsertTo)
        {
            Projectile proj = new();

            proj.SetDefaults(ProjType);

            cardInsertTo.RemoveAllChildren();

            UIText txt = cardInsertTo.InsertText(UIUtilities.LoadCard(proj), .45f);

            txt.IsWrapped = true;
        }

        public override void LeftClick(UIMouseEvent evt)
        {
            Projectile.NewProjectile(new EntitySource_Misc("ProjectileHacker"), Main.LocalPlayer.Center, Vector2.Zero, ProjType, 0, 0, Main.myPlayer);

            base.LeftClick(evt);
        }

        public override void RightClick(UIMouseEvent evt)
        {
            AlienBloxUtility.ButcherProjType(ProjType);

            base.RightClick(evt);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            this.SetUIBase(ContentIDToString.ProjectileIdToString(ProjType));

            base.DrawSelf(spriteBatch);

            Projectile drawTemplate = new();

            drawTemplate.SetDefaults(ProjType);

            DrawProjInUI(spriteBatch, drawTemplate, new((int)GetDimensions().X, (int)GetDimensions().Y, (int)Width.Pixels, (int)Height.Pixels));
        }

        private static void DrawProjInUI(SpriteBatch spriteBatch, Projectile proj, Rectangle drawBox)
        {
            Texture2D texture = TextureAssets.Projectile[proj.type].Value;
            Rectangle frame = new(0, 0, proj.width, proj.height / Main.projFrames[proj.type]);
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
                proj.GetAlpha(Color.White),
                0f,
                origin,
                scale,
                SpriteEffects.None,
                0f
            );
        }
    }
}