using AlienBloxUtility.Utilities.Helpers;
using AlienBloxUtility.Utilities.UIUtilities.UIElements;
using AlienBloxUtility.Utilities.UIUtilities.UIRenderers;
using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.CSharp;
using ICSharpCode.Decompiler.IL;
using ICSharpCode.Decompiler.Metadata;
using Steamworks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;
using Version = System.Version;

namespace AlienBloxUtility.Utilities.Core
{
    public class TModInspector : ModSystem
    {
        public static Dictionary<string, (TmodFile, Assembly)> ModFileData;

        public static Assembly[] LoadedAssemblies => AppDomain.CurrentDomain.GetAssemblies();

        public static string WorkshopSearch => GetWorkshopInstall();

        public static string LocalSearch => $"{Main.SavePath}\\Mods\\";

        public override void OnModLoad()
        {
            ModFileData = [];
            SearchAllTMods();
        }

        public override void OnModUnload()
        {
            ModFileData = null;
        }

        private static void SearchAllTMods()
        {
            try
            {
                Task.Run(async () =>
                {
                    foreach (var localMod in GetFilesWithExtension(LocalSearch, ".tmod"))
                    {
                        LoadFile(localMod);

                        AlienBloxUtility.Instance.Logger.Debug($"Found mod in scan: {localMod}");
                    }

                    foreach (var workshopMod in GetFilesWithExtension(WorkshopSearch, ".tmod"))
                    {
                        LoadFile(WorkshopSearch);

                        AlienBloxUtility.Instance.Logger.Debug($"Found mod in scan: {workshopMod}");
                    }
                });
            }
            catch 
            {

            }
        }

        private static string GetWorkshopInstall()
        {
            SteamApps.GetAppInstallDir((AppId_t)1281930, out string steamInstallPath, 512);

            string workshopFolder = Path.Combine(steamInstallPath, "steamapps", "workshop", "content", "105600");

            if (Directory.Exists(workshopFolder))
            {
                Console.WriteLine("tModLoader Workshop Path: " + workshopFolder);

                return workshopFolder;
            }
            else
            {
                Console.WriteLine("Workshop directory not found.");

                return "";
            }
        }

        static string[] GetFilesWithExtension(string path, string fileExtension)
        {
            List<string> fileList = new List<string>();  // List to store the file paths

            try
            {
                // Check if the directory exists before attempting to get files or directories
                if (string.IsNullOrWhiteSpace(path))  // Check if the path is empty or just whitespace
                {
                    Console.WriteLine("The provided directory path is empty or whitespace.");
                    return new string[0];  // Return an empty array if the path is invalid
                }

                if (!Directory.Exists(path))
                {
                    Console.WriteLine($"The directory {path} does not exist.");
                    return new string[0];  // Return an empty array if the directory doesn't exist
                }

                // Search for files with the specific extension in the current directory
                string[] files = Directory.GetFiles(path, fileExtension);
                fileList.AddRange(files);  // Add found files to the list

                // Recursively search in all subdirectories
                string[] directories = Directory.GetDirectories(path);
                foreach (var directory in directories)
                {
                    // Recursively add files from subdirectories
                    fileList.AddRange(GetFilesWithExtension(directory, fileExtension));
                }
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine($"Access denied: {e.Message}");
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine($"Directory not found: {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"An unexpected error occurred: {e.Message}");
            }

            // Return the list as an array
            return [.. fileList];
        }

        /// <summary>
        /// Decompiles a tModLoader mod with thread safety
        /// </summary>
        /// <param name="ModName">The mod to decompile</param>
        public static void DecompileModThreadSafe(string ModName = "AlienBloxUtility")
        {
            MessWithMod(ModName, false);
            Task.Run(async () => DecompileAssembly(ModName));
        }

        /// <summary>
        /// Loads a new tMod file from the path
        /// </summary>
        /// <param name="path">The file path to load</param>
        /// <returns>The tMod File</returns>
        public static TmodFile LoadFile(string path, string name = null, Version version = null)
        {
            // 1) Get the Type
            Type tmodFileType = typeof(TmodFile);

            // 2) Find the constructor
            ConstructorInfo ctor = tmodFileType.GetConstructor(
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public,
                binder: null,
                types: [typeof(string), typeof(string), typeof(Version)],
                modifiers: null
            ) ?? throw new Exception("Constructor not found on TmodFile");

            // 3) Invoke it
            var file = (TmodFile)ctor.Invoke([path, name, version]);

            using (file.Open())
            {
                //Ensure proper opening for compatibility
            }

            ExternalTModInspection.LoadedFiles.Add(file);

            return file;
        }

        /// <summary>
        /// Decompiles an assembly of the selected mod
        /// </summary>
        /// <param name="ModName">The mod name to try to find</param>
        public static async Task DecompileAssembly(string ModName)
        {
            if (!Directory.Exists(AlienBloxUtility.ModDumpLocation + $"\\{ModName}") || !ModFileData.TryGetValue(ModName, out var ModContents))
            {
                return;
            }

            try
            {
                if (AlienBloxUtilityConfig.Instance.DecompilerMessages)
                {
                    Main.NewText(Language.GetTextValue("Mods.AlienBloxUtility.Messages.Decompiler.DecompilerStart"));
                    AlienBloxUtility.Instance.Logger.Info(Language.GetTextValue("Mods.AlienBloxUtility.Messages.Decompiler.DecompilerStart"));
                }

                byte[] dllBytes = ModContents.Item1.GetModAssembly();
                byte[] pdbBytes = ModContents.Item1.GetModPdb();

                string outputRoot = AlienBloxUtility.DecompLocation;

                // Step 1: Create a temp folder
                string tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
                Directory.CreateDirectory(tempDir);

                string tempDllPath = Path.Combine(tempDir, "MainAssembly.dll");
                File.WriteAllBytes(tempDllPath, dllBytes);

                string tempPdbPath = null;
                if (pdbBytes != null && pdbBytes.Length > 0)
                {
                    tempPdbPath = Path.Combine(tempDir, "MainAssembly.pdb");
                    File.WriteAllBytes(tempPdbPath, pdbBytes);
                }

                // 2. Create the resolver using the temp folder
                var resolver = new UniversalAssemblyResolver(
                    mainAssemblyFileName: tempDllPath,
                    throwOnError: false,
                    targetFramework: null
                );

                resolver.AddSearchDirectory(tempDir); // ensure dependencies in temp folder are found

                foreach (var asm in LoadedAssemblies)
                {
                    if (!string.IsNullOrEmpty(asm.Location) && File.Exists(asm.Location))
                    {
                        string dir = Path.GetDirectoryName(asm.Location);
                        resolver.AddSearchDirectory(dir);
                    }
                }

                // 3. Initialize decompiler
                var settings = new DecompilerSettings();
                var decompiler = new CSharpDecompiler(tempDllPath, resolver, settings);

                // 4. Decompile all types
                foreach (var type in decompiler.TypeSystem.MainModule.TypeDefinitions)
                {
                    string code = decompiler.DecompileTypeAsString(type.FullTypeName);

                    // Map namespace to folder
                    string nsPath = string.IsNullOrEmpty(type.Namespace)
                        ? ""
                        : type.Namespace.Replace('.', Path.DirectorySeparatorChar);
                    string fullDir = Path.Combine(outputRoot, nsPath);
                    Directory.CreateDirectory(fullDir);

                    string filePath = Path.Combine(fullDir, type.Name.SanitizeFileName() + ".cs");
                    File.WriteAllText(filePath, code);

                    if (AlienBloxUtilityConfig.Instance.DecompilerMessages)
                    {
                        string Content = Language.GetText("Mods.AlienBloxUtility.Messages.Decompiler.DecompilerFile").Format(type.Name.SanitizeFileName() + ".cs", PacketSpyUtility.UnixTime);

                        Main.NewText(Content);
                        AlienBloxUtility.Instance.Logger.Info(Content);
                    }
                }

                if (AlienBloxUtilityConfig.Instance.DecompilerMessages)
                {
                    Main.NewText(Language.GetTextValue("Mods.AlienBloxUtility.Messages.Decompiler.DecompilerEnd"));
                    AlienBloxUtility.Instance.Logger.Info(Language.GetTextValue("Mods.AlienBloxUtility.Messages.Decompiler.DecompilerEnd"));
                }

                string DecompModPath = $"{AlienBloxUtility.DecompLocation}\\{ModName}\\";

                File.WriteAllBytes(DecompModPath + $"{ModName}.dll", ModContents.Item1.GetModAssembly());
                File.WriteAllBytes(DecompModPath + $"{ModName}.pdb", ModContents.Item1.GetModPdb());
                File.WriteAllBytes($"{AlienBloxUtility.DecompLocation}\\{ModName}.tmod", File.ReadAllBytes(ModContents.Item1.path));

                if (Environment.OSVersion.Platform == PlatformID.Win32NT && File.Exists($"{AlienBloxUtility.CacheLocation}\\tModUnpacker.exe"))
                {
                    await AlienBloxUtility.ExtractTmodFile($"{AlienBloxUtility.DecompLocation}\\{ModName}", $"{AlienBloxUtility.CacheLocation}\\tModUnpacker.exe");
                }

                Directory.Delete(tempDir, true);
            }
            catch (Exception ex)
            {
                AlienBloxUtility.Instance.Logger.Warn(ex.ToString());
            }
        }

        /// <summary>
        /// Puts a mod on the Mods list and also dumps the mod's content but async
        /// </summary>
        /// <param name="ModName">The name of the mod to have it's content dumped</param>
        /// <returns>The async task</returns>
        public static async Task MessWithModAsync(string ModName)
        {
            MessWithMod(ModName);
        }

        /// <summary>
        /// Puts a mod on the Mods list and also dumps the mod's content (WARNING: MAY CAUSE PERFORMANCE ISSUES)
        /// </summary>
        /// <param name="ModName">The name of the mod to have it's content dumped</param>
        public static void MessWithMod(string ModName, bool dump = true)
        {
            AddMod(ModName);

            if (ModFileData.ContainsKey(ModName) && dump)
            {
                DumpMod(ModName);
            }
        }

        /// <summary>
        /// Dumps but doesn't decompile a mod's content
        /// </summary>
        /// <param name="ModName">The name of the mod to have it's content dumped</param>
        public static void DumpMod(string ModName)
        {
            if (ModFileData.TryGetValue(ModName, out (TmodFile, Assembly) ModOutput) && (!File.Exists(AlienBloxUtility.ModDumpLocation + $"\\{ModName}\\{ModName}.dll") || !File.Exists(AlienBloxUtility.ModDumpLocation + $"\\{ModName}\\{ModName}.pdb") || !File.Exists(AlienBloxUtility.ModDumpLocation + $"\\{ModName}\\{ModName}.tmod")))
            {
                try
                {
                    Directory.CreateDirectory(AlienBloxUtility.ModDumpLocation + $"\\{ModName}");

                    TmodFile.FileEntry[] FESet = GetFESet(ModOutput.Item1);

                    byte[] Assembly = ModOutput.Item1.GetModAssembly();
                    byte[] PDB = ModOutput.Item1.GetModPdb();
                    File.WriteAllBytes(AlienBloxUtility.ModDumpLocation + $"\\{ModName}\\{ModName}.dll", Assembly);
                    File.WriteAllBytes(AlienBloxUtility.ModDumpLocation + $"\\{ModName}\\{ModName}.pdb", PDB);
                    File.WriteAllBytes(AlienBloxUtility.ModDumpLocation + $"\\{ModName}\\{ModName}.tmod", File.ReadAllBytes(ModOutput.Item1.path));
                    AlienBloxUtility.Instance.Logger.Info($"Mod Path: {ModOutput.Item1.path}");
                }
                catch (Exception ex)
                {
                    AlienBloxUtility.Instance.Logger.Warn("Can't dump mod due to exception:" + ex.Message);
                }
            }
        }
        
        /// <summary>
        /// Gets the file entry set for this TmodFile
        /// </summary>
        /// <param name="ModFile">The TmodFile to query</param>
        /// <returns>The file entry, null if none</returns>
        public static TmodFile.FileEntry[] GetFESet(TmodFile ModFile)
        {
            try
            {
                Type T = ModFile.GetType();
                FieldInfo FI = T.GetField("fileTable", BindingFlags.Instance | BindingFlags.NonPublic);

                if (FI != null && FI.GetValue(ModFile) is TmodFile.FileEntry[] v)
                {
                    ConHostRender.Write("File entries retrieved!");

                    return v;
                }
            }
            catch (Exception ex)
            {
                AlienBloxUtility.Instance.Logger.Error("Can't get File Entry set:" + ex.Message);

                if (Main.netMode != NetmodeID.Server)
                {
                    ConHostRender.Write("Can't get File Entry set:" + ex.Message);
                }
            }

            return null;
        }
            
        /// <summary>
        /// Adds the following mod to the Mod file data list
        /// </summary>
        /// <param name="ModName">The mod to add</param>
        public static void AddMod(string ModName)
        {
            try
            {
                if (ModLoader.TryGetMod(ModName, out Mod Result))
                {
                    Type ModType = Result.GetType();
                    PropertyInfo Info = ModType.GetProperty("File", BindingFlags.Instance | BindingFlags.NonPublic);

                    if (Info != null)
                    {
                        object Output = Info.GetValue(Result);

                        if (Output is TmodFile file)
                        {
                            ModFileData.TryAdd(ModName, (file, Result.Code));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AlienBloxUtility.Instance.Logger.Error($"Can't set mod named '{ModName}':" + ex.Message);
            }
        }

        /// <summary>
        /// Returns a list of mods to be decompiled at the decompiler
        /// </summary>
        /// <returns>The thing for every mod</returns>
        public static DecompilerModListDisplay[] GetAllMods()
        {
            List<DecompilerModListDisplay> Mods = [];

            foreach (Mod M in ModLoader.Mods)
            {
                Mods.Add(new(M.Name));
            }

            return [.. Mods];
        }
    }
}