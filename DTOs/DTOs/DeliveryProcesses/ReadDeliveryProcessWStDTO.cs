using DTOs.DTOs.Stores;
using DTOs.DTOs.Warehouses;

namespace DTOs.DTOs.DeliveryProcesses
{
    public class ReadDeliveryProcessWStDTO
    {
        public int ProcessID { get; set; }     
        public int WarehouseID { get; set; }   
        public int TotalAssets { get; set; }
        public DateTime DateTime { get; set; }
        public string FormattedDate => DateTime.ToString("yyyy-MM-dd HH:mm:ss");
        public List<ReadStoreProcessDTO> StoreProcesses { get; set; }
        public StageCompletionStep StageCompletionStep { get; set; }
        public ReadWarehouseDTO Warehouse { get; set; }
    }
}
