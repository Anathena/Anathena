using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Capture.Interface
{
    [Serializable]   
    public class MessageReceivedEventArgs: MarshalByRefObject
    {
        public MessageType MessageType { get; set; }
        public string Message { get; set; }

        public MessageReceivedEventArgs(MessageType messageType, string message)
        {
            MessageType = messageType;
            Message = message;
        }

        public override string ToString()
        {
            return String.Format("{0}: {1}", MessageType, Message);
        }
    }
}