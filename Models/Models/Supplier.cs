using NetTopologySuite.Geometries;

namespace Models.Models
{
    public class Supplier
    {
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public Point Location { get; set; }


        //RELATIONS
        public List<SupplierAsset> SupplierAssets { get; set; }
    }
}
