namespace Models.Models
{
    public class WarehouseAsset
    {
        public int AssetId { get; set; }
        public int WarehouseId {  get; set; }
        public int Count { get; set; }
        public string Condition { get; set; }

        
        //RELATIONS
        public Asset Asset { get; set; }
        public Warehouse Warehouse { get; set; }
    }
}
