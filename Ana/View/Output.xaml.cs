namespace Ana.View
{
    using System;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for Output.xaml.
    /// </summary>
    internal partial class Output : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Output" /> class.
        /// </summary>
        public Output()
        {
            this.InitializeComponent();

            this.OutputScrollViewer.ScrollToBottom();
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not to automatically scroll to bottom.
        /// </summary>
        private Boolean AutoScroll { get; set; }

        /// <summary>
        /// Event that is triggered when the scroll viewer size changes. Will automatically scroll to bottom if already at the bottom.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event args.</param>
        private void OutputScrollViewerScrollChanged(Object sender, ScrollChangedEventArgs e)
        {
            // User scroll event: set or unset autoscroll mode
            if (e.ExtentHeightChange == 0)
            {
                // Content unchanged: user scroll event
                if (this.OutputScrollViewer.VerticalOffset == this.OutputScrollViewer.ScrollableHeight)
                {
                    // Scroll bar is in bottom Set autoscroll mode
                    this.AutoScroll = true;
                }
                else
                {
                    // Scroll bar is not in bottomUnset autoscroll mode
                    this.AutoScroll = false;
                }
            }

            // Content scroll event: autoscroll eventually
            if (this.AutoScroll && e.ExtentHeightChange != 0)
            {
                // Content changed and autoscroll mode set Autoscroll
                this.OutputScrollViewer.ScrollToVerticalOffset(this.OutputScrollViewer.ExtentHeight);
            }
        }
    }
    //// End class
}
//// End namespace