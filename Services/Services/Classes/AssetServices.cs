using AutoMapper;
using DTOs.DTOs.Assets;
using Models.Models;
using Repository.Interfaces;
using Services.Services.Interface;

namespace Services.Services.Classes
{
    public class AssetServices : IAssetServices
    {
        protected IAssetRepository assetRepository;
        protected IMapper mapper;
        public AssetServices(IAssetRepository assetRepository, IMapper mapper)
        {
            this.assetRepository = assetRepository;
            this.mapper = mapper;
        }

        // __________________________ Create Asset ___________________________
        public async Task<bool> Create(AddOrUpdateAssetDTO assetDTO)
        {
            if (assetDTO == null || assetDTO.AssetName == null)
            {
                throw new AggregateException("There is no data in body");
            }
            else
            {
                var findAsset = await assetRepository.ReadByName(assetDTO.AssetName);
                if (findAsset != null)
                {
                    throw new AggregateException("This Asset already exists.");
                }
                else
                {
                    assetDTO.AssetName = char.ToUpper(assetDTO.AssetName[0]) + assetDTO.AssetName.Substring(1).ToLower();
                    var newAsset = mapper.Map<Asset>(assetDTO);
                    await assetRepository.Create(newAsset);
                    return true;
                }
            }
        }

        // __________________________ Read Assets ___________________________
        public async Task<List<ReadAssetDTO>> ReadAll()
        {
            var assets = await assetRepository.Read();
            if (assets.Any())
            {
                return mapper.Map<List<ReadAssetDTO>>(assets);
            }
            else
            {
                throw new AggregateException("There are no assets.");
            }
        }
        public async Task<ReadAssetDTO> ReadByID(int ID)
        {
            var asset = await assetRepository.ReadByID(ID);
            if (asset != null)
            {
                return mapper.Map<ReadAssetDTO>(asset);
            }
            else
            {
                throw new AggregateException("There is no asset by this ID.");
            }
        }
        public async Task<ReadAssetDTO> ReadByName(string name)
        {
            var asset = await assetRepository.ReadByName(name);
            if (asset != null)
            {
                return mapper.Map<ReadAssetDTO>(asset);
            }
            else
            {
                throw new AggregateException("There is no asset by this name.");
            }
        }

        // __________________________ Search for Assets ___________________________
        public async Task<List<ReadAssetDTO>> SearchByName(string name)
        {
            var assetsList = await assetRepository.SearchByName(name);
            if (assetsList.Any())
            {
                return mapper.Map<List<ReadAssetDTO>>(assetsList);
            }
            else
            {
                throw new AggregateException("There is no assets.");
            }
        }
        public async Task<List<ReadAssetDTO>> SearchByCategory(int categoryID)
        {
            var assetsList = await assetRepository.SearchByCategory(categoryID);
            if (assetsList.Any())
            {
                return mapper.Map<List<ReadAssetDTO>>(assetsList);
            }
            else
            {
                throw new AggregateException("There is no assets.");
            }
        }

        // __________________________ Update an Asssets ___________________________
        public async Task<ReadAssetDTO> Update(AddOrUpdateAssetDTO assetDTO, int ID)
        {
            var findAsset = await assetRepository.ReadByID(ID);
            if (findAsset == null)
            {
                throw new AggregateException("There is no asset by this ID.");
            }
            else
            {
                assetDTO.AssetName = char.ToUpper(assetDTO.AssetName[0]) + assetDTO.AssetName.Substring(1).ToLower();
                mapper.Map(assetDTO, findAsset);
                await assetRepository.Update();
                return mapper.Map<ReadAssetDTO>(findAsset);
            }
        }

        // __________________________ Delete an Asset ___________________________
        public async Task<bool> Delete(int ID)
        {
            var findasset = await assetRepository.ReadByID(ID);
            if (findasset == null)
            {
                throw new AggregateException("There is no asset by this ID.");
            }
            else
            {
                await assetRepository.Delete(findasset);
                return true;
            }
        }
    }
}



