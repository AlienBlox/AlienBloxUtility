using Terraria.ModLoader;

namespace AlienBloxUtility.Utilities.Core
{
    public class UtilityPlayer : ModPlayer
    {
        public override void OnEnterWorld()
        {
            AlienBloxUtility.SendSteamID(Player);
            //AlienBloxUtility.RetrieveSteamID();
        }
    }
}