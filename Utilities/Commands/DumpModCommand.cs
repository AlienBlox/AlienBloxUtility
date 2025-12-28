using Terraria;
using AlienBloxUtility.Utilities.Helpers;
using AlienBloxUtility.Utilities.UIUtilities.UIStates;
using AlienBloxUtility.Utilities.Core;

namespace AlienBloxUtility.Utilities.Commands
{
    public class DumpModCommand : CmdHelperSystem.CommandHelper
    {
        public override string CommandName => "dumpmod";

        public override void LaunchCommand(ConHostSystem Conhost, params string[] Params)
        {
            try
            {
                string name = Params.SafeAccess(0);

                if (name != default)
                {
                    TModInspector.AddMod(name);
                    TModInspector.DumpMod(name);
                }
            }
            catch
            {

            }
        }
    }
}