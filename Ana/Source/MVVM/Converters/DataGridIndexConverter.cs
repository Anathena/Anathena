namespace Ana.Source.Mvvm.Converters
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Controls;
    using System.Windows.Data;
    using Utils.Extensions;

    /// <summary>
    /// Converts a <see cref="DataGridCell"/> to its row index in the parent data grid.
    /// </summary>
    internal class DataGridIndexConverter : IValueConverter
    {
        /// <summary>
        /// Converts an <see cref="DataGridCell"/> to its row index.
        /// </summary>
        /// <param name="value">Value to be converted.</param>
        /// <param name="targetType">Type to convert to.</param>
        /// <param name="parameter">Optional conversion parameter.</param>
        /// <param name="culture">Globalization info.</param>
        /// <returns>The index of the cell. If none found, will return -1.</returns>
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            DataGridCell item = value as DataGridCell;
            DataGridRow dataGridRow = item?.Ancestors().OfType<DataGridRow>().FirstOrDefault();
            DataGrid dataGrid = dataGridRow?.Ancestors().OfType<DataGrid>().FirstOrDefault();
            Int32 index = dataGrid?.ItemContainerGenerator.IndexFromContainer(dataGridRow) ?? -1;

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