using LayotsMvvm.ViewModel.Base.ImageConverter;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LayotsMvvm.ViewModel.Base.ImageConverter
{
    public class ImageSourceConverter : IValueConverter
    {
        #region Oreginal

        //public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        //{
        //    if (targetType == typeof(ImageSource))
        //    {
        //        if (value is string)
        //        {
        //            string str = (string)value;
        //            return new BitmapImage(new Uri(str, UriKind.RelativeOrAbsolute));
        //        }
        //        else if (value is Uri)
        //        {
        //            Uri uri = (Uri)value;
        //            return new BitmapImage(uri);
        //        }
        //    }

        //    return value;
        //}

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                BitmapImage bmImage = new BitmapImage();

                if (targetType == typeof(ImageSource))
                {
                    if (value is string)
                    {
                        string str = (string)value;
                        
                        //return new BitmapImage(new Uri(str, UriKind.RelativeOrAbsolute));
                        return ConvertFsStream.FromImageToBitMapImage(Image.FromFile(str));
                        
                    }
                    else if (value is Uri)
                    {
                        Uri uri = (Uri)value;
                        return ConvertFsStream.Image_http_Loaded(uri);
                        // return new BitmapImage(uri); //old script

                    }
                }

                return value;
            }
            catch (System.IO.FileNotFoundException e)
            {
                MessageBox.Show("Файл не найден: " + e.Message);
                throw;
            }
        }

        #endregion
    }
}
