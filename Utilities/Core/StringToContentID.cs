using AlienBloxUtility.Utilities.Reflector.Engine;
using System.Linq;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlienBloxUtility.Utilities.Core
{
    public static class StringToContentID
    {
        public static int ItemFromString(string str)
        {
            if (str == null)
                return 0;

            string[] strSplit = str.Split(':');

            if (strSplit[0] == "Terraria")
            {
                return ItemID.Search.GetId(strSplit[1]);
            }

            if (ModLoader.TryGetMod(strSplit[0], out var M))
            {
                if (M.TryFind<ModItem>(strSplit[1], out var I))
                {
                    return I.Type;
                }
            }

            return -1;
        }

        public static int ProjFromString(string str)
        {
            if (str == null)
                return 0;

            string[] strSplit = str.Split(':');

            if (strSplit[0] == "Terraria")
            {
                return ProjectileID.Search.GetId(strSplit[1]);
            }

            if (ModLoader.TryGetMod(strSplit[0], out var M))
            {
                if (M.TryFind<ModProjectile>(strSplit[1], out var I))
                {
                    return I.Type;
                }
            }

            return -1;
        }

        public static int NPCFromString(string str)
        {
            if (str == null)
                return 0;

            string[] strSplit = str.Split(':');

            if (strSplit[0] == "Terraria")
            {
                return NPCID.Search.GetId(strSplit[1]);
            }

            if (ModLoader.TryGetMod(strSplit[0], out var M))
            {
                if (M.TryFind<ModNPC>(strSplit[1], out var I))
                {
                    return I.Type;
                }
            }

            return -1;
        }

        public static int BuffFromString(string str)
        {
            if (str == null)
                return 0;

            string[] strSplit = str.Split(':');

            if (strSplit[0] == "Terraria")
            {
                return BuffID.Search.GetId(strSplit[1]);
            }

            if (ModLoader.TryGetMod(strSplit[0], out var M))
            {
                if (M.TryFind<ModBuff>(strSplit[1], out var I))
                {
                    return I.Type;
                }
            }

            return -1;
        }

        public static int PrefixFromString(string str)
        {
            if (str == null || str == "Terraria:None")
                return 0;

            string[] strSplit = str.Split(':');

            if (strSplit[0] == "Terraria")
            {
                return PrefixID.Search.GetId(strSplit[1]);
            }

            if (ModLoader.TryGetMod(strSplit[0], out var M))
            {
                if (M.TryFind<ModPrefix>(strSplit[1], out var I))
                {
                    return I.Type;
                }
            }

            return -1;
        }

        public static int TileFromString(string str)
        {
            if (str == null)
                return 0;

            string[] strSplit = str.Split(':');

            if (strSplit[0] == "Terraria")
            {
                return TileID.Search.GetId(strSplit[1]);
            }

            if (ModLoader.TryGetMod(strSplit[0], out var M))
            {
                if (M.TryFind<ModTile>(strSplit[1], out var I))
                {
                    return I.Type;
                }
            }

            return -1;
        }

        public static int TEFromString(string TEName)
        {
            if (TEName == null)
                return -1;

            string[] strSplit = TEName.Split(':');

            if (strSplit[0] == "Terraria")
            {
                TileEntity TE = TEUtilities.GetTEObjects().FirstOrDefault(te => te.GetTypeWithCache().Name == strSplit[1]);

                return TE.ID;
            }

            if (ModLoader.TryGetMod(strSplit[0], out var M))
            {
                if (M.TryFind<ModTileEntity>(strSplit[1], out var I))
                {
                    return I.Type;
                }
            }

            return -1;
        }
    }
}