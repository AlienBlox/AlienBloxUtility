using AlienBloxUtility.Utilities.Abstracts;
using AlienBloxUtility.Utilities.UIUtilities.UIStates;
using Terraria.Localization;

namespace AlienBloxUtility.Utilities.Commands
{
    public class MovieCreditsCommand : CommandBase
    {
        public override string CommandName => "credits";

        public override bool DocumentationEnabled => false;

        public override void LaunchCommand(ConHostSystem Conhost, params string[] Params)
        {
            Conhost.AddConsoleText(Language.GetText("Mods.AlienBloxUtility.Messages.ConsoleCredits.AlienBloxCredit").Value);
            Conhost.AddConsoleText(Language.GetText("Mods.AlienBloxUtility.Messages.ConsoleCredits.Testers").Value);
            Conhost.AddConsoleText(Language.GetText("Mods.AlienBloxUtility.Messages.ConsoleCredits.Libraries").Value);
        }
    }
}