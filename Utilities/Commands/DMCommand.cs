using AlienBloxUtility.Utilities.UIUtilities.UIStates;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace AlienBloxUtility.Utilities.Commands
{
    public class DMCommand : ModCommandHelper
    {
        public override string CommandName => "dm";

        public override void LaunchCommand(ConHostSystem Conhost, params string[] Params)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                try
                {
                    MemoryStream stream = new();
                    BinaryWriter writer = new(stream);

                    string sendTo = Params[0];
                    string message = $"[{Main.LocalPlayer.name}]:";

                    if (sendTo == "me")
                    {
                        writer.Write(Main.LocalPlayer.name);
                    }
                    else
                    {
                        writer.Write(sendTo);
                    }
                    
                    for (int i = 1; i < Params.Length; i++)
                    {
                        message += " " + Params[i];
                    }

                    writer.Write(message);
                    writer.Dispose();

                    AlienBloxUtility.SendAlienBloxPacket("DirectMessagePacket", stream.ToArray());

                    Conhost.AddConsoleText(message);

                    stream.Dispose();
                }
                catch
                {

                }
            }
        }
    }
}