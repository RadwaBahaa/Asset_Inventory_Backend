﻿using AutoMapper;
using DTOs.DTOs.Stores;
using Models.DTOs;
using Repository.Classes;

namespace Services.Services.Interface
{
    public interface IStoreServices
    {
        public Task<bool> CreateByData(AddOrUpdateStoreDTO storeDTO);
        public Task<bool> CreateByGeoJSON(AddStoreGeoJsonDTO storeDTO);
        public Task<List<ReadStoreDTO>> ReadAll();
        public Task<List<ReadStoreGeoJsonDTO>> ReadAllStoresAsGeoJson();
        public Task<ReadStoreDTO> ReadByID(int storeID);
        public Task<ReadStoreGeoJsonDTO> ReadStoreAsGeoJson(int id);
        public Task<List<ReadStoreDTO>> Search(string name, string address);
        public Task<ReadStoreDTO> Update(AddOrUpdateStoreDTO storeDTO, int storeID);
        public Task<bool> Delete(int storeID);
    }
}
