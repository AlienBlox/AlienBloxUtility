using AlienBloxUtility.Utilities.Abstracts;
using AlienBloxUtility.Utilities.UIUtilities.UIStates;
using Terraria;
using Terraria.ID;

namespace AlienBloxUtility.Utilities.Commands
{
    public class ServerLuaCommand : CommandBase
    {
        public override string CommandName => "serverlua";

        public override void LaunchCommand(ConHostSystem Conhost, params string[] Params)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                try
                {
                    if (!Params[0].Equals("dofile", System.StringComparison.CurrentCultureIgnoreCase))
                    {
                        string luaTotal = string.Empty;

                        foreach (string param in Params)
                        {
                            luaTotal += ' ' + param;
                        }

                        AlienBloxUtility.LuaServer(luaTotal);
                    }
                    else
                    {
                        AlienBloxUtility.LuaServer(Params[1], true);
                    }
                }
                catch
                {

                }
            }
        }
    }
}