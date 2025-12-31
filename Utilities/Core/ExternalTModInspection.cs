using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;

namespace AlienBloxUtility.Utilities.Core
{
    public class ExternalTModInspection : ModSystem
    {
        public static List<TmodFile> LoadedFiles;

        public override void Load()
        {
            LoadedFiles = [];
        }

        public override void Unload()
        {
            LoadedFiles?.Clear();
            LoadedFiles = null;
        }
    }
}