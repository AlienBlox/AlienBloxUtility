using AlienBloxUtility.Utilities.Core;
using System;
using System.IO;
using Terraria;
using Terraria.ModLoader;

namespace AlienBloxUtility.Utilities.Helpers
{
    public class PacketSpyUtility : ModSystem
    {
        public static event Action<byte, long, BinaryReader> OnPacketReceive;

        public override void Load()
        {
            OnPacketReceive += RunPacketSpy;
        }

        public override void Unload()
        {
            OnPacketReceive -= RunPacketSpy;
        }

        public static void RunPacketSpy(byte MessageType, long size, BinaryReader reader)
        {
            if (DebugUtilityList.PacketSpyEnabled)
            {
                
            }

            OnPacketReceive?.Invoke(MessageType, size, reader);
        }
    }
}