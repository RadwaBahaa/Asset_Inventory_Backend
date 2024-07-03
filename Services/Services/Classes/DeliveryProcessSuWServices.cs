using AutoMapper;
using DTOs.DTOs.DeliveryProcesses;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repository.Classes;
using Repository.Interfaces;
using Services.Services.Interface;

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

            foreach (var warehouseProcess in deliveryProcess.WarehouseProcesses)
            {
                warehouseProcess.Status = "Start Process Confirmed";
                warehouseProcess.Quantity = 0;

                foreach (var assetShipment in warehouseProcess.AssetShipmentSuWs)
                {
                    var asset = await supplierAssetRepository.ReadOne(supplierID, assetShipment.AssetID, assetShipment.SerialNumber);
                    if (asset == null)
                        throw new KeyNotFoundException("There is no asset by this ID");
                    if (assetShipment.Quantity > asset.Count)
                        throw new KeyNotFoundException($"This quantity of the asset {asset.Asset.AssetName} in not avilable");

                    assetShipment.SupplierID = supplierID;
                    warehouseProcess.Quantity += assetShipment.Quantity;

                    asset.Count -= assetShipment.Quantity;

                }
                deliveryProcess.TotalAssets += warehouseProcess.Quantity;
            }

            await deliveryProcessSuWRepository.Create(deliveryProcess);
            await supplierAssetRepository.Update();
            return true;
        }

        // ________________ Read Processes from Supplier to Werhouses ________________
        public async Task<List<ReadDeliveryProcessSuWDTO>> ReadAll()
        {
            var processesList = await deliveryProcessSuWRepository.Read();
            var mappedProcessesList = await processesList
                .Include(p => p.WarehouseProcesses)
                    .ThenInclude(wp => wp.AssetShipmentSuWs)
                .Select(p => mapper.Map<ReadDeliveryProcessSuWDTO>(p))
                .ToListAsync();
            if (mappedProcessesList.Any())
            {
                return mappedProcessesList;
            }

            throw new ArgumentException("There are no prosesses to be retrieved.");
        }
        public async Task<ReadDeliveryProcessSuWDTO> ReadByID(int ID)
        {
            var process = await deliveryProcessSuWRepository.ReadByID(ID);
            if (process == null)
            {
                throw new ArgumentException("There is no process by this ID.");
            }

            return mapper.Map<ReadDeliveryProcessSuWDTO>(process);
        }

        // _________________________ Search for Processes _________________________
        public async Task<List<ReadDeliveryProcessSuWDTO>> SearchBySupplier(int supplierID)
        {
            var searchedProcesses = await deliveryProcessSuWRepository.SearchBySupplier(supplierID);
            if (searchedProcesses.Any())
            {
                return mapper.Map<List<ReadDeliveryProcessSuWDTO>>(searchedProcesses);
            }

            throw new ArgumentException("There are no Process from this Supplier.");
        }
        public async Task<List<ReadDeliveryProcessSuWDTO>> SearchByDate(DateTime date)
        {
            var searchedProcesses = await deliveryProcessSuWRepository.SearchByDate(date);
            if (searchedProcesses.Any())
            {
                return mapper.Map<List<ReadDeliveryProcessSuWDTO>>(searchedProcesses);
            }

            throw new ArgumentException("There are no Process on this date.");
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
    }
}
