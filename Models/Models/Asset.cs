namespace Models.Models
{
    public class Asset
    {
        public int AssetID { get; set; }    // Primary key
        public string AssetName { get; set; }
        public int CategoryID { get; set; }     // Forign key from Category Entity
        public int Price { get; set; }
        public string Description { get; set; }
        public int SerialNumber { get; set; }
        public byte[] Picture { get; set; }   // Property to store photo as binary data in the database

        //public DateOnly CtreationDate { get; set; } 
        // That means that all assets with the same type have to be created in the same date
        // So I moved this property to the assests in each supplier to be as PK....


        //RELATIONS
        public Category Category { get; set; }
        public List<StoreAsset> StoreAssets { get; set; }
        public List<SupplierAsset> SupplierAssets { get; set; }
        public List<WarehouseAsset> WarehouseAssets { get; set; }
        public List<StoreRequestAsset> StoreRequestAssets { get; set; }     // There is a relation many-to-many with StoreRequests Entity 
        public List<WarehouseRequestAsset> WarehouseRequestAssets { get; set; }     // There is a relation many-to-many with WarehouseRequests Entity 
    }
}
