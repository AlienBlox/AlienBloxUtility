using AlienBloxUtility.Utilities.Core;
using Terraria;

namespace AlienBloxUtility
{
    public static class AlienBloxUtilitySpecials
    {
        public static UtilityPlayer AlienBloxUtility(this Player player)
        {
            return player.GetModPlayer<UtilityPlayer>();
        }
    }
}