using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace AlienBloxUtility.Utilities.Core
{
    public class UtilityPlayer : ModPlayer
    {
        public bool noClipHack;

        public bool Immortal;

        public bool HitboxTool;

        public bool toolSudo;

        public int ForceSyncTimer;

        public Vector2 noClipHackPos;

        public override void PreUpdate()
        {
            if (!noClipHack)
            {
                noClipHackPos = Player.position;
            }
        }

        public override void PostUpdate()
        {
            if (noClipHack)
            {
                Player.position = noClipHackPos;
            }

            if (Immortal)
            {
                Player.statLife = Player.statLifeMax2;
                
                for (int i = 0; i < Main.debuff.Length; i++)
                {
                    Player.buffImmune[i] = true;
                }
            }
        }

        public override void PreUpdateBuffs()
        {
            if (Immortal)
            {
                foreach (var buffs in Player.buffType)
                {
                    if (Main.debuff[buffs])
                    {
                        Player.ClearBuff(buffs);
                    }
                }
            }
        }

        public override void PostUpdateBuffs()
        {
            if (Immortal)
            {
                foreach (var buffs in Player.buffType)
                {
                    if (Main.debuff[buffs])
                    {
                        Player.ClearBuff(buffs);
                    }
                }
            }
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (AlienBloxKeybinds.SudoKeybind.JustPressed)
            {
                toolSudo = !toolSudo;
            }

            if (noClipHack)
            {

                float secondaryModifier = 1f;
                Vector2 prevNoClipHack = noClipHackPos;

                if (AlienBloxKeybinds.SudoKeybind.Current)
                {
                    secondaryModifier = 2f;
                }

                if (triggersSet.Up)
                {
                    noClipHackPos.Y -= 40 * secondaryModifier;
                }

                if (triggersSet.Down)
                {
                    noClipHackPos.Y += 40 * secondaryModifier;
                }

                if (triggersSet.Left)
                {
                    noClipHackPos.X += Player.direction * 40 * secondaryModifier;
                }

                if (triggersSet.Right)
                {
                    noClipHackPos.X += Player.direction * 40 * secondaryModifier;
                }

                if (prevNoClipHack != noClipHackPos)
                {
                    AlienBloxUtility.SendNoclipHack(noClipHackPos, noClipHack);
                }
            }
        }

        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            if (!newPlayer)
                AlienBloxUtility.SendNoclipHack(noClipHackPos, noClipHack);
        }

        public override void OnEnterWorld()
        {
            noClipHack = false;
            AlienBloxUtility.SendNoclipHack(new Vector2(Main.spawnTileX * 16, Main.spawnTileY * 16), noClipHack);

            //DebugSidebarRender.Instance.RegenUI();
            AlienBloxUtility.SendSteamID(Player);
            AlienBloxUtility.RetrieveWallhackData();
            AlienBloxUtility.RetrieveSteamID();
        }

        public override bool CanBeHitByNPC(NPC npc, ref int cooldownSlot)
        {
            return !Immortal;
        }

        public override bool CanBeHitByProjectile(Projectile proj)
        {
            return !Immortal;
        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genDust, ref PlayerDeathReason damageSource)
        {
            noClipHackPos = Player.position;
            noClipHack = false;

            return !Immortal;
        }
    }
}