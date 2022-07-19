using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace IconViewer.View.Resources.Converter
{
    internal class DynamicPolygonConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 0)
            {
                return null;
            }

            double width = Math.Round((double)values[0] / 1.5, 0);
            double height = Math.Round((double)values[1] / 2, 0);

            return new PointCollection()
            {
                new Point(0, 0),
                new Point(width, 0),
                new Point(width, height),
            };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
