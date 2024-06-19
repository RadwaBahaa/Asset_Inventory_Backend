using DTOs.DTOs.Categories;
using DTOs.DTOs.Stores;
using System.Drawing;

namespace DTOs.DTOs.Stores
{
    public class ReadStoreDTO
    {
        public int StoreID { get; set; }
        public string StoreName { get; set; }
        public Point Location { get; set; }
        public string Address { get; set; }
        public List<ReadStoreAssetsDTO> Assets { get; set; }
    }
}
