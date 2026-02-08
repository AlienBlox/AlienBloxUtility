using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace AlienBloxUtility.Utilities.EntityManipulation.Freezes
{
    public class GlobalProjectileFreeze : GlobalProjectile
    {
        public static bool GlobalFrozen;

        public override bool InstancePerEntity => true;

        public bool Frozen;

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            Frozen = GlobalFrozen;
            projectile.netUpdate = true;
        }

        public override bool PreAI(Projectile projectile)
        {
            if (Frozen)
            {
                projectile.velocity = Vector2.Zero;

                return false;
            }

            return base.PreAI(projectile);
        }

        public override void SendExtraAI(Projectile projectile, BitWriter bitWriter, BinaryWriter binaryWriter)
        {
            bitWriter.WriteBit(Frozen);
        }

        public override void ReceiveExtraAI(Projectile projectile, BitReader bitReader, BinaryReader binaryReader)
        {
            Frozen = bitReader.ReadBit();
        }
    }
}