using AlienBloxUtility.Utilities.Helpers;
using System.IO;
using Terraria;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AlienBloxUtility
{
    public partial class AlienBloxUtility
    {
        public enum Messages : byte
        {
            SpawnNPC,
            ServerLua,
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            Messages Msg = (Messages)reader.ReadByte();
            Player PlrNet = Main.player[whoAmI];

            switch (Msg)
            {
                case Messages.SpawnNPC:
                    if (Main.netMode == NetmodeID.Server)
                    {
                        int type = reader.ReadInt32();
                        int X = reader.ReadInt32();
                        int Y = reader.ReadInt32();

                        NPC.NewNPC(PlrNet.GetSource_Misc("SpawnServer"), X, Y, type);
                        ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Mods.AlienBloxUtility.Messages.Server.SpawnNPC", PlrNet.name, type, PacketSpyUtility.UnixTime), Colors.CoinSilver);
                    }
                    break;
            }
        }

        public static void SpawnNPCClient(int type, int x, int y)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket Pkt = Instance.GetPacket();

                Pkt.Write((byte)Messages.SpawnNPC);
                Pkt.Write(type);
                Pkt.Write(x);
                Pkt.Write(y);
                Pkt.Send();
            }
            else
            {
                NPC.NewNPC(new EntitySource_Misc("SpawnServer"), x, y, type);
            }
        }
    }
}