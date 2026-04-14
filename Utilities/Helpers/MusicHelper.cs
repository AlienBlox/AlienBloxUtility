using log4net;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader.IO;

namespace AlienBloxUtility.Utilities.Helpers
{
    public static class MusicHelper
    {
        public static int CurrentMusic => Main.curMusic;

        public static LegacyAudioSystem AudioSystem => (LegacyAudioSystem)Main.audioSystem;

        public static string GetCurPath() => GetPath(CurrentMusic);

        public static string GetPath(int music)
        {
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