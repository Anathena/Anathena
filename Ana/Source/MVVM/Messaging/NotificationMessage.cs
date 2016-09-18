namespace Ana.Source.Mvvm.Messaging
{
    using System;

    /// <summary>
    /// Passes a string message (Notification) to a recipient.
    /// <para>Typically, notifications are defined as unique strings in a static class. To define
    /// a unique string, you can use Guid.NewGuid().ToString() or any other unique
    /// identifier.</para>
    /// </summary>
    public class NotificationMessage : MessageBase
    {
        /// <summary>
        /// Initializes a new instance of the NotificationMessage class.
        /// </summary>
        /// <param name="notification">A string containing any arbitrary message to be
        /// passed to recipient(s)</param>
        public NotificationMessage(String notification)
        {
            this.Notification = notification;
        }

        /// <summary>
        /// Initializes a new instance of the NotificationMessage class.
        /// </summary>
        /// <param name="sender">The message's sender.</param>
        /// <param name="notification">A string containing any arbitrary message to be
        /// passed to recipient(s)</param>
        public NotificationMessage(Object sender, String notification) : base(sender)
        {
            this.Notification = notification;
        }

        /// <summary>
        /// Initializes a new instance of the NotificationMessage class.
        /// </summary>
        /// <param name="sender">The message's sender.</param>
        /// <param name="target">The message's intended target. This parameter can be used
        /// to give an indication as to whom the message was intended for. Of course
        /// this is only an indication, amd may be null.</param>
        /// <param name="notification">A string containing any arbitrary message to be
        /// passed to recipient(s)</param>
        public NotificationMessage(Object sender, Object target, String notification) : base(sender, target)
        {
            this.Notification = notification;
        }

        /// <summary>
        /// Gets a string containing any arbitrary message to be
        /// passed to recipient(s).
        /// </summary>
        public String Notification { get; private set; }
    }
    //// End class
}
//// End namespace