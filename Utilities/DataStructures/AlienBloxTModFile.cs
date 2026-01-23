using AlienBloxUtility.Utilities.Core;
using System;
using System.IO;
using System.Reflection;
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
        public byte[] ReadFile(TmodFile.FileEntry entry) => AssociatedFile.GetBytes(entry);

        /// <summary>
        /// Reads the tMod file with a fullname
        /// </summary>
        /// <param name="name">The file's fullname (Path + Name + Extension)</param>
        /// <returns>The file contents</returns>
        public byte[] ReadFile(string name) => AssociatedFile.GetBytes(name);

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

            SaveMethod?.Invoke(AssociatedFile, null);
        }
    }
}