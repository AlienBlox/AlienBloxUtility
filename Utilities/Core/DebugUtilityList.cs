using Terraria.ModLoader;

namespace AlienBloxUtility.Utilities.Core
{
    /// <summary>
    /// The debug utility list's core functionality
    /// </summary>
    public class DebugUtilityList : ModSystem
    {
        /// <summary>
        /// Is the debug bar menu enabled
        /// </summary>
        public static bool DebugMenuEnabled { get; set; } = false;
        /// <summary>
        /// Is the console window enabled
        /// </summary>
        public static bool ConsoleWindowEnabled { get; set; } = false;
        /// <summary>
        /// Is the stats menu enabled
        /// </summary>
        public static bool StatsMenuEnabled { get; set; } = false;
        /// <summary>
        /// Is the mod decompiler menu enabled
        /// </summary>
        public static bool DecompilerMenuEnabled { get; set; } = false;
        /// <summary>
        /// Is the packet spy utility enabled
        /// </summary>
        public static bool PacketSpyEnabled { get; set; } = false;
        /// <summary>
        /// Is the milker (debugger stats) enabled
        /// </summary>
        public static bool MilkerEnabled { get; set; } = false;
        /// <summary>
        /// Is the extras menu enabled
        /// </summary>
        public static bool ExtrasMenuEnabled { get; set; } = false;
    }
}