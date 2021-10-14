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
            Console.WriteLine("Client running.");
            var handler = new TargetScriptMessageHandler<Message>();
            SocketClient client = new SocketClient(handler, "192.168.1.2");
            client.InitClient();
            await client.StartClient();

            SystemHealthChecker shc = new SystemHealthChecker();
            TimeSpan tss;
            string s;
            bool pingCheck;

            while (true)
            {
                var CurrentCpuPercentageUsage = Math.Round(shc.GetCpuPercentageUsageRaw());
                tss = shc.GetSystemUpTimeMinutes();
                pingCheck = shc.PingAsync().Result;

                bool cpuCheck = false;
                bool ramCheck = false;

                if (CurrentCpuPercentageUsage > 15)
                {
                    cpuCheck = true;
                }

                if (shc.GetRamPercentageUsageRaw() <= 3000)
                {
                    ramCheck = true;
                }


                var systemInformation = new SystemInformation
                {
                    CurrentSystemUpTime = Math.Round(tss.TotalMinutes).ToString(),
                    CurrentCpuPercentageUsage = CurrentCpuPercentageUsage,
                    CurrentRamPercentageUsage = shc.GetRamPercentageUsage(),
                    CurrentInternetConnectivity = pingCheck
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
