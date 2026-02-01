using Terraria;

namespace AlienBloxUtility.Utilities.Interfaces
{
    public interface ITileManagement : ICanBeSaved
    {
        public int Width { get; set; }

        public int Height { get; set; }

        public Tilemap Grid { get; set; }
    }
}