using System;
using websocket_client;

namespace TargetScriptManager
{
    class Program
    {
        static void Main(string[] args)
        {
            SocketClient socketClient = new SocketClient(new TargetScriptMessageHandler<Message>());

            Console.WriteLine("Hello World!");
        }
    }
}
