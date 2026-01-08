using AlienBloxUtility.Utilities.Helpers;
using AlienBloxUtility.Utilities.UIUtilities.UIStates;
using System;

namespace AlienBloxUtility.Utilities.Commands
{
    public class TModLoaderRunCommand : CmdHelperSystem.CommandHelper
    {
        public override string CommandName => "tmodloader";

        public override void LaunchCommand(ConHostSystem Conhost, params string[] Params)
        {
            string ExtraArgs = "";
            bool RedirOutput = false;

            try
            {
                foreach (string arg in Params)
                {
                    switch (Array.IndexOf(Params, arg))
                    {
                        case 0:
                            ExtraArgs = arg;
                            break;
                        case 1:
                            if (arg.Equals("true", StringComparison.CurrentCultureIgnoreCase))
                            {
                                RedirOutput = true;
                            }
                            break;
                    }
                }
            }
            catch
            {

            }

            Conhost.AddConsoleText($"tModLoader launched! (Args: {ExtraArgs}, Redirect Output: {RedirOutput})");

            MultiLaunchUtility.RunTModLoader();
        }
    }
}