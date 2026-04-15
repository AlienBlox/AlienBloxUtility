using log4net;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlienBloxUtility.Utilities.Helpers
{
    public static class MusicHelper
    {
        public static int CurrentMusic => Main.curMusic;

        public static LegacyAudioSystem AudioSystem => (LegacyAudioSystem)Main.audioSystem;

        public static string GetCurPath() => GetPath(CurrentMusic);

        public static string GetPath(int music)
        {
            if (MusicID.Search.TryGetName(music, out var name))
                return name;

            return AudioSystem.TrackNamesByIndex.TryGetValue(music, out var value) ? value : string.Empty;
        }

        public static void DownloadCurrentMusic(string pathToDown)
        {
            TotallyLegallyDownloadMusic(pathToDown, CurrentMusic);
        }

        public static bool TotallyLegallyDownloadMusic(string pathToDown, int musicID)
        {
            try
            {
                string music = GetPath(musicID);
                string modName = music.Split('/')[0];

                if (ModLoader.TryGetMod(modName, out var mod))
                {
                    byte[] file = null;

                    file ??= mod.GetTModFile().GetBytes(mod.GetCleanPath(music) + ".ogg");

                    file ??= mod.GetTModFile().GetBytes(mod.GetCleanPath(music) + ".mp3");

                    using FileStream fs = File.Create(Path.Combine(pathToDown + @"\" + Path.GetFileName(music)));

                    fs.Write(file);

                    LogManager.GetLogger("AlienBloxUtility").Info($"Successfully downloaded music to {pathToDown}");
                }
                else
                {
                    Main.NewText($"Failed to download music. Mod {modName} not found.", Microsoft.Xna.Framework.Color.Red);
                }

                return true;
            }
            catch (Exception e)
            {
                LogManager.GetLogger("AlienBloxUtility").Warn($"Can't download music. ({e.GetType().Name}: {e.Message})");

                return false;
            }
        }
    }
}