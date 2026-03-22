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

        public override void Unload()
        {
            ItemID.Sets.ItemsThatShouldNotBeInInventory[ItemID.FirstFractal] = true;
            ItemID.Sets.ItemsThatShouldNotBeInInventory[ItemID.SkeletonBow] = true;
            ItemID.Sets.ItemsThatShouldNotBeInInventory[ItemID.OgreMask] = true;
            ItemID.Sets.ItemsThatShouldNotBeInInventory[ItemID.ApplePieSlice] = true;
            ItemID.Sets.ItemsThatShouldNotBeInInventory[ItemID.KoboldDynamiteBackpack] = true;
            ItemID.Sets.ItemsThatShouldNotBeInInventory[ItemID.EtherianJavelin] = true;
            ItemID.Sets.ItemsThatShouldNotBeInInventory[ItemID.GoblinBomberCap] = true;
            ItemID.Sets.ItemsThatShouldNotBeInInventory[ItemID.GoblinMask] = true;
            ItemID.Sets.ItemsThatShouldNotBeInInventory[ItemID.PhasicWarpEjector] = true;
            ItemID.Sets.ItemsThatShouldNotBeInInventory[ItemID.VortexAxe] = true;
            ItemID.Sets.ItemsThatShouldNotBeInInventory[ItemID.StardustAxe] = true;
            ItemID.Sets.ItemsThatShouldNotBeInInventory[ItemID.NebulaAxe] = true;
            ItemID.Sets.ItemsThatShouldNotBeInInventory[ItemID.SolarFlareAxe] = true;
            ItemID.Sets.ItemsThatShouldNotBeInInventory[ItemID.VortexChainsaw] = true;
            ItemID.Sets.ItemsThatShouldNotBeInInventory[ItemID.NebulaChainsaw] = true;
            ItemID.Sets.ItemsThatShouldNotBeInInventory[ItemID.SolarFlareChainsaw] = true;
            ItemID.Sets.ItemsThatShouldNotBeInInventory[ItemID.StardustChainsaw] = true;
            ItemID.Sets.Deprecated[ItemID.FirstFractal] = true;
            ItemID.Sets.Deprecated[ItemID.SkeletonBow] = true;
            ItemID.Sets.Deprecated[ItemID.OgreMask] = true;
            ItemID.Sets.Deprecated[ItemID.ApplePieSlice] = true;
            ItemID.Sets.Deprecated[ItemID.KoboldDynamiteBackpack] = true;
            ItemID.Sets.Deprecated[ItemID.EtherianJavelin] = true;
            ItemID.Sets.Deprecated[ItemID.GoblinBomberCap] = true;
            ItemID.Sets.Deprecated[ItemID.GoblinMask] = true;
            ItemID.Sets.Deprecated[ItemID.PhasicWarpEjector] = true;
            ItemID.Sets.Deprecated[ItemID.VortexAxe] = true;
            ItemID.Sets.Deprecated[ItemID.StardustAxe] = true;
            ItemID.Sets.Deprecated[ItemID.NebulaAxe] = true;
            ItemID.Sets.Deprecated[ItemID.SolarFlareAxe] = true;
            ItemID.Sets.Deprecated[ItemID.VortexChainsaw] = true;
            ItemID.Sets.Deprecated[ItemID.NebulaChainsaw] = true;
            ItemID.Sets.Deprecated[ItemID.SolarFlareChainsaw] = true;
            ItemID.Sets.Deprecated[ItemID.StardustChainsaw] = true;
        }
    }
}