namespace Models.Models
{
    public class AssetShipmentWSt
    {
        // AssetID, SupplierID, AssetCreationDate, ProcessID and StoreID are Composite key
        public int AssetID { get; set; }        // Forign key from WarehouseAsset Entity   
        public int WarehouseID { get; set; }    // Forign key from WarehouseAsset Entity 
<<<<<<< HEAD
        public string SerialNo { get; set; }    // Forign key from SupplierAsset Entity
        public int ProcessID { get; set; }      // Forign key from StoreProcess Entity 
        public int StoreID { get; set; }        // Forign key from StoreProcess Entity
=======
        public string SerialNumber { get; set; }     // Forign key from SupplierAsset Entity
        public int ProcessID { get; set; }      // Forign key from DeliveryProcessWSt Entity 
>>>>>>> a2f6e0375224a5cb3ea9f16d95c6e15d04075860
        public int Quantity { get; set; }


        //RELATIONS
        public WarehouseAsset WarehouseAsset { get; set; }
        public StoreProcess StoreProcess { get; set; }
    }
}
