using System.ComponentModel.DataAnnotations;

namespace DTOs.Validation.Location
{
    public class QuantityValidationAttribute :ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var quantity = (int?)value;
            if (quantity == null || quantity == 0)
            {
                return new ValidationResult("The 'Quantity' must be mentioned!...");
            }
            else if (quantity < 0)
            {
                return new ValidationResult("'Quantity' needs to be a positive integer!....");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}
