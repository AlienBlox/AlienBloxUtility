using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace AlienBloxUtility.Utilities.Lua
{
    public class LuaFileManagement : ModSystem
    {
        /// <summary>
        /// Requests a file from the web!
        /// </summary>
        /// <param name="url">The url to request from.</param>
        /// <param name="outputPath">The output location.</param>
        /// <returns>The task.</returns>
        public static async Task Request(string url, string outputPath)
        {
            using HttpClient client = new();

            client.DefaultRequestHeaders.Add("User-Agent", "CSharpApp");

            try
            {
                byte[] fileBytes = await client.GetByteArrayAsync(url);

                await File.WriteAllBytesAsync(outputPath, fileBytes);

                AlienBloxUtility.Instance.Logger.Debug("File downloaded successfully!");
            }
            catch (HttpRequestException e)
            {
                AlienBloxUtility.Instance.Logger.Debug($"Request error: {e.Message}");
            }
        }
    }
}