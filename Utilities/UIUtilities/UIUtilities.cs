using AlienBloxUtility.Utilities.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.UI;

namespace AlienBloxUtility.Utilities.UIUtilities
{
    public static class UIUtilities
    {
        public static string[] LoadCard(Item i)
        {
            List<string> cards = [];

            cards.Add(Language.GetText("Mods.AlienBloxUtility.UI.ItemCard.ItemName").Format(i.Name));
            cards.Add(Language.GetText("Mods.AlienBloxUtility.UI.ItemCard.ItemSize").Format(i.width, i.height));
            cards.Add(Language.GetText("Mods.AlienBloxUtility.UI.ItemCard.ItemMaxStack").Format(i.maxStack));
            cards.Add(Language.GetText("Mods.AlienBloxUtility.UI.ItemCard.ItemValue").Format(Main.ValueToCoins(i.value)));
            cards.Add(Language.GetText("Mods.AlienBloxUtility.UI.ItemCard.ItemQuickIdentifier").Format(ContentIDToString.ItemIdToString(i.type)));
            cards.Add(Language.GetText("Mods.AlienBloxUtility.UI.ItemCard.ItemID").Format(i.type));
            cards.Add(Language.GetText("Mods.AlienBloxUtility.UI.ItemCard.ItemCombat").Format(i.damage, i.knockBack, i.crit, i.DamageType.DisplayName.Value));
            cards.Add(Language.GetText("Mods.AlienBloxUtility.UI.ItemCard.UseTime").Format(i.useTime, i.useAnimation, i.useStyle));
            cards.Add(Language.GetText("Mods.AlienBloxUtility.UI.ItemCard.AutoReuse").Format(i.autoReuse));
            cards.Add(Language.GetText("Mods.AlienBloxUtility.UI.ItemCard.CreateTile").Format(ContentIDToString.TileToString(i.createTile)));
            cards.Add(Language.GetText("Mods.AlienBloxUtility.UI.ItemCard.CreateWall").Format(ContentIDToString.TileToString(i.createWall)));
            cards.Add(Language.GetText("Mods.AlienBloxUtility.UI.ItemCard.GivesBuff").Format(ContentIDToString.BuffIdToString(i.buffType), i.buffTime));
            cards.Add(Language.GetText("Mods.AlienBloxUtility.UI.ItemCard.Projectile").Format(ContentIDToString.ProjectileIdToString(i.shoot), i.shootSpeed));
            cards.Add(Language.GetText("Mods.AlienBloxUtility.UI.ItemCard.Channel").Format(i.channel, i.pick, i.axe, i.hammer));

            return [.. cards];
        }

        public static string LoadCard(NPC i)
        {
            return Language.GetText("Mods.AlienBloxUtility.UI.NPCCard").Format(i.GivenOrTypeName, i.lifeMax, i.defense, i.damage, i.aiStyle, i.Hitbox.X, i.Hitbox.Y, i.noGravity, i.noTileCollide, i.boss, i.CountsAsACritter, i.townNPC);
        }

        public static string LoadCard(Projectile p)
        {
            return Language.GetText("Mods.AlienBloxUtility.UI.ProjCard").Format(p.Name, p.aiStyle, p.type, p.friendly, p.hostile, p.ignoreWater, p.tileCollide, p.timeLeft, p.netImportant);
        }

        /// <summary>
        /// Quickly draws a 9-slice UI system
        /// </summary>
        /// <param name="uiState"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="texture"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="color"></param>
        public static void DrawNineSlice(this UIState uiState, SpriteBatch spriteBatch, Texture2D texture, float x, float y, float width, float height, Color color)
        {
            int sliceWidth = texture.Width / 3;   // Assuming 3x3 texture layout
            int sliceHeight = texture.Height / 3;  // Assuming 3x3 texture layout

            // Corner positions (top-left, top-right, bottom-left, bottom-right)
            spriteBatch.Draw(texture, new Rectangle((int)x, (int)y, sliceWidth, sliceHeight), new Rectangle(0, 0, sliceWidth, sliceHeight), color); // Top-left
            spriteBatch.Draw(texture, new Rectangle((int)(x + width - sliceWidth), (int)y, sliceWidth, sliceHeight), new Rectangle(2 * sliceWidth, 0, sliceWidth, sliceHeight), color); // Top-right
            spriteBatch.Draw(texture, new Rectangle((int)x, (int)(y + height - sliceHeight), sliceWidth, sliceHeight), new Rectangle(0, 2 * sliceHeight, sliceWidth, sliceHeight), color); // Bottom-left
            spriteBatch.Draw(texture, new Rectangle((int)(x + width - sliceWidth), (int)(y + height - sliceHeight), sliceWidth, sliceHeight), new Rectangle(2 * sliceWidth, 2 * sliceHeight, sliceWidth, sliceHeight), color); // Bottom-right

            // Top and bottom edges (horizontal stretching)
            if (width > 2 * sliceWidth)
            {
                spriteBatch.Draw(texture, new Rectangle((int)(x + sliceWidth), (int)y, (int)(width - 2 * sliceWidth), sliceHeight), new Rectangle(sliceWidth, 0, sliceWidth, sliceHeight), color); // Top edge
                spriteBatch.Draw(texture, new Rectangle((int)(x + sliceWidth), (int)(y + height - sliceHeight), (int)(width - 2 * sliceWidth), sliceHeight), new Rectangle(sliceWidth, 2 * sliceHeight, sliceWidth, sliceHeight), color); // Bottom edge
            }

            // Left and right edges (vertical stretching)
            if (height > 2 * sliceHeight)
            {
                spriteBatch.Draw(texture, new Rectangle((int)x, (int)(y + sliceHeight), sliceWidth, (int)(height - 2 * sliceHeight)), new Rectangle(0, sliceHeight, sliceWidth, sliceHeight), color); // Left edge
                spriteBatch.Draw(texture, new Rectangle((int)(x + width - sliceWidth), (int)(y + sliceHeight), sliceWidth, (int)(height - 2 * sliceHeight)), new Rectangle(2 * sliceWidth, sliceHeight, sliceWidth, sliceHeight), color); // Right edge
            }

            // Center (the part that stretches in both directions)
            if (width > 2 * sliceWidth && height > 2 * sliceHeight)
            {
                spriteBatch.Draw(texture, new Rectangle((int)(x + sliceWidth), (int)(y + sliceHeight), (int)(width - 2 * sliceWidth), (int)(height - 2 * sliceHeight)), new Rectangle(sliceWidth, sliceHeight, sliceWidth, sliceHeight), color); // Center
            }
        }

        public static Vector2 Size(this CalculatedStyle style)
        {
            return new(style.Width, style.Height);
        }
    }
}