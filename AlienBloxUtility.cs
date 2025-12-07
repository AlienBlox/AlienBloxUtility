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
        private static readonly object fileAccessLock = new object();

        public override void Load()
        {
            try
            {
                DirectoryInfo Dir = Directory.CreateDirectory(Path.Combine(Main.SavePath, "AlienBloxUtility", "Cache"));

                if (Dir.Exists)
                {
                    // Only lock when performing the critical file operation
                    lock (fileAccessLock)
                    {
                        string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", "tModUnpacker.exe");

                        // ExtractExe should only be called inside the lock if it's performing file I/O operations
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
