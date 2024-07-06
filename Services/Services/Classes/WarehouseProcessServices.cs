using AutoMapper;
using DTOs.DTOs.DeliveryProcesses;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repository.Classes;
using Repository.Interfaces;
using Services.Services.Interface;

namespace Services.Services.Classes
{
    public class WarehouseProcessServices : IWarehouseProcessServices
    {
        protected IWarehouseProcessRepository warehouseProcessReopsitory { get; set; }
        protected IWarehouseAssetRepository warehouseAssetRepository { get; set; }
        protected IMapper mapper;
        public WarehouseProcessServices(IWarehouseProcessRepository warehouseProcessReopsitory, IWarehouseAssetRepository warehouseAssetRepository,IMapper mapper)
        {
            this.warehouseProcessReopsitory = warehouseProcessReopsitory;
            this.warehouseAssetRepository = warehouseAssetRepository;
            this.mapper = mapper;
        }
        // ___________________________ Read Processes  ___________________________
        public async Task<List<ReadWarehouseProcessDTO>> ReadAll()
        {
            var processes = await warehouseProcessReopsitory.Read();
            var processesList = await processes
                .Include(p => p.AssetShipment)
                    .ThenInclude(ash => ash.SupplierAsset)
                        .ThenInclude(sa => sa.Asset)
                .Select(p => mapper.Map<ReadWarehouseProcessDTO>(p))
                .ToListAsync();
            if (!processesList.Any())
                throw new KeyNotFoundException("There are no process.");
            else
                return processesList;
        }
        public async Task<ReadWarehouseProcessDTO> ReadByID(int processID, int warehouseID)
        {
            var process = await warehouseProcessReopsitory.ReadByID(processID, warehouseID);
            if (process == null)
            {
                throw new KeyNotFoundException("There is no process with this ID.");
            }
            else
            {
                return mapper.Map<ReadWarehouseProcessDTO>(process);
            }
        }
        public async Task<List<ReadWarehouseProcessDTO>> ReadByWarehouse(int warehouseID)
        {
            var processes = await warehouseProcessReopsitory.ReadByWarehouse(warehouseID);
            if (!processes.Any())
            {
                throw new KeyNotFoundException("There are no process to this warehouse.");
            }
            else
            {
                return mapper.Map<List<ReadWarehouseProcessDTO>>(processes);
            }
        }

        // _________________________ Update a Process  _________________________
        public async Task<bool> Update(int processId, int warehouseID, string userRole)
        {
            var process = await warehouseProcessReopsitory.ReadByID(processId, warehouseID);
            if (process == null)
                throw new ArgumentException("There is no process with this ID.");

            if (userRole == "Supplier" && process.Status == "Supplying")
            {
                process.Status = "Delivering";
            }
            else if (userRole == "Warehouse" && process.Status == "Delivering")
            {
                process.Status = "Inventory";

                // Update or create store assets
                foreach (var assetShipment in process.AssetShipment)
                {
                    var storeAsset = await warehouseAssetRepository.ReadOne(warehouseID, assetShipment.AssetID.Value, assetShipment.SerialNumber);

                    if (storeAsset != null)
                    {
                        // Update the quantity of existing store asset
                        storeAsset.Count += assetShipment.Quantity;
                        await warehouseAssetRepository.Update();
                    }
                    else
                    {
                        // Create a new store asset
                        var newWarehouseAsset = new WarehouseAsset
                        {
                            WarehouseID = warehouseID,
                            AssetID = assetShipment.AssetID.Value,
                            SerialNumber = assetShipment.SerialNumber,
                            Count = assetShipment.Quantity
                        };
                        await warehouseAssetRepository.Create(newWarehouseAsset);
                    }
                }
            }
            else
            {
                throw new ArgumentException("Invalid status transition or role.");
            }

            await warehouseProcessReopsitory.Update();
            return true;
        }
    }
}
