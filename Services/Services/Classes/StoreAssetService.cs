using AutoMapper;
using Repository.Classes;
using DTOs.DTOs.Stores;
using Services.Services.Interface;
using Models.Models;
using Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Services.Classes
{
    public class StoreAssetService : IStoreAssetService
    {
        protected StoreAssetRepository storeAssetRepository { get; set; }
        protected IMapper mapper { get; set; }

        public StoreAssetService(StoreAssetRepository storeAssetRepository, IMapper mapper)
        {
            this.storeAssetRepository = storeAssetRepository;
            this.mapper = mapper;
        }

        //________________ Create a new store Asset ______________
        public async Task<bool> CreateStoreAsset(AddOrUpdateStoreAssetsDTO addOrUpdateStoreAssetsDTO)
        {
            var storeAsset = mapper.Map<StoreAsset>(addOrUpdateStoreAssetsDTO);

            await storeAssetRepository.Create(storeAsset);

            return true;
        }

        //_______________Read all store assets_________________ 
        public async Task<List<ReadStoreAssetsDTO>> GetAllStoreAssets()
        {
            var storeAssets = await storeAssetRepository.Read();

            var storeAssetsDTOs = mapper.Map<List<ReadStoreAssetsDTO>>(storeAssets);

            return storeAssetsDTOs;
        }

        //_______________Read store Asset by Serial number_________________ 
        public async Task<ReadStoreAssetsDTO> GetOneBySerialNumber(string serialNumber)
        {
            var storeAsset = await storeAssetRepository.GetOneBySerialNumber(serialNumber);
            var storeAssetDTO = mapper.Map<ReadStoreAssetsDTO>(storeAsset);

            return storeAssetDTO;
        }

        //_______________Update store asset by ID_________________ 
        public async Task<ReadStoreAssetsDTO> UpdateStoreAsset(AddOrUpdateStoreAssetsDTO addOrUpdateStoreAssetsDTO, int AssetID, int SerialNumber)
        {
            var storeAsset = await storeAssetRepository.GetOneByID(AssetID, SerialNumber);

            if (storeAsset == null)
            {
                throw new KeyNotFoundException($"StoreAsset with AssetID {AssetID} and SerialNumber {SerialNumber} not found");
            }

            mapper.Map(addOrUpdateStoreAssetsDTO, storeAsset);

            await storeAssetRepository.Update();

            var updatedStoreAssetDTO = mapper.Map<ReadStoreAssetsDTO>(storeAsset);

            return updatedStoreAssetDTO;
        }

        //_______________Delete  store asset by ID_________________ 
        public async Task<bool> DeleteStoreAsset(int AssetID, int SerialNumber)
        {
            var storeAsset = await storeAssetRepository.GetOneByID(AssetID, SerialNumber);

            if (storeAsset == null)
            {
                throw new KeyNotFoundException($"StoreAsset with AssetID {AssetID} and SerialNumber {SerialNumber} not found");
            }

            await storeAssetRepository.Delete(storeAsset);

            return true;
        }
    }
}
