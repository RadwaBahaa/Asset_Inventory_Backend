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

        //________________ Create a new Warehouse ______________
        public async Task<bool> CreateWarehouse(AddOrUpdateWarehouseDTO createWarehouseDTO)
        {
            if (string.IsNullOrEmpty(createWarehouseDTO.WarehouseName))
            {
                throw new ArgumentException("Warehouse name cannot be empty!");
            }

            var newWarehouse = new Warehouse
            {
                WarehouseName = createWarehouseDTO.WarehouseName,
                Location = createWarehouseDTO.Location,
                Address = createWarehouseDTO.Address,

            };
            await warehouseRepository.Create(newWarehouse);

            return true; 
        }

        //_______________Read all Warehouses _________________ 

        public async Task<List<ReadWarehouseDTO>> GetAllWarehouses()
        {
            var warehouses = await warehouseRepository.Read();
            var readWarehouseDTO = mapper.Map<List<ReadWarehouseDTO>>(warehouses);
            return readWarehouseDTO;
        }


        //_______________Read Warehouse by ID_________________ 

        public async Task<ReadWarehouseDTO> GetWarehouseByID(int WarehouseID)
        {
            var warehouse = await warehouseRepository.GetOneByID(WarehouseID);
            if (warehouse == null)
            {
                throw new KeyNotFoundException("Warehouse not found");
            }

            var readWarehouseDTO = mapper.Map<ReadWarehouseDTO>(warehouse);
            return readWarehouseDTO;
        }

        //_______________Update Warehouse by ID_________________ 

        public async Task<ReadWarehouseDTO> UpdateWarehouse(AddOrUpdateWarehouseDTO updateWarehouseDTO, int WarehouseID)
        {
            var warehouse = await warehouseRepository.GetOneByID(WarehouseID);
            if (warehouse == null)
            {
                throw new KeyNotFoundException("Warehouse not found");
            }

            if (string.IsNullOrEmpty(updateWarehouseDTO.WarehouseName))
            {
                throw new ArgumentException("Warehouse name cannot be empty!");
            }

            warehouse.WarehouseName = updateWarehouseDTO.WarehouseName;
            warehouse.Location = updateWarehouseDTO.Location;
            warehouse.Address = updateWarehouseDTO.Address;

            await warehouseRepository.Update();

            var readWarehouseDTO = mapper.Map<ReadWarehouseDTO>(warehouse);
            return readWarehouseDTO;
        }

        //_______________Delete Warehouse by ID_________________ 

        public async Task<bool> DeleteWarehouse (int WarehouseID)
        {
            var warehouse = await warehouseRepository.GetOneByID(WarehouseID);
            if (warehouse == null)
            {
                throw new KeyNotFoundException("Warehouse not found");
            }

            await warehouseRepository.Delete(warehouse);
            return true;
        }

    }
}