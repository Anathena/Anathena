namespace Ana.View
{
    using Controls;
    using System.Windows.Controls;
    using System.Windows.Forms.Integration;

    /// <summary>
    /// Interaction logic for ManualScanner.xaml
    /// </summary>
    public partial class ManualScanner : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManualScanner" /> class
        /// </summary>
        public ManualScanner()
        {
            this.InitializeComponent();

            // [WINDOWS FORMS]
            WindowsFormsHost host = new WindowsFormsHost();
            HexDecTextBox hexDecBox = new HexDecTextBox();
            host.Child = hexDecBox;
            this.valueHexDecBox.Children.Add(host);
        }
    }
    //// End class
}
//// End namespace