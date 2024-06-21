using DTOs.DTOs.Assets;
using Models.Models;

namespace Services.Services.Interface
{
    public interface IAssetServices
    {
        public Task<bool> Create(AddOrUpdateAssetDTO assetDTO);
        public Task<ReadAssetDTO> ReadByID(int id);
        public Task<List<ReadAssetDTO>> SearchByName(string name);
        public Task<List<ReadAssetDTO>> SearchByCategory(int categoryID);
        public Task<ReadAssetDTO> Update(AddOrUpdateAssetDTO assetDTO, int id);
        public Task<bool> Delete(int ID);
    }
}
