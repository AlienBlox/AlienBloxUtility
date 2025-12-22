using AlienBloxUtility.Utilities.Helpers;
using AlienBloxUtility.Utilities.UIUtilities.UIStates;
using System;

namespace AlienBloxUtility.Utilities.Commands
{
    public class LuaCommand : CmdHelperSystem.CommandHelper
    {
        public override string CommandName => "lua";

        public override void LaunchCommand(ConHostSystem Conhost, params string[] Params)
        {
            try
            {
                AlienBloxUtility.Lua(Params[0]);
            }
            catch (Exception E)
            {
                Conhost.AddConsoleText($"{E.GetType().Name}: {E.Message}");
            }
        }
    }
}