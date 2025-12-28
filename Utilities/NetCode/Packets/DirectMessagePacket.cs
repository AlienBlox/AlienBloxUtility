using AlienBloxUtility.Utilities.Abstracts;
using AlienBloxUtility.Utilities.UIUtilities.UIRenderers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
                MemoryStream memoryStream = new();
                StreamWriter writer = new(memoryStream);

                AlienBloxUtility.Instance.Logger.Info("Msg received!");

                string sendTo = reader.ReadString();
                string msg = reader.ReadString();

                writer.Write(msg);
                writer.Dispose();

                foreach (Player p in Main.ActivePlayers)
                {
                    if (p.name == sendTo || p.name == sendTo.Replace('_', ' '))
                    {
                        AlienBloxUtility.SendAlienBloxPacket("DirectMessagePacket", memoryStream.ToArray(), p.whoAmI);
                    }
                }

                memoryStream.Dispose();
            }
            else
            {
                try
                {
                    string message = "";

                    var bytes = reader.ReadBytes((int)reader.BaseStream.Length);
                    List<char> Chars = [];

                    foreach (byte b in bytes)
                    {
                        Chars.Add((char)b);
                    }

                    message = new([.. Chars]);
                    
                    ConHostRender.Write(message);
                }
                catch
                {
                    ConHostRender.Write("Error receiving DM...");
                }
            }
        }
    }
}