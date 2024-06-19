using AutoMapper;
using DTOs.DTOs.Assets;
using DTOs.DTOs.Categories;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models.Models;
using Repository.Classes;
using Repository.Interfaces;
using Services.Services.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<ReadAssetDTO> Create(AddOrUpdateAssetDTO assetDTO)
        {
            var findasset = await assetRepository.GetOneByName(assetDTO.AssetName);

            if (findasset != null)
            {
                return null;
            }
            else
            {
                assetDTO.AssetName = char.ToUpper(assetDTO.AssetName[0]) + assetDTO.AssetName.Substring(1).ToLower();
                var createAsset = mapper.Map<Asset>(assetDTO);
                await assetRepository.Create(createAsset);
                var Assets = await assetRepository.GetOneByName(assetDTO.AssetName);

                var mappingasset = mapper.Map<ReadAssetDTO>(createAsset);
                return mappingasset;
            }

        }

   

        // __________________________ GetOne asset ___________________________


        public async Task<ReadAssetDTO> GetOneByName(string name)
        {
            var asset = await assetRepository.GetOneByName(name);

            // Process the retrieved assets
            
            return mapper.Map<ReadAssetDTO>(asset);
           
        }




        // __________________________ Search assets by name and category ___________________________

        public async Task<List<Asset>> SearchByName(string name)
        {
            var assetsList = await assetRepository.SearchByName(name);
                
            return assetsList;
        }
        public async Task<List<Asset>> SearchByCategory(Category category)
        {
            var assetsList = await assetRepository.SearchByCategory(category);
               
            return assetsList;
        }


        // __________________________ Update a Categories ___________________________
        public async Task<bool> Update(AddOrUpdateAssetDTO assetDTO, int ID)
        {
            var findasset = await assetRepository.GetOneByID(ID);
            if (findasset == null)
            {
                return false;
            }
            else
            {
                // Make the initial letter of the asset name capitalized.
                assetDTO.AssetName = char.ToUpper(assetDTO.AssetName[0]) + assetDTO.AssetName.Substring(1).ToLower();
                // Update the asset
                mapper.Map(assetDTO, findasset);
                await assetRepository.Update();
                return true;
            }
        }
        // __________________________ Delete an asset ___________________________
        public async Task<bool> Delete(int ID)
        {
            var findasset = await assetRepository.GetOneByID(ID);
            if (findasset == null)
            {
                return false;
            }
            else
            {
                await assetRepository.Delete(findasset);
                return true;
            }
        }
    }
}

    

