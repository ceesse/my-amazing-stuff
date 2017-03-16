using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace PhotoViewer
{
    public class PathToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BitmapImage bi = null;
            try
            {
                bi = new BitmapImage(new Uri(value.ToString()));
            }
            catch (Exception)
            {
                try
                {
                    bi = new BitmapImage(new Uri(@"C:\Temp\koala.jpg"));
                }
                catch (Exception)
                {
                }
            }
            return bi;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
