namespace Ana.Source.Results
{
    using System;
    using Utils.Validation;
    internal class ScanResult
    {
        public ScanResult(IntPtr address, String previousValue, String value, String label)
        {
            this.Address = Conversions.ToAddress(address);
            this.PreviousValue = previousValue;
            this.Value = value;
            this.Label = label;
        }

        public String Address
        {
            get;
            set;
        }

        public String PreviousValue
        {
            get;
            set;
        }

        public String Value
        {
            get;
            set;
        }

        public String Label
        {
            get;
            set;
        }
    }
    //// End class
}
//// End namespace