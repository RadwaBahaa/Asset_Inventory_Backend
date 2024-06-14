namespace Models.Models
{
    public class StoreProcess
    {
        public int ProcessID { get; set; }
        public int StoreID { get; set; }
        public int Quantity { get; set; }

        //RELATIONS 
        public Store Store {  get; set; } 
        public DeliveryProcessWSt DeliveryProcessWSt { get; set; }
    }
}
