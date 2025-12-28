using AlienBloxUtility.Utilities.Abstracts;
using AlienBloxUtility.Utilities.UIUtilities.UIStates;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlienBloxUtility.Utilities.Commands
{
    public class NetTest : CommandBase
    {
        public override string CommandName => "nettest";

        public override void LaunchCommand(ConHostSystem Conhost, params string[] Params)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket pkt = AlienBloxUtility.Instance.GetPacket();

                pkt.Write((byte)AlienBloxUtility.Messages.MsgTest);
                pkt.Send();
            }
        }
    }
}