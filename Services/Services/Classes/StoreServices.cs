using AutoMapper;
using DTOs.DTOs.Stores;
using Services.Services.Interface;
using Models.Models;
using Repository.Interfaces;
using NetTopologySuite.Geometries;
using Microsoft.EntityFrameworkCore;
using Models.DTOs;

namespace Services.Services.Classes
{
    public class StoreServices : IStoreServices
    {
        protected IStoreRepository storeRepository { get; set; }
        protected IMapper mapper { get; set; }
        public StoreServices(IStoreRepository storeRepository, IMapper mapper)
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
                var findStoreByName = await storeRepository.ReadByName(storeDTO.StoreName);
                var findStoreByLocation = await storeRepository.ReadByLocation(storeDTO.Longitude, storeDTO.Latitude);
                if (findStoreByName != null || findStoreByLocation !=null)
                {
                    throw new AggregateException("This Store already exists.");
                }
                else
                {
                var newStore = new Store
                {
                    StoreName = storeDTO.StoreName,
                    Location = new Point(storeDTO.Longitude.Value, storeDTO.Latitude.Value) { SRID = 4326 },
                    Address = storeDTO.Address,
                };
                await storeRepository.Create(newStore);
                return true;
                }
            }
        }

        //_______________Read stores _________________ 
        public async Task<List<ReadStoreDTO>> ReadAll()
        {
            var stores = await storeRepository.Read();
            var allStores = await stores
                .Include(s => s.StoreAssets)
                .ToListAsync();
            return mapper.Map<List<ReadStoreDTO>>(allStores);
        }
        public async Task<List<StoreGeoJsonDTO>> ReadAllStoresAsGeoJson()
        {
            var stores = await storeRepository.Read();
            var allStores = await stores
                .Include(s => s.StoreAssets)
                .Select(store => new StoreGeoJsonDTO(store))
                .ToListAsync();
            return allStores;
        }
        public async Task<ReadStoreDTO> ReadByID(int storeID)
        {
            var store = await storeRepository.ReadByID(storeID);
            return mapper.Map<ReadStoreDTO>(store);
        }
        public async Task<StoreGeoJsonDTO> ReadStoreAsGeoJson(int id)
        {
            var store = await storeRepository.ReadByID(id);
            return new StoreGeoJsonDTO(store);
        }

        //_______________Search for store _____________
        public async Task<List<ReadStoreDTO>> SearchByName(string storeName)
        {
            var storesList = await storeRepository.SearchByName(storeName);
            return mapper.Map<List<ReadStoreDTO>>(storesList);
        }
        public async Task<List<ReadStoreDTO>> SearchByAddress(string Address)
        {
            var storesList = await storeRepository.SearchByAddress(Address);
            return mapper.Map<List<ReadStoreDTO>>(storesList);
        }

        //_______________Update store by ID_________________ 
        public async Task<ReadStoreDTO> Update(AddOrUpdateStoreDTO storeDTO, int storeID)
        {
            var store = await storeRepository.ReadByID(storeID);
            if (store == null)
            {
                throw new KeyNotFoundException("There is no store by this ID.");
            }
            else
            {
                store.StoreName = storeDTO.StoreName;
                store.Location = new Point(storeDTO.Longitude.Value, storeDTO.Latitude.Value) { SRID = 4326 };
                store.Address = storeDTO.Address;
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
                throw new KeyNotFoundException("There is no store by this ID.");
            }
            else
            {
                await storeRepository.Delete(store);
                return true;
            }
        }
    }
}