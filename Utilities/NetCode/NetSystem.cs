using AlienBloxUtility.Utilities.Helpers;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlienBloxUtility.Utilities.NetCode
{
    public class NetSystem : ModSystem
    {
        private static Dictionary<byte, uint> lastPacketMsgTick = new();

        public override bool HijackGetData(ref byte messageType, ref BinaryReader reader, int playerNumber)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                if (!lastPacketMsgTick.TryGetValue(messageType, out uint lastTick))
                    lastTick = 0;

                uint now = Main.GameUpdateCount;

                if (now - lastTick >= 60)
                {
                    PacketSpyUtility.RunPacketSpy(messageType, reader.BaseStream.Length, reader);
                    lastPacketMsgTick[messageType] = now;
                }
            }

            return false;
        }
    }
}