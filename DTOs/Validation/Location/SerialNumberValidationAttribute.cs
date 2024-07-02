using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class SerialNumberValidationAttribute : ValidationAttribute
{
    private static readonly Regex SerialNumberRegex = new Regex(@"^[A-Z0-9]{12}$", RegexOptions.Compiled);

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is string serialNumber)
        {
            if (SerialNumberRegex.IsMatch(serialNumber))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Serial number must be in the format G6TZL899N70M (12 uppercase alphanumeric characters).");
        }
        return new ValidationResult("Invalid serial number value.");
    }
}
