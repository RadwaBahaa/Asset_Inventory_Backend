using DTOs.DTOs.Warehouses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Interface
{
    public interface IWarehouseService
    {
        public Task<bool> CreateStore (AddOrUpdateWarehouseDTO addOrUpdateWarehouseDTO);
        public Task<ReadWarehouseDTO> GetAllWarehouses();
        public Task<ReadWarehouseDTO> GetWarehouseByID(int WarehouseID);
        public Task<ReadWarehouseDTO> UpdateWarehouse(AddOrUpdateWarehouseDTO addOrUpdateWarehouseDTO, int WarehouseID);
        public Task<ReadWarehouseDTO> DeleteWarehouse(int WarehouseID);
    }
}
