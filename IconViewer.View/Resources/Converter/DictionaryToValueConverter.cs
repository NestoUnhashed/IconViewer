using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace IconViewer.View.Resources.Converter
{
    internal class DictionaryToValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return false;

            KeyValuePair<string, bool> item = (KeyValuePair<string, bool>)value;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
