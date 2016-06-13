using System;

namespace DirectXShell.Interface
{
    [Serializable]
    public class MessageReceivedEventArgs : MarshalByRefObject
    {
        public MessageType MessageType { get; set; }
        public String Message { get; set; }

        public MessageReceivedEventArgs(MessageType MessageType, String Message)
        {
            this.MessageType = MessageType;
            this.Message = Message;
        }

        public override String ToString()
        {
            return String.Format("{0}: {1}", MessageType, Message);
        }

    } // End class

} // End namespace