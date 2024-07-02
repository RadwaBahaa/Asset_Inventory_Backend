namespace DTOs.DTOs.Roles
{
    public class AddNewRoleDTO
    {
        [RoleNameValidation]
        public string Role { get; set; }
    }
}