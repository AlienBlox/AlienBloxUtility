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
                DirectoryInfo Dir = Directory.CreateDirectory(Path.Combine(Main.SavePath, "AlienBloxUtility", "Cache"));

                if (Dir.Exists)
                {
                    Stream TmodUnpacker = InitialiseUtilities.ExtractContentFromAssembly("AlienBloxTools.Utilities.IncludedExes.tModUnpacker.exe");
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }
        }

        /// <summary>
        /// Makes a new file
        /// </summary>
        /// <param name="PathToMakeFile">The path to put the file in</param>
        /// <param name="append">The appened</param>
        /// <param name="Encoded">What encoding is the file</param>
        /// <returns>The file stream</returns>
        [STAThread]
        public static StreamWriter MakeFile(string PathToMakeFile, bool append, Encoding Encoded)
        {
            FileStream FileStuff = File.Create(PathToMakeFile);
            FileStuff.Dispose();
            StreamWriter Writer = new(PathToMakeFile, append, Encoded);

            return Writer;
        }
    }
}
