using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;


namespace Client_ADBD.Helpers
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Dacă valoarea este true, returnează Visibility.Collapsed, altfel returnează Visibility.Visible
            return (value is bool b && b) ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}