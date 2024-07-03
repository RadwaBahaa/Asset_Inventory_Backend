using DTOs.Validation.Location;

namespace DTOs.DTOs.DeliveryProcesses
{
    public class AddAssetShipmentDTO
    {
        public int AssetID { get; set; }        
        public string SerialNumber { get; set; }
        [QuantityValidation]
        public int Quantity { get; set; }
    }
}