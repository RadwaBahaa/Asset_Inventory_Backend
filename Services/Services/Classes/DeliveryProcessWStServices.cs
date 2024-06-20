using AutoMapper;
using DTOs.DTOs.DeliveryProcesses;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repository.Classes;
using Services.Services.Interface;

namespace Services.Services.Classes
{
    public class DeliveryProcessWStServices : IDeliveryProcessWStServices
    {
        protected DeliveryProcessWStRepository deliveryProcessWStRepository { get; set; }
        protected IMapper mapper { get; set; }
        public DeliveryProcessWStServices(DeliveryProcessWStRepository deliveryProcessWStRepository, IMapper mapper)
        {
            this.deliveryProcessWStRepository = deliveryProcessWStRepository;
            this.mapper = mapper;
        }

        // ________________ Create a new Process from Supplier to Werhouses ________________
        public async Task<bool> Create(AddDeliveryProcessWStDTO deliveryProcessWStDTO, int warehouseID)
        {
            if (deliveryProcessWStDTO.StoreProcesses == null)
            {
                throw new ArgumentException("You choose not to deliver to any warehouses!...");
            }
            else
            {
                var newProcess = mapper.Map<DeliveryProcessWSt>(deliveryProcessWStDTO);
                newProcess.WarehouseID = warehouseID;
                newProcess.DateTime = DateTime.Now;
                newProcess.TotalAssets = 0;
                foreach (var process in deliveryProcessWStDTO.StoreProcesses)
                {
                    foreach (var asset in process.AssetShipmentWSt)
                    {
                        newProcess.TotalAssets += asset.Quantity ?? 0;
                    }
                }
                return true;
            }
        }

        // ________________ Read Processes from Supplier to Werhouses ________________
        public async Task<List<ReadDeliveryProcessWStDTO>> ReadAllProcess()
        {
            var processesList = await deliveryProcessWStRepository.Read();
            var mappedProcessesList = await processesList
                .Include(p => p.StoreProcesses)
                    .ThenInclude(wp => wp.AssetShipmentWSt)
                .Select(p => mapper.Map<ReadDeliveryProcessWStDTO>(p))
                .ToListAsync();
            if (mappedProcessesList.Any())
                return mappedProcessesList;
            else
                throw new ArgumentException("There are no assets to be retrieved.");
        }
        public async Task<ReadDeliveryProcessWStDTO> ReadOneByID(int id)
        {
            var process = await deliveryProcessWStRepository.ReadOneByID(id);
            var selectedProcess = await process
                .Include(p => p.StoreProcesses)
                    .ThenInclude(wp => wp.AssetShipmentWSt)
                .FirstOrDefaultAsync();
            if (process == null)
                throw new ArgumentException("There is no process by this ID.");
            else
                return mapper.Map<ReadDeliveryProcessWStDTO>(selectedProcess);
        }

        // _________________________ Search for Processes _________________________
        public async Task<List<ReadDeliveryProcessWStDTO>> SearchByWarehouse(int warehouseID)
        {
            var searchedProcesses = await deliveryProcessWStRepository.SearchByWarehouse(warehouseID);
            var mappedSearchedProcesses = await searchedProcesses
                .Include(p => p.StoreProcesses)
                    .ThenInclude(wp => wp.AssetShipmentWSt)
                .Select(p => mapper.Map<ReadDeliveryProcessWStDTO>(p))
                .ToListAsync();
            if (searchedProcesses.Any())
                return mappedSearchedProcesses;
            else
                throw new ArgumentException("There are no Process from this Warehouse.");
        }
        public async Task<List<ReadDeliveryProcessWStDTO>> SearchByDate(DateTime date)
        {
            var searchedProcesses = await deliveryProcessWStRepository.SearchByDate(date);
            var mappedSearchedProcesses = await searchedProcesses
                .Include(p => p.StoreProcesses)
                    .ThenInclude(wp => wp.AssetShipmentWSt)
                .Select(p => mapper.Map<ReadDeliveryProcessWStDTO>(p))
                .ToListAsync();
            if (searchedProcesses.Any())
                return mappedSearchedProcesses;
            else
                throw new ArgumentException("There are no Process on this date.");
        }

        // _________________________ Delete a Process _________________________
        public async Task<bool> DeleteProcess(int processID)
        {
            var findProcess = await deliveryProcessWStRepository.ReadOneByID(processID);
            var process = findProcess.FirstOrDefault();
            if (process != null)
            {
                await deliveryProcessWStRepository.Delete(process);
                return true;
            }
            else throw new ArgumentException("There is no process by this ID.");
        }
    }
}
