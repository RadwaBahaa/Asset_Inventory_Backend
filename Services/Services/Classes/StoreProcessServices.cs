using AutoMapper;
using DTOs.DTOs.DeliveryProcesses;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repository.Interfaces;
using Services.Services.Interface;

namespace Services.Services.Classes
{
    public class StoreProcessServices : IStoreProcessServices
    {
        protected IStoreProcessRepository storeProcessReopsitory { get; set; }
            protected IStoreAssetRepository storeAssetRepository { get; set; }
        protected IWarehouseRepository warehouseRepository { get; set; }
        protected IMapper mapper;
        public StoreProcessServices(IStoreProcessRepository storeProcessReopsitory,IStoreAssetRepository storeAssetRepository, IMapper mapper, IWarehouseRepository warehouseRepository)
        {
            this.storeProcessReopsitory = storeProcessReopsitory;
            this.storeAssetRepository = storeAssetRepository;
            this.warehouseRepository = warehouseRepository;
            this.mapper = mapper;
        }
        // ___________________________ Read Processes  ___________________________
        public async Task<List<ReadStoreProcessDTO>> ReadAll()
        {
            var processes = await storeProcessReopsitory.Read();
            var processesList = await processes
                    .Include(p => p.AssetShipment)
                        .ThenInclude(ash => ash.WarehouseAsset)
                            .ThenInclude(wa => wa.Asset)
                .Select(p => mapper.Map<ReadStoreProcessDTO>(p))
                .ToListAsync();

            if (!processesList.Any())
                throw new KeyNotFoundException("There are no process.");
            else
                return processesList;
        }
        public async Task<ReadStoreProcessDTO> ReadByID(int processID, int storeID)
        {
            var process = await storeProcessReopsitory.ReadByID(processID, storeID);
            if (process == null)
            {
                throw new KeyNotFoundException("There is no process with this ID.");
            }
            else
            {
                return mapper.Map<ReadStoreProcessDTO>(process);
            }
        }
        public async Task<List<ReadStoreProcessDTO>> ReadByStore(int storeID)
        {
            var processes = await storeProcessReopsitory.ReadByStore(storeID);
            if (!processes.Any())
            {
                throw new KeyNotFoundException("There are no process to this warehouse.");
            }
            else
            {
                return mapper.Map<List<ReadStoreProcessDTO>>(processes);
            }
        }

        // _________________________ Update a Process  _________________________
        public async Task<bool> Update(int processId, int storeID, string userRole)
        {
            var process = await storeProcessReopsitory.ReadByID(processId, storeID);
            if (process == null)
                throw new ArgumentException("There is no process with this ID.");

            if (userRole == "Warehouse" && process.Status == "Supplying")
            {
                process.Status = "Delivering";
            }
            else if (userRole == "Store" && process.Status == "Delivering")
            {
                process.Status = "Inventory";

                // Update or create store assets
                foreach (var assetShipment in process.AssetShipment)
                {
                    var storeAsset = await storeAssetRepository.ReadOne(storeID, assetShipment.AssetID.Value, assetShipment.SerialNumber);

                    if (storeAsset != null)
                    {
                        // Update the quantity of existing store asset
                        storeAsset.Count += assetShipment.Quantity;
                        await storeAssetRepository.Update();
                    }
                    else
                    {
                        // Create a new store asset
                        var newStoreAsset = new StoreAsset
                        {
                            StoreID = storeID,
                            AssetID = assetShipment.AssetID.Value,
                            SerialNumber = assetShipment.SerialNumber,
                            Count = assetShipment.Quantity
                        };
                        await storeAssetRepository.Create(newStoreAsset);
                    }
                }
            }
            else
            {
                throw new ArgumentException("Invalid status transition or role.");
            }

            await storeProcessReopsitory.Update();
            return true;
        }
    }
}
