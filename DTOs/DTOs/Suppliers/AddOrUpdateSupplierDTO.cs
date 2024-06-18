using DTOs.DTOs.Stores;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTOs.Suppliers

{
    public class AddOrUpdateSupplierDTO
    {
        public string? SupplierName { get; set; }
        public Point? Location { get; set; }
        public string? Address { get; set; }
        public AddOrUpdateStoreRequestsDTO SupplierAssets { get; set; }
    }
}
