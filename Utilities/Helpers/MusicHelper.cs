using log4net;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;

namespace AlienBloxUtility.Utilities.Helpers
{
    public static class MusicHelper
    {
        public static int CurrentMusic => Main.curMusic;

        public static LegacyAudioSystem AudioSystem => (LegacyAudioSystem)Main.audioSystem;

        public static string GetCurPath() => GetPath(CurrentMusic);

        public static string GetPath(int music)
        {
            Type t = typeof(MusicLoader);

            if (t.GetField("musicByPath", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static).GetValue(null) is Dictionary<string, int> musicByPath)
            {
                var key = musicByPath.FirstOrDefault(x => x.Value == music).Key;

                Main.NewText(key);

                return key;
            }

            return string.Empty;
        }

        public static void DownloadCurrentMusic(string pathToDown)
        {
            TotallyLegallyDownloadMusic(pathToDown, CurrentMusic);

            Main.NewText(GetPath(CurrentMusic));
        }

        public static bool TotallyLegallyDownloadMusic(string pathToDown, int musicID)
        {
            try
            {
                string music = GetPath(musicID);
                string modName = music.Split('/')[0];

                Main.NewText(music, null);

                if (ModLoader.TryGetMod(modName, out var mod))
                {
                    music = mod.GetCleanPath(music);

                    Main.NewText(music, null);

                    byte[] file = null;
                    string detectedExtension = ".ogg";
                    TmodFile tmod = mod.GetTModFile();

                    using (tmod.Open())
                    {
                        if (file == null)
                        {
                            file = mod.GetFileBytes(music + ".ogg");
                            detectedExtension = ".ogg";
                        }
                    
                        if (file == null)
                        {
                            file = mod.GetFileBytes(music + ".mp3");
                            detectedExtension = ".mp3";
                        }
                    }

                    using FileStream fs = File.Create(Path.Combine(pathToDown + @"\" + Path.GetFileName(music)) + detectedExtension);

                    fs.Write(file);

                    LogManager.GetLogger("AlienBloxUtility").Info($"Successfully downloaded music to {pathToDown}");
                }
                else
                {
                    Main.NewText($"Failed to download music. Mod {modName} not found.", Color.Red);
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