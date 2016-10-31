namespace Ana.View.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for RangeSlider.xaml
    /// </summary>
    internal partial class RangeSlider : UserControl
    {
        public RangeSlider()
        {
            InitializeComponent();

            this.Loaded += Slider_Loaded;
        }

        public void Slider_Loaded(object sender, RoutedEventArgs e)
        {
            LowerSlider.ValueChanged += LowerSlider_ValueChanged;
            UpperSlider.ValueChanged += UpperSlider_ValueChanged;
        }

        public Double Minimum
        {
            get
            {
                return (Double)GetValue(MinimumProperty);
            }

            set
            {
                SetValue(MinimumProperty, value);
            }
        }

        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(Double), typeof(RangeSlider), new UIPropertyMetadata(0d));

        public Double LowerValue
        {
            get
            {
                return (Double)GetValue(LowerValueProperty);
            }

            set
            {
                SetValue(LowerValueProperty, value);
            }
        }

        public static readonly DependencyProperty LowerValueProperty =
            DependencyProperty.Register("LowerValue", typeof(Double), typeof(RangeSlider), new UIPropertyMetadata(0d));

        public double UpperValue
        {
            get
            {
                return (Double)GetValue(UpperValueProperty);
            }

            set
            {
                SetValue(UpperValueProperty, value);
            }
        }

        public static readonly DependencyProperty UpperValueProperty =
            DependencyProperty.Register("UpperValue", typeof(Double), typeof(RangeSlider), new UIPropertyMetadata(0d));

        public Double Maximum
        {
            get
            {
                return (Double)GetValue(MaximumProperty);
            }

            set
            {
                SetValue(MaximumProperty, value);
            }
        }

        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(Double), typeof(RangeSlider), new UIPropertyMetadata(1d));

        private void LowerSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<Double> e)
        {
            UpperSlider.Value = Math.Max(UpperSlider.Value, LowerSlider.Value);
        }

        private void UpperSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<Double> e)
        {
            LowerSlider.Value = Math.Min(UpperSlider.Value, LowerSlider.Value);
        }
    }
    //// End class
}
//// End namespace