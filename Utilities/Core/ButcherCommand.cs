using AlienBloxUtility.Utilities.Abstracts;
using AlienBloxUtility.Utilities.NetCode.Packets;
using AlienBloxUtility.Utilities.UIUtilities.UIStates;
using Terraria;
using Terraria.ID;

namespace AlienBloxUtility.Utilities.Core
{
    public class ButcherCommand : CommandBase
    {
        public override string CommandName => "butcher";

        public override void LaunchCommand(ConHostSystem Conhost, params string[] Params)
        {
            string butcherType = Params[0];

            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                switch (butcherType.ToLower())
                {
                    case "item":
                        foreach (Item I in Main.ActiveItems)
                        {
                            I.active = false;
                        }
                        break;
                    case "npc":
                        foreach (NPC I in Main.ActiveNPCs)
                        {
                            if (!I.friendly || !I.isLikeATownNPC)
                                I.StrikeInstantKill();
                        }
                        break;
                    case "npcfriendly":
                        foreach (NPC I in Main.ActiveNPCs)
                        {
                            if (I.friendly || I.isLikeATownNPC)
                                I.StrikeInstantKill();
                        }
                        break;
                    case "proj":
                        foreach (Projectile P in Main.ActiveProjectiles)
                        {
                            P.Kill();
                        }
                        break;
                }
            }
            else
            {
                switch (butcherType.ToLower())
                {
                    case "item":
                        ButcherPacket.Butcher(0);
                        break;
                    case "npc":
                        ButcherPacket.Butcher(1);
                        break;
                    case "npcfriendly":
                        ButcherPacket.Butcher(2);
                        break;
                    case "proj":
                        ButcherPacket.Butcher(3);
                        break;
                }
            }
        }
    }
}