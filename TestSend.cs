using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlienBloxUtility
{
    public class TestSend : ModPlayer
    {
        public int timer = 0;

        public override void PostUpdate()
        {
            if (Main.netMode != NetmodeID.SinglePlayer)
            {
                if (timer++ >= 60 * 5)
                {
                    ModPacket pkt = AlienBloxUtility.Instance.GetPacket();

                    pkt.Write((byte)AlienBloxUtility.Messages.MsgTest);
                    pkt.Send();

                    timer = 0;
                }
            }
        }
    }
}