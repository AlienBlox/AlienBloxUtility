using Microsoft.Xna.Framework;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace AlienBloxUtility.Utilities.Core
{
    public class UtilityPlayer : ModPlayer
    {
        public bool noClipHack;

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

                /*
                if (prevNoClipHack != noClipHackPos)
                {
                    AlienBloxUtility.SendNoclipHack(noClipHackPos, noClipHack);
                }
                */
            }
        }

        public override void OnEnterWorld()
        {
            AlienBloxUtility.SendSteamID(Player);
            //AlienBloxUtility.RetrieveSteamID();
        }
    }
}