using AlienBloxUtility.Utilities.Helpers;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlienBloxUtility.Utilities.NetCode
{
    public class NetSystem : ModSystem
    {
        public override bool HijackGetData(ref byte messageType, ref BinaryReader reader, int playerNumber)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient && playerNumber == Main.myPlayer)
            {
                PacketSpyUtility.RunPacketSpy(messageType, reader.BaseStream.Length, reader);
            }

            return base.HijackGetData(ref messageType, ref reader, playerNumber);
        }
    }
}