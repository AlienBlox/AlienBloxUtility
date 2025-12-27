using Terraria;
using Terraria.ModLoader;

namespace AlienBloxUtility.Utilities.Helpers
{
    public class SteamHelper : ModSystem
    {
        public override void PostUpdatePlayers()
        {
            foreach (Player plr in Main.player)
            {
                if (!plr.active)
                {
                    AlienBloxUtility.SteamIDs.Remove(plr.whoAmI);
                }
            }
        }
    }
}