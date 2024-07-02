using AutoMapper;
using DTOs.DTOs.Warehouses;
using Services.Services.Interface;
using Models.Models;
using Repository.Interfaces;
using NetTopologySuite.Geometries;
using Microsoft.EntityFrameworkCore;
using Models.DTOs;

namespace Services.Services.Classes
{
    public class WarehouseServices : IWarehouseServices
    {
        protected IWarehouseRepository warehouseRepository { get; set; }
        protected IMapper mapper { get; set; }
        public WarehouseServices(IWarehouseRepository warehouseRepository, IMapper mapper)
        {
            this.warehouseRepository = warehouseRepository;
            this.mapper = mapper;
        }

        //________________ Create warehouse ______________
        public async Task<bool> CreateByData(AddWarehouseDTO warehouseDTO)
        {

            if (warehouseDTO == null)
            {
                throw new ArgumentException("Warehouse data cannot be empty!");
            }
            else
            {
                var findWarehouseByName = await warehouseRepository.ReadByName(warehouseDTO.WarehouseName);
                var findWarehouseByLocation = await warehouseRepository.ReadByLocation(warehouseDTO.Longitude, warehouseDTO.Latitude);
                if (findWarehouseByName != null)
                {
                    throw new ArgumentException("A warehouse with this name already exists.");
                }

                if (findWarehouseByLocation != null)
                {
                    throw new ArgumentException("A warehouse at this location already exists.");
                }
                else
                {
                    var newWarehouse = new Warehouse
                    {
                        WarehouseName = warehouseDTO.WarehouseName,
                        Location = new Point(warehouseDTO.Longitude, warehouseDTO.Latitude) { SRID = 4326 },
                        Address = warehouseDTO.Address,
                    };
                    await warehouseRepository.Create(newWarehouse);
                    return true;
                }
            }
        }
        public async Task<bool> CreateByGeoJSON(AddWarehouseGeoJsonDTO warehouseDTO)
        {
            if (warehouseDTO == null)
            { throw new ArgumentException("Warehouse data cannot be empty!"); }
            else
            {
                var findWarehouseByName = await warehouseRepository.ReadByName(warehouseDTO.properties.warehouseName);
                var findWarehouseByLocation = await warehouseRepository.ReadByLocation(warehouseDTO.geometry.coordinates[0], warehouseDTO.geometry.coordinates[1]);
                if (findWarehouseByName != null)
                {
                    throw new ArgumentException("A warehouse with this name already exists.");
                }

                if (findWarehouseByLocation != null)
                {
                    throw new ArgumentException("A warehouse at this location already exists.");
                }
                else
                {
                    var newWarehouse = new Warehouse
                    {
                        WarehouseName = warehouseDTO.properties.warehouseName,
                        Location = new Point(warehouseDTO.geometry.coordinates[0], warehouseDTO.geometry.coordinates[1]) { SRID = 4326 },
                        Address = warehouseDTO.properties.address,
                    };
                    await warehouseRepository.Create(newWarehouse);
                    return true;
                }
            }
        }

        //_______________Read warehouses _________________ 
        public async Task<List<ReadWarehouseDTO>> ReadAll()
        {
            var warehouses = await warehouseRepository.Read();
            var allWarehouses = await warehouses
                .ToListAsync();
            return mapper.Map<List<ReadWarehouseDTO>>(allWarehouses);
        }
        public async Task<List<ReadWarehouseGeoJsonDTO>> ReadAllWarehousesAsGeoJson()
        {
            var warehouses = await warehouseRepository.Read();
            var allWarehouses = await warehouses
                .Select(warehouse => new ReadWarehouseGeoJsonDTO(warehouse))
                .ToListAsync();
            return allWarehouses;
        }
        public async Task<ReadWarehouseDTO> ReadByID(int warehouseID)
        {
            var warehouse = await warehouseRepository.ReadByID(warehouseID);
            return mapper.Map<ReadWarehouseDTO>(warehouse);
        }
        public async Task<ReadWarehouseDTO> ReadByName(string name)
        {
            var warehouse = await warehouseRepository.ReadByName(name);
            return mapper.Map<ReadWarehouseDTO>(warehouse);
        }
        public async Task<ReadWarehouseGeoJsonDTO> ReadWarehouseAsGeoJson(int id)
        {
            var warehouse = await warehouseRepository.ReadByID(id);
            return new ReadWarehouseGeoJsonDTO(warehouse);
        }

        //_______________Search for warehouse _____________
        public async Task<List<ReadWarehouseDTO>> Search(string name, string address)
        {
            var warehousesList = await warehouseRepository.Search(name, address);
            return mapper.Map<List<ReadWarehouseDTO>>(warehousesList);
        }

        //_______________Update warehouse by ID_________________ 
        public async Task<ReadWarehouseDTO> Update(UpdateWarehouseDTO warehouseDTO, int warehouseID)
        {
            var warehouse = await warehouseRepository.ReadByID(warehouseID);
            if (warehouse == null)
            {
                throw new KeyNotFoundException("There is no warehouse by this ID.");
            }
            else
            {
                warehouse.WarehouseName = warehouseDTO.WarehouseName ?? warehouse.WarehouseName;
                warehouse.Location = new Point(warehouseDTO.Longitude ?? warehouse.Location.X, warehouseDTO.Latitude ?? warehouse.Location.Y) { SRID = 4326 };
                warehouse.Address = warehouseDTO.Address ?? warehouse.Address;

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
                throw new KeyNotFoundException("There is no warehouse by this ID.");
            }
            else
            {
                await warehouseRepository.Delete(warehouse);
                return true;
            }
        }
    }
}