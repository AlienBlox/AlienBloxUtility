using AlienBloxUtility.Utilities.Core;
using AlienBloxUtility.Utilities.Reflector.Engine;
using Steamworks;
using System;
using System.IO;
using System.Reflection;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader.Core;

namespace AlienBloxUtility.Utilities.Reflector.InternalHooks
{
    /// <summary>
    /// Contains all the helpers to quickly reflect things from Terraria
    /// Useful for making dummy instances for testing
    /// </summary>
    public static class TerrariaHandler
    {
        /// <summary>
        /// Creates a new instance of the LocalizedText class using the specified key and content for testing or
        /// placeholder purposes.
        /// </summary>
        /// <remarks>This method is intended for scenarios where a valid LocalizedText object is needed
        /// without relying on actual localization data, such as in unit tests or placeholder implementations.</remarks>
        /// <param name="fakeKey">The key to associate with the dummy localized text. This value is used as the identifier for the text entry.</param>
        /// <param name="fakeContent">The content string to use for the dummy localized text. This value represents the text that will be returned
        /// for the specified key.</param>
        /// <returns>A LocalizedText instance initialized with the provided key and content.</returns>
        public static LocalizedText CreateDummyLocalization(string fakeKey, string fakeContent)
        {
            Type type = LocalizedText.Empty.GetTypeWithCache();

            // Fix CS1744: Use only positional arguments or only named arguments, not both.
            // Fix CS1001: Remove stray '.' at the end.
            ConstructorInfo ctor = type.GetConstructor(
                BindingFlags.Instance | BindingFlags.NonPublic,
                null,
                [typeof(string), typeof(string)],
                null
            );

            return (LocalizedText)ctor.Invoke([fakeKey, fakeContent]);
        }

        /// <summary>
        /// Creates a new instance of a dummy <see cref="TmodFile.FileEntry"/> with the specified parameters.
        /// </summary>
        /// <remarks>This method uses reflection to instantiate a <see cref="TmodFile.FileEntry"/> even if
        /// its constructor is non-public. Use with caution, as changes to the internal structure of <see
        /// cref="TmodFile.FileEntry"/> may affect compatibility.</remarks>
        /// <param name="name">The name to assign to the file entry. Cannot be null.</param>
        /// <param name="offset">The offset, in bytes, where the file entry begins within the containing file. Must be non-negative.</param>
        /// <param name="length">The uncompressed length, in bytes, of the file entry. Must be non-negative.</param>
        /// <param name="compressedLength">The compressed length, in bytes, of the file entry. Must be non-negative.</param>
        /// <param name="cachedBytes">An optional byte array containing the cached file data. May be null if no cached data is available.</param>
        /// <returns>A new <see cref="TmodFile.FileEntry"/> instance initialized with the specified parameters.</returns>
        public static TmodFile.FileEntry CreateDummyFEEntry(string name, int offset, int length, int compressedLength, byte[] cachedBytes = null)
        {
            Type type = typeof(TmodFile.FileEntry).GetTypeWithCache();

            ConstructorInfo ctor = type.GetConstructor(
                BindingFlags.Instance | BindingFlags.NonPublic,
                null,
                [typeof(string), typeof(int), typeof(int), typeof(int), typeof(byte[])],
                null
            );

            return (TmodFile.FileEntry)ctor.Invoke([name, offset, length, compressedLength, cachedBytes]);
        }

        /// <summary>
        /// Loads a Tmod file from the specified file path.
        /// </summary>
        /// <param name="path">The path to the Tmod file to load. Must refer to an existing file.</param>
        /// <returns>A <see cref="TmodFile"/> instance representing the loaded Tmod file.</returns>
        public static TmodFile LoadTMod(string path) => TModInspector.LoadFile(path);

        /// <summary>
        /// Dynamically compiles a tModLoader mod with reflection
        /// </summary>
        /// <param name="path">The path to turn into a tModLoader tMod file</param>
        public static void DynamicCompileTmod(string path)
        {
            Type t = typeof(TmodFile).Assembly.GetType("Terraria.ModLoader.Core.ModCompile");

            var method = t.GetMethod("Build", BindingFlags.Static | BindingFlags.NonPublic, [typeof(string)]);

            method?.Invoke(null, [path]);
        }

        /// <summary>
        /// Creates a new Tilemap
        /// </summary>
        /// <param name="width">The width of the Tilemap</param>
        /// <param name="height">The height of the Tilemap</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static Tilemap CreateTilemap(int width, int height)
        {
            // 1) Get the Type
            Type TilemapType = typeof(Tilemap);

            // 2) Find the constructor
            ConstructorInfo ctor = TilemapType.GetConstructor(
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public,
                binder: null,
                types: [typeof(ushort), typeof(ushort)],
                modifiers: null
            ) ?? throw new Exception("Constructor not found on Tilemap");

            // 3) Invoke it
            return (Tilemap)ctor.Invoke([(ushort)width, (ushort)height]);
        }
    }
}