using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Supplier
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }

        //public string Location { get; set; }
        public List<Supplier_Asset> Supplier_Assets { get; set; }
    }
}
