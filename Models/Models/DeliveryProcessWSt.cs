namespace Models.Models
{
    public class DeliveryProcessWSt
    {
        public int ProcessID { get; set; }
        public int WarehouseID { get; set; }
        public int TotalAssets { get; set; }
        public DateTime DateAndTime { get; set; }
        public string Status { get; set; }


        //RELATIONS
        public Warehouse Warehouse { get; set; }
        public List<AssetShipmentWSt> AssetShipmentWSt { get; set; }
        public List<StoreProcess> StoreProcesses { get; set; }
    }
}
