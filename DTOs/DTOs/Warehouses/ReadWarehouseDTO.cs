namespace DTOs.DTOs.Warehouses
{
    public class ReadWarehouseDTO
    {
        public int WarehouseID { get; set; }
        public string WarehouseName { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int SRID { get; set; }
        public string Address { get; set; }
    }
}
