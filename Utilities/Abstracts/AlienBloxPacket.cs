using AlienBloxUtility.Common.Exceptions;
using AlienBloxUtility.Utilities.NetCode.AlienBloxPacketSystem;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlienBloxUtility.Utilities.Abstracts
{
    public abstract class AlienBloxPacket : ILoadable
    {
        public string Name => GetType().Name;

        public void Load(Mod mod)
        {
            foreach (var pkt in AlienBloxPacketHandler.PacketHandlers)
            {
                if (pkt.Name == Name)
                {
                    throw new ConflictException();
                }
            }

            AlienBloxPacketHandler.PacketHandlers.Add(this);
        }

        public void Unload()
        {
            AlienBloxPacketHandler.PacketHandlers.Remove(this);
        }

        /// <summary>
        /// Safely handles an AlienBloxPacket instance.
        /// </summary>
        /// <param name="reader">The binary reader to give</param>
        public void SafePacketHandle(BinaryReader reader)
        {
            //Console.WriteLine("len: " + reader.BaseStream.Length + $"; pos: {reader.BaseStream.Position}");

            reader.BaseStream.Position = 0;

            try
            {
                OnPacketHandled(reader);
            }
            catch (Exception ex)
            {
                if (Main.netMode == NetmodeID.Server)
                {
                    Console.WriteLine($"Exception on packet handle: {ex.GetType().Name}, {ex.Message}");
                }
            }

            reader.Close();
        }

        /// <summary>
        /// Override this to safe handle a packet.
        /// </summary>
        /// <param name="reader">The <see cref="BinaryReader"/> connected to this</param>
        public virtual void OnPacketHandled(BinaryReader reader)
        {

        }
    }
}