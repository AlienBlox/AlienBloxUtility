using AlienBloxUtility.Utilities.Helpers;
using AlienBloxUtility.Utilities.UIUtilities.UIStates;
using System;
using System.IO;

namespace AlienBloxUtility.Utilities.Commands
{
    public class JavaFileCommand : CmdHelperSystem.CommandHelper
    {
        public override string CommandName => "jsfile";

        public override void LaunchCommand(ConHostSystem Conhost, params string[] Params)
        {
            try
            {
                if (!Params[0].Equals("serverrun", StringComparison.CurrentCultureIgnoreCase))
                {
                    string filePath = Params[0];

                    AlienBloxUtility.RunJavaScript(File.ReadAllText(AlienBloxUtility.JSStorageLocation + $"\\{filePath}"));
                }
                else
                {
                    string filePath = Params[0];

                    AlienBloxUtility.JSServer(filePath, true);
                }
            }
            catch (Exception E)
            {
                Conhost.AddConsoleText($"{E.GetType().Name}: {E.Message}");
            }
        }
    }
}