using AlienBloxUtility.Utilities.Core;
using System;
using System.IO;
using System.Reflection;
using Terraria.ModLoader.Core;

namespace AlienBloxUtility.Utilities.DataStructures
{
    /// <summary>
    /// A non-value type variant of <see cref="ValueModProperties"/>
    /// </summary>
    public class ModProperties(ValueModProperties internalProperty)
    {
        private readonly ValueModProperties _internalProperty = internalProperty;

        /// <summary>
        /// The associated .tmod file with this property sheet
        /// </summary>
        public TmodFile AssociatedFile => _internalProperty.AssociatedFile;
        /// <summary>
        /// The tModLoader version of the associated mod
        /// </summary>
        public Version TModLoaderVersion => _internalProperty.TModLoaderVersion;
        /// <summary>
        /// The Mod version of the associated mod
        /// </summary>
        public Version ModVersion => _internalProperty.ModVersion;
        /// <summary>
        /// The dll references of the associated mod
        /// </summary>
        public string[] DllReferences => _internalProperty.dllReferences;
        /// <summary>
        /// The build.txt of the mod as a byte array
        /// </summary>
        public byte[] BuildTxtFile => _internalProperty.buildTxtFile;
        /// <summary>
        /// The author of the associated mod
        /// </summary>
        public string Author => _internalProperty.author;
        /// <summary>
        /// The workshop name of the associated mod
        /// </summary>
        public string DisplayName => _internalProperty.displayName;
        /// <summary>
        /// Is the code (DLL) of the associated mod hidden (Does nothing for AlienBlox's Utilities as it decompiles the mod anyways)
        /// </summary>
        public bool HideCode => _internalProperty.hideCode;
        /// <summary>
        /// Is the source code of the associated mod hidden (Does nothing for AlienBlox's Utilities as it decompiles the mod anyways)
        /// </summary>
        public bool IncludeSource => _internalProperty.includeSource;
        /// <summary>
        /// Is the assets of the associated mod hidden (Does nothing for AlienBlox's Utilities as it decompiles the mod anyways)
        /// </summary>
        public bool HideResources => _internalProperty.hideResources;
        /// <summary>
        /// Is the PDB of the associated mod hidden (Does nothing for AlienBlox's Utilities as it decompiles the mod anyways)
        /// </summary>
        public bool IncludePDB => _internalProperty.includePDB;

        /// <summary>
        /// Dumps an entire build.txt file of the connected mod
        /// </summary>
        /// <param name="Path">The path to dump at.</param>
        public void DumpBuildTxt(string Path)
        {
            _internalProperty.DumpBuildTxt(Path);
        }

        public override string ToString() => _internalProperty.ToString();

        /// <summary>
        /// Converts the Mod Property to a string
        /// </summary>
        /// <param name="instance">The instance to convert.</param>
        public static implicit operator string(ModProperties instance)
        {
            return instance.ToString();
        }

        //// <summary>
        /// Converts the Mod Property to its Value type counterpart
        /// </summary>
        /// <param name="instance">The instance to convert.</param>
        public static implicit operator ValueModProperties(ModProperties instance)
        {
            return instance._internalProperty;
        }
    }

    /// <summary>
    /// Defines a mod property sheet used for mod info, see <see cref="ModProperties"/> for it's reference type brother
    /// </summary>
    public readonly struct ValueModProperties
    {
        /// <summary>
        /// The associated .tmod file with this property sheet
        /// </summary>
        public readonly TmodFile AssociatedFile;
        /// <summary>
        /// The tModLoader version of the associated mod
        /// </summary>
        public readonly Version TModLoaderVersion;
        /// <summary>
        /// The Mod version of the associated mod
        /// </summary>
        public readonly Version ModVersion;
        /// <summary>
        /// The dll references of the associated mod
        /// </summary>
        public readonly string[] dllReferences;
        /// <summary>
        /// The build.txt of the mod as a byte array
        /// </summary>
        public readonly byte[] buildTxtFile;
        /// <summary>
        /// The author of the associated mod
        /// </summary>
        public readonly string author;
        /// <summary>
        /// The workshop name of the associated mod
        /// </summary>
        public readonly string displayName;
        /// <summary>
        /// Is the code (DLL) of the associated mod hidden (Does nothing for AlienBlox's Utilities as it decompiles the mod anyways)
        /// </summary>
        public readonly bool hideCode;
        /// <summary>
        /// Is the source code of the associated mod hidden (Does nothing for AlienBlox's Utilities as it decompiles the mod anyways)
        /// </summary>
        public readonly bool includeSource;
        /// <summary>
        /// Is the assets of the associated mod hidden (Does nothing for AlienBlox's Utilities as it decompiles the mod anyways)
        /// </summary>
        public readonly bool hideResources;
        /// <summary>
        /// Is the PDB of the associated mod hidden (Does nothing for AlienBlox's Utilities as it decompiles the mod anyways)
        /// </summary>
        public readonly bool includePDB;

        /// <summary>
        /// Creates a new <see cref="ModProperties"/> instance with the data from the tMod file
        /// </summary>
        /// <param name="file"></param>
        public ValueModProperties(TmodFile file)
        {
            AssociatedFile = file;

            TModLoaderVersion = file.TModLoaderVersion;
            ModVersion = file.Version;

            try
            {
                foreach (var obj in ExternalTModInspection.AllTModLoaderDetectedMods)
                {
                    if (obj.GetType() == typeof(TmodFile).Assembly.GetType("Terraria.ModLoader.Core.LocalMod"))
                    {
                        foreach (var field in obj.GetType().GetFields())
                        {
                            if (field.Name == "modFile" && field.GetValue(obj) == file)
                            {
                                foreach (var propsAttempt in obj.GetType().GetFields())
                                {
                                    if (propsAttempt.Name == "properties")
                                    {
                                        var props = propsAttempt.GetValue(obj);

                                        if (props != null)
                                        {
                                            dllReferences = (string[])props.GetType().GetField("dllReferences", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(props);
                                            author = (string)props.GetType().GetField("author", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(props);
                                            displayName = (string)props.GetType().GetField("displayName", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(props);
                                            hideCode = (bool)props.GetType().GetField("hideCode", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(props);
                                            includeSource = (bool)props.GetType().GetField("includeSource", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(props);
                                            hideResources = (bool)props.GetType().GetField("hideResources", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(props);
                                            buildTxtFile = (byte[])props.GetType().GetMethod("ToBytes", BindingFlags.Instance | BindingFlags.NonPublic)?.Invoke(props, null);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// Dumps an entire build.txt file of the connected mod
        /// </summary>
        /// <param name="Path">The path to dump at.</param>
        public void DumpBuildTxt(string Path)
        {
            using var f = File.Create(Path);

            for (int i = 0; i < buildTxtFile.Length; i++)
            {
                f.WriteByte(buildTxtFile[i]);
            }
        }

        public override string ToString()
        {
            return $"Properties: hideCode: {hideCode}, hideResources: {hideResources}, includeSource: {includeSource}, Mod Version: {ModVersion}, tModLoader Version: {TModLoaderVersion}, Filename: {AssociatedFile.Name}";
        }

        /// <summary>
        /// Converts the Value Type Mod Property to a string
        /// </summary>
        /// <param name="instance">The instance to convert.</param>
        public static implicit operator string(ValueModProperties instance)
        {
            return instance.ToString();
        }


        //// <summary>
        /// Converts the Value type Mod Property to its reference type counterpart
        /// </summary>
        /// <param name="instance">The instance to convert.</param>
        public static implicit operator ModProperties(ValueModProperties instance)
        {
            return new(instance);
        }
    }
}