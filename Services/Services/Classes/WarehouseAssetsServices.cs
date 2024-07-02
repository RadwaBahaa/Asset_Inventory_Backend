using AutoMapper;
using DTOs.DTOs.Warehouses;
using Models.Models;
using Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Services.Services.Interface;

namespace Services.Services.Classes
{
    public class WarehouseAssetsServices : IWarehouseAssetsServices
    {
        protected IWarehouseAssetRepository warehouseAssetRepository { get; set; }
        protected IAssetRepository assetRepository { get; set; }
        protected IWarehouseRepository warehouseRepository { get; set; }
        protected IMapper mapper { get; set; }

        public WarehouseAssetsServices(IWarehouseAssetRepository warehouseAssetRepository, IAssetRepository assetRepository, IWarehouseRepository warehouseRepository, IMapper mapper)
        {
            this.warehouseAssetRepository = warehouseAssetRepository;
            this.assetRepository = assetRepository;
            this.warehouseRepository = warehouseRepository;
            this.mapper = mapper;
        }

        //________________ Create a new warehouse Asset ______________
        public async Task<bool> Create(int warehouseID, AddWarehouseAssetsDTO warehouseAssetsDTO)
        {
            if (warehouseAssetsDTO == null)
            {
                throw new AggregateException("There is no data in body");
            }
            else
            {
                var findWarehouse = await warehouseRepository.ReadByID(warehouseID);
                if (findWarehouse == null)
                {
                    throw new AggregateException("There is no warehouse by this name.");
                }
                var findAsset = await assetRepository.ReadByID(warehouseAssetsDTO.AssetID);
                if (findAsset == null)
                {
                    throw new AggregateException("There is no asset by this name.");
                }
                var findWarehouseAsset = await warehouseAssetRepository.ReadOne(warehouseID, warehouseAssetsDTO.AssetID, warehouseAssetsDTO.SerialNumber);
                if (findWarehouseAsset != null)
                {
                    throw new AggregateException("There is an asset by the same data.");
                }
                var warehouseAsset = mapper.Map<WarehouseAsset>(warehouseAssetsDTO);
                warehouseAsset.WarehouseID = warehouseID;
                await warehouseAssetRepository.Create(warehouseAsset);
                return true;
            }
        }

        //_______________Read warehouse assets_________________ 
        public async Task<List<ReadWarehouseAssetsDTO>> ReadAll()
        {
            var warehouseAssets = await warehouseAssetRepository.Read();
            var warehouseAssetsList = await warehouseAssets
                .Include(sa => sa.Asset)
                    .ThenInclude(a => a.Category)
                .ToListAsync();
            return mapper.Map<List<ReadWarehouseAssetsDTO>>(warehouseAssetsList);
        }
        public async Task<List<ReadWarehouseAssetsDTO>> ReadByWarehouse(int warehouseID)
        {
            var warehouseAssets = await warehouseAssetRepository.ReadByWarehouse(warehouseID);
            return mapper.Map<List<ReadWarehouseAssetsDTO>>(warehouseAssets);
        }
        //_______________Search warehouse assets_________________ 
        public async Task<List<ReadWarehouseAssetsDTO>> Search(int warehouseID, string? name, string? serialNumber)
        {
            var warehouseAsset = await warehouseAssetRepository.Search(warehouseID, name, serialNumber);
            return mapper.Map<List<ReadWarehouseAssetsDTO>>(warehouseAsset);
        }

        //_______________Update warehouse asset by ID_________________ 
        public async Task<ReadWarehouseAssetsDTO> Update(UpdateWarehouseAssetsDTO warehouseAssetsDTO, int warehouseID, int assetID, string serialNumber)
        {
            var warehouseAsset = await warehouseAssetRepository.ReadOne(warehouseID, assetID, serialNumber);
            if (warehouseAsset == null)
            {
                throw new KeyNotFoundException("There is no asset by this ID and Serial Number.");
            }
            warehouseAssetsDTO.SerialNumber = warehouseAssetsDTO.SerialNumber ?? warehouseAsset.SerialNumber;
            warehouseAssetsDTO.Count = warehouseAssetsDTO.Count ?? warehouseAsset.Count;

            mapper.Map(warehouseAssetsDTO, warehouseAsset);
            await warehouseAssetRepository.Update();
            return mapper.Map<ReadWarehouseAssetsDTO>(warehouseAsset);
        }

        //_______________Delete warehouse asset by ID_________________ 
        public async Task<bool> Delete(int warehouseID, int assetID, string serialNumber)
        {
            var warehouseAsset = await warehouseAssetRepository.ReadOne(warehouseID, assetID, serialNumber);

            if (warehouseAsset == null)
            {
                throw new KeyNotFoundException("There is no asset by this ID and Serial Number.");
            }

            await warehouseAssetRepository.Delete(warehouseAsset);
            return true;
        }
    }
}
