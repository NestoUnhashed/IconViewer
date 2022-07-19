using System;
using System.Globalization;
using System.Windows.Data;

namespace IconViewer.View.Resources.Converter
{
    internal class DictionaryToValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? false : (object)false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
