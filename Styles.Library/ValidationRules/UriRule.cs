using System;
using System.Globalization;
using System.Windows.Controls;

namespace Styles.Library.ValidationRules
  {

  //  https://stackoverflow.com/questions/19539492/implement-validation-for-wpf-textboxes
  public class UriRule : ValidationRule
    {
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
      {
      if (value is string)
        {
        string strValue = Convert.ToString(value);

        if (string.IsNullOrEmpty(strValue))
          {
          return new ValidationResult(false, $"Value cannot be converted to string.");
          }

        if (Uri.IsWellFormedUriString(strValue, UriKind.Absolute))
          {
          return new ValidationResult(true, "Uri OK");
          }

        return new ValidationResult(false, "URL should be in valid format");
        }
      return new ValidationResult(false, "URL should be in valid format");
      }
    }
  }
