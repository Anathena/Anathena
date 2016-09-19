namespace Ana.Source.Docking
{
    using ProcessSelector;
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using Xceed.Wpf.AvalonDock.Layout;

    internal class PanesTemplateSelector : DataTemplateSelector
    {
        public PanesTemplateSelector()
        {
        }

        public DataTemplate ProcessSelectorViewTemplate { get; set; }

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