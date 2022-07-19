using System;
using System.Globalization;
using System.Windows.Data;

namespace IconViewer.View.Resources.Converter
{
    internal class InvalidInputConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return 0;
            }

            bool isValid = (bool)value;

            return !isValid ? 0 : (object)1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
