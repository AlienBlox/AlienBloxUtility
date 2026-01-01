using System.IO;

namespace AlienBloxUtility.Utilities.IO
{
    public static class AlienBloxFileHandler
    {
        /// <summary>
        /// Attempts to read all bytes of a file.
        /// </summary>
        /// <param name="path">The path to read</param>
        /// <returns>The bytes</returns>
        public static byte[] ReadAllBytes(string path)
        {
            try
            {
                using var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                using var br = new BinaryReader(fs);

                br.BaseStream.Position = 0;

                return br.ReadBytes((int)br.BaseStream.Length);
            }
            catch
            {
                return [];
            }
        }
    }
}