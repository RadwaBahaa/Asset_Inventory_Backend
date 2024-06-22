using AutoMapper;
using Repository.Classes;
using DTOs.DTOs.Suppliers;
using Services.Services.Interface;
using Models.Models;

namespace Services.Services.Classes
{
    public class SupplierAssetsServices : ISupplierAssetsServices
    {
        protected SupplierAssetRepository supplierAssetRepository { get; set; }
        protected IMapper mapper { get; set; }

        public SupplierAssetsServices(SupplierAssetRepository supplierAssetRepository, IMapper mapper)
        {
            this.supplierAssetRepository = supplierAssetRepository;
            this.mapper = mapper;
        }

        //________________ Create a new supplier Asset ______________
        public async Task<bool> CreateSupplierAsset(AddOrUpdateSupplierAssetsDTO addOrUpdateSupplierAssetsDTO)
        {
            var supplierAsset = mapper.Map<SupplierAsset>(addOrUpdateSupplierAssetsDTO);

            await supplierAssetRepository.Create(supplierAsset);

            return true;
        }

        //_______________Read all supplier assets_________________ 
        public async Task<List<ReadSupplierAssetsDTO>> GetAllSupplierAssets()
        {
            var supplierAssets = await supplierAssetRepository.Read();

            var supplierAssetsDTOs = mapper.Map<List<ReadSupplierAssetsDTO>>(supplierAssets);

            return supplierAssetsDTOs;
        }

        //_______________Read supplier Asset by Serial number_________________ 
        public async Task<ReadSupplierAssetsDTO> GetOneBySerialNumber(string serialNumber)
        {
            var supplierAsset = await supplierAssetRepository.GetOneBySerialNumber(serialNumber);
            var supplierAssetDTO = mapper.Map<ReadSupplierAssetsDTO>(supplierAsset);

            return supplierAssetDTO;
        }

        //_______________Update supplier asset by ID_________________ 
        public async Task<ReadSupplierAssetsDTO> UpdateSupplierAsset(AddOrUpdateSupplierAssetsDTO addOrUpdateSupplierAssetsDTO, int AssetID, int SerialNumber)
        {
            var supplierAsset = await supplierAssetRepository.GetOneByID(AssetID, SerialNumber);

            if (supplierAsset == null)
            {
                throw new KeyNotFoundException($"SupplierAsset with AssetID {AssetID} and SerialNumber {SerialNumber} not found");
            }

            mapper.Map(addOrUpdateSupplierAssetsDTO, supplierAsset);

            await supplierAssetRepository.Update();

            var updatedSupplierAssetDTO = mapper.Map<ReadSupplierAssetsDTO>(supplierAsset);

            return updatedSupplierAssetDTO;
        }

        //_______________Delete  supplier asset by ID_________________ 
        public async Task<bool> DeleteSupplierAsset(int AssetID, int SerialNumber)
        {
            var supplierAsset = await supplierAssetRepository.GetOneByID(AssetID, SerialNumber);

            if (supplierAsset == null)
            {
                throw new KeyNotFoundException($"SupplierAsset with AssetID {AssetID} and SerialNumber {SerialNumber} not found");
            }

            await supplierAssetRepository.Delete(supplierAsset);

            return true;
        }
    }
}
