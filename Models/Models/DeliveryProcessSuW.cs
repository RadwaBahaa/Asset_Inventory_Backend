namespace Models.Models
{
    public class DeliveryProcessSuW
    {
        public int ProcessID { get; set; }
        public int SupplierID { get; set; }
        public int TotalAssets { get; set; }
        public DateTime DateAndTime { get; set; }
        public string Status { get; set; }

        //RELATIONS
        public Supplier Supplier { get; set; }
        public List<AssetShipmentSuW> AssetShipmentSuW { get; set;}
        public List<WarehouseProcess> WarehouseProcesses { get; set; }
    }
}
