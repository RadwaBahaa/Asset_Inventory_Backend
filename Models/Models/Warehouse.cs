using NetTopologySuite.Geometries;

namespace Models.Models
{
    public class Warehouse
    {
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public Point Location { get; set; }


        //RELATIONS
        public List<WarehouseAsset> WarehouseAssets { get; set; }
    }
}
