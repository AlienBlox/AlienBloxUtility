using AlienBloxUtility.Utilities.UIUtilities.UIStates;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlienBloxUtility.Utilities.Commands
{
    public class DMCommand : ModCommandHelper
    {
        public override string CommandName => "dm";

        public override void LaunchCommand(ConHostSystem Conhost, params string[] Params)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                try
                {
                    ModPacket pkt = AlienBloxUtility.Instance.GetPacket();

                    pkt.Write((byte)AlienBloxUtility.Messages.DMUser);
                    pkt.Write(Params[0]);
                    pkt.Write($"[{Main.LocalPlayer.name}]:" + Params[1]);
                    pkt.Send();
                }
                catch
                {

                }
            }
        }
    }
}