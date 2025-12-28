using AlienBloxUtility.Utilities.Abstracts;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlienBloxUtility.Utilities.NetCode.AlienBloxPacketSystem
{
    public class AlienBloxPacketHandler : ModSystem
    {
        public static List<AlienBloxPacket> PacketHandlers;

        public override void Load()
        {
            PacketHandlers = [];
        }

        public override void Unload()
        {
            PacketHandlers?.Clear();
            PacketHandlers = null;
        }

        /// <summary>
        /// Sends a packet.
        /// </summary>
        /// <param name="packetName">The name of the packet to send</param>
        /// <param name="dataToWrite">The data to write (as a byte aray)</param>
        /// <param name="packet">The packet output</param>
        /// <param name="sendPacket">Should the packet be sent</param>
        public static void SendPacket(string packetName, byte[] dataToWrite, out ModPacket packet, bool sendPacket = true)
        {
            packet = null;

            if (Main.netMode != NetmodeID.SinglePlayer)
            {
                ModPacket pkt = AlienBloxUtility.Instance.GetPacket();

                pkt.Write((byte)AlienBloxUtility.Messages.AlienBloxPacket);
                pkt.Write(packetName);
                pkt.Write(dataToWrite.Length);

                for (int i = 0; i < dataToWrite.Length; i++)
                {
                    pkt.Write(dataToWrite[i]);
                }

                if (sendPacket)
                {
                    pkt.Send();
                }
                else
                {
                    packet = pkt;
                }
            }
        }

        public static void HandlePacket(BinaryReader reader, string packetName)
        {
            foreach (var handler in PacketHandlers)
            {
                if (handler.Name == packetName)
                {
                    handler.SafePacketHandle(reader);
                }
            }
        }
    }
}