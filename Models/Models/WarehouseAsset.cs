namespace Models.Models
{
    public class WarehouseAsset
    {
        // AssetID, WarehouseID and SerialNo are Composite key
        public int? AssetID { get; set; }    // Forign key from Asset Entity
        public int? WarehouseID {  get; set; }   // Forign key from Warehouse Entity
        public string SerialNumber { get; set; }    
        public int Count { get; set; }

        
        //RELATIONS
        public Asset Asset { get; set; }
        public Warehouse Warehouse { get; set; }
        public List<AssetShipmentWSt> AssetShipmentWSts { get; set; }
    }
}
