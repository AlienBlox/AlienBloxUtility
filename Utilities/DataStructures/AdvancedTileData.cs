using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;

namespace AlienBloxUtility.Utilities.DataStructures
{
    #pragma warning disable CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
    #pragma warning disable CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
    public struct AdvancedTileData
    #pragma warning restore CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
    #pragma warning restore CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
    {
        public int PosX, PosY;

        public readonly bool HasAssociatedEntity => TileEntity.ByPosition.TryGetValue(PositionP16, out _);

        public readonly TileEntity AssociatedTE { get => TileEntity.ByPosition[PositionP16]; set => TileEntity.ByPosition[PositionP16] = value; }

        public readonly Point Position => new(PosX, PosY);

        public readonly Point16 PositionP16 => new(PosX, PosY);

        public readonly Tile AssociatedTile => Main.tile[PositionP16];

        public AdvancedTileData() : this(0, 0)
        {

        }

        public AdvancedTileData(int x, int y)
        {
            PosX = x;
            PosY = y;
        }

        public AdvancedTileData(Point p)
        {
            PosX = p.X;
            PosY = p.Y;
        }

        public AdvancedTileData(Point16 p)
        {
            PosX += p.X;
            PosY += p.Y;
        }

        public static bool operator !=(AdvancedTileData data1, AdvancedTileData data2)
        {
            return data1.AssociatedTile.Equals(data2.AssociatedTile);
        }

        public static bool operator ==(AdvancedTileData data1, AdvancedTileData data2)
        {
            return data1.AssociatedTile.Equals(data2.AssociatedTile);
        }
    }
}