using System.ComponentModel.DataAnnotations;

namespace DTOs.Validation.Assets
{
    public class PriceValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var price = (float?)value;
            if (price == null || price == 0)
            {
                return new ValidationResult("The 'Product's Price' must be mentioned!...");
            }
            else if (price < 0)
            {
                return new ValidationResult("'Product price' needs to be a positive float!....");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}
