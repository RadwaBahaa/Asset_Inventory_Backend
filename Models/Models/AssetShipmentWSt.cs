namespace Models.Models
{
    public class AssetShipmentWSt
    {
        public int AssetID { get; set; }
        public int WarehouseID { get; set; }
        public int ProcessID { get; set; }
        public int Quantity { get; set; }


        //RELATIONS
        public WarehouseAsset WarehouseAsset { get; set; }
        public DeliveryProcessWSt DeliveryProcessWSt { get; set; }
    }
}
