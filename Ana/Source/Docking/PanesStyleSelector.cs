namespace Ana.Source.Docking
{
    using ProcessSelector;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    internal class PanesStyleSelector : StyleSelector
    {
        public Style ToolStyle { get; set; }

        public Style ProcessSelectorStyle { get; set; }

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