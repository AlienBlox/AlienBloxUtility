using AlienBloxUtility.Utilities.Helpers;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlienBloxUtility.Utilities
{
    public class UtilityUnloader : ModSystem
    {
        public override void OnModUnload()
        {
            ItemID.Sets.ItemsThatShouldNotBeInInventory = ItemEnable.ItemsThatShouldNotBeInInventory;
            ItemID.Sets.Deprecated = ItemEnable.Deprecated;
        }
    }
}