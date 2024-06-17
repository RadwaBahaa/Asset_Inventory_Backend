namespace Models.Models
{
    public class StoreAsset
    {
        // AssetID, StoreID and AssetCreationDate are Composite key
        public int AssetID { get; set; }    // Forign key from Asset Entity
        public int StoreID { get; set; }    // Forign key from Store Entity
        public DateOnly AssetCreationDate { get; set; }
        public int Count { get; set; }


        //RELATIONS
        public Asset Asset { get; set; }
        public Store Store { get; set; }
    }
}
