using AlienBloxUtility.Utilities.Abstracts;
using System.IO;
using Terraria;
using Terraria.ID;

namespace AlienBloxUtility.Utilities.NetCode.Packets
{
    public class OnToolUsePacket : AlienBloxPacket
    {
        public override void OnPacketHandled(BinaryReader reader)
        {
            string toolType = reader.ReadString();
            int plr = reader.ReadInt32();
            bool sudo = reader.ReadBoolean();

            foreach (var tool in DebugTool.Tools)
            {
                if (tool.Name == toolType)
                {
                    tool.OnToolUse(Main.player[plr], sudo);
                }
            }

            if (Main.netMode == NetmodeID.Server)
            {
                DebugTool.UseTool(toolType, plr, sudo);
            }
        }
    }
}