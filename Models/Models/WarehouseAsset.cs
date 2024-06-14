namespace Models.Models
{
    public class WarehouseAsset
    {
        public int AssetID { get; set; }
        public int WarehouseID {  get; set; }
        public int Count { get; set; }

        
        //RELATIONS
        public Asset Asset { get; set; }
        public Warehouse Warehouse { get; set; }
        public List<AssetShipmentWSt> AssetShipmentWSts { get; set; }
    }
}
