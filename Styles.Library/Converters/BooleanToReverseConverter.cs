using System;
using System.Globalization;
using System.Windows.Data;

// https://stackoverflow.com/questions/1039636/how-to-bind-inverse-boolean-properties-in-wpf


namespace Styles.Library.Converters
  {
  public class BooleanToReverseConverter : IValueConverter
    {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
     => !(bool?)value ?? true;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
     => !(value as bool?);
    }
  }
