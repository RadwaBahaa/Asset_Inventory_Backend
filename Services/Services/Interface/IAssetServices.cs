﻿using DTOs.DTOs.Assets;

namespace Services.Services.Interface
{
    public interface IAssetServices
    {
        public Task<bool> Create(AddOrUpdateAssetDTO assetDTO);
        public Task<List<ReadAssetDTO>> ReadAll();
        public Task<ReadAssetDTO> ReadByID(int ID);
        public Task<ReadAssetDTO> ReadByName(string name);
        public Task<List<ReadAssetDTO>> Search(string? name, int? categoryID);
        public Task<ReadAssetDTO> Update(AddOrUpdateAssetDTO assetDTO, int ID);
        public Task<bool> Delete(int ID);
    }
}
