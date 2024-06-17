﻿using Models.Models;

namespace Repository.Interfaces
{
    public interface IAssetRepository : IGenericRepository<Asset>
    {
        public Task<Asset> GetOneByID(int id);
        public Task<Asset> GetOneByName(string name);
        public Task<List<Asset>> SearchByName(string name);
        public Task<List<Asset>> SearchByCategory(Category category);

    }
}
