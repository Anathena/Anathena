namespace Ana.Source.Docking
{
    using System.Windows;
    using System.Windows.Controls;
    using Xceed.Wpf.AvalonDock.Layout;

    internal class PanesTemplateSelector : DataTemplateSelector
    {
        public PanesTemplateSelector()
        {

        }


        public DataTemplate FileViewTemplate
        {
            get;
            set;
        }

        public DataTemplate FileStatsViewTemplate
        {
            get;
            set;
        }

        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            var itemAsLayoutContent = item as LayoutContent;

            if (item is FileViewModel)
                return FileViewTemplate;

            if (item is FileStatsViewModel)
                return FileStatsViewTemplate;

            return base.SelectTemplate(item, container);
        }
    }
}
