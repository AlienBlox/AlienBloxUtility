using AlienBloxUtility.Utilities.Core;
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
            OnPacketReceive += RunPacketSpy;
        }

        public override void Unload()
        {
            OnPacketReceive -= RunPacketSpy;
        }

        public static void RunPacketSpy(byte MessageType, long size, BinaryReader reader)
        {
            if (!DebugUtilityList.PacketSpyEnabled || !Main.LocalPlayer.AlienBloxUtility().CanReceivePacketSpyMessage)
            {
                return;
            }

            Main.NewText(Language.GetText("Mods.AlienBloxUtility.Messages.PacketSpy.PacketDataReceived").Format(MessageType, size, UnixTime));
            AlienBloxUtility.Instance.Logger.Info(Language.GetText("Mods.AlienBloxUtility.Messages.PacketSpy.PacketDataReceived").Format(MessageType, size, UnixTime));

            OnPacketReceive?.Invoke(MessageType, size, reader);

            Main.LocalPlayer.AlienBloxUtility().CanReceivePacketSpyMessage = false;
        }
    }
}