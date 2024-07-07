using AutoMapper;
using DTOs.DTOs.DeliveryProcesses;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repository.Interfaces;
using Services.Services.Interface;
using System.Diagnostics;

namespace Services.Services.Classes
{
    public class DeliveryProcessSuWServices : IDeliveryProcessSuWServices
    {
        protected IDeliveryProcessSuWRepository deliveryProcessSuWRepository { get; set; }
        protected ISupplierAssetRepository supplierAssetRepository { get; set; }
        protected IMapper mapper { get; set; }
        public DeliveryProcessSuWServices(IDeliveryProcessSuWRepository deliveryProcessSuWRepository, ISupplierAssetRepository supplierAssetRepository, IMapper mapper)
        {
            this.deliveryProcessSuWRepository = deliveryProcessSuWRepository;
            this.supplierAssetRepository = supplierAssetRepository;
            this.mapper = mapper;
        }

        // ________________ Create a new Process from Supplier to Werhouses ________________
        public async Task<bool> Create(AddDeliveryProcessSuWDTO deliveryProcessSuWDTO, int supplierID)
        {
            if (deliveryProcessSuWDTO.WarehouseProcesses == null)
            {
                throw new ArgumentException("You choose not to deliver to any store!...");
            }

            var deliveryProcess = mapper.Map<DeliveryProcessSuW>(deliveryProcessSuWDTO);

            deliveryProcess.SupplierID = supplierID;
            deliveryProcess.DateTime = DateTime.Now;
            deliveryProcess.TotalAssets = 0;

            foreach (var storeProcess in deliveryProcess.WarehouseProcesses)
            {
                storeProcess.Status = "Supplying";
                storeProcess.Quantity = 0;

                foreach (var assetShipment in storeProcess.AssetShipment)
                {
                    var asset = await supplierAssetRepository.ReadOne(supplierID, assetShipment.AssetID, assetShipment.SerialNumber);
                    if (asset == null)
                        throw new KeyNotFoundException("There is no asset by this ID");
                    if (assetShipment.Quantity > asset.Count)
                        throw new KeyNotFoundException($"This quantity of the asset {asset.Asset.AssetName} in not avilable");


                    assetShipment.SupplierID = supplierID;
                    storeProcess.Quantity += assetShipment.Quantity;

                    asset.Count -= assetShipment.Quantity;
                }
                deliveryProcess.TotalAssets += storeProcess.Quantity;
            }

            await deliveryProcessSuWRepository.Create(deliveryProcess);
            await supplierAssetRepository.Update();
            return true;
        }

        // ________________ Read Processes from Supplier to Werhouses ________________
        public async Task<List<ReadDeliveryProcessSuWDTO>> ReadAll()
        {
            var processesList = await deliveryProcessSuWRepository.Read();
            var processes = await processesList
                .Include(p => p.Supplier)
                .Include(p => p.WarehouseProcesses)
                    .ThenInclude(wp=>wp.Warehouse)
                .ToListAsync();

            if (!processes.Any())
            {
                throw new KeyNotFoundException("There are no processes to be retrieved.");
            }

            var mappedProcessesList = processes.Select(p => {
                var dto = mapper.Map<ReadDeliveryProcessSuWDTO>(p);
                dto.StageCompletionStep = CalculateStageCompletion(p.WarehouseProcesses);
                return dto;
            }).ToList();

            return mappedProcessesList;
        }
        public async Task<ReadDeliveryProcessSuWDTO> ReadByID(int ID)
        {
            var process = await deliveryProcessSuWRepository.ReadByID(ID);
            if (process == null)
            {
                throw new ArgumentException("There is no process by this ID.");
            }

            var mappedProcess = mapper.Map<ReadDeliveryProcessSuWDTO>(process);
            mappedProcess.StageCompletionStep = CalculateStageCompletion(process.WarehouseProcesses);
            return mappedProcess;
        }
        public async Task<List<ReadDeliveryProcessSuWDTO>> ReadBySupplier(int supplierID)
        {
            var processes = await deliveryProcessSuWRepository.ReadBySupplier(supplierID);
            if (processes == null)
            {
                throw new KeyNotFoundException("There are no Process from this Supplier.");
            }

            var mappedProcessesList = processes.Select(p => {
                var dto = mapper.Map<ReadDeliveryProcessSuWDTO>(p);
                dto.StageCompletionStep = CalculateStageCompletion(p.WarehouseProcesses);
                return dto;
            }).ToList();

            return mappedProcessesList;
        }
        public async Task<List<ReadDeliveryProcessSuWDTO>> ReadByWarehouse(int warehouseID)
        {
            var processes = await deliveryProcessSuWRepository.ReadByWarehouse(warehouseID);
            if (processes == null)
            {
                throw new KeyNotFoundException("There are no Process from this Warehouse.");
            }

            var mappedProcessesList = processes.Select(p => {
                var dto = mapper.Map<ReadDeliveryProcessSuWDTO>(p);
                dto.StageCompletionStep = CalculateStageCompletion(p.WarehouseProcesses);
                return dto;
            }).ToList();

            return mappedProcessesList;
        }

        // _________________________ Search for Processes _________________________
        public async Task<List<ReadDeliveryProcessSuWDTO>> Search(DateTime? date)
        {
            var searchedProcesses = await deliveryProcessSuWRepository.Search(date);
            if (!searchedProcesses.Any())
            {
                throw new KeyNotFoundException("There are no processes on this date.");
            }

            var mappedProcessesList = searchedProcesses.Select(p => {
                var dto = mapper.Map<ReadDeliveryProcessSuWDTO>(p);
                dto.StageCompletionStep = CalculateStageCompletion(p.WarehouseProcesses);
                return dto;
            }).ToList();

            return mappedProcessesList;
        }

        // _________________________ Delete a Process _________________________
        public async Task<bool> DeleteProcess(int processID)
        {
            var findProcess = await deliveryProcessSuWRepository.ReadByID(processID);
            if (findProcess != null)
            {
                await deliveryProcessSuWRepository.Delete(findProcess);
                return true;
            }

            throw new ArgumentException("There is no process by this ID.");
        }

        // _________________________ Calculate Stage Completion _________________________
        private StageCompletionStep CalculateStageCompletion(List<WarehouseProcess> WarehouseProcesses)
        {
            int totalStores = WarehouseProcesses.Count;
            int supplyingCount = 0;
            int deliveringCount = 0;
            int inventoryCount = 0;

            foreach (var sp in WarehouseProcesses)
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