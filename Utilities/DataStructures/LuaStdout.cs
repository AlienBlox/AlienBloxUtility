using AlienBloxUtility.Utilities.UIUtilities.UIRenderers;
using System;
using System.IO;
using System.Text;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;

namespace AlienBloxUtility.Utilities.DataStructures
{
    public class LuaStdout : TextWriter
    {
        public override Encoding Encoding => Encoding.UTF8;

        public override void Write(string value)
        {
            if (Main.netMode != NetmodeID.Server)
            {
                ConHostRender.Write(value);
            }
            else
            {
                //AlienBloxUtility.OutputTo(value);
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(value), Colors.CoinSilver);
                Console.WriteLine(value);
            }
        }
    }
}