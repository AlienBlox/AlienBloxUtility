using AlienBloxUtility.Utilities.Interfaces;
using AlienBloxUtility.Utilities.Reflector.InternalHooks;
using Terraria;
using Terraria.ModLoader.IO;

namespace AlienBloxUtility.Utilities.DataStructures
{
    public struct TileStructure(int width, int height) : ITileManagement
    {
        public int Width { get; set; } = width;
        public int Height { get; set; } = height;
        public Tilemap Grid { get; set; } = TerrariaHandler.CreateTilemap(width, height);

        public readonly void Load(TagCompound tag)
        {
            
        }

        public readonly void Save(TagCompound tag)
        {
            tag["Width"] = Width;
            tag["Height"] = Height;

            tag["TileGrid"] = Grid.Save();
        }
    }
}