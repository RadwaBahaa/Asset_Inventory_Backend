using AutoMapper;
using DTOs.DTOs.Stores;
using Services.Services.Interface;
using Models.Models;
using Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Services.Services.Classes
{
    public class StoreAssetsServices : IStoreAssetsServices
    {
        protected IStoreAssetRepository storeAssetRepository { get; set; }
        protected IAssetRepository assetRepository { get; set; }
        protected IStoreRepository storeRepository { get; set; }
        protected IMapper mapper { get; set; }

        public StoreAssetsServices(IStoreAssetRepository storeAssetRepository, IAssetRepository assetRepository, IStoreRepository storeRepository, IMapper mapper)
        {
            this.storeAssetRepository = storeAssetRepository;
            this.assetRepository = assetRepository;
            this.storeRepository = storeRepository;
            this.mapper = mapper;
        }

        //________________ Create a new store Asset ______________
        public async Task<bool> Create(int storeID, AddStoreAssetsDTO storeAssetsDTO)
        {
            if (storeAssetsDTO == null)
            {
                throw new AggregateException("There is no data in body");
            }
            else
            {
                var findStore = await storeRepository.ReadByID(storeID);
                if (findStore == null)
                {
                    throw new AggregateException("There is no store by this name.");
                }
                var findAsset = await assetRepository.ReadByID(storeAssetsDTO.AssetID);
                if (findAsset == null)
                {
                    throw new AggregateException("There is no asset by this name.");
                }
                var findStoreAsset = await storeAssetRepository.ReadOne(storeID, storeAssetsDTO.AssetID, storeAssetsDTO.SerialNumber);
                if (findStoreAsset != null)
                {
                    throw new AggregateException("There is an asset by the same data.");
                }
                var storeAsset = mapper.Map<StoreAsset>(storeAssetsDTO);
                storeAsset.StoreID = storeID;
                await storeAssetRepository.Create(storeAsset);
                return true;
            }
        }

        //_______________Read store assets_________________ 
        public async Task<List<ReadStoreAssetsDTO>> ReadAll()
        {
            var storeAssets = await storeAssetRepository.Read();
            var storeAssetsList = await storeAssets
                .Include(sa => sa.Asset)
                    .ThenInclude(a => a.Category)
                .ToListAsync();
            return mapper.Map<List<ReadStoreAssetsDTO>>(storeAssetsList);
        }
        public async Task<List<ReadStoreAssetsDTO>> ReadByStore(int storeID)
        {
            var storeAssets = await storeAssetRepository.ReadByStore(storeID);
            return mapper.Map<List<ReadStoreAssetsDTO>>(storeAssets);
        }
        //_______________Search store assets_________________ 
        public async Task<List<ReadStoreAssetsDTO>> Search(int storeID, string? name, string? serialNumber)
        {
            var storeAsset = await storeAssetRepository.Search(storeID, name, serialNumber);
            return mapper.Map<List<ReadStoreAssetsDTO>>(storeAsset);
        }

        //_______________Update store asset by ID_________________ 
        public async Task<ReadStoreAssetsDTO> Update(UpdateStoreAssetsDTO storeAssetsDTO, int storeID, int assetID, string serialNumber)
        {
            var storeAsset = await storeAssetRepository.ReadOne(storeID, assetID, serialNumber);
            if (storeAsset == null)
            {
                throw new KeyNotFoundException("There is no asset by this ID and Serial Number.");
            }
            storeAssetsDTO.SerialNumber = storeAssetsDTO.SerialNumber ?? storeAsset.SerialNumber;
            storeAssetsDTO.Count = storeAssetsDTO.Count ?? storeAsset.Count;

            mapper.Map(storeAssetsDTO, storeAsset);
            await storeAssetRepository.Update();
            return mapper.Map<ReadStoreAssetsDTO>(storeAsset);
        }

        //_______________Delete store asset by ID_________________ 
        public async Task<bool> Delete(int storeID, int assetID, string serialNumber)
        {
            var storeAsset = await storeAssetRepository.ReadOne(storeID, assetID, serialNumber);

            if (storeAsset == null)
            {
                throw new KeyNotFoundException("There is no asset by this ID and Serial Number.");
            }

            await storeAssetRepository.Delete(storeAsset);
            return true;
        }
    }
}
