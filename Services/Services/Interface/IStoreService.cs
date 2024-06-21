﻿using DTOs.DTOs.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Interface
{
    public interface IStoreServices
    {
        public Task<bool> CreateStore (AddOrUpdateStoreDTO addOrUpdateStoreDTO);
        Task<List<ReadStoreDTO>> GetAllStores();
        public Task<ReadStoreDTO> GetStoreByID(int StoreID);
        public Task<ReadStoreDTO> UpdateStore(AddOrUpdateStoreDTO addOrUpdateStoreDTO, int StoreID);
        public Task<bool> DeleteStore(int StoreID);
    }
}
