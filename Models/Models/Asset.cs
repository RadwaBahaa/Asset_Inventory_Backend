namespace Models.Models
{
    public class Asset
    {
        public int AssetID { get; set; }    // Primary key
        public string AssetName { get; set; }
        public int CategoryID { get; set; }     // Forign key from Category Entity
        public float Price { get; set; }
        public string Description { get; set; }
        //public byte[] Picture { get; set; }   // Property to store photo as binary data in the database


        //RELATIONS
        public Category Category { get; set; }
        public List<StoreAsset> StoreAssets { get; set; }
        public List<SupplierAsset> SupplierAssets { get; set; }
        public List<WarehouseAsset> WarehouseAssets { get; set; }
        public List<StoreRequestAsset> StoreRequestAssets { get; set; }     // There is a relation many-to-many with StoreRequests Entity 
        public List<WarehouseRequestAsset> WarehouseRequestAssets { get; set; }     // There is a relation many-to-many with WarehouseRequests Entity 
    }
}
