namespace DTOs.DTOs.Users
{
    public class ReadUserDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public IList<string> Roles { get; set; }
    }
}
