using Terraria;
using Terraria.ModLoader.IO;

namespace AlienBloxUtility.Utilities.DataStructures
{
    public struct WireData(Tile tile)
    {
        public bool WireRed = tile.RedWire;
        public bool WireGreen = tile.GreenWire;
        public bool WireBlue = tile.BlueWire;
        public bool WireYellow = tile.YellowWire;

        public readonly TagCompound Save()
        {
            TagCompound t = [];

            t["WireR"] = WireRed;
            t["WireG"] = WireGreen;
            t["WireB"] = WireBlue;
            t["WireY"] = WireYellow;

            return t;
        }
    }
}