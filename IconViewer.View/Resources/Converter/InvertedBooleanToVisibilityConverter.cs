using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace IconViewer.View.Resources.Converter
{
    internal class InvertedBooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            return (bool)value ? Visibility.Hidden : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
