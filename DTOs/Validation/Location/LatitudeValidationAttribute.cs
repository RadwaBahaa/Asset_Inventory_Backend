using System;
using System.ComponentModel.DataAnnotations;

public class LatitudeValidationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is double latitude)
        {
            if (latitude >= -90 && latitude <= 90)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Latitude must be between -90 and 90 degrees.");
        }
        return new ValidationResult("Invalid latitude value.");
    }
}
