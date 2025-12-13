using AlienBloxUtility.Utilities.Helpers;
using AlienBloxUtility.Utilities.UIUtilities.UIStates;
using Terraria;

namespace AlienBloxUtility.Utilities.Commands
{
    public class TestCommand : CmdHelperSystem.CommandHelper
    {
        public override void OnLoad()
        {
            CommandName = "Test";
        }

        public override void LaunchCommand(ConHostSystem Conhost, params string[] Params)
        {
            Main.NewText("Command example!");
        }
    }
}