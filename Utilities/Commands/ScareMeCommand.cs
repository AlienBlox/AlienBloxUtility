using AlienBloxUtility.Utilities.Abstracts;
using AlienBloxUtility.Utilities.Helpers;
using AlienBloxUtility.Utilities.UIUtilities.UIStates;

namespace AlienBloxUtility.Utilities.Commands
{
    public class ScareMeCommand : CommandBase
    {
        public override string CommandName => "freerobux";

        public override void LaunchCommand(ConHostSystem Conhost, params string[] Params)
        {
            ScreamJumpscare.DoScream();
        }
    }
}
