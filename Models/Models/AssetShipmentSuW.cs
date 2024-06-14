namespace Models.Models
{
    public class AssetShipmentSuW
    {
        public int AssetID { get; set; }
        public int SupplierID { get; set; }
        public int ProcessID { get; set; }
        public int  Quantity { get; set; }


        //RELATIONS
        public SupplierAsset SupplierAsset { get; set; }
        public DeliveryProcessSuW DeliveryProcessSuW { get; set; }
    }
}
