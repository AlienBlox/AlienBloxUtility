using Terraria.ID;
using Terraria.ModLoader;

namespace AlienBloxUtility.Utilities.Helpers
{
    public class ItemEnable : GlobalItem
    {
        public override void SetStaticDefaults()
        {
            if (!AlienBloxUtilityServerConfig.Instance.EnableBrokenItems)
                return;

            ItemID.Sets.ItemsThatShouldNotBeInInventory[ItemID.FirstFractal] = false;
            ItemID.Sets.ItemsThatShouldNotBeInInventory[ItemID.SkeletonBow] = false;
            ItemID.Sets.ItemsThatShouldNotBeInInventory[ItemID.OgreMask] = false;
            ItemID.Sets.ItemsThatShouldNotBeInInventory[ItemID.ApplePieSlice] = false;
            ItemID.Sets.ItemsThatShouldNotBeInInventory[ItemID.KoboldDynamiteBackpack] = false;
            ItemID.Sets.ItemsThatShouldNotBeInInventory[ItemID.EtherianJavelin] = false;
            ItemID.Sets.ItemsThatShouldNotBeInInventory[ItemID.GoblinBomberCap] = false;
            ItemID.Sets.ItemsThatShouldNotBeInInventory[ItemID.GoblinMask] = false;
            ItemID.Sets.ItemsThatShouldNotBeInInventory[ItemID.PhasicWarpEjector] = false;
            ItemID.Sets.ItemsThatShouldNotBeInInventory[ItemID.VortexAxe] = false;
            ItemID.Sets.ItemsThatShouldNotBeInInventory[ItemID.StardustAxe] = false;
            ItemID.Sets.ItemsThatShouldNotBeInInventory[ItemID.NebulaAxe] = false;
            ItemID.Sets.ItemsThatShouldNotBeInInventory[ItemID.SolarFlareAxe] = false;
            ItemID.Sets.ItemsThatShouldNotBeInInventory[ItemID.VortexChainsaw] = false;
            ItemID.Sets.ItemsThatShouldNotBeInInventory[ItemID.NebulaChainsaw] = false;
            ItemID.Sets.ItemsThatShouldNotBeInInventory[ItemID.SolarFlareChainsaw] = false;
            ItemID.Sets.ItemsThatShouldNotBeInInventory[ItemID.StardustChainsaw] = false;
            ItemID.Sets.Deprecated[ItemID.FirstFractal] = false;
            ItemID.Sets.Deprecated[ItemID.SkeletonBow] = false;
            ItemID.Sets.Deprecated[ItemID.OgreMask] = false;
            ItemID.Sets.Deprecated[ItemID.ApplePieSlice] = false;
            ItemID.Sets.Deprecated[ItemID.KoboldDynamiteBackpack] = false;
            ItemID.Sets.Deprecated[ItemID.EtherianJavelin] = false;
            ItemID.Sets.Deprecated[ItemID.GoblinBomberCap] = false;
            ItemID.Sets.Deprecated[ItemID.GoblinMask] = false;
            ItemID.Sets.Deprecated[ItemID.PhasicWarpEjector] = false;
            ItemID.Sets.Deprecated[ItemID.VortexAxe] = false;
            ItemID.Sets.Deprecated[ItemID.StardustAxe] = false;
            ItemID.Sets.Deprecated[ItemID.NebulaAxe] = false;
            ItemID.Sets.Deprecated[ItemID.SolarFlareAxe] = false;
            ItemID.Sets.Deprecated[ItemID.VortexChainsaw] = false;
            ItemID.Sets.Deprecated[ItemID.NebulaChainsaw] = false;
            ItemID.Sets.Deprecated[ItemID.SolarFlareChainsaw] = false;
            ItemID.Sets.Deprecated[ItemID.StardustChainsaw] = false;
        }
    }
}