using AlienBloxUtility.Utilities.Abstracts;
using AlienBloxUtility.Utilities.Core;
using AlienBloxUtility.Utilities.UIUtilities.UIStates;

namespace AlienBloxUtility.Utilities.Commands
{
    public class TModFilePeekerCommand : CommandBase
    {
        public override string CommandName => "tmodpeek";

        public override bool DocumentationEnabled => false;

        public override void LaunchCommand(ConHostSystem Conhost, params string[] Params)
        {
            try
            {
                TModInspector.MessWithMod(Params[0], false);

                foreach (var item in TModInspector.GetFESet(TModInspector.ModFileData[Params[0]].Item1))
                {
                    Conhost.AddConsoleText($"File: {item.Name}");
                }
            }
            catch
            {

            }
        }
    }
}