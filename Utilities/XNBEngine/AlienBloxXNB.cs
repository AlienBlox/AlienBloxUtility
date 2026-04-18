using System;
using System.IO;
using System.Text;

namespace AlienBloxUtility.Utilities.XNBEngine
{
    /// <summary>
    /// A helper class used to read XNBs
    /// </summary>
    public class AlienBloxXNB
    {
        public const string CRAP_HAPPENED = "Sh*t happened";

        public byte[] ParsedData;

        public AlienBloxXNB(byte[] xnb)
        {
            using MemoryStream ms = new(xnb);
            using BinaryReader reader = new(ms);

            reader.ReadBytes(6); // Magic + Platform + Version + Flags
            int fileSize = reader.ReadInt32();

            int formatSize = reader.ReadInt32();
            byte[] formatHeader = reader.ReadBytes(formatSize);

            int dataSize = reader.ReadInt32();

            ParsedData = reader.ReadBytes(dataSize);
        }

        public AlienBloxXNB(Stream xnb)
        {
            try
            {
                using BinaryReader reader = new(xnb);

                reader.ReadBytes(6); // Magic + Platform + Version + Flags
                int fileSize = reader.ReadInt32();

                int formatSize = reader.ReadInt32();
                byte[] formatHeader = reader.ReadBytes(formatSize);

                int dataSize = reader.ReadInt32();

                ParsedData = reader.ReadBytes(dataSize);
            }
            catch
            {
                ParsedData = Encoding.ASCII.GetBytes(CRAP_HAPPENED);
            }
        }

        public byte[] PackageAsWav(int sampleRate, short channels, short bitsPerSample)
        {
            int headerSize = 44;
            int totalDataSize = ParsedData.Length;
            byte[] wavFile = new byte[headerSize + totalDataSize];

            // RIFF Chunk
            Buffer.BlockCopy(Encoding.ASCII.GetBytes("RIFF"), 0, wavFile, 0, 4);
            BitConverter.GetBytes(totalDataSize + 36).CopyTo(wavFile, 4);
            Buffer.BlockCopy(Encoding.ASCII.GetBytes("WAVE"), 0, wavFile, 8, 4);

            // Format Chunk
            Buffer.BlockCopy(Encoding.ASCII.GetBytes("fmt "), 0, wavFile, 12, 4);
            BitConverter.GetBytes(16).CopyTo(wavFile, 16); // Subchunk size (16 for PCM)
            BitConverter.GetBytes((short)1).CopyTo(wavFile, 20); // AudioFormat (1 = PCM)
            BitConverter.GetBytes(channels).CopyTo(wavFile, 22);
            BitConverter.GetBytes(sampleRate).CopyTo(wavFile, 24);
            BitConverter.GetBytes(sampleRate * channels * (bitsPerSample / 8)).CopyTo(wavFile, 28); // ByteRate
            BitConverter.GetBytes((short)(channels * (bitsPerSample / 8))).CopyTo(wavFile, 32); // BlockAlign
            BitConverter.GetBytes(bitsPerSample).CopyTo(wavFile, 34);

            // Data Chunk
            Buffer.BlockCopy(Encoding.ASCII.GetBytes("data"), 0, wavFile, 36, 4);
            BitConverter.GetBytes(totalDataSize).CopyTo(wavFile, 40);

            // Actual PCM Data
            Buffer.BlockCopy(ParsedData, 0, wavFile, headerSize, totalDataSize);

            return wavFile;
        }
    }
}