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
                if (!Params[0].Equals("serverrun", StringComparison.CurrentCultureIgnoreCase))
                {
                    string luaTotal = string.Empty;

                    foreach (string param in Params)
                    {
                        luaTotal += ' ' + param;
                    }

                    AlienBloxUtility.Lua(luaTotal);
                }
                else
                {
                    string LuaTotal = string.Empty;

                    for (int i = 1; i < Params.Length; i++)
                    {
                        LuaTotal += ' ' + Params[1];
                    }

                    AlienBloxUtility.LuaServer(LuaTotal);
                }
            }
            catch (Exception E)
            {
                Conhost.AddConsoleText($"{E.GetType().Name}: {E.Message}");
            }
        }
    }
}