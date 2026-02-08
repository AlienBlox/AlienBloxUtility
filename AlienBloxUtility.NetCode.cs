using AlienBloxUtility.Utilities.Core;
using AlienBloxUtility.Utilities.EntityManipulation.Freezes;
using AlienBloxUtility.Utilities.Helpers;
using AlienBloxUtility.Utilities.NetCode.AlienBloxPacketSystem;
using AlienBloxUtility.Utilities.UIUtilities.UIRenderers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Terraria;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.GameContent.Tile_Entities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.ObjectData;

namespace AlienBloxUtility
{
    public partial class AlienBloxUtility : Mod
    {
        public enum Messages : byte
        {
            SpawnNPC,
            ServerLua,
            ServerJavaScript,
            ServerCPP,
            SendSteamID,
            RemoveSteamID,
            RetrieveSteamID,
            AlienBloxPacket,
            Wallhack,
            RetrieveWallhackData,
            SyncFlight,
            MsgTest,
            ButcherProjectile,
            ReqServerProj,
            TEDestruction,
            TimeFreeze,
            ReceiveProjectileFreeze,
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
                            int npctype = reader.ReadInt32();
                            int X = reader.ReadInt32();
                            int Y = reader.ReadInt32();

                            NPC.NewNPC(PlrNet.GetSource_Misc("SpawnServer"), X, Y, npctype);
                            ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Mods.AlienBloxUtility.Messages.Server.SpawnNPC", PlrNet.name, npctype, PacketSpyUtility.UnixTime), Colors.CoinSilver);
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
                    case Messages.ServerCPP:
                        if (Main.netMode == NetmodeID.Server)
                        {
                            string code = reader.ReadString();
                            bool file = reader.ReadBoolean();

                            ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Mods.AlienBloxUtility.Messages.Server.DoCPP", PlrNet.name, PacketSpyUtility.UnixTime), Colors.CoinSilver);

                            if (!file)
                            {
                                Task.Run(() => RunJavaScript(code));
                            }
                            else
                            {
                                try
                                {
                                    Task.Run(() => SharedCPP.Run(File.ReadAllText(JSStorageLocation + $"\\{code}")));
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
                    case Messages.Wallhack:
                        if (Main.netMode == NetmodeID.Server)
                        {
                            bool Wallhack = reader.ReadBoolean();
                            float X = reader.ReadSingle();
                            float Y = reader.ReadSingle();

                            PlrNet.position = new(X, Y);
                            PlrNet.AlienBloxUtility().noClipHackPos = PlrNet.position = new(X, Y);
                            PlrNet.AlienBloxUtility().noClipHack = Wallhack;

                            ModPacket pkt = GetPacket();

                            pkt.Write((byte)Messages.Wallhack);
                            pkt.Write(whoAmI);
                            pkt.Write(Wallhack);
                            pkt.Write(X);
                            pkt.Write(Y);
                            pkt.Send(-1, whoAmI);
                        }
                        else
                        {
                            int plrToGet = reader.ReadInt32();
                            bool Wallhack = reader.ReadBoolean();
                            float X = reader.ReadSingle();
                            float Y = reader.ReadSingle();

                            Main.player[plrToGet].position = new(X, Y);
                            Main.player[plrToGet].AlienBloxUtility().noClipHackPos = new(X, Y);
                            Main.player[plrToGet].AlienBloxUtility().noClipHack = Wallhack;
                        }
                        break;
                    case Messages.RetrieveWallhackData:
                        if (Main.netMode == NetmodeID.Server)
                        {
                            ModPacket pkt = GetPacket();

                            pkt.Write((byte)Messages.RetrieveWallhackData);

                            foreach (Player plr in Main.ActivePlayers)
                            {
                                pkt.Write(plr.position.X);
                                pkt.Write(plr.position.Y);
                                pkt.Write(plr.AlienBloxUtility().noClipHack);
                            }

                            pkt.Send(whoAmI);
                        }
                        else
                        {
                            foreach (Player p in Main.ActivePlayers)
                            {
                                float X = reader.ReadSingle();
                                float Y = reader.ReadSingle();
                                bool Wallhack = reader.ReadBoolean();

                                p.position = p.AlienBloxUtility().noClipHackPos = new(X, Y);
                                p.AlienBloxUtility().noClipHack = true;

                                Logger.Warn("Received Sync! Position is: " + p.position);
                                Logger.Warn("Received Sync! Noclip hack status is: " + p.AlienBloxUtility().noClipHack);
                            }
                        }
                        break;
                    case Messages.SyncFlight:
                        if (Main.netMode == NetmodeID.MultiplayerClient)
                        {
                            int Plr = reader.ReadInt32();
                            float X = reader.ReadSingle();
                            float Y = reader.ReadSingle();
                            bool Wallhack = reader.ReadBoolean();

                            Main.player[Plr].position = new Vector2(X, Y);
                            Main.player[Plr].AlienBloxUtility().noClipHackPos = new Vector2(X, Y);
                            Main.player[Plr].AlienBloxUtility().noClipHack = Wallhack;
                        }
                        break;
                    case Messages.ButcherProjectile:
                        int type = reader.ReadInt32();

                        foreach (Projectile p in Main.ActiveProjectiles)
                        {
                            if (p.owner == Main.myPlayer)
                            {
                                if ((type != -1 && p.type == type) || type == -1)
                                    p.Kill();

                                NetMessage.SendData(MessageID.SyncItem, -1, -1, null, p.whoAmI, 0f, 0f, 0f, 0);
                            }
                        }

                        if (Main.netMode == NetmodeID.Server)
                        {
                            ModPacket pkt = GetPacket();

                            pkt.Write((byte)Messages.ButcherProjectile);
                            pkt.Write(type);
                            pkt.Send();
                        }
                        
                        break;
                    case Messages.ReqServerProj:
                        if (Main.netMode == NetmodeID.Server)
                        {
                            int projtype = reader.ReadInt32();
                            Vector2 pos = reader.ReadVector2();

                            Projectile.NewProjectile(new EntitySource_Misc("Hackies"), pos, Vector2.Zero, projtype, 0, 0, Main.myPlayer);
                        }
                        break;
                    case Messages.TEDestruction:
                        if (Main.netMode == NetmodeID.Server)
                        {
                            int x = reader.ReadInt32();
                            int y = reader.ReadInt32();

                            if (TileEntity.ByPosition.TryGetValue(new(x, y), out TileEntity TE))
                            {
                                if (TE.ID != -1)
                                {
                                    TileEntity.ByID.Remove(TE.ID);
                                    TileEntity.ByPosition.Remove(new Point16(x, y));

                                    // 2. Broadcast the removal to all clients (including the sender)
                                    // Sending -1 as the entityID tells clients to "Clear at this position"
                                    NetMessage.SendData(MessageID.TileEntitySharing, -1, -1, null, -1, x, y);
                                }
                            }
                        }
                        break;
                    case Messages.TimeFreeze:
                        byte FreezeType = reader.ReadByte();
                        bool Freeze = reader.ReadBoolean();

                        switch (FreezeType)
                        {
                            case 0:
                                if (Main.netMode == NetmodeID.Server)
                                {
                                    GlobalNPCFreeze.GlobalFrozen = Freeze;

                                    foreach (NPC npc in Main.ActiveNPCs)
                                    {
                                        npc.GetGlobalNPC<GlobalNPCFreeze>().Frozen = Freeze;
                                        npc.netUpdate = true;
                                    }
                                }
                                break;
                            case 1:
                                GlobalProjectileFreeze.GlobalFrozen = Freeze;

                                foreach (Projectile p in Main.ActiveProjectiles)
                                {
                                    if (p.owner == Main.myPlayer)
                                    {
                                        p.GetGlobalProjectile<GlobalProjectileFreeze>().Frozen = Freeze;
                                        p.netUpdate = true;
                                    }
                                }

                                if (Main.netMode == NetmodeID.Server)
                                {
                                    ModPacket pkt = GetPacket();

                                    pkt.Write((byte)Messages.TimeFreeze);
                                    pkt.Write((byte)1);
                                    pkt.Write(Freeze);
                                    pkt.Send();
                                }
                                break;
                        }
                        break;
                    case Messages.ReceiveProjectileFreeze:
                        if (Main.netMode == NetmodeID.Server)
                        {
                            ModPacket pkt = GetPacket();

                            pkt.Write((byte)Messages.ReceiveProjectileFreeze);
                            pkt.Write(GlobalProjectileFreeze.GlobalFrozen);
                            pkt.Send(whoAmI);
                        }
                        else
                        {
                            GlobalProjectileFreeze.GlobalFrozen = reader.ReadBoolean();
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

        public static void RetrieveWallhackData()
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket pkt = Instance.GetPacket();

                pkt.Write((byte)Messages.RetrieveWallhackData);
                pkt.Send();
            }
        }

        public static void RetrieveProjectileFreeze()
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket pkt = Instance.GetPacket();

                pkt.Write((byte)Messages.ReceiveProjectileFreeze);
                pkt.Send();
            }
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

        public static void NativeServer(string code, bool file = false)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket Pkt = Instance.GetPacket();

                Pkt.Write((byte)Messages.ServerCPP);
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

        public static void ButcherNPCType(int type)
        {
            foreach (NPC n in Main.ActiveNPCs)
            {
                if (n.type == type)
                {
                    n.RequestKill();
                }
            }
        }

        public static void ButcherProjType(int type)
        {   
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                foreach (Projectile p in Main.ActiveProjectiles)
                {
                    if (p.owner == Main.myPlayer)
                    {
                        if ((type != -1 && p.type == type) || type == -1)
                            p.Kill();
                    }
                }
            }
            else
            {
                ModPacket pkt = Instance.GetPacket();

                pkt.Write((byte)Messages.ButcherProjectile);
                pkt.Write(type);
                pkt.Send();
            }
        }

        public static void ButcherProjType()
        {
            ButcherProjType(-1);
        }

        public static void RequestServerProjectile(int type, Vector2 position)
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
                return;

            ModPacket pkt = Instance.GetPacket();

            pkt.Write((byte)Messages.ReqServerProj);
            pkt.Write(type);
            pkt.WriteVector2(position);
            pkt.Send();
        }

        public static void SendNoclipHack(Vector2 pos, bool Wallhack)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket pkt = Instance.GetPacket();

                pkt.Write((byte)Messages.Wallhack);
                pkt.Write(Wallhack);
                pkt.Write(pos.X);
                pkt.Write(pos.Y);
                pkt.Send();
            }
        }

        public static void SmartDestroyTE(Vector2 pos)
        {
            Point16 Refine = pos.ToTileCoordinates16();

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (TileEntity.ByPosition.TryGetValue(Refine, out TileEntity TE))
                {
                    TileEntity.ByPosition.Remove(Refine);
                    TileEntity.ByID.Remove(TE.ID);
                }
            }
            else
            {
                ModPacket pkt = Instance.GetPacket();

                pkt.Write((byte)Messages.TEDestruction);
                pkt.Write((int)Refine.X);
                pkt.Write((int)Refine.Y);
                pkt.Send();
            }
        }

        public static void SmartEditTE(Vector2 pos, int type)
        {
            Point16 Refine = pos.ToTileCoordinates16();

            if (type == -1)
            {
                SmartDestroyTE(pos);

                return;
            }

            TileEntity TE = TEUtilities.GetTEObjects()[type];//TEUtilities.FromID(type);

            if (TE is ModTileEntity MTE)
            {
                //MTE.Hook_AfterPlacement(Refine.X, Refine.Y, 0, 0, 0, 0);
                MTE.Place(Refine.X, Refine.Y);
            }
            else
            {
                switch (type)
                {
                    case 0:
                        TETrainingDummy.Place(Refine.X, Refine.Y);
                        break;
                    case 1:
                        TEItemFrame.Place(Refine.X, Refine.Y);
                        break;
                    case 2:
                        TELogicSensor.Place(Refine.X, Refine.Y);
                        break;
                    case 3:
                        TEDisplayDoll.Place(Refine.X, Refine.Y);
                        break;
                    case 4:
                        TEWeaponsRack.Place(Refine.X, Refine.Y);
                        break;
                    case 5:
                        TEHatRack.Place(Refine.X, Refine.Y);
                        break;
                    case 6:
                        TETeleportationPylon.Place(Refine.X, Refine.Y);
                        break;
                }
                //MethodInfo TEPlace = TE.GetType().GetMethod("Place", BindingFlags.Static, [typeof(int), typeof(int)]);
                //TEPlace?.Invoke(null, [(int)Refine.X, (int)Refine.Y]);
            }

            if (type != -1 && Main.netMode == NetmodeID.MultiplayerClient)
            {
                NetMessage.SendData(MessageID.TileEntityPlacement, -1, -1, null, Refine.X, Refine.Y, type);
            }
        }

        public static void SmartWallModify(Vector2 pos, int type)
        {
            Point posRefined = new((int)pos.X / 16, (int)pos.Y / 16);

            Main.tile[posRefined].WallType = (ushort)type;

            if (Main.netMode == NetmodeID.MultiplayerClient)
                NetMessage.SendTileSquare(-1, posRefined.X, posRefined.Y);
        }

        public static void SmartTileModify(Vector2 pos, int type)
        {
            Point posRefined = new((int)pos.X / 16, (int)pos.Y / 16);

            Main.tile[posRefined].TileType = (ushort)type;
            WorldGen.PlaceTile(posRefined.X, posRefined.Y, type);

            if (Main.netMode == NetmodeID.MultiplayerClient)
                NetMessage.SendTileSquare(-1, posRefined.X, posRefined.Y);
        }

        public static bool SmartTilePlace(Vector2 pos, int tileToPlace)
        {
            bool result = false;
            Point posRefined = new((int)pos.X / 16, (int)pos.Y / 16);
            TileObjectData dat = TileObjectData.GetTileData(tileToPlace, 0);

            if (Main.tileSolid[tileToPlace])
            {
                SmartTileModify(pos, tileToPlace);
            }

            if (WorldGen.InWorld(posRefined.X, posRefined.Y))
            {
                result = WorldGen.PlaceObject(posRefined.X, posRefined.Y, tileToPlace);

                if (Main.netMode == NetmodeID.MultiplayerClient)
                    NetMessage.SendTileSquare(-1, posRefined.X, posRefined.Y, dat.Width, dat.Height);
            }

            return result;
        }

        public static Tile ParanoidTileRetrieval(int x, int y)
        {
            if (!WorldGen.InWorld(x, y))
                return new Tile();

            return Main.tile[x, y];
        }

        /// <summary>
        /// Sends a packet to AlienBlox's Packet Handler
        /// </summary>
        /// <param name="packetName">The name of the packet.</param>
        /// <param name="data">The data to send.</param>
        /// <param name="toPlayer"></param> 
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