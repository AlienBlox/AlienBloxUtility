using System;
using System.IO;

namespace AlienBloxUtility.Utilities.XNBEngine
{
    public static class XNBExtractor
    {
        public static void ExtractXNB(string filePath)
        {
            using FileStream fs = new(filePath, FileMode.Open, FileAccess.Read);
            using BinaryReader reader = new(fs);
            // Read the header information (for example, magic number)
            byte[] header = reader.ReadBytes(4); // Usually the first 4 bytes are the magic number "XNB"

            // You may need to skip over additional header data (such as version, content type, etc.)
            int unknownHeaderData1 = reader.ReadInt32();
            int unknownHeaderData2 = reader.ReadInt32();
            int dataLength = reader.ReadInt32();  // Length of the data block

            // Optionally, read more data depending on the structure of the XNB file
            byte[] data = reader.ReadBytes(dataLength);

            // You can then handle the extracted data based on the type (e.g., texture, audio, etc.)
            //Console.WriteLine("XNB File Extracted Successfully!");
        }
    }
}