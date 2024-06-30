namespace DTOs.DTOs.Roles
{
    public class AddNewRoleDTO
    {
        [RoleNameValidation]
        public string RoleName { get; set; }
    }
}
