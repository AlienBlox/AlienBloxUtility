using AlienBloxUtility.Utilities.Core;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using Terraria;
using Terraria.ModLoader.Core;

namespace AlienBloxUtility.Utilities.DataStructures
{
    /// <summary>
    /// Represents AlienBlox's version of a tMod file <br/>
    /// Contains methods to read and write to a tMod file <br/>
    /// Insanely useful for reverse engineering tMod files
    /// </summary>
    public class AlienBloxTModFile
    {
        /// <summary>
        /// Creates an instance of AlienBlox's tMod File
        /// </summary>
        /// <param name="readPath">The path to read the tMod from</param>
        public AlienBloxTModFile(string readPath)
        {
            AssociatedFile = TModInspector.LoadFile(readPath);
            AssociatedEntries = TModInspector.GetFESet(AssociatedFile);
        }

        /// <summary>
        /// Initializes a new instance of the AlienBloxTModFile class using the specified tModLoader file.
        /// </summary>
        /// <remarks>This constructor loads and associates entries from the provided tModLoader file. The
        /// file must be a valid tModLoader mod file; otherwise, subsequent operations may fail.</remarks>
        /// <param name="file">The tModLoader file to associate with this instance. Cannot be null.</param>
        public AlienBloxTModFile(TmodFile file)
        {
            AssociatedFile = file;
            AssociatedEntries = TModInspector.GetFESet(AssociatedFile);
        }

        /// <summary>
        /// Creates a new tMod file and provides an AlienBlox's tMod File
        /// </summary>
        /// <param name="readPath">The path to read</param>
        /// <param name="contents">The contents to write onto the file</param>
        public AlienBloxTModFile(string readPath, byte[] contents)
        {
            using var fs = File.Create(readPath);

            //Paranoid set
            fs.Position = 0;
            fs.Write(contents);

            AssociatedFile = TModInspector.LoadFile(readPath);
            AssociatedEntries = TModInspector.GetFESet(AssociatedFile);
        }

        /// <summary>
        /// The associated tMod file.
        /// </summary>
        public TmodFile AssociatedFile { get; private set;  }
        /// <summary>
        /// Contains all the associated file entries with this AlienBlox's tMod File
        /// </summary>
        public TmodFile.FileEntry[] AssociatedEntries { get; private set; }

        /// <summary>
        /// Reads the tMod file contained inside AlienBlox's tMod File
        /// </summary>
        /// <returns>The <see cref="IDisposable"/> connected to this file</returns>
        public IDisposable Read() => AssociatedFile.Open();

        /// <summary>
        /// Reads the tMod file with a file entry
        /// </summary>
        /// <param name="entry">The entry to use</param>
        /// <returns>The file bytes</returns>
        public byte[] ReadFile(TmodFile.FileEntry entry)
        {
            if (AssociatedFile.IsOpen)
                return AssociatedFile.GetBytes(entry);

            using (Read())
            {
                return AssociatedFile.GetBytes(entry);
            }
        }

        /// <summary>
        /// Reads the tMod file with a fullname
        /// </summary>
        /// <param name="name">The file's fullname (Path + Name + Extension)</param>
        /// <returns>The file contents</returns>
        public byte[] ReadFile(string name)
        {
            if (AssociatedFile.IsOpen)
                return AssociatedFile.GetBytes(name);

            using (Read())
            {
                return AssociatedFile.GetBytes(name);
            }
        }

        /// <summary>
        /// Writes content to a tMod file
        /// </summary>
        /// <param name="fileName">The file's name</param>
        /// <param name="data">The data to write onto</param>
        public void WriteFile(string fileName, byte[] data)
        {
            Type tModType = typeof(TmodFile);
            MethodInfo WriteMethod = tModType.GetMethod("AddFile", BindingFlags.NonPublic | BindingFlags.Instance, [typeof(string), typeof(byte[])]);
            
            WriteMethod?.Invoke(AssociatedFile, [fileName, data]);
        }

        /// <summary>
        /// Destroys (removes) a file from the associated tMod file
        /// </summary>
        /// <param name="fileName">The file to destroy</param>
        public void DestroyFile(string fileName)
        {
            Type tModType = typeof(TmodFile);
            MethodInfo DestroyMethod = tModType.GetMethod("RemoveFile", BindingFlags.NonPublic | BindingFlags.Instance, [typeof(string)]);

            DestroyMethod?.Invoke(AssociatedFile, [fileName]);
        }

        /// <summary>
        /// Saves this tMod file.
        /// </summary>
        public void SaveFile()
        {
            Type tModType = typeof(TmodFile);
            MethodInfo SaveMethod = tModType.GetMethod("Save", BindingFlags.NonPublic | BindingFlags.Instance);
            FieldInfo FESet = tModType.GetField("fileTable", BindingFlags.NonPublic | BindingFlags.Instance);
            AlienBloxTModFile Clone = Copy(null, true);

            FESet?.SetValue(Clone.AssociatedFile, AssociatedEntries);  
            SaveMethod?.Invoke(Clone.AssociatedFile, null);
        }

        /// <summary>
        /// Closes this tMod file
        /// </summary>
        public void Close()
        {
            Type tModType = typeof(TmodFile);
            MethodInfo CloseMethod = tModType.GetMethod("Close", BindingFlags.NonPublic | BindingFlags.Instance);

            CloseMethod?.Invoke(AssociatedFile, null);
        }

#nullable enable
        /// <summary>
        /// Copies this TMod file handler to the specified path
        /// </summary>
        /// <param name="path">The path to copy to</param>
        /// <param name="patch"></param>
        /// <returns>The clone</returns>
        public AlienBloxTModFile Copy(string? path = null, bool patch = false)
        {
            path ??= Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads") + "\\";
            string name = $"{AssociatedFile.Name}.tmod";

            if (patch)
            {
                name = $"{AssociatedFile.Name}-patch.tmod";
            }

            byte[] copy = File.ReadAllBytes(AssociatedFile.path);

            if (Directory.Exists(path)) //Sanity check
            {
                File.WriteAllBytes(path + name, copy);
            }

            TmodFile file = TModInspector.LoadFile(path + $"{AssociatedFile.Name}.tmod");

            return new(file);
        }
        #nullable disable

        /// <summary>
        /// Reads the associated tMod file
        /// </summary>
        public void ReadTMod()
        {
            Type tModType = typeof(TmodFile);
            MethodInfo DestroyMethod = tModType.GetMethod("Read", BindingFlags.NonPublic | BindingFlags.Instance, Type.EmptyTypes);

            DestroyMethod?.Invoke(AssociatedFile, null);
        }

        /// <summary>
        /// Dumps the associated tMod file's contents
        /// </summary>
        /// <param name="source">Should it also decompile the mod's DLL</param>
        public void DumpFile(bool source)
        {
            ExternalTModInspection.ExportToLocation(AssociatedFile.path, AssociatedFile, source);
        }

        /// <summary>
        /// Patches the associated tMod file with the chosen contents
        /// </summary>
        /// <param name="fileName">The file to patch</param>
        /// <param name="patch">The patch to deploy</param>
        /// <param name="save">Whether or not to save the tMod file</param>
        public void Patch(string fileName, byte[] patch, bool save = true)
        {
            //DestroyFile(fileName);
            WriteFile(fileName, patch);

            if (save)
                SaveFile();
        }

        public TModFileMetadata ReadMetadata()
        {
            return new(AssociatedFile);
        }
    }

    /// <summary>
    /// Represents a metadata table for a TMod file
    /// </summary>
    public struct TModFileMetadata
    {
        /// <summary>
        /// The version of the mod
        /// </summary>
        public Version TModLoaderVersion { get; private set; }
        /// <summary>
        /// The hash of the mod
        /// </summary>
        public byte[] Hash { get; private set; }
        /// <summary>
        /// The signature of the mod
        /// </summary>
        public byte[] Sig { get; private set; }
        /// <summary>
        /// The associated tMod file (possibly null)
        /// </summary>
        public TmodFile AssociatedFile { get; private set; }

        /// <summary>
        /// Initializes a new instance of the TModFileMetadata class with the specified version, hash, and signature.
        /// </summary>
        /// <param name="ver">The version of the tModLoader used to create the mod file.</param>
        /// <param name="hash">A byte array containing the hash of the mod file. Cannot be null.</param>
        /// <param name="sig">A byte array containing the digital signature of the mod file. Cannot be null.</param>
        public TModFileMetadata(Version ver, byte[] hash, byte[] sig)
        {
            TModLoaderVersion = ver;
            Hash = hash;
            Sig = sig;
        }

        public TModFileMetadata(TmodFile file)
        {
            AssociatedFile = file;

            using FileStream FS = File.OpenRead(AssociatedFile.path);
            using BinaryReader reader = new(FS);

            if (Encoding.ASCII.GetString(reader.ReadBytes(4)) != "TMOD")
            {
                throw new("Well, that is NOT a tMod file but something evil instead");
            }

            TModLoaderVersion = new(reader.ReadString());
            Hash = reader.ReadBytes(20);
            Sig = reader.ReadBytes(256);
        }
    }
}