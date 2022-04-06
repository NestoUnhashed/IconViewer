using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace IconViewer.View.Resources.Converter
{
    internal class StringToGeometryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value == null)
                return Geometry.Empty;

            return Geometry.Parse(value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
