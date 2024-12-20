
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.IO;
using System.Windows.Media.Imaging;
using System.Net.NetworkInformation;
using System.ComponentModel;

namespace Client_ADBD.Helpers
{

    public class ImageSourceConverter : IValueConverter
    {
        public static BitmapImage GetImageSource(string path)
        {
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                try
                {
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(path, UriKind.Absolute);
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    return bitmap;
                }
                catch
                {
                    // Log or handle errors
                }
            }

            return new BitmapImage(new Uri("C:\\Users\\laris\\Desktop\\Client_ADBD\\Client_ADBD\\Views\\UserControl\\no-image_image.jpg"));
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string path)
            {
                return GetImageSource(path);  // Utilizează metoda GetImageSource
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

      
    }


    public class Utilities
    {
        public static string Username;

        public static string _status = "default";

        public static string Status
        {
            get => _status;
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnStatusChanged?.Invoke(null, EventArgs.Empty);

                }
            }
        }


        public static event EventHandler OnStatusChanged;
    }

}
