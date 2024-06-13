using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Supplier_Asset
    {
        public int AssetId { get; set; }
        public int SupplierId { get; set; }
        public int Count { get; set; }
        public string Condition { get; set; }

        public Supplier Suppliers { get; set; }

        public Asset Assets { get; set; }




    }
}
