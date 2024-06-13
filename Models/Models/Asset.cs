using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Models.Models
{
    public class Asset
    {
        public int AssetId { get; set; }
        public string AssetName { get; set; }
        public int Serial_Number { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int Price { get; set; }
        public DateOnly Creation_Date { get; set; }
        public List<Supplier_Asset> Supplier_Assets { get; set; }
        public List<Warehouse_Asset> Warehouse_Assets { get; set; }
        public List<Store_Asset> Store_Assets { get; set; }
    
    }
}
