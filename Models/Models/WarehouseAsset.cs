namespace Models.Models
{
    public class WarehouseAsset
    {
        public int AssetID { get; set; }
        public int WarehouseID {  get; set; }
        public int Count { get; set; }

        
        //RELATIONS
        public Asset Assets { get; set; }
        public Warehouse Warehouses { get; set; }
    }
}
