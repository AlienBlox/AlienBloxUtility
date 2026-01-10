using AlienBloxUtility.Utilities.Abstracts;
using AlienBloxUtility.Utilities.Helpers;
using AlienBloxUtility.Utilities.UIUtilities.UIStates;
using Terraria;

namespace AlienBloxUtility.Utilities.Commands
{
    public class TestCommand : CmdHelperSystem.CommandHelper
    {
        public override string CommandName => "test";

        public override bool DocumentationEnabled => false;

        public override string FriendlyDescription => "Test command.";

        public override void LaunchCommand(ConHostSystem Conhost, params string[] Params)
        {
            Main.NewText("Command example!");

            DebugTool.UseTool("TestTool", Main.myPlayer, Main.LocalPlayer.AlienBloxUtility().toolSudo);
        }
    }
}