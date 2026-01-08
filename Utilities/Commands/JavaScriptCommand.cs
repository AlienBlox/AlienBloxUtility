using AlienBloxUtility.Utilities.Helpers;
using AlienBloxUtility.Utilities.UIUtilities.UIStates;
using System;

namespace AlienBloxUtility.Utilities.Commands
{
    public class JavaScriptCommand : CmdHelperSystem.CommandHelper
    {
        public override string CommandName => "javascript";

        public override bool DocumentationEnabled => false;

        public override void LaunchCommand(ConHostSystem Conhost, params string[] Params)
        {
            try
            {
                if (!Params[0].Equals("serverrun", StringComparison.CurrentCultureIgnoreCase))
                {
                    string jsTotal = string.Empty;

                    foreach (string param in Params)
                    {
                        jsTotal += ' ' + param;
                    }

                    AlienBloxUtility.RunJavaScript(jsTotal);
                }
                else
                {
                    string jsTotal = string.Empty;

                    for (int i = 1; i < Params.Length; i++)
                    {
                        jsTotal += ' ' + Params[1];
                    }

                    AlienBloxUtility.JSServer(jsTotal);
                }
            }
            catch (Exception E)
            {
                Conhost.AddConsoleText($"{E.GetType().Name}: {E.Message}");
            }
        }
    }
}