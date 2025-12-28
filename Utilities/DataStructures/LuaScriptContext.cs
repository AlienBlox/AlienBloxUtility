using System;

namespace AlienBloxUtility.Utilities.DataStructures
{
    public class LuaScriptContext(int maxMilliseconds)
    {
        public DateTime StartTime { get; } = DateTime.UtcNow;
        public int MaxMilliseconds { get; } = maxMilliseconds;
    }
}