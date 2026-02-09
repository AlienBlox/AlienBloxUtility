using AlienBloxUtility.Utilities.EntityManipulation.Freezes;
using Microsoft.Xna.Framework;
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

        public static void GrabNPC(int WhoAmI, bool Grab, Vector2 GrabPos)
        {
            if (WhoAmI == -1 || WhoAmI > Main.maxNPCs)
                return;

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (Main.npc[WhoAmI].active)
                {
                    GlobalNPCFreeze freezer = Main.npc[WhoAmI].GetGlobalNPC<GlobalNPCFreeze>();

                    freezer.Grabbed = Grab;
                    freezer.GrabPosition = GrabPos;
                }
            }
            else
            {
                ModPacket pkt = AlienBloxUtility.PacketRetrieve;

                pkt.Write((byte)AlienBloxUtility.Messages.GrabNPC);
                pkt.Write(WhoAmI);
                pkt.Write(Grab);
                pkt.WriteVector2(GrabPos);
                pkt.Send();
            }
        }
    }
}