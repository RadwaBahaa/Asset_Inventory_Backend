using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class AssetShipmentSuW
    {
        public int AssetID { get; set; }
        public int SupplierID { get; set; }
        public int ProcessID { get; set; }
        public int  Quantity { get; set; }


        //RELATIONS
        public List<SupplierAsset> SupplierAssets { get; set; }
        public List<DeliveryProcessSuW> DeliveryProcessSuWa { get; set; }
    }
}
