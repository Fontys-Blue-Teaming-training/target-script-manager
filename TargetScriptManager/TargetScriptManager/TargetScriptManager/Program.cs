using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using websocket_client;
using System.Threading;

namespace TargetScriptManager
{
    class Program
    {
        static void Main(string[] args)
        {
            //TimeSpan ts = shc.GetSystemUpTimeMinutes();
            //Console.WriteLine(ts.TotalMinutes);
            //Console.WriteLine(shc.GetCpuPercentageUsage() + "%");
            //Console.WriteLine(shc.GetRamPercentageUsage());
            //shc.PingAsync().Start();
            //bool b = shc.PingAsync().Result;
            //Console.WriteLine(b);

            SystemHealthChecker shc = new SystemHealthChecker();
            TimeSpan tss;
            bool bb;
            string s;

            while (true)
            {
                tss = shc.GetSystemUpTimeMinutes();
                bb = shc.PingAsync().Result;
                if (bb)
                {
                    s = "The system can connect to the internet";
                }
                else
                {
                    s = "The system cannot connect to the internet";
                }
                var systemInformation = new SystemInformation
                {
                    CurrentSystemUpTime = tss.TotalMinutes.ToString(),
                    CurrentCpuPercentageUsage = shc.GetCpuPercentageUsage() + "%",
                    CurrentRamPercentageUsage = shc.GetRamPercentageUsage(),
                    CurrentInternetConnectivity = s
                };
                string jsonString = JsonSerializer.Serialize<SystemInformation>(systemInformation);
                Console.WriteLine(jsonString);
                Thread.Sleep(2000);
            }




            //SystemInformation si = new SystemInformation();
            //si.CurrentSystemUpTime = tss.TotalMinutes.ToString();
            //si.CurrentCpuPercentageUsage = shc.GetCpuPercentageUsage() + "%";
            //si.CurrentRamPercentageUsage = shc.GetRamPercentageUsage();
            //si.CurrentInternetConnectivity = s;

        }
    }
}
