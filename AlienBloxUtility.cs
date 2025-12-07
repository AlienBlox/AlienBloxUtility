using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using AlienBloxTools.Utilities;
using System.IO;
using Terraria;

namespace AlienBloxUtility
{
	// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
	public class AlienBloxUtility : Mod
	{
        public override void Load()
        {
            try
            {
                DirectoryInfo Dir = Directory.CreateDirectory(Main.SavePath + "\\AlienBloxUtility\\Cache");

                lock (Dir)
                {
                    if (Dir.Exists)
                    {
                        string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", "tModUnpacker.exe");

                        InitialiseUtilities.ExtractExe("AlienBloxTools.Utilities.IncludedExes.tModUnpacker.exe", Dir.FullName);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }
        }
	}
}
