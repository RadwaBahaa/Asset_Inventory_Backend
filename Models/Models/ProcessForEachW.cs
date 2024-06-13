using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Models.Models
{
    public class ProcessForEachW
    {
        public int ProcessID { get; set; }
        public int WarehouseID { get; set; }
        public int Quantity { get; set; }

        //RELATIONS
        public DeliveryProcessSuW DeliveryProcessSuWa { get; set; }
        public Warehouse Warehouses { get; set; }

    }
}
