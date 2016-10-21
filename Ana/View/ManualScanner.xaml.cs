namespace Ana.View
{
    using Controls;
    using Source.Results;
    using System;
    using System.Threading.Tasks;
    using System.Windows.Controls;
    using System.Windows.Forms.Integration;

    /// <summary>
    /// Interaction logic for ManualScanner.xaml
    /// </summary>
    internal partial class ManualScanner : UserControl, IResultsObserver
    {
        private HexDecTextBox hexDecBox;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManualScanner" /> class
        /// </summary>
        public ManualScanner()
        {
            this.InitializeComponent();

            // Windows Forms hosting -- TODO: Phase this out
            WindowsFormsHost host = new WindowsFormsHost();
            hexDecBox = new HexDecTextBox();
            hexDecBox.TextChanged += ValueUpdated;
            host.Child = hexDecBox;
            this.valueHexDecBox.Children.Add(host);

            Task.Run(() => Source.Results.ScanResultsViewModel.GetInstance().Subscribe(this));
        }

        public void Update(Type activeType)
        {
            hexDecBox?.SetElementType(activeType);
        }

        private void ValueUpdated(Object sender, EventArgs e)
        {
            Source.Scanners.ManualScanner.ManualScannerViewModel.GetInstance().UpdateActiveValueCommand.Execute(hexDecBox.GetValue());
        }
    }
    //// End class
}
//// End namespace