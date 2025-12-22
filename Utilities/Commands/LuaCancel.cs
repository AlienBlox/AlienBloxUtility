using AlienBloxUtility.Utilities.Helpers;
using AlienBloxUtility.Utilities.UIUtilities.UIStates;

namespace AlienBloxUtility.Utilities.Commands
{
    public class LuaCancel : CmdHelperSystem.CommandHelper
    {
        public override string CommandName => "luacancel";

        public override void LaunchCommand(ConHostSystem Conhost, params string[] Params)
        {
            Conhost.AddConsoleText("Lua execution canceled");

            AlienBloxUtility.Cancel();
        }
    }
}