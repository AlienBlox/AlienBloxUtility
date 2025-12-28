using AlienBloxUtility.Utilities.Helpers;
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
    public partial class AlienBloxUtility
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
            }
        }

        public static void OutputTo(string output)
        {
            /*
            if (Main.netMode == NetmodeID.Server)
            {
                ModPacket Pkt = Instance.GetPacket();

                Pkt.Write((byte)Messages.OutputTo);
                Pkt.Write(output);
                Pkt.Send();
            }
            */
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
    }
}