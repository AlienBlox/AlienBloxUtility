using AlienBloxUtility.Utilities.Abstracts;
using AlienBloxUtility.Utilities.UIUtilities.UIStates;
using System;
using System.IO;

namespace AlienBloxUtility.Utilities.Commands
{
    public class CPPFileCommand : CommandBase
    {
        public override string CommandName => "cppfile";

        public override void LaunchCommand(ConHostSystem Conhost, params string[] Params)
        {
            try
            {
                if (!Params[0].Equals("serverrun", StringComparison.CurrentCultureIgnoreCase))
                {
                    string filePath = Params[0];

                    AlienBloxUtility.CPP(File.ReadAllText(AlienBloxUtility.JSStorageLocation + $"\\{filePath}"));
                }
                else
                {
                    string filePath = Params[0];

                    AlienBloxUtility.NativeServer(filePath, true);
                }
            }
            catch (Exception E)
            {
                Conhost.AddConsoleText($"{E.GetType().Name}: {E.Message}");
            }
        }
    }
}