using AutoMapper;
using DTOs.DTOs.Stores;
using Services.Services.Interface;
using Models.Models;
using Repository.Interfaces;

namespace Services.Services.Classes
{
    public class StoreAssetsServices : IStoreAssetsServices
    {
        protected IStoreAssetRepository storeAssetRepository { get; set; }
        protected IMapper mapper { get; set; }

        public StoreAssetsServices(IStoreAssetRepository storeAssetRepository, IMapper mapper)
        {
            this.storeAssetRepository = storeAssetRepository;
            this.mapper = mapper;
        }

        //________________ Create a new store Asset ______________
        public async Task<bool> Create(AddOrUpdateStoreAssetsDTO storeAssetsDTO)
        {
            if (storeAssetsDTO == null)
            {
                throw new AggregateException("There is no data in body");
            }
            else
            {
                var storeAsset = mapper.Map<StoreAsset>(storeAssetsDTO);
                await storeAssetRepository.Create(storeAsset);
                return true;
            }
        }

        //_______________Read store assets_________________ 
        public async Task<List<ReadStoreAssetsDTO>> ReadAll()
        {
            var storeAssets = await storeAssetRepository.Read();
            if (storeAssets.Any())
            {
                return mapper.Map<List<ReadStoreAssetsDTO>>(storeAssets);
            }
            else
            {
                throw new AggregateException("There are no assets.");
            }
        }
        public async Task<ReadStoreAssetsDTO> ReadBySerialNumber(string serialNumber)
        {
            var storeAsset = await storeAssetRepository.ReadBySerialNumber(serialNumber);
            if (storeAsset != null)
            {
                return mapper.Map<ReadStoreAssetsDTO>(storeAsset);
            }
            else
            {
                throw new AggregateException("There is no asset by this Serial Number.");
            }
        }

        //_______________Update store asset by ID_________________ 
        public async Task<ReadStoreAssetsDTO> Update(AddOrUpdateStoreAssetsDTO addOrUpdateStoreAssetsDTO, int AssetID, int SerialNumber)
        {
            var storeAsset = await storeAssetRepository.ReadByID(AssetID, SerialNumber);
            if (storeAsset == null)
            {
                throw new AggregateException("There is no asset by this ID and Serial Number.");
            }
            else
            {
                mapper.Map(addOrUpdateStoreAssetsDTO, storeAsset);
                await storeAssetRepository.Update();
                return mapper.Map<ReadStoreAssetsDTO>(storeAsset);
            }
        }

        //_______________Delete store asset by ID_________________ 
        public async Task<bool> Delete(int AssetID, int SerialNumber)
        {
            var storeAsset = await storeAssetRepository.ReadByID(AssetID, SerialNumber);

            if (storeAsset == null)
            {
                throw new AggregateException("There is no asset by this ID and Serial Number.");
            }
            else
            {
                await storeAssetRepository.Delete(storeAsset);
                return true;
            }
        }
    }
}
