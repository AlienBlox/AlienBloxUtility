using AlienBloxUtility.Utilities.Abstracts;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.ID;

namespace AlienBloxUtility.Utilities.NetCode.Packets
{
    public class BlackHolePacket : AlienBloxPacket
    {
        public override void OnPacketHandled(BinaryReader reader)
        {
            byte blackHoleType = reader.ReadByte();
            int PosX = reader.ReadInt32();
            int PosY = reader.ReadInt32();

            if (Main.netMode == NetmodeID.Server)
            {
                switch (blackHoleType)
                {
                    case 0:
                        foreach (var item in Main.ActiveItems)
                        {
                            item.position = new(PosX, PosY);

                            using MemoryStream ms = new();
                            using BinaryWriter br = new(ms);

                            br.Write((byte)0);
                            br.Write(PosX);
                            br.Write(PosY);

                            AlienBloxUtility.SendAlienBloxPacket("BlackHolePacket", ms.ToArray());
                        }
                        break;
                    case 1:
                        foreach (var proj in Main.ActiveProjectiles)
                        {
                            if (Main.myPlayer == proj.owner)
                            {
                                proj.position = new(PosX, PosY);
                                proj.netUpdate = true;
                            }
                            else
                            {
                                using MemoryStream ms = new();
                                using BinaryWriter br = new(ms);

                                br.Write((byte)1);
                                br.Write(PosX);
                                br.Write(PosY);

                                AlienBloxUtility.SendAlienBloxPacket("BlackHolePacket", ms.ToArray(), proj.owner);
                            }
                        }
                        break;
                    case 2:
                        foreach (var npc in Main.ActiveNPCs)
                        {
                            npc.position = new(PosX, PosY);
                            npc.netUpdate = true;
                        }
                        break;
                }
            }
            else
            {
                switch (blackHoleType)
                {
                    case 0:
                        foreach (var item in Main.ActiveItems)
                        {
                            item.position = new(PosX, PosY);
                        }
                        break;
                    case 1:
                        foreach (var proj in Main.ActiveProjectiles)
                        {
                            proj.position = new(PosX, PosY);
                            proj.netUpdate = true;
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Sends black hole data from client to server
        /// </summary>
        /// <param name="blackHoleType">The type of black hole to send:<br/> 0 for items,<br/> 1 for projectiles,<br/> 2 for NPCs</param>
        /// <param name="Pos">The position of the player's black hole</param>
        public static void SendBlackHole(byte blackHoleType, Vector2 Pos)
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
                return;

            using MemoryStream ms = new();
            using BinaryWriter br = new(ms);

            br.Write(blackHoleType);
            br.Write(Pos.X);
            br.Write(Pos.Y);

            AlienBloxUtility.SendAlienBloxPacket("BlackHolePacket", ms.ToArray());
        }
    }
}