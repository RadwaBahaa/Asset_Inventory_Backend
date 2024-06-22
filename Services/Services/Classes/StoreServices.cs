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

        //________________ Create store ______________
        public async Task<bool> Create(AddOrUpdateStoreDTO storeDTO)
        {
            if (storeDTO == null)
            {
                throw new ArgumentException("Store name cannot be empty!");
            }
            else
            {
                var newStore = new Store
                {
                    StoreName = storeDTO.StoreName,
                    Location = storeDTO.Location,
                    Address = storeDTO.Address,
                };
                await storeRepository.Create(newStore);
                return true;
            }
        }

        //_______________Read stores _________________ 
        public async Task<List<ReadStoreDTO>> ReadAll()
        {
            var stores = await storeRepository.Read();
            if (stores.Any())
            {
                throw new AggregateException("There are no stores.");
            }
            else
            {
                return mapper.Map<List<ReadStoreDTO>>(stores);
            }
        }
        public async Task<ReadStoreDTO> ReadByID(int storeID)
        {
            var store = await storeRepository.ReadByID(storeID);
            if (store != null)
            {
                return mapper.Map<ReadStoreDTO>(store);
            }
            else
            {
                throw new AggregateException("There are no stores.");
            }
        }
        //_______________Search for store _____________
        public async Task<List<ReadStoreDTO>> SearchByName(string storeName)
        {
            var storesList = await storeRepository.SearchByName(storeName);
            if (storesList.Any())
            {
                return mapper.Map<List<ReadStoreDTO>>(storesList);
            }
            else
            {
                throw new AggregateException("There are no stores.");
            }
        }
        public async Task<List<ReadStoreDTO>> SearchByAddress(string Address)
        {
            var storesList = await storeRepository.SearchByAddress(Address);
            if (storesList.Any())
            {
                return mapper.Map<List<ReadStoreDTO>>(storesList);
            }
            else
            {
                throw new AggregateException("There are no stores.");
            }
        }

        //_______________Update store by ID_________________ 

        public async Task<ReadStoreDTO> Update(AddOrUpdateStoreDTO updateStoreDTO, int storeID)
        {
            var store = await storeRepository.ReadByID(storeID);
            if (store == null)
            {
                throw new AggregateException("There is no store by this ID.");
            }
            else
            {
                store.StoreName = updateStoreDTO.StoreName;
                store.Location = updateStoreDTO.Location;
                store.Address = updateStoreDTO.Address;
                await storeRepository.Update();
                return mapper.Map<ReadStoreDTO>(store);
            }
        }

        //_______________Delete store by ID_________________ 

        public async Task<bool> Delete(int storeID)
        {
            var store = await storeRepository.ReadByID(storeID);
            if (store == null)
            {
                throw new AggregateException("There is no store by this ID.");
            }
            else
            {
                await storeRepository.Delete(store);
                return true;
            }
        }
    }
}