using AlienBloxUtility.Utilities.UIUtilities.UIRenderers;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Terraria;
using Terraria.ID;

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
                MemoryStream memoryStream = new();
                StreamWriter writer = new(memoryStream);

                writer.Write(value);
                writer.Dispose();

                AlienBloxUtility.SendAlienBloxPacket("ServerOutputPacket", memoryStream.ToArray());
                Console.WriteLine(value);

                memoryStream.Close();
            }
        }

        public static string RemoveIntrusiveChars(string input)
        {
            // Remove common intrusive characters like tabs, newlines, and carriage returns
            string pattern = @"[\t\r\n]";

            // Replace these characters with an empty string
            return Regex.Replace(input, pattern, "");
        }

        public static string RemoveInvalidCharacters(string input)
        {
            // Regular expression to match any non-printable characters
            string pattern = @"[^\x20-\x7E]"; // Matches anything outside of printable ASCII range

            // Replace invalid characters with an empty string
            return Regex.Replace(input, pattern, "");
        }
    }
}