namespace Ana.Source.Mvvm.Messaging
{
    /// <summary>
    /// Passes a string property name (PropertyName) and a generic value
    /// (<see cref="OldValue" /> and <see cref="NewValue" />) to a recipient.
    /// This message type can be used to propagate a PropertyChanged event to
    /// a recipient using the messenging system.
    /// </summary>
    /// <typeparam name="T">The type of the OldValue and NewValue property.</typeparam>
    ////[ClassInfo(typeof(Messenger))]
    public class PropertyChangedMessage<T> : PropertyChangedMessageBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyChangedMessage{T}" /> class.
        /// </summary>
        /// <param name="sender">The message's sender.</param>
        /// <param name="oldValue">The property's value before the change occurred.</param>
        /// <param name="newValue">The property's value after the change occurred.</param>
        /// <param name="propertyName">The name of the property that changed.</param>
        public PropertyChangedMessage(object sender, T oldValue, T newValue, string propertyName)
            : base(sender, propertyName)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyChangedMessage{T}" /> class.
        /// </summary>
        /// <param name="oldValue">The property's value before the change occurred.</param>
        /// <param name="newValue">The property's value after the change occurred.</param>
        /// <param name="propertyName">The name of the property that changed.</param>
        public PropertyChangedMessage(T oldValue, T newValue, string propertyName)
            : base(propertyName)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyChangedMessage{T}" /> class.
        /// </summary>
        /// <param name="sender">The message's sender.</param>
        /// <param name="target">The message's intended target. This parameter can be used
        /// to give an indication as to whom the message was intended for. Of course
        /// this is only an indication, amd may be null.</param>
        /// <param name="oldValue">The property's value before the change occurred.</param>
        /// <param name="newValue">The property's value after the change occurred.</param>
        /// <param name="propertyName">The name of the property that changed.</param>
        public PropertyChangedMessage(object sender, object target, T oldValue, T newValue, string propertyName)
            : base(sender, target, propertyName)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }

        /// <summary>
        /// Gets the value that the property has after the change.
        /// </summary>
        public T NewValue
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value that the property had before the change.
        /// </summary>
        public T OldValue
        {
            get;
            private set;
        }
    }
    //// End class
}
//// End namespace