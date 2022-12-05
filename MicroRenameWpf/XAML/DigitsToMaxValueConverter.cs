using System;
using System.Globalization;
using System.Windows.Data;

namespace MicroRenameWpf.XAML
{
    internal class DigitsToMaxValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => Math.Pow(10, System.Convert.ToDouble(value)) - 1;
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}