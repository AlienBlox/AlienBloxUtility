using AlienBloxTools;
using AlienBloxUtility.Utilities.Core;
using AlienBloxUtility.Utilities.Helpers;
using AlienBloxUtility.Utilities.UIUtilities.UIStates;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AlienBloxUtility.Utilities.Commands
{
    public class GetModInfoCommand : CmdHelperSystem.CommandHelper
    {
        public override string CommandName => "getmod";

        public override string FriendlyDescription => "(Gets the details about every mod installed)";

        public override void LaunchCommand(ConHostSystem Conhost, params string[] Params)
        {
            try
            {
                LocalizedText L = Language.GetText("Mods.AlienBloxUtility.Commands.ModInfo");

                foreach (Mod M in ModLoader.Mods)
                {
                    string Path = "";

                    if (TModInspector.ModFileData.TryGetValue(M.Name, out var data))
                    {
                        Path = data.Item1.path;
                    }

                    Conhost.AddConsoleText($"Modname: {M.Name}, Display Name: {M.DisplayName}, Mod Path: {Path}");
                }
            }
            catch
            {

            }
        }
    }
}
