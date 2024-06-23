using DTOs.DTOs.Suppliers;
using Models.DTOs;

namespace Services.Services.Interface
{
    public interface ISupplierServices
    {
        public Task<bool> Create(AddOrUpdateSupplierDTO supplierDTO);
        public Task<List<ReadSupplierDTO>> ReadAll();
        public Task<List<SupplierGeoJsonDTO>> ReadAllSuppliersAsGeoJson();
        public Task<ReadSupplierDTO> ReadByID(int supplierID);
        public Task<SupplierGeoJsonDTO> ReadSupplierAsGeoJson(int id);
        public Task<List<ReadSupplierDTO>> SearchByName(string supplierName);
        public Task<List<ReadSupplierDTO>> SearchByAddress(string Address);
        public Task<ReadSupplierDTO> Update(AddOrUpdateSupplierDTO supplierDTO, int supplierID);
        public Task<bool> Delete(int supplierID);
    }
}
