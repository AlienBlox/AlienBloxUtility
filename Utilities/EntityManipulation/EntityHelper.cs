using AlienBloxUtility.Utilities.EntityManipulation.Freezes;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlienBloxUtility.Utilities.EntityManipulation
{
    public static class EntityHelper
    {
        public static void FreezeProjectiles(bool freeze)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                GlobalProjectileFreeze.GlobalFrozen = freeze;

                foreach (Projectile p in Main.ActiveProjectiles)
                {
                    p.GetGlobalProjectile<GlobalProjectileFreeze>().Frozen = freeze;
                }
            }
            else
            {
                ModPacket pkt = AlienBloxUtility.PacketRetrieve;

                pkt.Write((byte)AlienBloxUtility.Messages.TimeFreeze);
                pkt.Write((byte)1);
                pkt.Write(freeze);
                pkt.Send();
            }
        }

        public static void FreezeNPCs(bool freeze)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                GlobalNPCFreeze.GlobalFrozen = freeze;

                foreach (NPC n in Main.ActiveNPCs)
                {
                    n.GetGlobalNPC<GlobalNPCFreeze>().Frozen = freeze;
                }
            }
            else
            {
                ModPacket pkt = AlienBloxUtility.PacketRetrieve;

                pkt.Write((byte)AlienBloxUtility.Messages.TimeFreeze);
                pkt.Write((byte)0);
                pkt.Write(freeze);
                pkt.Send();
            }
        }
    }
}