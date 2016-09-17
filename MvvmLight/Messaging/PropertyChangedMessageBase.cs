namespace GalaSoft.MvvmLight.Messaging
{
    /// <summary>
    /// Basis class for the <see cref="PropertyChangedMessage{T}" /> class. This
    /// class allows a recipient to register for all PropertyChangedMessages without
    /// having to specify the type T.
    /// </summary>
    ////[ClassInfo(typeof(Messenger))]
    public abstract class PropertyChangedMessageBase : MessageBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyChangedMessageBase" /> class.
        /// </summary>
        /// <param name="sender">The message's sender.</param>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected PropertyChangedMessageBase(object sender, string propertyName)
            : base(sender)
        {
            PropertyName = propertyName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyChangedMessageBase" /> class.
        /// </summary>
        /// <param name="sender">The message's sender.</param>
        /// <param name="target">The message's intended target. This parameter can be used
        /// to give an indication as to whom the message was intended for. Of course
        /// this is only an indication, amd may be null.</param>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected PropertyChangedMessageBase(object sender, object target, string propertyName)
            : base(sender, target)
        {
            PropertyName = propertyName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyChangedMessageBase" /> class.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected PropertyChangedMessageBase(string propertyName)
        {
            PropertyName = propertyName;
        }

        /// <summary>
        /// Gets or sets the name of the property that changed.
        /// </summary>
        public string PropertyName
        {
            get;
            protected set;
        }
    }
    //// End class
}
//// End namespace