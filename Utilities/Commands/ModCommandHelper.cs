using AlienBloxUtility.Utilities.Abstracts;
using AlienBloxUtility.Utilities.UIUtilities.UIStates;
using System;
using System.Collections.Generic;

namespace AlienBloxUtility.Utilities.Commands
{
    public class ModCommandHelper : CommandBase
    {
        public static Dictionary<string, Action<string[]>> Commands;

        public override string CommandName => "modrun";

        public override void OnLoad()
        {
            Commands = [];
        }

        public override void OnUnload()
        {
            try
            {
                Commands.Clear();
                Commands = null;
            }
            catch
            {

            }
        }

        public override void LaunchCommand(ConHostSystem Conhost, params string[] Params)
        {
            try
            {
                if (Commands.TryGetValue(Params[0], out var act))
                {
                    List<string> argsTrim = [];

                    for (int i = 0; i < Params.Length; i++)
                    {
                        if (i != 0 || i != 1)
                        {
                            argsTrim.Add(Params[i]);
                        }
                    }

                    act.Invoke([.. argsTrim]);
                }
            }
            catch (Exception e)
            {
                Conhost.AddConsoleText($"{e.GetType().Name}: {e.Message}");
            }
        }
    }
}