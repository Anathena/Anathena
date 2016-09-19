namespace Ana.Source.Docking
{
    using ProcessSelector;
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using Xceed.Wpf.AvalonDock.Layout;

    /// <summary>
    /// Provides the template required to view a pane
    /// </summary>
    internal class PanesTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PanesTemplateSelector" /> class
        /// </summary>
        public PanesTemplateSelector()
        {
        }

        /// <summary>
        /// Gets or sets the template for the Process Selector
        /// </summary>
        public DataTemplate ProcessSelectorViewTemplate { get; set; }

        /// <summary>
        /// Returns the required template to display the given view model
        /// </summary>
        /// <param name="item">The view model</param>
        /// <param name="container">The dependency object</param>
        /// <returns>The template associated with the provided view model</returns>
        public override DataTemplate SelectTemplate(Object item, DependencyObject container)
        {
            LayoutContent itemAsLayoutContent = item as LayoutContent;

            if (item is ProcessSelectorViewModel)
            {
                return this.ProcessSelectorViewTemplate;
            }

            return base.SelectTemplate(item, container);
        }
    }
    //// End class
}
//// End namespace