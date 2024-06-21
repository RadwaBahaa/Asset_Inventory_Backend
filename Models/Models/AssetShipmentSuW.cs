namespace Models.Models
{
    public class AssetShipmentSuW
    {
        // AssetID, SupplierID, AssetCreationDate, ProcessID and WarehouseID are Composite key
        public int AssetID { get; set; }        // Forign key from SupplierAsset Entity
        public int SupplierID { get; set; }     // Forign key from SupplierAsset Entity
<<<<<<< HEAD
        public string SerialNo { get; set; }     // Forign key from SupplierAsset Entity
        public int ProcessID { get; set; }      // Forign key from WarehouseProcess Entity
        public int WarehouseID { get; set; }      // Forign key from WarehouseProcess Entity
=======
        public string SerialNumber { get; set; }     // Forign key from SupplierAsset Entity
        public int ProcessID { get; set; }      // Forign key from DeliveryProcessSuW Entity
>>>>>>> a2f6e0375224a5cb3ea9f16d95c6e15d04075860
        public int Quantity { get; set; }


        //RELATIONS
        public SupplierAsset SupplierAsset { get; set; }
        public WarehouseProcess WarehouseProcess { get; set; }
    }
}
