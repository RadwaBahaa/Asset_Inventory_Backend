using AutoMapper;
using Repository.Classes;
using DTOs.DTOs.Warehouses;
using Services.Services.Interface;
using Models.Models;
using Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Services.Classes
{
    public class WarehouseAssetsServices : IWarehouseAssetsServices
    {
        protected WarehouseAssetRepository warehouseAssetRepository { get; set; }
        protected IMapper mapper { get; set; }

        public WarehouseAssetsServices(WarehouseAssetRepository warehouseAssetRepository, IMapper mapper)
        {
            this.warehouseAssetRepository = warehouseAssetRepository;
            this.mapper = mapper;
        }

        //________________ Create a new warehouse Asset ______________
        public async Task<bool> CreateWarehouseAsset(AddOrUpdateWarehouseAssetsDTO addOrUpdateWarehouseAssetsDTO)
        {
            var warehouseAsset = mapper.Map<WarehouseAsset>(addOrUpdateWarehouseAssetsDTO);

            await warehouseAssetRepository.Create(warehouseAsset);

            return true;
        }

        //_______________Read all warehouse assets_________________ 
        public async Task<List<ReadWarehouseAssetsDTO>> GetAllWarehouseAssets()
        {
            var warehouseAssets = await warehouseAssetRepository.Read();

            var warehouseAssetsDTOs = mapper.Map<List<ReadWarehouseAssetsDTO>>(warehouseAssets);

            return warehouseAssetsDTOs;
        }

        //_______________Read warehouse Asset by Serial number_________________ 
        public async Task<ReadWarehouseAssetsDTO> GetOneBySerialNumber(string serialNumber)
        {
            var warehouseAsset = await warehouseAssetRepository.GetOneBySerialNumber(serialNumber);
            var warehouseAssetDTO = mapper.Map<ReadWarehouseAssetsDTO>(warehouseAsset);

            return warehouseAssetDTO;
        }

        //_______________Update warehouse asset by ID_________________ 
        public async Task<ReadWarehouseAssetsDTO> UpdateWarehouseAsset(AddOrUpdateWarehouseAssetsDTO addOrUpdateWarehouseAssetsDTO, int AssetID, int SerialNumber)
        {
          
            var warehouseAsset = await warehouseAssetRepository.GetOneByID(AssetID, SerialNumber);

            if (warehouseAsset == null)
            {
                throw new KeyNotFoundException($"WarehouseAsset with AssetID {AssetID} and SerialNumber {SerialNumber} not found");
            }

            mapper.Map(addOrUpdateWarehouseAssetsDTO, warehouseAsset);

            await warehouseAssetRepository.Update();

            var updatedWarehouseAssetDTO = mapper.Map<ReadWarehouseAssetsDTO>(warehouseAsset);

            return updatedWarehouseAssetDTO;
        }

        //_______________Delete  warehouse asset by ID_________________ 
        public async Task<bool> DeleteWarehouseAsset(int AssetID, int SerialNumber)
        {
            var warehouseAsset = await warehouseAssetRepository.GetOneByID(AssetID, SerialNumber);

            if (warehouseAsset == null)
            {
                throw new KeyNotFoundException($"WarehouseAsset with AssetID {AssetID} and SerialNumber {SerialNumber} not found");
            }
            await warehouseAssetRepository.Delete(warehouseAsset);

            return true;
        }
    }
}
