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
        private IntPtr address;

        /// <summary>
        /// The value of the scan result
        /// </summary>
        private String elementValue;

        /// <summary>
        /// The previous value of the scan result
        /// </summary>
        private String previousValue;

        /// <summary>
        /// The label of the scan result
        /// </summary>
        private String label;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScanResult" /> class
        /// </summary>
        /// <param name="address">The memory address</param>
        /// <param name="value">The current scan value</param>
        /// <param name="previousValue">The previous scan value</param>
        /// <param name="label">The label of this result</param>
        public ScanResult(IntPtr address, String value, String previousValue, String label)
        {
            this.Address = address;
            this.PreviousValue = previousValue;
            this.ElementValue = value;
            this.Label = label;
        }

        /// <summary>
        /// Event notifying that a property of this object has changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the address of the scan result
        /// </summary>
        public IntPtr Address
        {
            get
            {
                return this.address;
            }

            set
            {
                this.address = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Address)));
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
        public String PreviousValue
        {
            get
            {
                return this.previousValue;
            }

            set
            {
                this.previousValue = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.PreviousValue)));
            }
        }

        /// <summary>
        /// Gets or sets the label of the scan result
        /// </summary>
        public String Label
        {
            get
            {
                return this.label;
            }

            set
            {
                this.label = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Label)));
            }
        }
    }
    //// End class
}
//// End namespace