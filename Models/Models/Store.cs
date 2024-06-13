using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Store
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }

        //public string Location { get; set; }
        public List<Store_Asset> Store_Assets { get; set; }
    }
}
