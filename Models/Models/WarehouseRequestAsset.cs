namespace Models.Models
{
    public class WarehouseRequestAsset
    {
        // RequestID and AsesetID and composite key
        public int? RequestID { get; set; }  // Forign key from WarehouseRequest Entity
        public int? AsesetID { get; set; }   // Forign key from Asset Entity
        public int Quantity { get; set; }
        public string Note { get; set; } 

        
        //RELATIONS
        public Asset Asset { get; set; }
        public WarehouseRequest WarehouseRequest { get; set; }
    }
}
