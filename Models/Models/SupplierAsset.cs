namespace Models.Models
{
    public class SupplierAsset
    {
        // AssetID, SupplierID and SerialNo are Composite key
        public int? AssetID { get; set; }    // Forign key from Asset Entity
        public int? SupplierID { get; set; }     // Forign key from Supplier Entity
        public string SerialNumber { get; set; }
        public int Count { get; set; }


        //RELATIONS
        public Supplier Supplier { get; set; }
        public Asset Asset { get; set; }
        public List<AssetShipmentSuW> AssetShipmentSuWs {  get; set; }
    }
}
