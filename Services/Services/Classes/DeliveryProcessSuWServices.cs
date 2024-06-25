using AutoMapper;
using DTOs.DTOs.DeliveryProcesses;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repository.Interfaces;
using Services.Services.Interface;

namespace Services.Services.Classes
{
    public class DeliveryProcessSuWServices : IDeliveryProcessSuWServices
    {
        protected IDeliveryProcessSuWRepository deliveryProcessSuWRepository { get; set; }
        protected IMapper mapper { get; set; }
        public DeliveryProcessSuWServices(IDeliveryProcessSuWRepository deliveryProcessSuWRepository, IMapper mapper)
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
            await deliveryProcessSuWRepository.Create(newProcess);
            return true;
        }

        // ________________ Read Processes from Supplier to Werhouses ________________
        public async Task<List<ReadDeliveryProcessSuWDTO>> ReadAll()
        {
            var processesList = await deliveryProcessSuWRepository.Read();
            var mappedProcessesList = await processesList
                .Include(p => p.WarehouseProcesses)
                    .ThenInclude(wp => wp.AssetShipmentSuW)
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
