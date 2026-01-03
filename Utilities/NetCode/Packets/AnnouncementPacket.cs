using AlienBloxUtility.Utilities.Abstracts;
using AlienBloxUtility.Utilities.DataStructures;
using AlienBloxUtility.Utilities.UIUtilities.UIRenderers;
using System.IO;
using System.Text;
using Terraria;
using Terraria.ID;

namespace AlienBloxUtility.Utilities.NetCode.Packets
{
    public class AnnouncementPacket : AlienBloxPacket
    {
        public override void OnPacketHandled(BinaryReader reader)
        {
            var bytes = reader.ReadBytes((int)reader.BaseStream.Length);

            string message = Encoding.Default.GetString(bytes);

            if (Main.netMode != NetmodeID.Server && AlienBloxUtilityConfig.Instance.Noisy)
            {
                ConHostRender.Write($"[Announcement]: {LuaStdout.RemoveIntrusiveChars(message)}");
                Main.NewText($"[Announcement]: {LuaStdout.RemoveIntrusiveChars(message)}");
            }
        }
    }
}