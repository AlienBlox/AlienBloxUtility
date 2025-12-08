using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlienBloxUtility.Utilities.Core
{
    public class UtilityPlayer : ModPlayer
    {
        public bool ItemUsage = true;
        public bool CanReceivePacketSpyMessage = true;

        public int PacketTimer = 0;

        public override void ResetEffects()
        {
            //ItemUsage = true;
        }

        public override bool CanUseItem(Item item)
        {
            if (Main.netMode != NetmodeID.Server)
            {
                if (item.shoot != -1)
                {
                    Projectile P = new Projectile();

                    P.CloneDefaults(item.shoot);

                    if (P.aiStyle == ProjAIStyleID.Hook)
                    {
                        return true;
                    }
                }


                return ItemUsage;
            }
            
            return base.CanUseItem(item);
        }

        public override void PostUpdate()
        {
            if (!CanReceivePacketSpyMessage)
            {
                if (PacketTimer++ >= 60)
                {
                    CanReceivePacketSpyMessage = true;

                    PacketTimer = 0;
                }
            }
        }
    }
}