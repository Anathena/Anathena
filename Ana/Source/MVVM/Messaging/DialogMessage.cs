namespace Ana.Source.Mvvm.Messaging
{
    using System;
    using System.Windows;

    /// <summary>
    /// Use this class to send a message requesting to display a message box with features corresponding to this message's properties. The Callback property should be used
    /// to notify the message's sender about the user's choice in the message box. Typically, you can use this message typ's recipient will be an element of the View,
    /// and the sender will possibly be a ViewModel.
    /// </summary>
    [Obsolete("This class is not recommended because it uses MessageBoxResult which is not good in VMs. See http://www.galasoft.ch/s/dialogmessage.")]
    internal class DialogMessage : GenericMessage<string>
    {
        /// <summary>
        /// Initializes a new instance of the DialogMessage class.
        /// </summary>
        /// <param name="content">The text displayed by the message box.</param>
        /// <param name="callback">A callback method that should be executed to deliver the result of the message box to the object that sent the message.</param>
        public DialogMessage(string content, Action<MessageBoxResult> callback) : base(content)
        {
            this.Callback = callback;
        }

        /// <summary>
        /// Initializes a new instance of the DialogMessage class.
        /// </summary>
        /// <param name="sender">The message's original sender.</param>
        /// <param name="content">The text displayed by the message box.</param>
        /// <param name="callback">A callback method that should be executed to deliver the result
        /// of the message box to the object that sent the message.</param>
        public DialogMessage(Object sender, String content, Action<MessageBoxResult> callback) : base(sender, content)
        {
            this.Callback = callback;
        }

        /// <summary>
        /// Initializes a new instance of the DialogMessage class.
        /// </summary>
        /// <param name="sender">The message's original sender.</param>
        /// <param name="target">
        /// The message's intended target. This parameter can be used to give an indication as to whom the message was intended for. Of course this is only an indication, amd may be null.
        /// </param>
        /// <param name="content">The text displayed by the message box.</param>
        /// <param name="callback">A callback method that should be executed to deliver the result of the message box to the object that sent the message.</param>
        public DialogMessage(Object sender, Object target, String content, Action<MessageBoxResult> callback) : base(sender, target, content)
        {
            this.Callback = callback;
        }

        /// <summary>
        /// Gets or sets the buttons displayed by the message box.
        /// </summary>
        public MessageBoxButton Button { get; set; }

        /// <summary>
        /// Gets a callback method that should be executed to deliver the result of the message box to the object that sent the message.
        /// </summary>
        public Action<MessageBoxResult> Callback { get; private set; }

        /// <summary>
        /// Gets or sets the caption for the message box.
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// Gets or sets which result is the default in the message box.
        /// </summary>
        public MessageBoxResult DefaultResult { get; set; }

        /// <summary>
        /// Gets or sets the icon for the message box.
        /// </summary>
        public MessageBoxImage Icon { get; set; }

        /// <summary>
        /// Gets or sets the options for the message box.
        /// </summary>
        public MessageBoxOptions Options { get; set; }

        /// <summary>
        /// Utility method, checks if the <see cref="Callback" /> property is null, and if it is not null, executes it.
        /// </summary>
        /// <param name="result">The result that must be passed to the dialog message caller.</param>
        public void ProcessCallback(MessageBoxResult result)
        {
            this.Callback?.Invoke(result);
        }
    }
    //// End class
}
//// End namespace