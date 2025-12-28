using AlienBloxUtility.Utilities.Helpers;
using AlienBloxUtility.Utilities.UIUtilities.UIStates;
using System;

namespace AlienBloxUtility.Utilities.Commands
{
    public class GetLuaCommand : CmdHelperSystem.CommandHelper
    {
        public override string CommandName => "getlua";

        public override void LaunchCommand(ConHostSystem Conhost, params string[] Params)
        {
            try
            {
                foreach (var Name in AlienBloxUtility.LuaEnv)
                {
                    Conhost.AddConsoleText($"Func Name: {Name.Key}");
                }
            }
            catch (Exception E)
            {
                Conhost.AddConsoleText($"{E.GetType().Name}: {E.Message}");
            }
        }
    }
}
