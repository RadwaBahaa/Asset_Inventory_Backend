namespace DTOs.DTOs.DeliveryProcesses
{
    public class AddWarehouseProcessDTO
    {
        public int WarehouseID { get; set; }
        public string? Note { get; set; }
        public List<AddAssetShipmentDTO> AssetShipment { get; set; }
    }
}
