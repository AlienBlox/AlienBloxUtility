using AlienBloxUtility.Utilities.Abstracts;
using AlienBloxUtility.Utilities.UIUtilities.UIRenderers;
using System;
using System.IO;
using Terraria;
using Terraria.ID;

namespace AlienBloxUtility.Utilities.NetCode.Packets
{
    public class DirectMessagePacket : AlienBloxPacket
    {
        public override void OnPacketHandled(BinaryReader reader)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                MemoryStream memoryStream = new MemoryStream();
                StreamWriter writer = new StreamWriter(memoryStream);

                AlienBloxUtility.Instance.Logger.Info("Msg received!");

                string sendTo = reader.ReadString();
                string msg = reader.ReadString();

                writer.Write(msg);
                writer.Dispose();

                foreach (Player p in Main.ActivePlayers)
                {
                    if (p.name == sendTo)
                    {
                        AlienBloxUtility.SendAlienBloxPacket("DirectMessagePacket", memoryStream.ToArray(), p.whoAmI);
                    }
                }

                memoryStream.Dispose();
            }
            else
            {
                string message = reader.ReadString();

                ConHostRender.Write(message);
            }
        }
    }
}