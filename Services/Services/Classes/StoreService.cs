using AutoMapper;
using Repository.Classes;
using DTOs.DTOs.Stores;
using Services.Services.Interface;
using Models.Models;

namespace Services.Services.Classes
{
    public class StoreServices : IStoreServices
    {
        protected StoreRepository storeRepository { get; set; }
        protected IMapper mapper { get; set; }
        public StoreServices(StoreRepository storeRepository, IMapper mapper)
        {
            this.storeRepository = storeRepository;
            this.mapper = mapper;
        }

        //________________ Create a new store ______________
        public async Task<bool> CreateStore(AddOrUpdateStoreDTO createStoreDTO)
        {
            if (string.IsNullOrEmpty(createStoreDTO.StoreName))
            {
                throw new ArgumentException("Store name cannot be empty!");
            }

            var newStore = new Store
            {
                StoreName = createStoreDTO.StoreName,
                Location = createStoreDTO.Location,
                Address = createStoreDTO.Address,

            };
            await storeRepository.Create(newStore);

            return true; 
        }

        //_______________Read all stores _________________ 

        public async Task<List<ReadStoreDTO>> GetAllStores()
        {
            var stores = await storeRepository.Read();
            var readStoreDTO = mapper.Map<List<ReadStoreDTO>>(stores);
            return readStoreDTO;
        }


        //_______________Read store by ID_________________ 
        public async Task<ReadStoreDTO> GetStoreByID(int StoreID)
        {
            var store = await storeRepository.GetOneByID(StoreID);
            if (store == null)
            {
                throw new KeyNotFoundException("Store not found");
            }

            var readStoreDTO = mapper.Map<ReadStoreDTO>(store);
            return readStoreDTO;
        }

        //_______________Update store by ID_________________ 

        public async Task<ReadStoreDTO> UpdateStore(AddOrUpdateStoreDTO updateStoreDTO, int StoreID)
        {
            var store = await storeRepository.GetOneByID(StoreID);
            if (store == null)
            {
                throw new KeyNotFoundException("Store not found");
            }

            if (string.IsNullOrEmpty(updateStoreDTO.StoreName))
            {
                throw new ArgumentException("Store name cannot be empty!");
            }

            store.StoreName = updateStoreDTO.StoreName;
            store.Location = updateStoreDTO.Location;
            store.Address = updateStoreDTO.Address;

            await storeRepository.Update();

            var readStoreDTO = mapper.Map<ReadStoreDTO>(store);
            return readStoreDTO;
        }

        //_______________Delete store by ID_________________ 

        public async Task<bool> DeleteStore(int StoreID)
        {
            var store = await storeRepository.GetOneByID(StoreID);
            if (store == null)
            {
                throw new KeyNotFoundException("Store not found");
            }

            await storeRepository.Delete(store);
            return true;
        }
    }
}