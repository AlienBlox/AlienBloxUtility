using Terraria.ID;
using Terraria.ModLoader;

namespace AlienBloxUtility.Utilities.Core
{
    public class ContentIDToString
    {
        public static string ItemIdToString(int ID)
        {
            if (ModContent.GetModItem(ID) == null)
            {
                return $"Terraria:{ItemID.Search.GetName(ID)}";
            }
            else
            {
                return $"{ModContent.GetModItem(ID).Mod.Name}:{ModContent.GetModItem(ID).Name}";
            }
        }

        public static string ProjectileIdToString(int ID)
        {
            if (ModContent.GetModProjectile(ID) == null)
            {
                return $"Terraria:{ProjectileID.Search.GetName(ID)}";
            }
            else
            {
                return $"{ModContent.GetModProjectile(ID).Mod.Name}:{ModContent.GetModProjectile(ID).Name}";
            }
        }

        public static string NPCIdToString(int buffID)
        {
            if (ModContent.GetModNPC(buffID) == null)
            {
                return $"Terraria:{NPCID.Search.GetName(buffID)}";
            }
            else
            {
                return $"{ModContent.GetModNPC(buffID).Mod.Name}:{ModContent.GetModNPC(buffID).Name}";
            }
        }

        public static string BuffIdToString(int buffID)
        {
            if (ModContent.GetModBuff(buffID) == null)
            {
                return $"Terraria:{BuffID.Search.GetName(buffID)}";
            }
            else
            {
                return $"{ModContent.GetModBuff(buffID).Mod.Name}:{ModContent.GetModBuff(buffID).Name}";
            }
        }

        public static string PrefixIdToString(int prefixID)
        {
            if (prefixID == 0)
            {
                return "Terraria:None";
            }

            if (PrefixLoader.GetPrefix(prefixID) == null)
            {
                return $"Terraria:{PrefixID.Search.GetName(prefixID)}";
            }
            else
            {
                return $"{PrefixLoader.GetPrefix(prefixID).Mod.Name}:{PrefixLoader.GetPrefix(prefixID).Name}";
            }
        }
    }
}