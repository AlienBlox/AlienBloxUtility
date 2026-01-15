using AlienBloxUtility.Utilities.Abstracts;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;

namespace AlienBloxUtility.Utilities.NetCode.Packets
{
    public class OneShotPacket : AlienBloxPacket
    {
        public override void OnPacketHandled(BinaryReader reader)
        {
            int whoAmI = reader.ReadInt32();
            byte oneShotType = reader.ReadByte();

            switch (oneShotType)
            {
                case 0:
                    Main.npc[whoAmI].StrikeInstantKill();
                    break;
                case 1:
                    Main.projectile[whoAmI].Kill();
                    break;
                case 2:
                    PlayerDeathReason death = new()
                    {
                        CustomReason = NetworkText.FromKey("Mods.AlienBloxUtility.Messages.OneShotPlayer", Main.player[whoAmI].name)
                    };

                    Main.player[whoAmI].KillMe(death, double.MaxValue, 1);
                    break;
            }
        }
    }
}