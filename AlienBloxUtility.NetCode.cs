using AlienBloxUtility.Utilities.Helpers;
using AlienBloxUtility.Utilities.NetCode.AlienBloxPacketSystem;
using AlienBloxUtility.Utilities.UIUtilities.UIRenderers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Terraria;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace AlienBloxUtility
{
    public partial class AlienBloxUtility : Mod
    {
        public enum Messages : byte
        {
            SpawnNPC,
            ServerLua,
            ServerJavaScript,
            SendSteamID,
            RemoveSteamID,
            RetrieveSteamID,
            AlienBloxPacket,
            MsgTest,
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            try
            {
                //Console.WriteLine("len: " + reader.BaseStream.Length);
                //Console.WriteLine("pos: " + reader.BaseStream.Position);
                //Logger.Info("len: " + reader.BaseStream.Length);
                //Logger.Info("pos: " + reader.BaseStream.Position);

                Messages Msg = (Messages)reader.ReadByte();

                //Logger.Info("wired " + Msg);

                Player PlrNet = null;// = Main.player[whoAmI];

                if (Main.netMode == NetmodeID.Server)
                {
                    PlrNet = Main.player[whoAmI];
                }

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
                    case Messages.ServerLua:
                        if (Main.netMode == NetmodeID.Server)
                        {
                            string code = reader.ReadString();
                            bool file = reader.ReadBoolean();

                            ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Mods.AlienBloxUtility.Messages.Server.DoLua", PlrNet.name, PacketSpyUtility.UnixTime), Colors.CoinSilver);

                            if (!file)
                            {
                                Task.Run(() => RunLuaAsync(code, GetToken()));
                            }
                            else
                            {
                                try
                                {
                                    Task.Run(() => RunLuaAsync(File.ReadAllText(LuaStorageLocation + $"\\{code}"), GetToken()));
                                }
                                catch (Exception e)
                                {
                                    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral($"{e.GetType().Name}: {e.Message}"), Colors.CoinSilver);
                                }

                            }
                        }
                        break;
                    case Messages.ServerJavaScript:
                        if (Main.netMode == NetmodeID.Server)
                        {
                            string code = reader.ReadString();
                            bool file = reader.ReadBoolean();

                            ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Mods.AlienBloxUtility.Messages.Server.DoJS", PlrNet.name, PacketSpyUtility.UnixTime), Colors.CoinSilver);

                            if (!file)
                            {
                                Task.Run(() => RunJavaScript(code));
                            }
                            else
                            {
                                try
                                {
                                    Task.Run(() => RunJavaScript(File.ReadAllText(JSStorageLocation + $"\\{code}")));
                                }
                                catch (Exception e)
                                {
                                    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral($"{e.GetType().Name}: {e.Message}"), Colors.CoinSilver);
                                }
                            }
                        }
                        break;
                    case Messages.SendSteamID:
                        if (Main.netMode == NetmodeID.Server)
                        {
                            ulong steamID = reader.ReadUInt64();
                            string Persona = reader.ReadString();

                            if (SteamIDs.TryAdd(whoAmI, (steamID, Persona)))
                            {
                                Console.WriteLine($"Added user {Persona}");
                            }

                            Logger.Info($"Steam ID received! ({steamID}, {Persona})");
                            Console.WriteLine($"Steam ID received! ({steamID}, {Persona})");
                        }
                        break;
                    case Messages.RemoveSteamID:
                        if (Main.netMode == NetmodeID.Server)
                        {
                            SteamIDs.Remove(whoAmI);
                        }
                        break;
                    case Messages.RetrieveSteamID:
                        if (Main.netMode == NetmodeID.Server)
                        {
                            ModPacket pkt = GetPacket();

                            pkt.Write((byte)Messages.RetrieveSteamID);
                            pkt.Write(SteamIDs.Count);

                            foreach (var item in SteamIDs)
                            {
                                pkt.Write(item.Key);
                                pkt.Write(item.Value.Item1);
                                pkt.Write(item.Value.Item2);
                            }

                            pkt.Send(whoAmI);
                        }
                        else
                        {
                            int Count = reader.ReadInt32();

                            Dictionary<int, (ulong, string)> Dict = [];

                            for (int i = 0; i < Count; i++)
                            {
                                KeyValuePair<int, (ulong, string)> kvp = new(reader.ReadInt32(), (reader.ReadUInt64(), reader.ReadString()));

                                Dict.Add(kvp.Key, kvp.Value);
                            }
                        }
                        break;
                    case Messages.AlienBloxPacket:
                        string packetName = reader.ReadString();
                        int packetSize = reader.ReadInt32();
                        byte[] data = new byte[packetSize];

                        for (int i = 0; i < packetSize; i++)
                        {
                            data[i] = reader.ReadByte();
                        }

                        BinaryReader HandledReader = new(new MemoryStream(data));

                        AlienBloxPacketHandler.HandlePacket(HandledReader, packetName);

                        break;
                    case Messages.MsgTest:
                        if (Main.netMode == NetmodeID.Server)
                        {
                            Console.WriteLine("Network test began...");

                            ModPacket pkt = GetPacket();

                            pkt.Write((byte)Messages.MsgTest);
                            pkt.Send(whoAmI);

                            Console.WriteLine("Nettest end...");
                        }
                        else
                        {
                            ConHostRender.Write("Net test done!");
                        }
                        break;
                }
            }
            catch (Exception e)
            {
                if (!AlienBloxUtilityConfig.Instance.DumpErrorLogs)
                {
                    Logger.Warn(e.Message, e);

                    return;
                }

                reader.BaseStream.Position = 0;

                byte[] entirePacket = reader.BaseStream.ReadBytes(reader.BaseStream.Length);

                //using var stream = File.Create(LogLocation + $"\\PacketError-{new Guid()}.txt");
                //stream.Write(entirePacket);

                Logger.Info("Packet dump.");
                Logger.Info(ByteArrayToHex(entirePacket));
                Logger.Info("Packet dump end.");

                throw new();
            }
        }

        public static string ByteArrayToHex(byte[] byteArray)
        {
            // This will convert the byte array to a string of hex values (without spaces or dashes)
            return BitConverter.ToString(byteArray).Replace("-", "");
        }

        public static void JSServer(string code, bool file = false)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket Pkt = Instance.GetPacket();

                Pkt.Write((byte)Messages.ServerJavaScript);
                Pkt.Write(code);
                Pkt.Write(file);
                Pkt.Send();
            }
        }

        public static void LuaServer(string code, bool file = false)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket Pkt = Instance.GetPacket();

                Pkt.Write((byte)Messages.ServerLua);
                Pkt.Write(code);
                Pkt.Write(file);
                Pkt.Send();
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

        /// <summary>
        /// Sends a packet to AlienBlox's Packet Handler
        /// </summary>
        /// <param name="packetName">The name of the packet.</param>
        /// <param name="data">The data to send.</param>
        public static void SendAlienBloxPacket(string packetName, byte[] data, int toPlayer = -1)
        {
            if (Main.netMode != NetmodeID.SinglePlayer)
            {
                ModPacket pkt = Instance.GetPacket();

                //Console.WriteLine(data.Length);
                //Instance.Logger.Info(data.Length + $" is packet size");

                pkt.Write((byte)Messages.AlienBloxPacket);
                pkt.Write(packetName);
                pkt.Write(data.Length);
                
                for (int i = 0; i < data.Length; i++)
                {
                    pkt.Write(data[i]);
                }

                pkt.Send(toPlayer);
            }
        }
    }
}