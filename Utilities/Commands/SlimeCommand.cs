using AlienBloxUtility.Utilities.Abstracts;
using AlienBloxUtility.Utilities.UIUtilities.UIStates;

namespace AlienBloxUtility.Utilities.Commands
{
    public class SlimeCommand : CommandBase
    {
        public override string CommandName => "slimegame";

        public override void LaunchCommand(ConHostSystem Conhost, params string[] Params)
        {
            Conhost.SlimeGameLaunch();
        }
    }
}