using AutoMapper;
using DTOs.DTOs.DeliveryProcesses;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repository.Interfaces;
using Services.Services.Interface;

namespace Services.Services.Classes
{
    public class DeliveryProcessWStServices : IDeliveryProcessWStServices
    {
        protected IDeliveryProcessWStRepository deliveryProcessWStRepository { get; set; }
        protected IWarehouseAssetRepository warehouseAssetRepository;
        protected IMapper mapper { get; set; }
        public DeliveryProcessWStServices(IDeliveryProcessWStRepository deliveryProcessWStRepository, IWarehouseAssetRepository warehouseAssetRepository, IMapper mapper)
        {
            this.deliveryProcessWStRepository = deliveryProcessWStRepository;
            this.warehouseAssetRepository = warehouseAssetRepository;
            this.mapper = mapper;
        }

        // ________________ Create a new Process from Supplier to Werhouses ________________
        public async Task<bool> Create(AddDeliveryProcessWStDTO deliveryProcessWStDTO, int warehouseID)
        {
            if (deliveryProcessWStDTO.StoreProcesses == null)
            {
                throw new ArgumentException("You choose not to deliver to any store!...");
            }

            var deliveryProcess = mapper.Map<DeliveryProcessWSt>(deliveryProcessWStDTO);

            deliveryProcess.WarehouseID = warehouseID;
            deliveryProcess.DateTime = DateTime.Now;
            deliveryProcess.TotalAssets = 0;

            foreach (var storeProcess in deliveryProcess.StoreProcesses)
            {
                storeProcess.Status = "Start Process Confirmed";
                storeProcess.Quantity = 0;

                foreach (var assetShipment in storeProcess.AssetShipmentWSts)
                {
                    var asset = await warehouseAssetRepository.ReadOne(warehouseID, assetShipment.AssetID, assetShipment.SerialNumber);
                    if (asset == null)
                        throw new KeyNotFoundException("There is no asset by this ID");
                    if (assetShipment.Quantity > asset.Count)
                        throw new KeyNotFoundException($"This quantity of the asset {asset.Asset.AssetName} in not avilable");
                    
                    
                    assetShipment.WarehouseID = warehouseID;
                    storeProcess.Quantity += assetShipment.Quantity;

                    asset.Count -= assetShipment.Quantity;
                }
                deliveryProcess.TotalAssets += storeProcess.Quantity;
            }

            await deliveryProcessWStRepository.Create(deliveryProcess);
            await warehouseAssetRepository.Update();
            return true;
        }


        // ________________ Read Processes from Supplier to Werhouses ________________
        public async Task<List<ReadDeliveryProcessWStDTO>> ReadAll()
        {
            var processesList = await deliveryProcessWStRepository.Read();
            var mappedProcessesList = await processesList
                .Include(p => p.StoreProcesses)
                    .ThenInclude(sp => sp.AssetShipmentWSts)
                        .ThenInclude(ash => ash.WarehouseAsset)
                            .ThenInclude(wa => wa.Asset)
                .Select(p => mapper.Map<ReadDeliveryProcessWStDTO>(p))
                .ToListAsync();
            if (mappedProcessesList.Any())
            {
                return mappedProcessesList;
            }

            throw new ArgumentException("There are no prosesses to be retrieved.");
        }
        public async Task<ReadDeliveryProcessWStDTO> ReadByID(int ID)
        {
            var process = await deliveryProcessWStRepository.ReadByID(ID);
            if (process == null)
            {
                throw new ArgumentException("There is no process by this ID.");
            }

            return mapper.Map<ReadDeliveryProcessWStDTO>(process);
        }
        public async Task<ReadDeliveryProcessWStDTO> ReadByWarehouse(int warehouseID)
        {
            var process = await deliveryProcessWStRepository.ReadByWarehouse(warehouseID);
            if (process == null)
            {
                throw new ArgumentException("There is no process by this ID.");
            }

            return mapper.Map<ReadDeliveryProcessWStDTO>(process);
        }

        // _________________________ Search for Processes _________________________
        public async Task<List<ReadDeliveryProcessWStDTO>> Search(DateTime? date)
        {
            var searchedProcesses = await deliveryProcessWStRepository.Search(, date);
            if (searchedProcesses.Any())
            {
                return mapper.Map<List<ReadDeliveryProcessWStDTO>>(searchedProcesses);
            }

            throw new ArgumentException("There are no Process from this Warehouse.");
        }

        // _________________________ Delete a Process _________________________
        public async Task<bool> DeleteProcess(int processID)
        {
            var findProcess = await deliveryProcessWStRepository.ReadByID(processID);
            if (findProcess != null)
            {
                await deliveryProcessWStRepository.Delete(findProcess);
                return true;
            }

            throw new ArgumentException("There is no process by this ID.");
        }
    }
}
