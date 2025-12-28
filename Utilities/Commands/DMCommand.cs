using AlienBloxUtility.Utilities.UIUtilities.UIStates;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlienBloxUtility.Utilities.Commands
{
    public class DMCommand : ModCommandHelper
    {
        public override string CommandName => "dm";

        public override void LaunchCommand(ConHostSystem Conhost, params string[] Params)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                try
                {
                    
                }
                catch
                {

                }
            }
        }
    }
}