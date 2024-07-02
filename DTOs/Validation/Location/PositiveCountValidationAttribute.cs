using System;
using System.ComponentModel.DataAnnotations;

public class PositiveCountValidationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is int count)
        {
            if (count > 0)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Count must be a positive integer.");
        }
        return new ValidationResult("Invalid count value.");
    }
}
