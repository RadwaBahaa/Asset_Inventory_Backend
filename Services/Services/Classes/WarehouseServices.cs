using AutoMapper;
using Repository.Classes;
using DTOs.DTOs.Warehouses;
using Services.Services.Interface;
using Models.Models;

namespace Services.Services.Classes
{
    public class WarehouseServices : IWarehouseServices
    {
        protected WarehouseRepository warehouseRepository { get; set; }
        protected IMapper mapper { get; set; }
        public WarehouseServices(WarehouseRepository warehouseRepository, IMapper mapper)
        {
            this.warehouseRepository = warehouseRepository;
            this.mapper = mapper;
        }

        //________________ Create warehouse ______________
        public async Task<bool> Create(AddOrUpdateWarehouseDTO warehouseDTO)
        {
            if (warehouseDTO == null)
            {
                throw new ArgumentException("Warehouse name cannot be empty!");
            }
            else
            {
                var newWarehouse = new Warehouse
                {
                    WarehouseName = warehouseDTO.WarehouseName,
                    Location = warehouseDTO.Location,
                    Address = warehouseDTO.Address,
                };
                await warehouseRepository.Create(newWarehouse);
                return true;
            }
        }

        //_______________Read warehouses _________________ 
        public async Task<List<ReadWarehouseDTO>> ReadAll()
        {
            var warehouses = await warehouseRepository.Read();
            if (warehouses.Any())
            {
                throw new AggregateException("There are no warehouses.");
            }
            else
            {
                return mapper.Map<List<ReadWarehouseDTO>>(warehouses);
            }
        }
        public async Task<ReadWarehouseDTO> ReadByID(int warehouseID)
        {
            var warehouse = await warehouseRepository.ReadByID(warehouseID);
            if (warehouse != null)
            {
                return mapper.Map<ReadWarehouseDTO>(warehouse);
            }
            else
            {
                throw new AggregateException("There are no warehouses.");
            }
        }
        //_______________Search for warehouse _____________
        public async Task<List<ReadWarehouseDTO>> SearchByName(string warehouseName)
        {
            var warehousesList = await warehouseRepository.SearchByName(warehouseName);
            if (warehousesList.Any())
            {
                return mapper.Map<List<ReadWarehouseDTO>>(warehousesList);
            }
            else
            {
                throw new AggregateException("There are no warehouses.");
            }
        }
        public async Task<List<ReadWarehouseDTO>> SearchByAddress(string address)
        {
            var warehousesList = await warehouseRepository.SearchByAddress(address);
            if (warehousesList.Any())
            {
                return mapper.Map<List<ReadWarehouseDTO>>(warehousesList);
            }
            else
            {
                throw new AggregateException("There are no warehouses.");
            }
        }

        //_______________Update warehouse by ID_________________ 

        public async Task<ReadWarehouseDTO> Update(AddOrUpdateWarehouseDTO updateWarehouseDTO, int warehouseID)
        {
            var warehouse = await warehouseRepository.ReadByID(warehouseID);
            if (warehouse == null)
            {
                throw new AggregateException("There is no warehouse by this ID.");
            }
            else
            {
                warehouse.WarehouseName = updateWarehouseDTO.WarehouseName;
                warehouse.Location = updateWarehouseDTO.Location;
                warehouse.Address = updateWarehouseDTO.Address;
                await warehouseRepository.Update();
                return mapper.Map<ReadWarehouseDTO>(warehouse);
            }
        }

        //_______________Delete warehouse by ID_________________ 

        public async Task<bool> Delete(int warehouseID)
        {
            var warehouse = await warehouseRepository.ReadByID(warehouseID);
            if (warehouse == null)
            {
                throw new AggregateException("There is no warehouse by this ID.");
            }
            else
            {
                await warehouseRepository.Delete(warehouse);
                return true;
            }
        }
    }
}