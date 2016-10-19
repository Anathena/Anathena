namespace Ana.View.Controls
{
    using Source.Controls;
    using System;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for HexDecBox.xaml
    /// </summary>
    public partial class HexDecBox : TextBox
    {
        public HexDecBox()
        {
            InitializeComponent();
        }

        public UInt64 Value
        {
            get
            {
                HexDecBoxViewModel hexDecBoxViewModel = this.DataContext as HexDecBoxViewModel;

                return hexDecBoxViewModel == null ? 0 : hexDecBoxViewModel.Value;
            }
        }
    }
    //// End class
}
//// End namespace