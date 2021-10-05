using System;
using System.Diagnostics;
using System.IO;
using websocket_client;

namespace TargetScriptManager
{
    class Program
    {
        static void Main(string[] args)
        {
            SystemHealthChecker shc = new SystemHealthChecker();
            TimeSpan ts = shc.GetSystemUpTimeMinutes();
            Console.WriteLine(ts.TotalMinutes);
            Console.WriteLine(shc.GetCpuPercentageUsage() + "%");
            Console.WriteLine(shc.GetRamPercentageUsage());
            //shc.PingAsync().Start();
            bool b = shc.PingAsync().Result;
            Console.WriteLine(b);
        }
    }
}
