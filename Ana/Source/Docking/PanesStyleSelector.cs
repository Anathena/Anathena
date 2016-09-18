namespace Ana.Source.Docking
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    internal class PanesStyleSelector : StyleSelector
    {
        public Style ToolStyle { get; set; }

        public Style FileStyle { get; set; }

        public override Style SelectStyle(Object item, DependencyObject container)
        {
            if (item is ToolViewModel)
            {
                return this.ToolStyle;
            }

            if (item is FileViewModel)
            {
                return this.FileStyle;
            }

            return base.SelectStyle(item, container);
        }
    }
    //// End class
}
//// End namespace