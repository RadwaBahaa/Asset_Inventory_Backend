using NetTopologySuite.Geometries;

namespace Models.Models
{
    public class Store
    {
        public int StoreID { get; set; }    // Primary key
        public string StoreName { get; set; }
        public Point Location { get; set; }
        public string Address { get; set; }


        //RELATIONS
        public List<StoreAsset> StoreAssets { get; set; }
        public List<StoreProcess> StoreProcesses {  get; set; }
        public List<StoreRequest> StoreRequests { get; set; }
    }
}
