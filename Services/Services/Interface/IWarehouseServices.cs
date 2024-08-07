﻿using DTOs.DTOs.Warehouses;
using Models.DTOs;

namespace Services.Services.Interface
{
    public interface IWarehouseServices
    {
        public Task<bool> CreateByData(AddWarehouseDTO warehouseDTO);
        public Task<bool> CreateByGeoJSON(AddWarehouseGeoJsonDTO warehouseDTO);
        public Task<List<ReadWarehouseDTO>> ReadAll();
        public Task<List<ReadWarehouseGeoJsonDTO>> ReadAllWarehousesAsGeoJson();
        public Task<ReadWarehouseDTO> ReadByID(int warehouseID);
        public Task<ReadWarehouseDTO> ReadByName(string name);
        public Task<ReadWarehouseGeoJsonDTO> ReadWarehouseAsGeoJson(int id);
        public Task<List<ReadWarehouseDTO>> Search(string name, string address);
        public Task<ReadWarehouseDTO> Update(UpdateWarehouseDTO warehouseDTO, int warehouseID);
        public Task<bool> Delete(int warehouseID);
    }
}