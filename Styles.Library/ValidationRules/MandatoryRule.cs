using System.Globalization;
using System.Windows.Controls;

namespace Styles.Library.ValidationRules
  {
  public class MandatoryRule: ValidationRule
    {
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
      {
      return value is string str && str.Length>0
        ? ValidationResult.ValidResult
        : new ValidationResult(false, "Field is mandatory");
      }
    }
  }
