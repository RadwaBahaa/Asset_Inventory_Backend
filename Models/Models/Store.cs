using NetTopologySuite.Geometries;

namespace Models.Models
{
    public class Store
    {
        public int StoreID { get; set; }
        public string StoreName { get; set; }
        public Point Location { get; set; }


        //RELATIONS
        public List<StoreAsset> StoreAssets { get; set; }
        public List <ProcessForEachS> ProcessForEachSt {  get; set; }

    }
}
