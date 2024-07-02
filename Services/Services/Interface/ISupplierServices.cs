using DTOs.DTOs.Suppliers;
using Models.DTOs;

namespace Services.Services.Interface
{
    public interface ISupplierServices
    {
        public Task<bool> CreateByData(AddSupplierDTO supplierDTO);
        public Task<bool> CreateByGeoJSON(AddSupplierGeoJsonDTO supplierDTO);
        public Task<List<ReadSupplierDTO>> ReadAll();
        public Task<List<ReadSupplierGeoJsonDTO>> ReadAllSuppliersAsGeoJson();
        public Task<ReadSupplierDTO> ReadByID(int supplierID);
        public Task<ReadSupplierDTO> ReadByName(string name);
        public Task<ReadSupplierGeoJsonDTO> ReadSupplierAsGeoJson(int id);
        public Task<List<ReadSupplierDTO>> Search(string name, string address);
        public Task<ReadSupplierDTO> Update(UpdateSupplierDTO supplierDTO, int supplierID);
        public Task<bool> Delete(int supplierID);
    }
}
