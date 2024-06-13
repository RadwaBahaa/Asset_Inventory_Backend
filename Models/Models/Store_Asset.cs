using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Store_Asset
    {
        public int AssetId { get; set; }
        public int StoreId { get; set; }
        public int Count { get; set; }
        public string Condition { get; set; }
        public Asset Assets { get; set; }
        public Store Stores { get; set; }
    }
}
