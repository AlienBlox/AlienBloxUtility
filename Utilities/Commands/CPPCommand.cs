using AlienBloxUtility.Utilities.Abstracts;
using AlienBloxUtility.Utilities.UIUtilities.UIStates;
using System;

namespace AlienBloxUtility.Utilities.Commands
{
    public class CPPCommand : CommandBase
    {
        public override string CommandName => "runc";

        public override void LaunchCommand(ConHostSystem Conhost, params string[] Params)
        {
            try
            {
                if (!Params[0].Equals("serverrun", StringComparison.CurrentCultureIgnoreCase))
                {
                    string CTotal = string.Empty;

                    foreach (string param in Params)
                    {
                        CTotal += ' ' + param;
                    }

                    AlienBloxUtility.CPP(CTotal);
                }
                else
                {
                    string CTotal = string.Empty;

                    for (int i = 1; i < Params.Length; i++)
                    {
                        CTotal += ' ' + Params[1];
                    }

                    AlienBloxUtility.NativeServer(CTotal);
                }
            }
            catch (Exception E)
            {
                Conhost.AddConsoleText($"{E.GetType().Name}: {E.Message}");
            }
        }
    }
}