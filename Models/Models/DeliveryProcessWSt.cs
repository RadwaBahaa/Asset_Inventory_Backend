using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class DeliveryProcessWSt
    {
        public int ProcessID { get; set; }
        public int WarehouseID { get; set; }
        public int TotalAssets { get; set; }
        public DateTime DateAndTime { get; set; }
        public string Status { get; set; }

        //RELATIONS
        public Warehouse Warehouses { get; set; }
        public List<AssetShipmentWSt> ShipmentSuWSto { get; set; }
        public List<ProcessForEachS> ProcessForEachSt { get; set; }

    }
}
