using DTOs.DTOs.Assets;
using Models.Models;



namespace Services.Services.Interface
{
    public interface IAssetServices
    {
        public Task<ReadAssetDTO> Create(AddOrUpdateAssetDTO assetDTO);

        public Task<ReadAssetDTO> GetOneByID(int ID);

        public Task<List<Asset>> SearchByName(string name);
        public Task<List<Asset>> SearchByCategory(Category category);



        //searchby name and category 
        public Task<bool> Update(AddOrUpdateAssetDTO assetDTO, int ID);

        public Task<bool> Delete(int ID);
    }
}
