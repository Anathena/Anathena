namespace Ana.Source.Mvvm.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Controls;
    using System.Windows.Data;

    /// <summary>
    /// Converts a <see cref="ListViewItem"/> to its row index in the parent data grid.
    /// </summary>
    internal class ListViewIndexConverter : IValueConverter
    {
        /// <summary>
        /// Converts an <see cref="ListViewItem"/> to its row index.
        /// </summary>
        /// <param name="value">Value to be converted.</param>
        /// <param name="targetType">Type to convert to.</param>
        /// <param name="parameter">Optional conversion parameter.</param>
        /// <param name="culture">Globalization info.</param>
        /// <returns>The index of the item. If none found, will return -1.</returns>
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            ListViewItem item = value as ListViewItem;
            ItemsControl listView = ItemsControl.ItemsControlFromItemContainer(item) as ListView;
            Int32 index = listView.ItemContainerGenerator.IndexFromContainer(item);

            return index.ToString();
        }

        /// <summary>
        /// Not used or implemented.
        /// </summary>
        /// <param name="value">Value to be converted.</param>
        /// <param name="targetType">Type to convert to.</param>
        /// <param name="parameter">Optional conversion parameter.</param>
        /// <param name="culture">Globalization info.</param>
        /// <returns>Throws see <see cref="NotImplementedException" /></returns>
        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    //// End class
}
//// End namespace