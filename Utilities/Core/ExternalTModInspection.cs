using AlienBloxUtility.Utilities.UIUtilities.UIRenderers;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Assets;
using Terraria.ModLoader.Core;

namespace AlienBloxUtility.Utilities.Core
{
    public class ExternalTModInspection : ModSystem
    {
        #pragma warning disable CA2211 // Non-constant fields should not be visible
        public static List<TmodFile> LoadedFiles;
        #pragma warning restore CA2211 // Non-constant fields should not be visible

        public override void Load()
        {
            LoadedFiles = [];
        }

        public override void Unload()
        {
            LoadedFiles?.Clear();
            LoadedFiles = null;
        }

        /// <summary>
        /// Gets every single files from a tMod file
        /// </summary>
        /// <param name="file">The file to get.</param>
        /// <returns>The list of files as an array of strings (filename) and byte arrays (content)</returns>
        public static (string, byte[])[] GetModFiles(TmodFile file, bool rawImgConvert = false)
        {
            List<(string, byte[])> Files = [];

            using (file.Open())
            {
                try
                {
                    foreach (var modFiles in file)
                    {
                        string trueName = "";

                        trueName = modFiles.Name;

                        if (rawImgConvert && Path.GetExtension(trueName) == ".rawimg")
                            trueName = Path.ChangeExtension(trueName, ".rawimg");

                        Files.Add((trueName, file.GetBytes(modFiles)));
                    }

                    return [.. Files];
                }
                catch
                {
                    return [];
                }
            }
        }

        public static async Task<Texture2D> RawToPng(byte[] image)
        {
            RawImgReader Reader = new(Main.graphics.GraphicsDevice);

            return await Reader.FromStream<Texture2D>(new MemoryStream(image), new());
        }

        /// <summary>
        /// Exports and dumps a tMod file.
        /// </summary>
        /// <param name="pathToExport">The path to export</param>
        /// <param name="file">The file to export</param>
        public static void ExportToLocation(string pathToExport, TmodFile file)
        {
            Main.QueueMainThreadAction( async () =>
            {
                try
                {
                    var files = GetModFiles(file);

                    foreach (var modFile in files)
                    {
                        string locationExport = pathToExport + $"{file.Name}\\" + Path.GetDirectoryName(modFile.Item1);

                        Directory.CreateDirectory(locationExport);

                        using (file.Open())
                        {
                            string trueName = Path.GetFileName(modFile.Item1);

                            if (trueName.EndsWith(".rawimg"))
                            {
                                trueName = Path.ChangeExtension(trueName, ".png");
                            }

                            using var fs = File.Create(locationExport + $"\\{trueName}");

                            if (Path.GetExtension(modFile.Item1) == ".rawimg")
                            {
                                Texture2D texture = await RawToPng(modFile.Item2);

                                texture.SaveAsPng(fs, texture.Width, texture.Height);
                            }
                            else
                            {
                                foreach (byte b in modFile.Item2)
                                {
                                    fs.WriteByte(b);
                                }
                            }
                        }
                    }
                }
                catch
                {
                    ConHostRender.Write("Exception on extraction.");
                }
            });
        }
    }
}