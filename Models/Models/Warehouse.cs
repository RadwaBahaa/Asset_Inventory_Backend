using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Warehouse
    {
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; }

        //public string Location { get; set; }
        public List<Warehouse_Asset> Warehouse_Assets { get; set; }

    }
}
