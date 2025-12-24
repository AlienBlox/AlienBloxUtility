using System;
using System.Security.Cryptography;
using Terraria.ModLoader;

namespace AlienBloxUtility.Utilities.Lua
{
    public class LuaGambling : ModSystem
    {
        /// <summary>
        /// Gambling addiction!
        /// </summary>
        public static RandomNumberGenerator RNG { get; private set; }

        public override void Load()
        {
            RNG = RandomNumberGenerator.Create();
        }

        public override void Unload()
        {
            RNG.Dispose();
            RNG = null;
        }

        public static int Next()
        {
            return Next(0, int.MaxValue);
        }

        // Generate int between 0 (inclusive) and max (exclusive)
        /// <summary>
        /// Generates int between 0 (inclusive) and max (exclusive) with CSPRNG
        /// </summary>
        /// <param name="max">The max value of the RNG</param>
        /// <returns>The secure int</returns>
        public static int Next(int max)
        {
            return Next(0, max);
        }

        // Generate int between min (inclusive) and max (exclusive)
        /// <summary>
        /// Generates int between min (inclusive) and max (exclusive) with CSPRNG
        /// </summary>
        /// <param name="min">The min value of the RNG</param>
        /// <param name="max">The max value of the RNG</param>
        /// <returns>The secure int</returns>
        public static int Next(int min, int max)
        {
            if (min >= max)
                throw new ArgumentException("min must be less than max");

            int range = max - min;

            // Generate random 4 bytes
            byte[] bytes = new byte[4];
            RNG.GetBytes(bytes);
            int value = BitConverter.ToInt32(bytes, 0) & int.MaxValue; // make non-negative

            return min + (value % range); // scale to desired range
        }
    }
}