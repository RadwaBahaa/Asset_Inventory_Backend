namespace Models.Models
{
    public class AssetShipmentSuW
    {
        // AssetID, SupplierID, AssetCreationDate and ProcessID are Composite key
        public int AssetID { get; set; }        // Forign key from SupplierAsset Entity
        public int SupplierID { get; set; }     // Forign key from SupplierAsset Entity
        public string SerialNumber { get; set; }     // Forign key from SupplierAsset Entity
        public int ProcessID { get; set; }      // Forign key from DeliveryProcessSuW Entity
        public int Quantity { get; set; }


        //RELATIONS
        public SupplierAsset SupplierAsset { get; set; }
        public DeliveryProcessSuW DeliveryProcessSuW { get; set; }
    }
}
