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
                storeProcess.Status = "Supplying";
                storeProcess.Quantity = 0;

                foreach (var assetShipment in storeProcess.AssetShipment)
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
            var processes = await processesList
                .Include(p=>p.Warehouse)
                .Include(p => p.StoreProcesses)
                    .ThenInclude(sp=>sp.Store)
                .ToListAsync();
            if (!processes.Any())
            {
                throw new KeyNotFoundException("There are no processes to be retrieved.");
            }

            var mappedProcessesList = processes.Select(p => {
                var dto = mapper.Map<ReadDeliveryProcessWStDTO>(p);
                dto.StageCompletionStep = CalculateStageCompletion(p.StoreProcesses);
                return dto;
            }).ToList();

            return mappedProcessesList;
        }

        public async Task<ReadDeliveryProcessWStDTO> ReadByID(int ID)
        {
            var process = await deliveryProcessWStRepository.ReadByID(ID);
            if (process == null)
            {
                throw new KeyNotFoundException("There is no process by this ID.");
            }

            var mappedProcess = mapper.Map<ReadDeliveryProcessWStDTO>(process);
            mappedProcess.StageCompletionStep = CalculateStageCompletion(process.StoreProcesses);
            return mappedProcess;
        }
        public async Task<List<ReadDeliveryProcessWStDTO>> ReadByWarehouse(int warehouseID)
        {
            var processes = await deliveryProcessWStRepository.ReadByWarehouse(warehouseID);
            if (processes == null)
            {
                throw new KeyNotFoundException("There are no Process from this Warehouse.");
            }

            var mappedProcessesList = processes.Select(p => {
                var dto = mapper.Map<ReadDeliveryProcessWStDTO>(p);
                dto.StageCompletionStep = CalculateStageCompletion(p.StoreProcesses);
                return dto;
            }).ToList();

            return mappedProcessesList;
        }
        public async Task<List<ReadDeliveryProcessWStDTO>> ReadByStore(int storeID)
        {
            var processes = await deliveryProcessWStRepository.ReadByStore(storeID);
            if (processes == null)
            {
                throw new KeyNotFoundException("There are no Process from this Store.");
            }

            var mappedProcessesList = processes.Select(p => {
                var dto = mapper.Map<ReadDeliveryProcessWStDTO>(p);
                dto.StageCompletionStep = CalculateStageCompletion(p.StoreProcesses);
                return dto;
            }).ToList();

            return mappedProcessesList;
        }

        // _________________________ Search for Processes _________________________
        public async Task<List<ReadDeliveryProcessWStDTO>> Search(DateTime? date)
        {
            var searchedProcesses = await deliveryProcessWStRepository.Search( date);
            if (!searchedProcesses.Any())
            {
                throw new KeyNotFoundException("There are no processes on this date.");
            }

            var mappedProcessesList = searchedProcesses.Select(p => {
                var dto = mapper.Map<ReadDeliveryProcessWStDTO>(p);
                dto.StageCompletionStep = CalculateStageCompletion(p.StoreProcesses);
                return dto;
            }).ToList();

            return mappedProcessesList;
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

            throw new KeyNotFoundException("There is no process by this ID.");
        }

        // _________________________ Calculate Stage Completion _________________________
        private StageCompletionStep CalculateStageCompletion(List<StoreProcess> storeProcesses)
        {
            int totalStores = storeProcesses.Count;
            int supplyingCount = 0;
            int deliveringCount = 0;
            int inventoryCount = 0;

            foreach (var sp in storeProcesses)
            {
                if (sp.Status == "Supplying" || sp.Status == "Delivering" || sp.Status == "Inventory")
                {
                    supplyingCount++;
                }
                if (sp.Status == "Delivering" || sp.Status == "Inventory")
                {
                    deliveringCount++;
                }
                if (sp.Status == "Inventory")
                {
                    inventoryCount++;
                }
            }

            return new StageCompletionStep
            {
                Supplying = (decimal)supplyingCount / totalStores * 100,
                Delivering = (decimal)deliveringCount / totalStores * 100,
                Inventory = (decimal)inventoryCount / totalStores * 100
            };
        }

    }
}
