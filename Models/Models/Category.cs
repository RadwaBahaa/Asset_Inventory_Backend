namespace Models.Models
{
    public class Category
    {
        public int CategoryID { get; set; }     // Primary Key
        public string CategoryName { get; set; }
        public string? Description { get; set; }


        //RELATIONS
        public List<Asset> Assets { get; set; }
    }
}
