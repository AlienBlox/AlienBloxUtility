using System;
using System.Diagnostics;

namespace AlienBloxUtility.Utilities.Helpers
{
    public static class UrlEngine
    {
        public static void OpenURL(string url)
        {
            try
            {
                ProcessStartInfo psi = new()
                {
                    FileName = url,
                    UseShellExecute = true
                };
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error opening URL: " + ex.Message);
            }
        }
    }
}