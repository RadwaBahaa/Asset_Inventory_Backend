namespace Models.Models
{
    public class StoreProcess
    {
        // ProcessID and StoreID are composit key
        public int ProcessID { get; set; }  // Forign key from DeliveryProcessWSt Entity
        public int StoreID { get; set; }    // Forign key from Store Entity
        public int Quantity { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }    


        //RELATIONS 
        public Store Store {  get; set; } 
        public DeliveryProcessWSt DeliveryProcessWSt { get; set; }
    }
}
