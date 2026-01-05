using AlienBloxUtility.Utilities.Abstracts;
using AlienBloxUtility.Utilities.Helpers;
using System.IO;
using Terraria;
using Terraria.ID;

namespace AlienBloxUtility.Utilities.NetCode.Packets
{
    public class TownieImmortality : AlienBloxPacket
    {
        public override void OnPacketHandled(BinaryReader reader)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                NPCImmortality.GlobalImmortality = reader.ReadBoolean();
            }
        }
    }
}