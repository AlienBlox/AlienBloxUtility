using System;
using System.Diagnostics;

namespace AlienBloxUtility.Utilities.Core
{
    internal class AlienBloxDiags
    {

        public static Process CurrentProc => Process.GetCurrentProcess();
        public static long RamFull => CurrentProc.WorkingSet64;
        public static long PrivateBytes => CurrentProc.PrivateMemorySize64;
        public static long GCMem => GC.GetTotalMemory(forceFullCollection: false);

        private readonly Process process;
        private TimeSpan lastTotalProcessorTime;
        private DateTime lastCheckTime;

        public AlienBloxDiags()
        {
            process = Process.GetCurrentProcess();
            lastTotalProcessorTime = process.TotalProcessorTime;
            lastCheckTime = DateTime.UtcNow;
        }

        public double GetCpuUsagePercent()
        {
            DateTime now = DateTime.UtcNow;
            TimeSpan cpuUsage = process.TotalProcessorTime - lastTotalProcessorTime;
            double secondsPassed = (now - lastCheckTime).TotalSeconds;

            int cpuCount = Environment.ProcessorCount;

            double cpuPercent = (cpuUsage.TotalSeconds / secondsPassed) * 100 / cpuCount;

            lastTotalProcessorTime = process.TotalProcessorTime;
            lastCheckTime = now;

            return cpuPercent;
        }
    }
}