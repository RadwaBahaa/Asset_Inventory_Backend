using AutoMapper;
using DTOs.DTOs.DeliveryProcesses;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repository.Classes;
using Services.Services.Interface;

namespace Services.Services.Classes
{
    public class DeliveryProcessSuWServices : IDeliveryProcessSuWServices
    {
        protected DeliveryProcessSuWRepository deliveryProcessSuWRepository { get; set; }
        protected IMapper mapper { get; set; }
        public DeliveryProcessSuWServices(DeliveryProcessSuWRepository deliveryProcessSuWRepository, IMapper mapper)
        {
            this.deliveryProcessSuWRepository = deliveryProcessSuWRepository;
            this.mapper = mapper;
        }

        // ________________ Create a new Process from Supplier to Werhouses ________________
        public async Task<bool> Create(AddDeliveryProcessSuWDTO addDeliveryProcessSuWDTO, int supplierID)
        {
            if (addDeliveryProcessSuWDTO.WarehouseProcesses == null)
            {
                throw new ArgumentException("You choose not to deliver to any warehouses!...");
            }
            else
            {
                var newProcess = mapper.Map<DeliveryProcessSuW>(addDeliveryProcessSuWDTO);
                newProcess.SupplierID = supplierID;
                newProcess.DateTime = DateTime.Now;
                newProcess.TotalAssets = 0;
                foreach (var process in addDeliveryProcessSuWDTO.WarehouseProcesses)
                {
                    foreach (var asset in process.AssetShipmentSuW)
                    {
                        newProcess.TotalAssets += asset.Quantity ?? 0;
                    }
                }
                return true;
            }
        }

        // ________________ Read Processes from Supplier to Werhouses ________________
        public async Task<List<ReadDeliveryProcessSuWDTO>> ReadAllProcess()
        {
            var processesList = await deliveryProcessSuWRepository.Read();
            var finalProcessesList = await processesList
                .Include(p => p.WarehouseProcesses)
                    .ThenInclude(wp => wp.AssetShipmentSuW)
                .ToListAsync();
            if (finalProcessesList.Any())
                return mapper.Map<List<ReadDeliveryProcessSuWDTO>>(finalProcessesList);
            else
                throw new ArgumentException("There are no assets to be retrieved.");
        }
        public async Task<ReadDeliveryProcessSuWDTO> ReadOneByID(int id)
        {
            var process = await deliveryProcessSuWRepository.ReadOneByID(id);
            var selectedProcess = await process
                .Include(p => p.WarehouseProcesses)
                    .ThenInclude(wp => wp.AssetShipmentSuW)
                .FirstOrDefaultAsync();
            if (process == null)
                throw new ArgumentException("There is no process by this ID.");
            else
                return mapper.Map<ReadDeliveryProcessSuWDTO>(selectedProcess);
        }

        // _________________________ Search for Processes _________________________
        public async Task<List<ReadDeliveryProcessSuWDTO>> SearchBySupplier(int supplierID)
        {
            var searchedProcesses = await deliveryProcessSuWRepository.SearchBySupplier(supplierID);
            var mappedSearchedProcesses = await searchedProcesses
                .Include(p => p.WarehouseProcesses)
                    .ThenInclude(wp => wp.AssetShipmentSuW)
                .Select(p => mapper.Map<ReadDeliveryProcessSuWDTO>(p))
                .ToListAsync();
            if (searchedProcesses.Any())
                return mappedSearchedProcesses;
            else
                throw new ArgumentException("There are no Process from this Supplier.");
        }
        public async Task<List<ReadDeliveryProcessSuWDTO>> SearchByDate(DateTime date)
        {
            var searchedProcesses = await deliveryProcessSuWRepository.SearchByDate(date);
            var mappedSearchedProcesses = await searchedProcesses
                .Include(p => p.WarehouseProcesses)
                    .ThenInclude(wp => wp.AssetShipmentSuW)
                .Select(p => mapper.Map<ReadDeliveryProcessSuWDTO>(p))
                .ToListAsync();
            if (searchedProcesses.Any())
                return mappedSearchedProcesses;
            else
                throw new ArgumentException("There are no Process on this date.");
        }

        // _________________________ Delete a Process _________________________
        public async Task<bool> DeleteProcess(int processID)
        {
            var findProcess = await deliveryProcessSuWRepository.ReadOneByID(processID);
            var process = findProcess.FirstOrDefault();
            if (process != null)
            {
                await deliveryProcessSuWRepository.Delete(process);
                return true;
            }
            else throw new ArgumentException("There is no process by this ID.");
        }
    }
}
