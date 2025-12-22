using Terraria.ModLoader;
using System.IO;
using Terraria;
using Terraria.ID;

namespace AlienBloxUtility
{
    public partial class AlienBloxUtility
    {
        public enum Messages : byte
        {
            SyncBasics,
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            byte MessageID = reader.ReadByte();

            switch ((Messages)MessageID)
            {
                case Messages.SyncBasics:
                    if (Main.netMode == NetmodeID.Server)
                    {
                        Player NetPlr = Main.player[reader.ReadInt32()];
                        int Life = reader.ReadInt32();
                        int LifeMax2 = reader.ReadInt32();
                        int Mana = reader.ReadInt32();
                        int Defense = reader.ReadInt32();

                        NetPlr.statLife = Life;
                        NetPlr.statLifeMax2 = LifeMax2;
                        NetPlr.statMana = Mana;
                        NetPlr.statDefense += Defense;
                    }
                    break;
            }
        }

        public static void SyncBasics(Player Plr, int defense)
        {
            SyncBasics(Plr.whoAmI, Plr.statLife, Plr.statManaMax2, Plr.statMana, defense);
        }

        public static void SyncBasics(int plr, int life, int lifemax2, int mana, int defense)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket Packet = AlienBloxUtility.Instance.GetPacket();

                Packet.Write((byte)Messages.SyncBasics);
                Packet.Write(plr);
                Packet.Write(life);
                Packet.Write(lifemax2);
                Packet.Write(mana);
                Packet.Write(defense);
                Packet.Send();
            }
        }
    }
}