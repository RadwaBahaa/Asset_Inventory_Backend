using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class AssetShipmentWSt
    {
        public int AssetID { get; set; }
        public int WarehouseID { get; set; }
        public int ProcessID { get; set; }
        public int Quantity { get; set; }


        //RELATIONS
        public List<WarehouseAsset> WarehouseAssets { get; set; }
        public List<DeliveryProcessWSt> DeliveryProcessWSto { get; set; }
    }
}
