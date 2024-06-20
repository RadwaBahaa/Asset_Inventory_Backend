using DTOs.DTOs.Suppliers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Interface
{
    public interface ISupplierService
    {
        public Task<bool> CreateSupplier(AddOrUpdateSupplierDTO addOrUpdateSupplierDTO);
        public Task<List<ReadSupplierDTO>> GetAllSuppliers();
        public Task<ReadSupplierDTO> GetSupplierByID(int SupplierID);
        public Task<ReadSupplierDTO> UpdateSupplier(AddOrUpdateSupplierDTO addOrUpdateSupplierDTO, int SupplierID);
        public Task<bool> DeleteSupplier(int SupplierID);
    }
}
