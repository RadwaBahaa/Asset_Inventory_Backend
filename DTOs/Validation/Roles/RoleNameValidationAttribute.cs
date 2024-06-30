using System.ComponentModel.DataAnnotations;

namespace DTOs.DTOs.Roles
{
    public class RoleNameValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var roleType = value.ToString();
                var validRoleTypes = new[] { "Warehouse", "Supplier", "Store", "Admin" };

                if (!Array.Exists(validRoleTypes, type => type.Equals(roleType, StringComparison.OrdinalIgnoreCase)))
                {
                    return new ValidationResult($"Invalid RoleType: '{roleType}'. RoleType must be one of 'Warehouse', 'Supplier', 'Store', 'Admin'.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
