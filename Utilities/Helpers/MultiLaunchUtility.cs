using AlienBloxUtility.Utilities.UIUtilities.UIRenderers;
using Steamworks;
using System.Diagnostics;
using Terraria;

namespace AlienBloxUtility.Utilities.Helpers
{
    public static class MultiLaunchUtility
    {
        public static void LogEvt(object sender, DataReceivedEventArgs evt)
        {
            ConHostRender.Write(evt.Data);
        }

        public static void LogErr(object sender, DataReceivedEventArgs evt)
        {
            ConHostRender.Write($"[c/FF0000:{evt.Data}]");
        }

        /// <summary>
        /// Runs an extra instance of tModLoader
        /// </summary>
        /// <param name="Extras">The extra arguments to run tModLoader with</param>
        /// <param name="redirectOutput">Should the output of tModLoader be redirected</param>
        /// <returns>The tModLoader process</returns>
        public static Process RunTModLoader(string Extras = "", bool redirectOutput = false)
        {
            SteamApps.GetAppInstallDir(new AppId_t(1281930), out string Path, 512);

            try
            {
                if (AlienBloxUtility.GetPlatform() == "Windows")
                {
                    ProcessStartInfo info = new(Path + "\\Start-tModLoader.bat")
                    {
                        Arguments = Extras,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        RedirectStandardOutput = redirectOutput,
                        RedirectStandardError = redirectOutput,
                    };

                    var proc = Process.Start(info);

                    if (info.RedirectStandardOutput)
                    {
                        proc.OutputDataReceived += LogEvt;
                    }

                    if (info.RedirectStandardError)
                    {
                        proc.ErrorDataReceived += LogErr;
                    }

                    return proc;
                }

                if (AlienBloxUtility.GetPlatform() == "macOS" || AlienBloxUtility.GetPlatform() == "Linux")
                {
                    ProcessStartInfo info = new(Path + "\\Start-tModLoader.sh")
                    {
                        Arguments = Extras,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        RedirectStandardOutput = redirectOutput,
                        RedirectStandardError = redirectOutput,
                    };

                    var proc = Process.Start(info);

                    if (info.RedirectStandardOutput)
                    {
                        proc.OutputDataReceived += LogEvt;
                    }

                    if (info.RedirectStandardError)
                    {
                        proc.ErrorDataReceived += LogErr;
                    }

                    return proc;
                }
            }
            catch
            {
                Main.NewText("Failed to start tModLoader!");
            }

            return null;
        }
    }
}