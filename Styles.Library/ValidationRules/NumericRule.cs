using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

// https://stackoverflow.com/questions/19539492/implement-validation-for-wpf-textboxes

//Very important: don 't forget to set ValidatesOnTargetUpdated="True" it won't work without this definition.

//<TextBox x:Name = "Int32Holder"
//IsReadOnly = "{Binding IsChecked,ElementName=CheckBoxEditModeController,Converter={converters:BooleanInvertConverter}}"
//Style = "{StaticResource ValidationAwareTextBoxStyle}"
//VerticalAlignment = "Center" >
//< !--Text = "{Binding Converter={cnv:TypeConverter}, ConverterParameter='Int32', Path=ValueToEdit.Value, UpdateSourceTrigger=PropertyChanged, RelativeSource={RelativeSource AncestorType={x:Type UserControl}}}"-- >
//< TextBox.Text >
//< Binding Path = "Name"
//Mode = "TwoWay"
//UpdateSourceTrigger = "PropertyChanged"
//Converter = "{cnv:TypeConverter}"
//ConverterParameter = "Int32"
//ValidatesOnNotifyDataErrors = "True"
//ValidatesOnDataErrors = "True"
//NotifyOnValidationError = "True" >
//< Binding.ValidationRules >
//< validationRules:NumericValidationRule ValidationType = "{x:Type system:Int32}"
//ValidatesOnTargetUpdated="True" />
//</Binding.ValidationRules>
//</Binding>
//</TextBox.Text>
//<!--NumericValidationRule-->
//</TextBox>


namespace Styles.Library.ValidationRules
  {
  public class NumericValidationRule : ValidationRule
    {
    public Type ValidationType { get; set; }
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
      {
      string strValue = Convert.ToString(value);

      if (string.IsNullOrEmpty(strValue))
        return new ValidationResult(false, $"Value cannot be converted to string.");
      bool canConvert = false;
      switch (ValidationType.Name)
        {

          case "Boolean":
            bool boolVal = false;
            canConvert = bool.TryParse(strValue, out boolVal);
            return canConvert ? new ValidationResult(true, null) : new ValidationResult(false, $"Input should be type of boolean");
          case "Int32":
            case "int":
            int intVal = 0;
            canConvert = int.TryParse(strValue, out intVal);
            return canConvert ? new ValidationResult(true, null) : new ValidationResult(false, $"Input should be type of int/Int32");
          case "Double":
          case "double":
          double doubleVal = 0;
            canConvert = double.TryParse(strValue, out doubleVal);
            return canConvert ? new ValidationResult(true, null) : new ValidationResult(false, $"Input should be type of double/Double");
          case "Int64":
            long longVal = 0;
            canConvert = long.TryParse(strValue, out longVal);
            return canConvert ? new ValidationResult(true, null) : new ValidationResult(false, $"Input should be type of Int64");
          default:
            throw new InvalidCastException($"{ValidationType.Name} is not supported");
        }
      }
    }
  }
