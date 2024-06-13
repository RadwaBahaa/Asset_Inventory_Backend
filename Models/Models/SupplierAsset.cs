namespace Models.Models
{
    public class SupplierAsset
    {
        public int AssetID { get; set; }
        public int SupplierID { get; set; }
        public int Count { get; set; }
        public string Condition { get; set; }


        //RELATIONS
        public Supplier Supplier { get; set; }
        public Asset Asset { get; set; }
    }
}
