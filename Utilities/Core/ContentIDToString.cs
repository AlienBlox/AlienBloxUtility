using System.Linq;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlienBloxUtility.Utilities.Core
{
    public class ContentIDToString
    {
        public static string ItemIdToString(int ID)
        {
            if (ID == -1)
            {
                return "ERROR:NAN";
            }

            try
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
            catch
            {
                return "ERROR:NAN";
            }
            
        }

        public static string ProjectileIdToString(int ID)
        {
            if (ID == -1)
            {
                return "ERROR:NAN";
            }

            try
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
            catch
            {
                return "ERROR:NAN";
            }
        }

        public static string NPCIdToString(int npcID)
        {
            if (npcID == -1)
            {
                return "ERROR:NAN";
            }

            try
            {
                if (ModContent.GetModNPC(npcID) == null)
                {
                    return $"Terraria:{NPCID.Search.GetName(npcID)}";
                }
                else
                {
                    return $"{ModContent.GetModNPC(npcID).Mod.Name}:{ModContent.GetModNPC(npcID).Name}";
                }
            }
            catch
            {
                return "ERROR:NAN";
            }            
        }

        public static string BuffIdToString(int buffID)
        {
            if (buffID == -1 || buffID == 0)
            {
                return "ERROR:NAN";
            }

            try
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
            catch
            {
                return "ERROR:NAN";
            }
        }

        public static string PrefixIdToString(int prefixID)
        {
            if (prefixID == 0)
            {
                return "Terraria:None";
            }

            try
            {
                if (PrefixLoader.GetPrefix(prefixID) == null)
                {
                    return $"Terraria:{PrefixID.Search.GetName(prefixID)}";
                }
                else
                {
                    return $"{PrefixLoader.GetPrefix(prefixID).Mod.Name}:{PrefixLoader.GetPrefix(prefixID).Name}";
                }
            }
            catch
            {
                return "ERROR:NAN";
            }
        }

        public static string TileToString(int tileID)
        {
            if (tileID == -1)
            {
                return "ERROR:NAN";
            }

            try
            {
                if (ModContent.GetModTile(tileID) == null)
                {
                    return $"Terraria:{TileID.Search.GetName(tileID)}";
                }
                else
                {
                    return $"{ModContent.GetModTile(tileID).Mod.Name}:{ModContent.GetModTile(tileID).Name}";
                }
            }
            catch
            {
                return "ERROR:NAN";
            }
        }

        public static string WallToString(int wallID)
        {
            if (wallID == -1)
            {
                return "ERROR:NAN";
            }

            try
            {
                if (ModContent.GetModWall(wallID) == null)
                {
                    return $"Terraria:{WallID.Search.GetName(wallID)}";
                }
                else
                {
                    return $"{ModContent.GetModWall(wallID).Mod.Name}:{ModContent.GetModWall(wallID).Name}";
                }
            }
            catch
            {
                return "ERROR:NAN";
            }
        }

        public static string TEToString(int TEID)
        {
            if (TEID == -1)
            {
                return "ERROR:NAN";
            }

            TileEntity TE = TEUtilities.GetTEObjects().FirstOrDefault(obj => obj.type == TEID);

            if (TE != null && TE.GetType() != null)
            {
                if (TE is ModTileEntity MTE)
                {
                    return $"{MTE.Mod.Name}:{MTE.Name}";
                }
                else
                {
                    return $"Terraria:{TE.GetType().Name}";
                }    
            }
            else
            {
                return "ERROR:NAN";
            }
        }
    }
}