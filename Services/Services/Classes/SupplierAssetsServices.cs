using AutoMapper;
using Repository.Classes;
using DTOs.DTOs.Suppliers;
using Services.Services.Interface;
using Models.Models;

namespace Services.Services.Classes
{
    public class SupplierAssetsServices : ISupplierAssetsServices
    {
        protected SupplierAssetRepository supplierAssetRepository { get; set; }
        protected IMapper mapper { get; set; }

        public SupplierAssetsServices(SupplierAssetRepository supplierAssetRepository, IMapper mapper)
        {
            this.supplierAssetRepository = supplierAssetRepository;
            this.mapper = mapper;
        }

        //________________ Create a new supplier Asset ______________
        public async Task<bool> Create(AddOrUpdateSupplierAssetsDTO supplierAssetsDTO)
        {
            if (supplierAssetsDTO == null)
            {
                throw new AggregateException("There is no data in body");
            }
            else
            {
                var supplierAsset = mapper.Map<SupplierAsset>(supplierAssetsDTO);
                await supplierAssetRepository.Create(supplierAsset);
                return true;
            }
        }

        //_______________Read supplier assets_________________ 
        public async Task<List<ReadSupplierAssetsDTO>> ReadAll()
        {
            var supplierAssets = await supplierAssetRepository.Read();
            if (supplierAssets.Any())
            {
                return mapper.Map<List<ReadSupplierAssetsDTO>>(supplierAssets);
            }
            else
            {
                throw new AggregateException("There are no assets.");
            }
        }
        public async Task<ReadSupplierAssetsDTO> ReadBySerialNumber(string serialNumber)
        {
            var supplierAsset = await supplierAssetRepository.ReadBySerialNumber(serialNumber);
            if (supplierAsset != null)
            {
                return mapper.Map<ReadSupplierAssetsDTO>(supplierAsset);
            }
            else
            {
                throw new AggregateException("There is no asset by this Serial Number.");
            }
        }

        //_______________Update supplier asset by ID_________________ 
        public async Task<ReadSupplierAssetsDTO> Update(AddOrUpdateSupplierAssetsDTO addOrUpdateSupplierAssetsDTO, int AssetID, int SerialNumber)
        {
            var supplierAsset = await supplierAssetRepository.ReadByID(AssetID, SerialNumber);
            if (supplierAsset == null)
            {
                throw new AggregateException("There is no asset by this ID and Serial Number.");
            }
            else
            {
                mapper.Map(addOrUpdateSupplierAssetsDTO, supplierAsset);
                await supplierAssetRepository.Update();
                return mapper.Map<ReadSupplierAssetsDTO>(supplierAsset);
            }
        }

        //_______________Delete supplier asset by ID_________________ 
        public async Task<bool> Delete(int AssetID, int SerialNumber)
        {
            var supplierAsset = await supplierAssetRepository.ReadByID(AssetID, SerialNumber);

            if (supplierAsset == null)
            {
                throw new AggregateException("There is no asset by this ID and Serial Number.");
            }
            else
            {
                await supplierAssetRepository.Delete(supplierAsset);
                return true;
            }
        }
    }
}
