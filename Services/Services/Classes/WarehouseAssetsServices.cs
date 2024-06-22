using AutoMapper;
using Repository.Classes;
using DTOs.DTOs.Warehouses;
using Services.Services.Interface;
using Models.Models;

namespace Services.Services.Classes
{
    public class WarehouseAssetsServices : IWarehouseAssetsServices
    {
        protected WarehouseAssetRepository warehouseAssetRepository { get; set; }
        protected IMapper mapper { get; set; }

        public WarehouseAssetsServices(WarehouseAssetRepository warehouseAssetRepository, IMapper mapper)
        {
            this.warehouseAssetRepository = warehouseAssetRepository;
            this.mapper = mapper;
        }

        //________________ Create a new warehouse Asset ______________
        public async Task<bool> Create(AddOrUpdateWarehouseAssetsDTO warehouseAssetsDTO)
        {
            if (warehouseAssetsDTO == null)
            {
                throw new AggregateException("There is no data in body");
            }
            else
            {
                var warehouseAsset = mapper.Map<WarehouseAsset>(warehouseAssetsDTO);
                await warehouseAssetRepository.Create(warehouseAsset);
                return true;
            }
        }

        //_______________Read all warehouse assets_________________ 
        public async Task<List<ReadWarehouseAssetsDTO>> ReadAll()
        {
            var warehouseAssets = await warehouseAssetRepository.Read();
            if (warehouseAssets.Any())
            {
                return mapper.Map<List<ReadWarehouseAssetsDTO>>(warehouseAssets);
            }
            else
            {
                throw new AggregateException("There are no assets.");

            }
        }

        //_______________Read warehouse Asset by Serial number_________________ 
        public async Task<ReadWarehouseAssetsDTO> ReadBySerialNumber(string serialNumber)
        {
            var warehouseAsset = await warehouseAssetRepository.ReadBySerialNumber(serialNumber);
            if (warehouseAsset != null)
            {
                return mapper.Map<ReadWarehouseAssetsDTO>(warehouseAsset);
            }
            else
            {
                throw new AggregateException("There is no asset by this Serial Number.");
            }
        }

        //_______________Update warehouse asset by ID_________________ 
        public async Task<ReadWarehouseAssetsDTO> Update(AddOrUpdateWarehouseAssetsDTO addOrUpdateWarehouseAssetsDTO, int AssetID, int SerialNumber)
        {
            var warehouseAsset = await warehouseAssetRepository.ReadByID(AssetID, SerialNumber);
            if (warehouseAsset == null)
            {
                throw new AggregateException("There is no asset by this ID and Serial Number.");
            }
            else
            {
                mapper.Map(addOrUpdateWarehouseAssetsDTO, warehouseAsset);
                await warehouseAssetRepository.Update();
                return mapper.Map<ReadWarehouseAssetsDTO>(warehouseAsset);
            }
        }

        //_______________Delete  warehouse asset by ID_________________ 
        public async Task<bool> Delete(int AssetID, int SerialNumber)
        {
            var warehouseAsset = await warehouseAssetRepository.ReadByID(AssetID, SerialNumber);
            if (warehouseAsset == null)
            {
                throw new AggregateException("There is no asset by this ID and Serial Number.");
            }
            else
            {
                await warehouseAssetRepository.Delete(warehouseAsset);
                return true;
            }
        }
    }
}
