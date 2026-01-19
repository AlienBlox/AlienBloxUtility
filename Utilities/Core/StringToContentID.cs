using Terraria.ID;
using Terraria.ModLoader;

namespace AlienBloxUtility.Utilities.Core
{
    public static class StringToContentID
    {
        public static int ItemFromString(string str)
        {
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
    }
}