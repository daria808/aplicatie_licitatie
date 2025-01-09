
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
using System.Windows.Media.Animation;
using System.Windows.Threading;

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

            return new BitmapImage(new Uri("C:\\Users\\laris\\Desktop\\New folder\\aplicatie_licitatie\\Client_ADBD\\photos\\no-image_image.jpg"));
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

    public class Timer
    {
        private static bool _isTimerRunning = false;

        private static DispatcherTimer _timer = null;

        private static void InitiateTimer()
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };

        }

        /// <summary>
        /// seconds=-1 =>setare minute  
        /// minutes=-1 =>setare secunde
        /// </summary>
        /// <param name="function"></param>
        /// <param name="seconds"> </param>
        /// <param name="minutes"></param>
        public static void AddEventToTimer(Action function,int seconds,int minutes)
        {
            if (_timer == null)
            {
                InitiateTimer();
            }

            if (seconds > 0)
            {
                _timer.Interval = TimeSpan.FromSeconds(seconds);
            }
            else if (minutes > 0)
            {
                _timer.Interval = TimeSpan.FromMinutes(minutes);
            }

            _timer.Tick -= (s, e) => function();
            _timer.Tick += (s, e) => function();

            if (!_isTimerRunning)
            {
                _timer.Start();
                _isTimerRunning = true;
            }
        }

        public static void StopTimer()
        {
            _timer.Stop();
            _isTimerRunning = false;
        }


    }
    public class Utilities
    {

        public static DateTime ConvertDateTimeNullToNotNull(DateTime? date)
        {
            return date ?? default(DateTime);
        }

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
