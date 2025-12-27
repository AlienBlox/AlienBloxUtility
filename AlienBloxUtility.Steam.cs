using Steamworks;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlienBloxUtility
{
    public partial class AlienBloxUtility
    {
        public static Dictionary<int, (ulong, string)> SteamIDs;

        public static void SendSteamID(Player plr)
        {
            if (Main.myPlayer == plr.whoAmI)
            {
                // Get the SteamID for the current player using Steamworks API
                CSteamID steamID = SteamUser.GetSteamID();

                // The Steam ID can be retrieved like this:
                ulong steamID64 = steamID.m_SteamID;

                // Get the player's Steam Persona Name (username)
                string steamName = SteamFriends.GetPersonaName();

                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    ModPacket pkt = Instance.GetPacket();

                    pkt.Write((byte)Messages.SendSteamID);
                    pkt.Write(steamID64);
                    pkt.Write(steamName);
                    pkt.Send();
                }
            }
        }

        public static void RetrieveSteamID()
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket pkt = Instance.GetPacket();

                pkt.Write((byte)Messages.RetrieveSteamID);
                pkt.Send();
            }
        }

        public static void RemoveSteamID(Player plr)
        {
            if (Main.myPlayer == plr.whoAmI)
            {
                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    ModPacket pkt = Instance.GetPacket();

                    pkt.Write((byte)Messages.RemoveSteamID);
                    pkt.Send();
                }
            }
        }
    }
}