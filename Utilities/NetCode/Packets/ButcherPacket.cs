using AlienBloxUtility.Utilities.Abstracts;
using System.IO;
using Terraria;
using Terraria.ID;

namespace AlienBloxUtility.Utilities.NetCode.Packets
{
    public class ButcherPacket : AlienBloxPacket
    {
        /// <summary>
        /// Butchers something like items
        /// </summary>
        /// <param name="type">The butcher type: <br/>0 for item <br/> 1 for enemies NPC <br/> 2 for friendly NPC <br/> 3 for projectiles</param>
        public static void Butcher(byte type)
        {
            AlienBloxUtility.SendAlienBloxPacket(typeof(ButcherPacket).Name, [type]);
        }

        public override void OnPacketHandled(BinaryReader reader)
        {
            byte butcherType = reader.ReadByte();

            switch (butcherType)
            {
                case 0:
                    foreach (Item I in Main.ActiveItems)
                    {
                        I.SetDefaults(ItemID.None);
                        NetMessage.SendData(MessageID.SyncItem, -1, -1, null, I.whoAmI, 0f, 0f, 0f, 0);
                    }
                    break;
                case 1:
                    foreach (var npc in Main.npc)
                    {
                        if (npc.active && !npc.friendly && !npc.isLikeATownNPC)
                        {
                            npc.StrikeInstantKill();
                        }
                    }
                    break;
                case 2:
                    foreach (var npc in Main.npc)
                    {
                        if (npc.active && (npc.friendly || npc.isLikeATownNPC))
                        {
                            npc.StrikeInstantKill();
                        }
                    }
                    break;
                case 3:
                    foreach (var I in Main.ActiveProjectiles)
                    {
                        I.Kill();
                        NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, I.whoAmI, 0f, 0f, 0f, 0);
                    }  
                break;
            }
        }
    }
}