using AlienBloxUtility.Utilities.Abstracts;
using AlienBloxUtility.Utilities.Core;
using AlienBloxUtility.Utilities.UIUtilities.UIStates;

namespace AlienBloxUtility.Utilities.Commands
{
    public class GetLoadedModCount : CommandBase
    {
        public override string CommandName => "getloadedcount";

        public override bool DocumentationEnabled => false;

        public override void LaunchCommand(ConHostSystem Conhost, params string[] Params)
        {
            try
            {
                Conhost.AddConsoleText(TModInspector.ModFileData.Count.ToString());
            }
            catch
            {

            }
        }
    }
}