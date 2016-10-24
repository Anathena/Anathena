namespace Ana.View
{
    using Controls;
    using Source.Results;
    using System;
    using System.Threading.Tasks;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for ManualScanner.xaml
    /// </summary>
    internal partial class ManualScanner : UserControl, IResultsObserver
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManualScanner"/> class
        /// </summary>
        public ManualScanner()
        {
            this.InitializeComponent();

            // Windows Forms hosting -- TODO: Phase this out
            this.ValueHexDecBox = new HexDecTextBox();
            this.ValueHexDecBox.TextChanged += this.ValueUpdated;
            this.valueHexDecBox.Children.Add(WinformsHostingHelper.CreateHostedControl(this.ValueHexDecBox));

            Task.Run(() => ScanResultsViewModel.GetInstance().Subscribe(this));
        }

        private HexDecTextBox ValueHexDecBox { get; set; }

        public void Update(Type activeType)
        {
            this.ValueHexDecBox?.SetElementType(activeType);
        }

        private void ValueUpdated(Object sender, EventArgs e)
        {
            Source.Scanners.ManualScanner.ManualScannerViewModel.GetInstance().UpdateActiveValueCommand.Execute(this.ValueHexDecBox.GetValue());
        }
    }
    //// End class
}
//// End namespace