using AlienBloxUtility.Utilities.Abstracts;
using AlienBloxUtility.Utilities.Helpers;
using AlienBloxUtility.Utilities.UIUtilities.UIStates;
using System.IO;

namespace AlienBloxUtility.Utilities.Commands
{
    public class QuickPatchCommand : CommandBase
    {
        public override string CommandName => "quickpatchimage";

        public override bool IsCommandEnabled()
        {
            return false;
        }

        public override void LaunchCommand(ConHostSystem Conhost, params string[] Params)
        {
            if (Params.Length != 4)
            {
                return;
            }

            string modToPatch = Params[0];
            string fileName = Params[1];
            string imagePath = Params[2];
            bool save = false;

            if (bool.TryParse(Params[3], out bool b))
            {
                save = b; 
            }

            try
            {
                byte[] img = File.ReadAllBytes(imagePath);

                PatchATMod.Patch(modToPatch, fileName, img, save);
            }
            catch
            {

            }
        }
    }
}