using Terraria;
using Terraria.ModLoader;

namespace AlienBloxUtility.Utilities.Core
{
    public class UtilityPlayer : ModPlayer
    {
        public int updateTimer;

        public override void OnEnterWorld()
        {
            AlienBloxUtility.SendSteamID(Player);
            //AlienBloxUtility.RetrieveSteamID();
        }

        public override void PostUpdateMiscEffects()
        {
            if (Main.myPlayer == Player.whoAmI)
            {
                if (updateTimer++ >= 30)
                {
                    AlienBloxUtility.RetrieveSteamID();

                    updateTimer = 0; 
                }
            }
        }
    }
}