using AlienBloxUtility.Utilities.Helpers;
using AlienBloxUtility.Utilities.UIUtilities.UIStates;

namespace AlienBloxUtility.Utilities.Commands
{
    public class HelpCommand : CmdHelperSystem.CommandHelper
    {
        public override string CommandName => "help";

        public override void LaunchCommand(ConHostSystem Conhost, params string[] Params)
        {
            foreach (string name in CmdHelperSystem.GetCmdNames())
            {
                if (CommandName != name)
                {
                    Conhost.AddConsoleText("Command Name: " + name + FriendlyDescription);
                }
            }
        }
    }
}