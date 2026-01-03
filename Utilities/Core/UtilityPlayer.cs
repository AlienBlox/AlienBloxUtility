using Microsoft.Xna.Framework;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace AlienBloxUtility.Utilities.Core
{
    public class UtilityPlayer : ModPlayer
    {
        public bool noClipHack;

        public Vector2 noClipHackPos;

        public override void PostUpdate()
        {
            bool hack = noClipHack;

            if (hack != noClipHack)
            {
                noClipHackPos = Player.position;
            }

            if (noClipHack)
            {
                Player.position = noClipHackPos;
            }
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (noClipHack)
            {
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
                    noClipHackPos.X += (Player.direction * 40);
                }

                if (triggersSet.Right)
                {
                    noClipHackPos.X += (Player.direction * 40);
                }
            }
        }

        public override void OnEnterWorld()
        {
            AlienBloxUtility.SendSteamID(Player);
            //AlienBloxUtility.RetrieveSteamID();
        }
    }
}