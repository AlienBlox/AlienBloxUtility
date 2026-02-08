using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace AlienBloxUtility.Utilities.EntityManipulation.Freezes
{
    public class GlobalNPCFreeze : GlobalNPC
    {
        public static bool GlobalFrozen;

        public override bool InstancePerEntity => true;

        public bool Frozen;

        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            Frozen = GlobalFrozen;
            npc.netUpdate = true;
        }

        public override bool PreAI(NPC npc)
        {
            if (Frozen)
            {
                npc.velocity = Vector2.Zero;

                return false;
            }

            return base.PreAI(npc);
        }

        public override void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
        {
            bitWriter.WriteBit(Frozen);
        }

        public override void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
        {
            Frozen = bitReader.ReadBit();
        }
    }
}