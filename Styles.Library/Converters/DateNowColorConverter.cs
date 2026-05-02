using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace Styles.Library.Converters
  {
  [ValueConversion(typeof(DateOnly), typeof(SolidColorBrush))]
  public class DateNowColorConverter : IValueConverter
    {
  
  #region Implementation of IValueConverter

  /// <summary>
  /// 
  /// </summary>
  /// <param name="value">DateOnly value controlling whether to apply color change</param>
  /// <param name="targetType"></param>
  /// <param name="parameter">A CSV string on the format [ColorNameIfTrue;ColorNameIfFalse;OpacityNumber] may be provided for customization, default is [LimeGreen;Transparent;1.0].</param>
  /// <param name="culture"></param>
  /// <returns>A SolidColorBrush in the supplied or default colors depending on the state of value.</returns>
  public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
      {
      SolidColorBrush color;
      // Setting default values
      var colorIfTrue = Colors.Red;
      var colorIfFalse = Colors.Black;
      Double opacity = 1;

      // Parsing converter parameter
      if (parameter != null)
        {
        // Parameter format: [ColorNameIfTrue;ColorNameIfFalse;OpacityNumber]
        var ParameterString = parameter.ToString();
        if (!string.IsNullOrEmpty(ParameterString))
          {
          var parameters = ParameterString.Split(';');
          var count = parameters.Length;
          if (count > 0 && !string.IsNullOrEmpty(parameters[0]))
            {
            colorIfTrue = ColorFromName(parameters[0]);
            }
          if (count > 1 && !string.IsNullOrEmpty(parameters[1]))
            {
            colorIfFalse = ColorFromName(parameters[1]);
            }
          if (count > 2 && !string.IsNullOrEmpty(parameters[2]))
            {
            if (double.TryParse(parameters[2], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture.NumberFormat, out var dblTemp))
              opacity = dblTemp;
            }
          }
        }
      // Creating Color Brush
      if (value != null && (DateOnly)value >= DateOnly.FromDateTime(DateTime.Now).AddDays(-1))
        {
        color = new SolidColorBrush(colorIfTrue) { Opacity = opacity };
        }
      else
        {
        color = new SolidColorBrush(colorIfFalse) { Opacity = opacity };
        }
      return color;
      }

    public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
      {
      throw new NotImplementedException();
      }

    #endregion

    public static Color ColorFromName(String colorName)
      {
      System.Drawing.Color systemColor = System.Drawing.Color.FromName(colorName);
      return Color.FromArgb(systemColor.A, systemColor.R, systemColor.G, systemColor.B);
      }
    }
  }

