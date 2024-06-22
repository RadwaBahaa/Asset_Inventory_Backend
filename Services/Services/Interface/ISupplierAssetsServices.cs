using AutoMapper;
using DTOs.DTOs.Suppliers;
using Models.Models;
using Repository.Classes;


namespace Services.Services.Interface
{
    public interface ISupplierAssetsServices
    {
        public Task<bool> Create(AddOrUpdateSupplierAssetsDTO supplierAssetsDTO);
        public Task<List<ReadSupplierAssetsDTO>> ReadAll();
        public Task<ReadSupplierAssetsDTO> ReadBySerialNumber(string serialNumber);
        public Task<ReadSupplierAssetsDTO> Update(AddOrUpdateSupplierAssetsDTO addOrUpdateSupplierAssetsDTO, int AssetID, int SerialNumber);
        public Task<bool> Delete(int AssetID, int SerialNumber);
    }
}
