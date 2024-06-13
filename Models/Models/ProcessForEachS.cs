using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class ProcessForEachS
    {
        public int ProcessID { get; set; }
        public int StoreID { get; set; }
        public int Quantity { get; set; }

        //RELATIONS 
        public Store Stores {  get; set; } 
        public DeliveryProcessWSt DeliveryProcessWSto { get; set; }

    }
}
