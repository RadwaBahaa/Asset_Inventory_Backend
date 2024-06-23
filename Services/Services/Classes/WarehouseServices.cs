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
        public async Task<bool> Create(AddOrUpdateWarehouseDTO warehouseDTO)
        {

            if (warehouseDTO == null)
            {
                throw new ArgumentException("Warehouse name cannot be empty!");
            }
            else
            {
                var findWarehouseByName = await warehouseRepository.ReadByName(warehouseDTO.WarehouseName);
                var findWarehouseByLocation = await warehouseRepository.ReadByLocation(warehouseDTO.Longitude, warehouseDTO.Latitude);
                if (findWarehouseByName != null || findWarehouseByLocation != null)
                {
                    throw new AggregateException("This Warehouse already exists.");
                }
                else
                {
                    var newWarehouse = new Warehouse
                    {
                        WarehouseName = warehouseDTO.WarehouseName,
                        Location = new Point(warehouseDTO.Longitude.Value, warehouseDTO.Latitude.Value) { SRID = 4326 },
                        Address = warehouseDTO.Address,
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
                .Include(s => s.WarehouseAssets)
                .ToListAsync();
            return mapper.Map<List<ReadWarehouseDTO>>(allWarehouses);
        }
        public async Task<List<WarehouseGeoJsonDTO>> ReadAllWarehousesAsGeoJson()
        {
            var warehouses = await warehouseRepository.Read();
            var allWarehouses = await warehouses
                .Include(s => s.WarehouseAssets)
                .Select(warehouse => new WarehouseGeoJsonDTO(warehouse))
                .ToListAsync();
            return allWarehouses;
        }
        public async Task<ReadWarehouseDTO> ReadByID(int warehouseID)
        {
            var warehouse = await warehouseRepository.ReadByID(warehouseID);
            return mapper.Map<ReadWarehouseDTO>(warehouse);
        }
        public async Task<WarehouseGeoJsonDTO> ReadWarehouseAsGeoJson(int id)
        {
            var warehouse = await warehouseRepository.ReadByID(id);
            return new WarehouseGeoJsonDTO(warehouse);
        }

        //_______________Search for warehouse _____________
        public async Task<List<ReadWarehouseDTO>> SearchByName(string warehouseName)
        {
            var warehousesList = await warehouseRepository.SearchByName(warehouseName);
            return mapper.Map<List<ReadWarehouseDTO>>(warehousesList);
        }
        public async Task<List<ReadWarehouseDTO>> SearchByAddress(string Address)
        {
            var warehousesList = await warehouseRepository.SearchByAddress(Address);
            return mapper.Map<List<ReadWarehouseDTO>>(warehousesList);
        }

        //_______________Update warehouse by ID_________________ 
        public async Task<ReadWarehouseDTO> Update(AddOrUpdateWarehouseDTO warehouseDTO, int warehouseID)
        {
            var warehouse = await warehouseRepository.ReadByID(warehouseID);
            if (warehouse == null)
            {
                throw new KeyNotFoundException("There is no warehouse by this ID.");
            }
            else
            {
                warehouse.WarehouseName = warehouseDTO.WarehouseName;
                warehouse.Location = new Point(warehouseDTO.Longitude.Value, warehouseDTO.Latitude.Value) { SRID = 4326 };
                warehouse.Address = warehouseDTO.Address;
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