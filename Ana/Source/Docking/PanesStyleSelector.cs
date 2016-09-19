namespace Ana.Source.Docking
{
    using ProcessSelector;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Provides the style required to view a pane
    /// </summary>
    internal class PanesStyleSelector : StyleSelector
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PanesStyleSelector" /> class
        /// </summary>
        public PanesStyleSelector()
        {
        }

        /// <summary>
        /// Gets or sets the style for generic tools
        /// </summary>
        public Style ToolStyle { get; set; }

        /// <summary>
        /// Gets or sets the style for the Process Selector
        /// </summary>
        public Style ProcessSelectorStyle { get; set; }

        /// <summary>
        /// Returns the required style to display the given view model
        /// </summary>
        /// <param name="item">The view model</param>
        /// <param name="container">The dependency object</param>
        /// <returns>The style associated with the provided view model</returns>
        public override Style SelectStyle(Object item, DependencyObject container)
        {
            if (item is ProcessSelectorViewModel)
            {
                return this.ProcessSelectorStyle;
            }

            if (item is ToolViewModel)
            {
                return this.ToolStyle;
            }

            return base.SelectStyle(item, container);
        }
    }
    //// End class
}
//// End namespace