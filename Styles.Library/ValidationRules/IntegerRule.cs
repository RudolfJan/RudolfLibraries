using System.Globalization;
using System.Windows.Controls;

namespace Styles.Library.ValidationRules
  {
  // Usage example:
  //<Binding Path = "ClonedScenarioName" Mode="TwoWay" UpdateSourceTrigger="PropertyChanged">
  //<Binding.ValidationRules>
  //<xxx:IntegerRule />
  //</Binding.ValidationRules>
  //</Binding>

  // https://stackoverflow.com/questions/58717392/validation-rule-for-checking-if-text-has-only-ascii

  public class IntegerRule : ValidationRule
    {
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
      {
      return value is string str && str.Length>0 &&int.TryParse(str, out var x)
        ? ValidationResult.ValidResult
        : new ValidationResult(false, $"Not a valid integer {(string) value}");
      }
    }



  }
