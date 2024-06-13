using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class DeliveryProcessSuW
    {
        public int ProcessID { get; set; }
        public int SupplierID { get; set; }
        public int TotalAssets { get; set; }
        public DateTime DateAndTime { get; set; }
        public string Status { get; set; }

        //RELATIONS
        public Supplier Suppliers { get; set; }
        public List<AssetShipmentSuW> ShipmentSuWa { get; set;}
        public List<ProcessForEachW> ProcessForEachWa { get; set; }


    }
}
