using AlienBloxUtility.Utilities.Core;
using AlienBloxUtility.Utilities.Helpers;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AlienBloxUtility.Utilities.NetCode
{
    public class NetSystem : ModSystem
    {
        public static bool PacketEnabled = true;

        public override bool HijackGetData(ref byte messageType, ref BinaryReader reader, int playerNumber)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient && DebugUtilityList.PacketSpyEnabled)
            {
                PacketSpyUtility.RunPacketSpy(messageType, reader.BaseStream.Length, reader);
            }

            return !PacketEnabled;
        }

        public override bool HijackSendData(int whoAmI, int msgType, int remoteClient, int ignoreClient, NetworkText text, int number, float number2, float number3, float number4, int number5, int number6, int number7)
        {
            return !PacketEnabled;
        }
    }
}