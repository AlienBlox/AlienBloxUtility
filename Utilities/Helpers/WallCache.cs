using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace AlienBloxUtility.Utilities.Helpers
{
    public class WallCache : ModSystem
    {
        private static Dictionary<int, Item> WallToItemTable;

        public override void PostSetupContent()
        {
            WallToItemTable = [];

            for (int i = 0; i < ItemLoader.ItemCount; i++)
            {
                Item item = new(i);

                WallToItemTable.TryAdd(item.createWall, item);
            }
        }

        public override void Unload()
        {
            WallToItemTable = null;
        }

        public static int ItemFromWall(int wall)
        {
            if (WallToItemTable.TryGetValue(wall, out var item))
            {
                return item.type;
            }

            return 0;
        }
    }
}