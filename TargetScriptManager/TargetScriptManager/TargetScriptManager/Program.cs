using System;
using websocket_client;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TargetScriptManager
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //TimeSpan ts = shc.GetSystemUpTimeMinutes();
            //Console.WriteLine(ts.TotalMinutes);
            //Console.WriteLine(shc.GetCpuPercentageUsage() + "%");
            //Console.WriteLine(shc.GetRamPercentageUsage());
            //shc.PingAsync().Start();
            //bool b = shc.PingAsync().Result;
            //Console.WriteLine(b);
            var handler = new TargetScriptMessageHandler<Message>();
            SocketClient client = new SocketClient(handler);
            client.InitClient();
            await client.StartClient();

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
                string jsonString = JsonConvert.SerializeObject(systemInformation);
                var bla = new InfoMessage(InfoMessageType.INFO, jsonString);
                handler.SendMessage(JsonConvert.SerializeObject(bla));
                Thread.Sleep(2000);
            }

            Console.ReadLine();
            //SystemInformation si = new SystemInformation();
            //si.CurrentSystemUpTime = tss.TotalMinutes.ToString();
            //si.CurrentCpuPercentageUsage = shc.GetCpuPercentageUsage() + "%";
            //si.CurrentRamPercentageUsage = shc.GetRamPercentageUsage();
            //si.CurrentInternetConnectivity = s;
        }
    }
}
