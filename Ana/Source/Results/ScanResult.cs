namespace Ana.Source.Results
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// A scan result object that can be displayed to the user and added to the project explorer
    /// </summary>
    internal class ScanResult : INotifyPropertyChanged
    {
        /// <summary>
        /// The address of the scan result
        /// </summary>
        private IntPtr elementAddress;

        /// <summary>
        /// The value of the scan result
        /// </summary>
        private String elementValue;

        /// <summary>
        /// The previous value of the scan result
        /// </summary>
        private String previousElementValue;

        /// <summary>
        /// The label of the scan result
        /// </summary>
        private String elementLabel;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScanResult" /> class
        /// </summary>
        /// <param name="elementAddress">The memory address</param>
        /// <param name="elementValue">The current scan value</param>
        /// <param name="elementPreviousValue">The previous scan value</param>
        /// <param name="elementLabel">The label of this result</param>
        public ScanResult(IntPtr elementAddress, String elementValue, String elementPreviousValue, String elementLabel)
        {
            this.ElementAddress = elementAddress;
            this.ElementPreviousValue = elementPreviousValue;
            this.ElementValue = elementValue;
            this.ElementLabel = elementLabel;
        }

        /// <summary>
        /// Event notifying that a property of this object has changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the address of the scan result
        /// </summary>
        public IntPtr ElementAddress
        {
            get
            {
                return this.elementAddress;
            }

            set
            {
                this.elementAddress = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.ElementAddress)));
            }
        }

        /// <summary>
        /// Gets or sets the value of the scan result
        /// </summary>
        public String ElementValue
        {
            get
            {
                return this.elementValue;
            }

            set
            {
                this.elementValue = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.ElementValue)));
            }
        }

        /// <summary>
        /// Gets or sets the previous value of the scan result
        /// </summary>
        public String ElementPreviousValue
        {
            get
            {
                return this.previousElementValue;
            }

            set
            {
                this.previousElementValue = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.ElementPreviousValue)));
            }
        }

        /// <summary>
        /// Gets or sets the label of the scan result
        /// </summary>
        public String ElementLabel
        {
            get
            {
                return this.elementLabel;
            }

            set
            {
                this.elementLabel = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.ElementLabel)));
            }
        }
    }
    //// End class
}
//// End namespace