using AlienBloxTools.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.IO;
using Terraria.ModLoader;

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

                if (Dir.Exists && !File.Exists(Dir.FullName + "\\tModUnpacker.exe"))
                {
                    using (Stream inputStream = InitialiseUtilities.ExtractContentFromAssembly("AlienBloxTools.Utilities.IncludedExes.tModUnpacker.exe"))
                    {
                        using FileStream fileStream = new(Dir.FullName + "\\tModUnpacker.exe", FileMode.Create, FileAccess.Write);
                        inputStream.CopyTo(fileStream);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }
        }

        public override void Unload()
        {
            if (Directory.Exists(Path.Combine(Main.SavePath, "AlienBloxUtility", "Cache")))
            {
                ClearDirectory(Path.Combine(Main.SavePath, "AlienBloxUtility", "Cache"));
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

        static void ClearDirectory(string directoryPath)
        {
            // Delete all files in the directory
            string[] files = Directory.GetFiles(directoryPath);
            foreach (var file in files)
            {
                try
                {
                    File.Delete(file);
                    Console.WriteLine($"File deleted: {file}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting file {file}: {ex.Message}");
                }
            }

            // Delete all subdirectories
            string[] subdirectories = Directory.GetDirectories(directoryPath);
            foreach (var subdirectory in subdirectories)
            {
                try
                {
                    Directory.Delete(subdirectory, true);  // true to delete subdirectories recursively
                    Console.WriteLine($"Subdirectory deleted: {subdirectory}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting subdirectory {subdirectory}: {ex.Message}");
                }
            }
        }
    }
}
