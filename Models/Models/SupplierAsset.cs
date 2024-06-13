namespace Models.Models
{
    public class SupplierAsset
    {
        public int AssetID { get; set; }
        public int SupplierID { get; set; }
        public int Count { get; set; }


        //RELATIONS
        public Supplier Suppliers { get; set; }
        public Asset Assets { get; set; }
        public List<AssetShipmentSuW> AssetShipmentSuWa {  get; set; }
    }
}
