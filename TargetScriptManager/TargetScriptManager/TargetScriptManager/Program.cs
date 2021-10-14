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
            //SystemHealthChecker shcc = new SystemHealthChecker();
            //Console.WriteLine(shcc.GetSystemUpTimeMinutes());
            //Console.WriteLine(shcc.GetCpuPercentageUsage());
            //Console.WriteLine(shcc.GetRamPercentageUsage());

            //if (shcc.GetCpuPercentageUsageRaw() > 10)
            //{
            //    Console.WriteLine("above 10");
            //}
            //if (shcc.GetCpuPercentageUsageRaw() < 20)
            //{
            //    Console.WriteLine("below 20");
            //}
            //if (shcc.GetRamPercentageUsageRaw() < 3000)
            //{
            //    Console.WriteLine("test");
            //}

            Console.WriteLine("Client running.");
            var handler = new TargetScriptMessageHandler<Message>();
            SocketClient client = new SocketClient(handler);
            client.InitClient();
            await client.StartClient();

            SystemHealthChecker shc = new SystemHealthChecker();
            TimeSpan tss;
            string s;
            bool pingCheck;

            while (true)
            {
                tss = shc.GetSystemUpTimeMinutes();
                pingCheck = shc.PingAsync().Result;
                if (pingCheck)
                {
                    s = "The system can connect to the internet";
                }
                else
                {
                    s = "The system cannot connect to the internet";
                }

                bool cpuCheck = false;
                bool ramCheck = false;

                if (shc.GetCpuPercentageUsageRaw() > 10)
                {
                    cpuCheck = true;
                }

                if (shc.GetRamPercentageUsageRaw() > 3000)
                {
                    cpuCheck = true;
                }


                var systemInformation = new SystemInformation
                {
                    CurrentSystemUpTime = tss.TotalMinutes.ToString(),
                    CurrentCpuPercentageUsage = shc.GetCpuPercentageUsage() + "%",
                    CurrentRamPercentageUsage = shc.GetRamPercentageUsage(),
                    CurrentInternetConnectivity = s
                };
                string jsonString = JsonConvert.SerializeObject(systemInformation);

                if (pingCheck == false || cpuCheck || ramCheck)
                {
                    var info = new InfoMessage(InfoMessageType.WARNING, jsonString);
                    handler.SendMessage(JsonConvert.SerializeObject(info));
                }
                else
                {
                    var info = new InfoMessage(InfoMessageType.INFO, jsonString);
                    handler.SendMessage(JsonConvert.SerializeObject(info));
                }
                Thread.Sleep(2000);
            }
        }
    }
}
