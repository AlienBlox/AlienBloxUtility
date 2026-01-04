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
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (noClipHack)
            {
                Vector2 prevNoClipHack = noClipHackPos;

                if (triggersSet.Up)
                {
                    noClipHackPos.Y -= 40;
                }

                if (triggersSet.Down)
                {
                    noClipHackPos.Y += 40;
                }

                if (triggersSet.Left)
                {
                    noClipHackPos.X += Player.direction * 40;
                }

                if (triggersSet.Right)
                {
                    noClipHackPos.X += Player.direction * 40;
                }

                if (prevNoClipHack != noClipHackPos)
                {
                    AlienBloxUtility.SendNoclipHack(noClipHackPos, noClipHack);
                }
            }
        }

        public override void PlayerConnect()
        {
            noClipHack = false;

            if (Main.myPlayer == Player.whoAmI)
                AlienBloxUtility.SendNoclipHack(noClipHackPos, noClipHack);
        }

        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            if (!newPlayer)
                AlienBloxUtility.SendNoclipHack(noClipHackPos, noClipHack);
        }

        public override void OnEnterWorld()
        {
            //DebugSidebarRender.Instance.RegenUI();
            AlienBloxUtility.SendSteamID(Player);
            //AlienBloxUtility.RetrieveWallhackData();
            AlienBloxUtility.RetrieveSteamID();
        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genDust, ref PlayerDeathReason damageSource)
        {
            noClipHackPos = Player.position;
            noClipHack = false;

            return Immortal;
        }
    }
}