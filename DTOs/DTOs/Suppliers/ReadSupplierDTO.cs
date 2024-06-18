using DTOs.DTOs.Categories;
using DTOs.DTOs.Stores;
using System.Drawing;

namespace DTOs.DTOs.Suppliers
{
    public class ReadSupplierDTO
    {
        public string SupplierName { get; set; }
        public Point Location { get; set; }
        public string Address { get; set; }
        public ReadSupplierAssetsDTO SupplierAssets { get; set; }
    }
}
