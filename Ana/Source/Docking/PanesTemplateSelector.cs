namespace Ana.Source.Docking
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using Xceed.Wpf.AvalonDock.Layout;

    internal class PanesTemplateSelector : DataTemplateSelector
    {
        public PanesTemplateSelector()
        {
        }

        public DataTemplate FileViewTemplate { get; set; }

        public DataTemplate FileStatsViewTemplate { get; set; }

        public override DataTemplate SelectTemplate(Object item, DependencyObject container)
        {
            LayoutContent itemAsLayoutContent = item as LayoutContent;

            if (item is FileViewModel)
            {
                return this.FileViewTemplate;
            }

            if (item is FileStatsViewModel)
            {
                return this.FileStatsViewTemplate;
            }

            return base.SelectTemplate(item, container);
        }
    }
    //// End class
}
//// End namespace