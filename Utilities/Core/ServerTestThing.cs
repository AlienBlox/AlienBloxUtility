using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlienBloxUtility.Utilities.Core
{
    public class AlienBloxServerTracker : ModSystem
    {
        public event Action OnPlayerJoin;

        private int PlayerPrev;

        private int PlayerCurrent;

        public override void Load()
        {
            OnPlayerJoin += SyncPlayerData;
        }

        public override void Unload()
        {
            OnPlayerJoin -= SyncPlayerData;
        }

        public override void PreUpdatePlayers()
        {
            if (Main.netMode == NetmodeID.Server)
                PlayerPrev = Main.player.Length;
        }

        public override void PostUpdatePlayers()
        {
            if (Main.netMode == NetmodeID.Server)
            {
                PlayerCurrent = Main.player.Length;

                if (PlayerPrev != PlayerCurrent)
                {
                    Console.WriteLine("Player joined...");

                    OnPlayerJoin?.Invoke();
                }
            }
        }

        public static void SyncPlayerData()
        {
            if (Main.netMode != NetmodeID.Server)
                return;

            foreach (var player in Main.ActivePlayers)
            {
                ModPacket pkt = AlienBloxUtility.Instance.GetPacket();

                pkt.Write((byte)AlienBloxUtility.Messages.SyncFlight);
                pkt.Write(player.position.X);
                pkt.Write(player.position.Y);
                pkt.Write(player.AlienBloxUtility().noClipHack);
                pkt.Send(-1, player.whoAmI);
            }
        } 
    }
}