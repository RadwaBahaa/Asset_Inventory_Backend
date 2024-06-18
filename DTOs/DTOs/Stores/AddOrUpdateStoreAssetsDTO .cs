using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTOs.Stores
{
    public class AddOrUpdateStoreAssetsDTO
    {
        public int? SerialNumber { get; set; }
        public DateOnly? ProductionDate { get; set; }
        public int? Count { get; set; }

    }
}
