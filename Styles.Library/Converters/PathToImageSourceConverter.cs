using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;


// http://www.aspbucket.com/2016/10/how-to-implement-string-path-to-image.html

namespace Styles.Library.Converters
  {
  public class PathToImageSourceConverter : IValueConverter
    {
       public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
        if (targetType == typeof(ImageSource))
          {
          if (value is string)
            {
            string str = (string)value;
            var output= new BitmapImage(new Uri(str, UriKind.RelativeOrAbsolute));
            return output;
            }
          else if (value is Uri)
            {
            Uri uri = (Uri)value;
            return new BitmapImage(uri);
            }
          }
        return value;
        }

      public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
        throw new NotImplementedException();
        }
      }
  }
