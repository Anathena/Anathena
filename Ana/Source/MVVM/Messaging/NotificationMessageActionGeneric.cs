namespace Ana.Source.Mvvm.Messaging
{
    using System;

    /// <summary>
    /// Provides a message class with a built-in callback. When the recipient
    /// is done processing the message, it can execute the callback to
    /// notify the sender that it is done. Use the <see cref="Execute" />
    /// method to execute the callback. The callback method has one parameter.
    /// <seealso cref="NotificationMessageAction"/>.
    /// </summary>
    /// <typeparam name="TCallbackParameter">The type of the callback method's
    /// only parameter.</typeparam>
    public class NotificationMessageAction<TCallbackParameter> : NotificationMessageWithCallback
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="NotificationMessageAction&lt;TCallbackParameter&gt;" /> class.
        /// </summary>
        /// <param name="notification">An arbitrary string that will be
        /// carried by the message.</param>
        /// <param name="callback">The callback method that can be executed
        /// by the recipient to notify the sender that the message has been
        /// processed.</param>
        public NotificationMessageAction(String notification, Action<TCallbackParameter> callback) : base(notification, callback)
        {
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="NotificationMessageAction&lt;TCallbackParameter&gt;" /> class.
        /// </summary>
        /// <param name="sender">The message's sender.</param>
        /// <param name="notification">An arbitrary string that will be
        /// carried by the message.</param>
        /// <param name="callback">The callback method that can be executed
        /// by the recipient to notify the sender that the message has been
        /// processed.</param>
        public NotificationMessageAction(Object sender, String notification, Action<TCallbackParameter> callback) : base(sender, notification, callback)
        {
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="NotificationMessageAction&lt;TCallbackParameter&gt;" /> class.
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
        public NotificationMessageAction(Object sender, Object target, String notification, Action<TCallbackParameter> callback) : base(sender, target, notification, callback)
        {
        }

        /// <summary>
        /// Executes the callback that was provided with the message.
        /// </summary>
        /// <param name="parameter">A parameter requested by the message's
        /// sender and providing additional information on the recipient's
        /// state.</param>
        public void Execute(TCallbackParameter parameter)
        {
            base.Execute(parameter);
        }
    }
    //// End class
}
//// End namespace