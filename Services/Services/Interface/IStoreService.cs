using DTOs.DTOs.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Interface
{
    public interface IStoreService
    {
        public Task<bool> CreateStore (AddOrUpdateStoreDTO addOrUpdateStoreDTO);
        public Task<ReadStoreDTO> GetAllStores();
        public Task<ReadStoreDTO> GetStoreByID(int StoreID);
        public Task<ReadStoreDTO> UpdateStore(AddOrUpdateStoreDTO addOrUpdateStoreDTO, int StoreID);
        public Task<ReadStoreDTO> DeleteStore(int StoreID);
    }
}
