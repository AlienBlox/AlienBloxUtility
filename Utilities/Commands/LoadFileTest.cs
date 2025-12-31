using AlienBloxUtility.Utilities.Core;
using AlienBloxUtility.Utilities.UIUtilities.UIStates;

namespace AlienBloxUtility.Utilities.Commands
{
    public class LoadFileTest : ModCommandHelper
    {
        public override string CommandName => "loadfiletest";

        public override void LaunchCommand(ConHostSystem Conhost, params string[] Params)
        {
            try
            {
                TModInspector.MessWithMod("AlienBloxUtility", false);

                var file = TModInspector.LoadFile(TModInspector.ModFileData["AlienBloxUtility"].Item1.path);

                if (file != null)
                {
                    ExternalTModInspection.ExportToLocation(AlienBloxUtility.DecompLocation + "\\", file);

                    Conhost.AddConsoleText("Test complete!");

                    if (TModInspector.GetFESet(file) == null)
                    {
                        return;
                    }

                    foreach (var val in TModInspector.GetFESet(file))
                    {
                        Conhost.AddConsoleText($"{val.Name}");
                    }
                }
                else
                {
                    Conhost.AddConsoleText("Test failed!");
                }
            }
            catch
            {
                Conhost.AddConsoleText("exception");
            }
        }
    }
}