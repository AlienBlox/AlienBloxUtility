using Terraria.ID;
using Terraria.ModLoader;

namespace AlienBloxUtility.Utilities.Helpers
{
    public class ItemEnable : GlobalItem
    {
        internal static bool[] ItemsThatShouldNotBeInInventory;

        internal static bool[] Deprecated;

        public override void SetStaticDefaults()
        {
            bool instance = AlienBloxUtilityServerConfig.Instance.EnableBrokenItems;

            ItemsThatShouldNotBeInInventory = (bool[])ItemID.Sets.ItemsThatShouldNotBeInInventory.Clone();
            Deprecated = (bool[])ItemID.Sets.Deprecated.Clone();

            for (int i = 0; i < ItemID.Sets.ItemsThatShouldNotBeInInventory.Length; i++)
            {
                if (ItemID.Sets.ItemsThatShouldNotBeInInventory[i])
                    ItemID.Sets.ItemsThatShouldNotBeInInventory[i] = !instance;
            }

            for (int i = 0; i < ItemID.Sets.Deprecated.Length; i++)
            {
                if (ItemID.Sets.ItemsThatShouldNotBeInInventory[i])
                    ItemID.Sets.Deprecated[i] = !instance;
            }
        }
    }
}