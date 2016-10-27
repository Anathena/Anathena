namespace Ana.Source.Mvvm.Messaging
{
    using System;

    /// <summary>
    /// Provides a message class with a built-in callback. When the recipient
    /// is done processing the message, it can execute the callback to
    /// notify the sender that it is done. Use the <see cref="Execute" />
    /// method to execute the callback. The callback method has one parameter.
    /// <seealso cref="NotificationMessageAction"/> and
    /// <seealso cref="NotificationMessageAction&lt;TCallbackParameter&gt;"/>.
    /// </summary>
    internal class NotificationMessageWithCallback : NotificationMessage
    {
        /// <summary>
        /// TODO TODO
        /// </summary>
        private readonly Delegate callback;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationMessageWithCallback" /> class.
        /// </summary>
        /// <param name="notification">An arbitrary string that will be
        /// carried by the message.</param>
        /// <param name="callback">The callback method that can be executed
        /// by the recipient to notify the sender that the message has been
        /// processed.</param>
        public NotificationMessageWithCallback(string notification, Delegate callback) : base(notification)
        {
            NotificationMessageWithCallback.CheckCallback(callback);
            this.callback = callback;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationMessageWithCallback" /> class.
        /// </summary>
        /// <param name="sender">The message's sender.</param>
        /// <param name="notification">An arbitrary string that will be
        /// carried by the message.</param>
        /// <param name="callback">The callback method that can be executed
        /// by the recipient to notify the sender that the message has been
        /// processed.</param>
        public NotificationMessageWithCallback(Object sender, String notification, Delegate callback) : base(sender, notification)
        {
            NotificationMessageWithCallback.CheckCallback(callback);
            this.callback = callback;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationMessageWithCallback" /> class.
        /// </summary>
        /// <param name="sender">The message's sender.</param>
        /// <param name="target">The message's intended target. This parameter can be used
        /// to give an indication as to whom the message was intended for. Of course
        /// this is only an indication, amd may be null.</param>
        /// <param name="notification">An arbitrary string that will be
        /// carried by the message.</param>
        /// <param name="callback">The callback method that can be executed
        /// by the recipient to notify the sender that the message has been
        /// processed.</param>
        public NotificationMessageWithCallback(Object sender, Object target, String notification, Delegate callback) : base(sender, target, notification)
        {
            NotificationMessageWithCallback.CheckCallback(callback);
            this.callback = callback;
        }

        /// <summary>
        /// Executes the callback that was provided with the message with an
        /// arbitrary number of parameters.
        /// </summary>
        /// <param name="arguments">A  number of parameters that will
        /// be passed to the callback method.</param>
        /// <returns>The object returned by the callback method.</returns>
        public virtual Object Execute(params Object[] arguments)
        {
            return this.callback.DynamicInvoke(arguments);
        }

        /// <summary>
        /// Ensures the provided callback is not null
        /// </summary>
        /// <param name="callback">Provided callback delegate function</param>
        private static void CheckCallback(Delegate callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException("callback", "Callback may not be null");
            }
        }
    }
    //// End class
}
//// End namespace