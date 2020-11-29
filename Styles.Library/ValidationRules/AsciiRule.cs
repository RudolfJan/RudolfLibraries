using System.Globalization;
using System.Linq;
using System.Windows.Controls;

// Usage example:
//<Binding Path = "ClonedScenarioName" Mode="TwoWay" UpdateSourceTrigger="PropertyChanged">
//<Binding.ValidationRules>
//<xxx:IntegerRule />
//</Binding.ValidationRules>
//</Binding>

// https://stackoverflow.com/questions/58717392/validation-rule-for-checking-if-text-has-only-ascii


namespace Styles.Library.ValidationRules
  {
  public class AsciiRule: ValidationRule
    {
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
      {
      return value is string str && str.All(ch => ch < 128)
        ? ValidationResult.ValidResult
        : new ValidationResult(false, "Field contains illegal characters");
      }
    }
  }
