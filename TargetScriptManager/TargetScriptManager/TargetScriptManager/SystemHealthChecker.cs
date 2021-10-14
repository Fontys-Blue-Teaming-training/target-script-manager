using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.VisualBasic;

namespace TargetScriptManager
{
    public class SystemHealthChecker
    {
        PerformanceCounter cpuCounter;

        public SystemHealthChecker()
        {
            cpuCounter = new PerformanceCounter("Processor Information", "% Processor Utility", "_Total", true);
            cpuCounter.NextValue();
        }


        public TimeSpan GetSystemUpTimeMinutes()
        {
            using (var uptime = new System.Diagnostics.PerformanceCounter("System", "System Up Time"))
            {
                uptime.NextValue();       //Call this an extra time before reading its value
                return TimeSpan.FromSeconds(uptime.NextValue());
            }
        }

        public float GetCpuPercentageUsageRaw()
        {
            var value = cpuCounter.NextValue();

            while (value > 100 || value < 1)
            {
                value = cpuCounter.NextValue();
            }

            return value;
        }

        public string GetCpuPercentageUsage()
        {
            var value = cpuCounter.NextValue();

            while(value > 100 || value < 1)
            {
                value = cpuCounter.NextValue();
            }

            return Math.Round(value).ToString();
        }

        public float GetRamPercentageUsageRaw()
        {
            PerformanceCounter ramCounter = new PerformanceCounter("Memory", "Available MBytes");
            return ramCounter.NextValue();
        }

        public double GetRamPercentageUsage()
        {
            PerformanceCounter performanceCounter = new PerformanceCounter("Memory", "Available MBytes");
            float f = 0;
            ObjectQuery wql = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(wql);
            ManagementObjectCollection results = searcher.Get();

            foreach (ManagementObject result in results)
            {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       
                f = float.Parse(result["TotalVisibleMemorySize"].ToString());
            }
            float maxRAM = f / 1000;
            float usedRAM = maxRAM - performanceCounter.NextValue();
            float ramPercentage = (usedRAM / maxRAM) * 100;
            return Math.Round(ramPercentage);
        }

        public async Task<bool> PingAsync()
        {
            var hostUrl = "www.google.com";
            Ping ping = new Ping();
            PingReply result = await ping.SendPingAsync(hostUrl);
            return result.Status == IPStatus.Success;

        }
    }
}
