namespace Ana.Source.Mvvm.Messaging
{
    using System;

    /// <summary>
    /// Base class for all messages broadcasted by the Messenger. You can create your own message types by extending this class.
    /// </summary>
    internal class MessageBase
    {
        /// <summary>
        /// Initializes a new instance of the MessageBase class.
        /// </summary>
        public MessageBase()
        {
        }

        /// <summary>
        /// Initializes a new instance of the MessageBase class.
        /// </summary>
        /// <param name="sender">The message's original sender.</param>
        public MessageBase(Object sender)
        {
            this.Sender = sender;
        }

        /// <summary>
        /// Initializes a new instance of the MessageBase class.
        /// </summary>
        /// <param name="sender">The message's original sender.</param>
        /// <param name="target">
        /// The message's intended target. This parameter can be used to give an indication as to whom the message was intended for. Of course this is only an indication, amd may be null.
        /// </param>
        public MessageBase(Object sender, Object target) : this(sender)
        {
            this.Target = target;
        }

        /// <summary>
        /// Gets or sets the message's sender.
        /// </summary>
        public Object Sender { get; protected set; }

        /// <summary>
        /// Gets or sets the message's intended target. This property can be used to give an indication as to whom the message was intended for.
        /// Of course this is only an indication, amd may be null.
        /// </summary>
        public Object Target { get; protected set; }
    }
    //// End class
}
//// End namespace