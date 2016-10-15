namespace Ana.Source.Results
{
    using System;

    /// <summary>
    /// A scan result object that can be displayed to the user and added to the project explorer
    /// </summary>
    internal class ScanResult
    {
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
            this.Value = value;
            this.Label = label;
        }

        /// <summary>
        /// Gets or sets the address of the scan result
        /// </summary>
        public IntPtr Address
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the previous value of the scan result
        /// </summary>
        public String PreviousValue
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the value of the scan result
        /// </summary>
        public String Value
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the label of the scan result
        /// </summary>
        public String Label
        {
            get;
            set;
        }
    }
    //// End class
}
//// End namespace