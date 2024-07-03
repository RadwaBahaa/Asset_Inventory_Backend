namespace Models.Models
{
    public class AssetShipmentWSt
    {
        // AssetID, SupplierID, AssetCreationDate, ProcessID and StoreID are Composite key
       public int? AssetID { get; set; }        // Forign key from WarehouseAsset Entity   
        public int? WarehouseID { get; set; }    // Forign key from WarehouseAsset Entity 
        public int? ProcessID { get; set; }      // Forign key from StoreProcess Entity 
        public int? StoreID { get; set; }        // Forign key from StoreProcess Entity
        public string? SerialNumber { get; set; }     // Forign key from SupplierAsset Entity
        public int Quantity { get; set; }


        //RELATIONS
        public WarehouseAsset WarehouseAsset { get; set; }
        public StoreProcess StoreProcess { get; set; }
    }
}
