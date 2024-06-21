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
            var findasset = await assetRepository.ReadByName(assetDTO.AssetName);

            if (findasset != null)
            {
                throw new AggregateException ("This Asset already exists.");
            }
            else
            {
                assetDTO.AssetName = char.ToUpper(assetDTO.AssetName[0]) + assetDTO.AssetName.Substring(1).ToLower();
                var newAsset = mapper.Map<Asset>(assetDTO);
                await assetRepository.Create(newAsset);
                return true;
            }
        }

        // __________________________ Read Assets ___________________________
        public async Task<ReadAssetDTO> ReadByID(int id)
        {
            var asset = await assetRepository.ReadByID(id);
            if (asset != null)
            {
                return mapper.Map<ReadAssetDTO>(asset);
            }
            else
            {
                throw new AggregateException("There is no asset by this ID.");
            }
        }
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
        public async Task<ReadAssetDTO> Update(AddOrUpdateAssetDTO assetDTO, int id)
        {
            var asset = await assetRepository.ReadByID(id);
            if (asset == null)
            {
                throw new AggregateException("There is no asset by this ID.");
            }
            else
            {
                assetDTO.AssetName = char.ToUpper(assetDTO.AssetName[0]) + assetDTO.AssetName.Substring(1).ToLower();
                mapper.Map(assetDTO, asset);
                await assetRepository.Update();
                return mapper.Map<ReadAssetDTO>(asset);
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

    

