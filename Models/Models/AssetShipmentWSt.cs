namespace Models.Models
{
    public class AssetShipmentWSt
    {
        // AssetID, SupplierID, AssetCreationDate and ProcessID are Composite key
        public int AssetID { get; set; }        // Forign key from WarehouseAsset Entity   
        public int WarehouseID { get; set; }    // Forign key from WarehouseAsset Entity 
        public string SerialNo { get; set; }     // Forign key from SupplierAsset Entity
        public int ProcessID { get; set; }      // Forign key from DeliveryProcessWSt Entity 
        public int Quantity { get; set; }


        //RELATIONS
        public WarehouseAsset WarehouseAsset { get; set; }
        public DeliveryProcessWSt DeliveryProcessWSt { get; set; }
    }
}
