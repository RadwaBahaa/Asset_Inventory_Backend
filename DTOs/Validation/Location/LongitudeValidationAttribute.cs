using System.ComponentModel.DataAnnotations;

public class LongitudeValidationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is double longitude)
        {
            if (longitude >= -180 && longitude <= 180)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Longitude must be between -180 and 180 degrees.");
        }
        return new ValidationResult("Invalid longitude value.");
    }
}
