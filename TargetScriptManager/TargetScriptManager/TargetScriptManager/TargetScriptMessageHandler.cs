using System;
using websocket_client;

namespace TargetScriptManager
{
    public class TargetScriptMessageHandler<T> : MessageHandler<T>
    {
        public override void HandleMessage(string message)
        {
            var obj = ReceiveAsObj(message);
            throw new NotImplementedException();
        }
    }
}
