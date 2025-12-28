using AlienBloxUtility.Utilities.Helpers;
using AlienBloxUtility.Utilities.UIUtilities.UIRenderers;
using AlienBloxUtility.Utilities.UIUtilities.UIStates;

namespace AlienBloxUtility.Utilities.Commands
{
    public class DVDCommand : CmdHelperSystem.CommandHelper
    {
        public override string CommandName => "dvd";

        public override void LaunchCommand(ConHostSystem Conhost, params string[] Params)
        {
            Conhost.AddConsoleText("DVD!");

            DVDRender.ShowDVDLogo = !DVDRender.ShowDVDLogo;
        }
    }
}