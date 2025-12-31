using AlienBloxUtility.Utilities.UIUtilities.UIStates;
using log4net;
using Terraria.ModLoader;

namespace AlienBloxUtility.Utilities.Commands
{
    public class ModImpersonateCommand : ModCommandHelper
    {
        public override string CommandName => "impersonate";

        public override void LaunchCommand(ConHostSystem Conhost, params string[] Params)
        {
            try
            {
                if (ModLoader.TryGetMod(Params[0], out Mod M))
                {
                    string msg = "";

                    for (int i = 1; i < Params.Length; i++)
                    {
                        msg += " " + Params[i];
                    }

                    M.Logger.Debug(msg);
                }
                else
                {
                    string msg = "";

                    for (int i = 1; i < Params.Length; i++)
                    {
                        msg += " " + Params[i];
                    }

                    LogManager.GetLogger(Params[0]).Debug(msg);
                }
            }
            catch
            {

            }
        }
    }
}