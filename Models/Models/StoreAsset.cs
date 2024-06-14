namespace Models.Models
{
    public class StoreAsset
    {
        public int AssetID { get; set; }
        public int StoreID { get; set; }
        public int Count { get; set; }


        //RELATIONS
        public Asset Asset { get; set; }
        public Store Store { get; set; }
    }
}
