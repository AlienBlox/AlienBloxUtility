using AlienBloxUtility.Utilities.UIUtilities.UIRenderers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;

namespace AlienBloxUtility.Utilities.LuaHelpers
{
    public class LuaFileManagement : ModSystem
    {
        /// <summary>
        /// Requests a file from the web!
        /// </summary>
        /// <param name="url">The url to request from.</param>
        /// <param name="outputPath">The output location.</param>
        /// <returns>The task.</returns>
        public static async Task Request(string url, string outputPath, string fileName = "Test")
        {
            using HttpClient client = new();

            client.DefaultRequestHeaders.Add("User-Agent", "CSharpApp");

            try
            {
                byte[] fileBytes = await client.GetByteArrayAsync(url);

                await File.WriteAllBytesAsync(outputPath + $"\\{fileName}.lua", fileBytes);

                ConHostRender.Write($"Installing...");
                AlienBloxUtility.Instance.Logger.Debug("File downloaded successfully!");
            }
            catch (HttpRequestException e)
            {
                ConHostRender.Write($"Request error: {e.Message}");
                AlienBloxUtility.Instance.Logger.Debug($"Request error: {e.Message}");
            }
        }

        public static string[][] GetAllLuaFiles()
        {
            List<string[]> LuaFile = [];

            foreach (string F in Directory.GetFiles(AlienBloxUtility.LuaStorageLocation))
            {
                LuaFile.Add(File.ReadAllLines(F));
            }

            return [.. LuaFile];
        }
    }
}