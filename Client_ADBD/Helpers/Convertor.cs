using Client_ADBD.ViewModels;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;


namespace Client_ADBD.Helpers
{
    //public class BooleanToVisibilityConverter : IValueConverter
    //{
    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        // Dacă valoarea este true, returnează Visibility.Collapsed, altfel returnează Visibility.Visible
    //        return (value is bool b && b) ? Visibility.Collapsed : Visibility.Visible;
    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
    //public class BoolToVisibilityConverter : IValueConverter
    //{
    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        return (value is bool && (bool)value) ? Visibility.Visible : Visibility.Collapsed;
    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        return value is Visibility && (Visibility)value == Visibility.Visible;
    //    }
    //}

    //public class BoolToDescriptionConverter : IValueConverter
    //{
    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        // Returnează descrierea completă sau scurtă în funcție de starea IsDescriptionExpanded
    //        return (bool)value ? ((VM_AuctionPage)parameter).FullDescription
    //                            : ((VM_AuctionPage)parameter).;
    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

}