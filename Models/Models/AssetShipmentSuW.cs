namespace Models.Models
{
    public class AssetShipmentSuW
    {
        // AssetID, SupplierID, AssetCreationDate, ProcessID and WarehouseID are Composite key
        public int? AssetID { get; set; }        // Forign key from SupplierAsset Entity
        public int? SupplierID { get; set; }     // Forign key from SupplierAsset Entity
        public int? ProcessID { get; set; }      // Forign key from WarehouseProcess Entity
        public int? WarehouseID { get; set; }      // Forign key from WarehouseProcess Entity
        public string? SerialNumber { get; set; }     // Forign key from SupplierAsset Entity
        public int Quantity { get; set; }


        //RELATIONS
        public SupplierAsset SupplierAsset { get; set; }
        public WarehouseProcess WarehouseProcess { get; set; }
    }
}
