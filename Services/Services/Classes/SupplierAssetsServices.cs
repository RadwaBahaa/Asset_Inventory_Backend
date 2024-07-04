using AutoMapper;
using DTOs.DTOs.Suppliers;
using Services.Services.Interface;
using Models.Models;
using Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Services.Services.Classes
{
    public class SupplierAssetsServices : ISupplierAssetsServices
    {
        protected ISupplierAssetRepository supplierAssetRepository { get; set; }
        protected IAssetRepository assetRepository { get; set; }
        protected ISupplierRepository supplierRepository { get; set; }
        protected IMapper mapper { get; set; }

        public SupplierAssetsServices(ISupplierAssetRepository supplierAssetRepository, IAssetRepository assetRepository, ISupplierRepository supplierRepository, IMapper mapper)
        {
            this.supplierAssetRepository = supplierAssetRepository;
            this.assetRepository = assetRepository;
            this.supplierRepository = supplierRepository;
            this.mapper = mapper;
        }

        //________________ Create a new supplier Asset ______________
        public async Task<bool> Create(int supplierID, AddSupplierAssetsDTO supplierAssetsDTO)
        {
            if (supplierAssetsDTO == null)
            {
                throw new AggregateException("There is no data in body");
            }

            var findSupplier = await supplierRepository.ReadByID(supplierID);
            if (findSupplier == null)
            {
                throw new AggregateException("There is no supplier by this name.");
            }

            var findAsset = await assetRepository.ReadByID(supplierAssetsDTO.AssetID);
            if (findAsset == null)
            {
                throw new AggregateException("There is no asset by this name.");
            }

            var findSupplierAsset = await supplierAssetRepository.ReadOne(supplierID, supplierAssetsDTO.AssetID, supplierAssetsDTO.SerialNumber);
            if (findSupplierAsset != null)
            {
                throw new AggregateException("This asset is already exect.");
            }

            var supplierAsset = mapper.Map<SupplierAsset>(supplierAssetsDTO);
            supplierAsset.SupplierID = supplierID;
            await supplierAssetRepository.Create(supplierAsset);
            return true;
        }

        //_______________Read supplier assets_________________ 
        public async Task<List<ReadSupplierAssetsDTO>> ReadAll()
        {
            var supplierAssets = await supplierAssetRepository.Read();
            var supplierAssetsList = await supplierAssets
                .Include(sa => sa.Asset)
                    .ThenInclude(a => a.Category)
                .ToListAsync();
            return mapper.Map<List<ReadSupplierAssetsDTO>>(supplierAssetsList);
        }
        public async Task<List<ReadSupplierAssetsDTO>> ReadBySupplier(int supplierID)
        {
            var supplierAssets = await supplierAssetRepository.ReadBySupplier(supplierID);
            return mapper.Map<List<ReadSupplierAssetsDTO>>(supplierAssets);
        }
        //_______________Search supplier assets_________________ 
        public async Task<List<ReadSupplierAssetsDTO>> Search(int supplierID, string? name, string? serialNumber)
        {
            var supplierAsset = await supplierAssetRepository.Search(supplierID, name, serialNumber);
            return mapper.Map<List<ReadSupplierAssetsDTO>>(supplierAsset);
        }

        //_______________Update supplier asset by ID_________________ 
        public async Task<ReadSupplierAssetsDTO> Update(UpdateSupplierAssetsDTO supplierAssetsDTO, int supplierID, int assetID, string serialNumber)
        {
            var supplierAsset = await supplierAssetRepository.ReadOne(supplierID, assetID, serialNumber);
            if (supplierAsset == null)
            {
                throw new KeyNotFoundException("There is no asset by this ID and Serial Number.");
            }
            supplierAssetsDTO.Count = supplierAssetsDTO.Count ?? supplierAsset.Count;

            mapper.Map(supplierAssetsDTO, supplierAsset);
            await supplierAssetRepository.Update();
            return mapper.Map<ReadSupplierAssetsDTO>(supplierAsset);
        }

        //_______________Delete supplier asset by ID_________________ 
        public async Task<bool> Delete(int supplierID, int assetID, string serialNumber)
        {
            var supplierAsset = await supplierAssetRepository.ReadOne(supplierID, assetID, serialNumber);

            if (supplierAsset == null)
            {
                throw new KeyNotFoundException("There is no asset by this ID and Serial Number.");
            }

            await supplierAssetRepository.Delete(supplierAsset);
            return true;
        }
    }
}
