using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace Styles.Library.ValidationRules
  {
  public class PosIntegerRule: ValidationRule
    {
      
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
      {
      return value is string str && str.Length > 0 && int.TryParse(str, out var x) && x>0
        ? ValidationResult.ValidResult
        : new ValidationResult(false, $"Not a valid integer {(string)value}");
      }
    }
  }
  
