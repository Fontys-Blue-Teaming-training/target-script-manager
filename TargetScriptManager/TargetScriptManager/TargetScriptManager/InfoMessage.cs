using System;
using System.Collections.Generic;
using System.Text;

namespace TargetScriptManager
{
    public class InfoMessage
    {
        public InfoMessageType Type { get; set; }
        public string Message { get; set; }

        public InfoMessage(InfoMessageType type, string message)
        {
            Type = type;
            Message = message;
        }
    }
}
