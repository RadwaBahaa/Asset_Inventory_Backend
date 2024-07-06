namespace DTOs.DTOs.DeliveryProcesses
{
    public class AddStoreProcessDTO
    {
        public int StoreID { get; set; }
        public string? Note { get; set; }
        public List<AddAssetShipmentDTO> AssetShipment { get; set; }
    }
}
