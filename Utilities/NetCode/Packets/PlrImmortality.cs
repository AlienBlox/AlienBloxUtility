using AlienBloxUtility.Utilities.Abstracts;
using System.IO;
using Terraria;
using Terraria.ID;

namespace AlienBloxUtility.Utilities.NetCode.Packets
{
    public class PlrImmortality : AlienBloxPacket
    {
        public override void OnPacketHandled(BinaryReader reader)
        {
            int plr = reader.ReadInt32();
            bool immortal = reader.ReadBoolean();

            Main.player[plr].AlienBloxUtility().Immortal = immortal;

            if (Main.netMode == NetmodeID.Server)
            {
                var ms = new MemoryStream();
                var bw = new BinaryWriter(ms);

                bw.Write(plr);
                bw.Write(immortal);

                AlienBloxUtility.SendAlienBloxPacket("PlrImmortality", ms.ToArray());
            }
        }
    }

    public class PlrImmortalitySync : AlienBloxPacket
    {
        public override void OnPacketHandled(BinaryReader reader)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                var ms = new MemoryStream();
                var bw = new BinaryWriter(ms);

                for (int i = 0; i < Main.player.Length; i++)
                {
                    if (Main.player[i].active)
                    {
                        bw.Write(Main.player[i].whoAmI);
                        bw.Write(Main.player[i].AlienBloxUtility().Immortal);
                    }
                }

                AlienBloxUtility.SendAlienBloxPacket("PlrImmortality", ms.ToArray());
            }
            else
            {
                for (int i = 0; i < Main.player.Length; i++)
                {
                    if (Main.player[i].active)
                    {
                        int whoAmI = reader.ReadInt32();
                        bool immortal = reader.ReadBoolean();

                        Main.player[whoAmI].AlienBloxUtility().Immortal = immortal;
                    }
                }
            }
        }
    }
}