using DTOs.DTOs.Assets;

namespace Services.Services.Interface
{
    public interface IAssetServices
    {
        public Task<bool> Create(AddAssetDTO assetDTO);
        public Task<List<ReadAssetDTO>> ReadAll();
        public Task<ReadAssetDTO> ReadByID(int ID);
        public Task<ReadAssetDTO> ReadByName(string name);
        public Task<List<ReadAssetDTO>> Search(string? name, string? categoryName);
        public Task<ReadAssetDTO> Update(UpdatAssetDTO assetDTO, int ID);
        public Task<bool> Delete(int ID);
    }
}
