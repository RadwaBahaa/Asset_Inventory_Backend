namespace Models.Models
{
    public class SupplierAsset
    {
        // AssetID, SupplierID and AssetCreationDate are Composite key
        public int AssetID { get; set; }    // Forign key from Asset Entity
        public int SupplierID { get; set; }     // Forign key from Supplier Entity
        public DateOnly AssetCreationDate { get; set; }
        public int Count { get; set; }


        //RELATIONS
        public Supplier Supplier { get; set; }
        public Asset Asset { get; set; }
        public List<AssetShipmentSuW> AssetShipmentSuW {  get; set; }
    }
}
