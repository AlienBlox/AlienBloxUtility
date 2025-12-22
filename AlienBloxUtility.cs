using AlienBloxTools.Utilities;
using Neo.IronLua;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.IO;
using Terraria.ModLoader;
    
namespace AlienBloxUtility
{
    // Proudly made in the lovely nation of Ireland
    // Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
    public partial class AlienBloxUtility : Mod
    {
        #pragma warning disable CA2211 // Non-constant fields should not be visible
        public static Lua GlobalLua;

        public static LuaGlobal LuaEnv;

        public static AlienBloxUtility Instance;
        #pragma warning restore CA2211 // Non-constant fields should not be visible

        public static string ModDumpLocation { get; private set; }

        public static string CacheLocation { get; private set; }

        public static string DecompLocation { get; private set; }

        public static string LogLocation { get; private set; }

        public static string LuaStorageLocation { get; private set; }

        public override void Load()
        {
            Instance = this;
            GlobalLua = new Lua();
            LuaEnv = GlobalLua.CreateEnvironment();
            Cts = new();
            MainThreadQueue = [];

            TestRun("return { x = 1, y = 2 }"); // Ensure lua is working

            try
            {
                DirectoryInfo ModDump = Directory.CreateDirectory(Path.Combine(Main.SavePath, "AlienBloxUtility", "ModDump"));
                DirectoryInfo ModDecomp = Directory.CreateDirectory(Path.Combine(Main.SavePath, "AlienBloxUtility", "ModDecompiledLocation"));
                DirectoryInfo Dir = Directory.CreateDirectory(Path.Combine(Main.SavePath, "AlienBloxUtility", "Cache"));
                DirectoryInfo Log = Directory.CreateDirectory(Path.Combine(Main.SavePath, "AlienBloxUtility", "Logs"));
                DirectoryInfo directoryInfo = Directory.CreateDirectory(Path.Combine(Main.SavePath, "AlienBloxUtility", "Lua"));

                ModDumpLocation = ModDump.FullName;
                DecompLocation = ModDecomp.FullName;
                CacheLocation = Dir.FullName;
                LogLocation = Log.FullName;
                LuaStorageLocation = directoryInfo.FullName;

                if (Dir.Exists && !File.Exists(Dir.FullName + "\\tModUnpacker.exe"))
                {
                    Stream inputStream = InitialiseUtilities.ExtractContentFromAssembly("AlienBloxTools.Utilities.IncludedExes.tModUnpacker.exe");
                    FileStream fileStream = new(Dir.FullName + "\\tModUnpacker.exe", FileMode.Create, FileAccess.Write);

                    inputStream.CopyTo(fileStream);

                    fileStream.Close();
                    inputStream.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }
        }

        public override void Unload()
        {
            Instance = null;
            GlobalLua = null;
            LuaEnv = null;
            Cts = null;
            MainThreadQueue.Clear();
            MainThreadQueue = null;

            if (Directory.Exists(Path.Combine(Main.SavePath, "AlienBloxUtility", "Cache")) && AlienBloxUtilityServerConfig.Instance.ClearCache)
            {
                ClearDirectory(Path.Combine(Main.SavePath, "AlienBloxUtility", "Cache"));
            }
        }

        public static void WriteDebugMsg(string msg)
        {
            if (!AlienBloxUtilityConfig.Instance.GeneralDebugMessages)
            {
                return;
            }

            Console.WriteLine(msg);
            Main.NewText(msg);
            
            Instance.Logger.Debug(msg);
        }

        public static string GetPlatform()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return "Windows";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return "macOS";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return "Linux";
            }

            return "Unknown";
        }

        /// <summary>
        /// Extracts an entire tMod file
        /// </summary>
        /// <param name="FileLocation">The location of the tMod file (No file extension)</param>
        /// <param name="UtilityToExtractWith">The utility to extract the tModLoader mod with</param>
        /// <returns>The task.</returns>
        public static async Task ExtractTmodFile(string FileLocation, string UtilityToExtractWith)
        {
            if (!File.Exists(UtilityToExtractWith))
            {
                InitialiseUtilities.ExtractTMODUnpacker();

                if (AlienBloxUtilityConfig.Instance.DecompilerMessages)
                {
                    Main.NewText("File not detected!");
                }   

                return;
            }

            if (AlienBloxUtilityConfig.Instance.DecompilerMessages)
            {
                Main.NewText("File Detected!, launching utility!");
            }

            if (File.Exists($"{FileLocation}.tmod"))
            {
                Main.NewText("Mod File Detected!, starting program!");

                ProcessStartInfo startInfo = new()
                {
                    FileName = UtilityToExtractWith,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    Arguments = $"\"{FileLocation}.tmod\""
                };

                using Process process = new()
                { StartInfo = startInfo, EnableRaisingEvents = true };
                process.Start();

                // Async read of output
                string output = await process.StandardOutput.ReadToEndAsync();

                // Wait asynchronously for exit
                await Task.Run(() => process.WaitForExit());

                Instance.Logger.Debug("Output:");
                Instance.Logger.Debug(output);

                if (AlienBloxUtilityConfig.Instance.DecompilerMessages)
                {
                    Main.NewText($"Location: {$"{FileLocation}.tmod"}");
                    Main.NewText("Output:");
                    Main.NewText(output);
                }
            }
            else
            {
                Main.NewText("Mod File unknown.");
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
