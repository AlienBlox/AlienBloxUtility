using AlienBloxUtility.Utilities.Core;
using AlienBloxUtility.Utilities.NetCode;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AlienBloxUtility.Utilities.Helpers
{
    public class PacketSpyUtility : ModSystem
    {
        public static event Action<byte, long, BinaryReader> OnPacketReceive;

        public static int UnixTime => (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

        public override void Load()
        {
            NetSystem.OnPacketReceived += RunPacketSpy;
        }

        public override void Unload()
        {
            NetSystem.OnPacketReceived -= RunPacketSpy;
        }

        public static void RunPacketSpy(byte MessageType, long size, BinaryReader reader)
        {
            if (!DebugUtilityList.PacketSpyEnabled)
            {
                return;
            }

            Main.NewText(Language.GetText("Mods.AlienBloxUtility.Messages.PacketSpy.PacketDataReceived").Format(MessageType, size, UnixTime));
            AlienBloxUtility.Instance.Logger.Info(Language.GetText("Mods.AlienBloxUtility.Messages.PacketSpy.PacketDataReceived").Format(MessageType, size, UnixTime));
            OnPacketReceive?.Invoke(MessageType, size, reader);
        }
    }
}