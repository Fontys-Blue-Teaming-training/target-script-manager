using System;
using System.Threading;
using System.Threading.Tasks;
using websocket_client;

namespace TargetScriptManager
{
    public class TargetScriptMessageHandler<T> : MessageHandler<T> where T : Message
    {
        public override void HandleMessage(string message)
        {
            Console.WriteLine(message);
        }
        
        
        public void ImIvoIWantToSendMessage(string message)
        {
            SendMessage(message);
        }
    }
}
