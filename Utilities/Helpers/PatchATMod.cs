using AlienBloxUtility.Utilities.Core;
using AlienBloxUtility.Utilities.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Linq;
using Terraria.ModLoader.Core;

namespace AlienBloxUtility.Utilities.Helpers
{
    /// <summary>
    /// A helper class to centralize everything relating to patching tMods
    /// </summary>
    public static class PatchATMod
    {
        /// <summary>
        /// Patches a tMod file
        /// </summary>
        /// <param name="ModToPatch">The name of the mod to patch</param>
        /// <param name="FileName">The filename to patch</param>
        /// <param name="Patch">The contents of the patch</param>
        /// <param name="save">Whether or not to save the tMod file</param>
        public static void Patch(string ModToPatch, string FileName, byte[] Patch, bool save = true)
        {
            TmodFile Mod = ExternalTModInspection.GetAllModsLoaded().First(mod => mod.Name == ModToPatch);

            if (Mod != null)
            {
                AlienBloxTModFile loadedFile = new(Mod);

                loadedFile.Patch(FileName, Patch, save);
            }
        }

        /// <summary>
        /// Changes the image assets inside a tMod file
        /// </summary>
        /// <param name="ModToPatch">The name of the mod to patch</param>
        /// <param name="FileName">The file inside the Mod's <see cref="TmodFile"/></param>
        /// <param name="PatchImg">The image to load the patch as</param>
        /// <param name="save">Whether or not to save the tMod file</param>
        public static void PatchImage(string ModToPatch, string FileName, Texture2D PatchImg, bool save = true)
        {
            using MemoryStream memoryStream = new();

            PatchImg.SaveAsPng(memoryStream, PatchImg.Width, PatchImg.Height);

            Patch(ModToPatch, FileName, memoryStream.ToArray(), save);
        }
    }
}