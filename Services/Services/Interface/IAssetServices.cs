using AutoMapper;
using DTOs.DTOs.Assets;
using Models.Models;
using Repository.Classes;

namespace Services.Services.Interface
{
    public interface IAssetServices
    {
        public Task<bool> Create(AddOrUpdateAssetDTO assetDTO);
        public Task<List<ReadAssetDTO>> ReadAll();
        public Task<ReadAssetDTO> ReadByID(int ID);
        public Task<ReadAssetDTO> ReadByName(string name);
        public Task<List<ReadAssetDTO>> SearchByName(string name);
        public Task<List<ReadAssetDTO>> SearchByCategory(int categoryID);
        public Task<ReadAssetDTO> Update(AddOrUpdateAssetDTO assetDTO, int ID);
        public Task<bool> Delete(int ID);
    }
}
