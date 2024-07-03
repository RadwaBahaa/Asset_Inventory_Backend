using AutoMapper;
using DTOs.DTOs.Assets;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repository.Interfaces;
using Services.Services.Interface;

namespace Services.Services.Classes
{
    public class AssetServices : IAssetServices
    {
        protected IAssetRepository assetRepository;
        protected ICategoryRepository categoryRepository;
        protected IMapper mapper;
        public AssetServices(IAssetRepository assetRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.assetRepository = assetRepository;
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        // __________________________ Create Asset ___________________________
        public async Task<bool> Create(AddAssetDTO assetDTO)
        {
            if (assetDTO == null || assetDTO.AssetName == null)
            {
                throw new AggregateException("There is no data in body");
            }

            var findAsset = await assetRepository.ReadByName(assetDTO.AssetName);
            if (findAsset != null)
            {
                throw new AggregateException("This Asset already exists.");
            }

            var findCategory = await categoryRepository.ReadByID(assetDTO.CategoryID);
            if (findCategory == null)
            {
                throw new KeyNotFoundException("There is no Category by this ID");
            }

            var newAsset = mapper.Map<Asset>(assetDTO);
            await assetRepository.Create(newAsset);
            return true;
        }

        // __________________________ Read Assets ___________________________
        public async Task<List<ReadAssetDTO>> ReadAll()
        {
            var assets = await assetRepository.Read();
            var mappedAssets = await assets
                .Include(a => a.Category)
                .Select(a => mapper.Map<ReadAssetDTO>(a))
                .ToListAsync();
            return mappedAssets;
        }
        public async Task<ReadAssetDTO> ReadByID(int ID)
        {
            var asset = await assetRepository.ReadByID(ID);
            return mapper.Map<ReadAssetDTO>(asset);
        }
        public async Task<ReadAssetDTO> ReadByName(string name)
        {
            var asset = await assetRepository.ReadByName(name);
            return mapper.Map<ReadAssetDTO>(asset);

        }

        // __________________________ Search for Assets ___________________________
        public async Task<List<ReadAssetDTO>> Search(string? name, string? categoryName)
        {
            var assetsList = await assetRepository.Search(name, categoryName);
            return mapper.Map<List<ReadAssetDTO>>(assetsList);
        }

        // __________________________ Update an Asssets ___________________________
        public async Task<ReadAssetDTO> Update(UpdatAssetDTO assetDTO, int ID)
        {
            var findAsset = await assetRepository.ReadByID(ID);
            if (findAsset == null)
            {
                throw new KeyNotFoundException("There is no asset by this ID.");
            }
            assetDTO.AssetName = assetDTO.AssetName ?? findAsset.AssetName;
            assetDTO.Price = assetDTO.Price ?? findAsset.Price;
            assetDTO.CategoryID = assetDTO.CategoryID ?? findAsset.CategoryID;
            assetDTO.Description = assetDTO.Description ?? findAsset.Description;

            mapper.Map(assetDTO, findAsset);
            await assetRepository.Update();
            return mapper.Map<ReadAssetDTO>(findAsset);
        }

        // __________________________ Delete an Asset ___________________________
        public async Task<bool> Delete(int ID)
        {
            var findasset = await assetRepository.ReadByID(ID);
            if (findasset == null)
            {
                throw new KeyNotFoundException("There is no asset by this ID.");
            }

            await assetRepository.Delete(findasset);
            return true;
        }
    }
}



