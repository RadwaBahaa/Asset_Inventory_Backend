using DTOs.DTOs.Stores;
using DTOs.DTOs.Suppliers;
using DTOs.DTOs.Warehouses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Interface
{
    public interface ISupplierServices
    {
        public Task<bool> CreateSupplier(AddOrUpdateSupplierDTO addOrUpdateSupplierDTO);
        public Task<List<ReadSupplierDTO>> GetAllSuppliers();
        public Task<ReadSupplierDTO> GetSupplierByID(int SupplierID);
        public Task<List<ReadSupplierDTO>> SearchByName(string SupplierName);
        public Task<List<ReadSupplierDTO>> SearchByAddress(string Address);
        public Task<ReadSupplierDTO> UpdateSupplier(AddOrUpdateSupplierDTO addOrUpdateSupplierDTO, int SupplierID);
        public Task<bool> DeleteSupplier(int SupplierID);
    }
}
