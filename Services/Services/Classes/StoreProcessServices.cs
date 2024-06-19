using AutoMapper;
using DTOs.DTOs.DeliveryProcesses;
using Microsoft.EntityFrameworkCore;
using Repository.Classes;
using Services.Services.Interface;

namespace Services.Services.Classes
{
    public class StoreProcessServices : IStoreProcessServices
    {
        protected StoreProcessReopsitory storeProcessReopsitory { get; set; }
        protected IMapper mapper;
        public StoreProcessServices(StoreProcessReopsitory storeProcessReopsitory, IMapper mapper)
        {
            this.storeProcessReopsitory = storeProcessReopsitory;
            this.mapper = mapper;
        }
        // ___________________________ Read Processes  ___________________________
        public async Task<List<ReadStoreProcessDTO>> ReadAllProcess()
        {
            var processes = await storeProcessReopsitory.Read();
            var processesList = await processes
                .Include(p => p.AssetShipmentWSt)
                .Select(p => mapper.Map<ReadStoreProcessDTO>(p))
                .ToListAsync();
            if (processesList.Any())
                throw new ArgumentException("There are no process.");
            else
                return processesList;
        }
        public async Task<ReadStoreProcessDTO> ReadOneProcess(int processID, int storeID)
        {
            var process = await storeProcessReopsitory.ReadOneByID(processID, storeID);
            var mappedProcess = await process
                .Select(p => mapper.Map<ReadStoreProcessDTO>(p))
                .FirstOrDefaultAsync();
            if (mappedProcess == null)
                throw new ArgumentException("There is no process with this ID.");
            else
                return mappedProcess;
        }
        public async Task<List<ReadStoreProcessDTO>> SearchByStore(int storeID)
        {
            var processes = await storeProcessReopsitory.SearchByStore(storeID);
            var processesList = await processes
                .Where(p => p.StoreID == storeID)
                .Include(p => p.AssetShipmentWSt)
                .Select(p => mapper.Map<ReadStoreProcessDTO>(p))
                .ToListAsync();
            if (processesList.Any())
                throw new ArgumentException("There are no process to this store.");
            else
                return processesList;
        }

        // _________________________ Update a Process  _________________________
        public async Task<ReadStoreProcessDTO> UpdateProcess(int processId, int storeID, UpdateStoreProcessDTO storeProcessDTO)
        {
            var process = await storeProcessReopsitory.ReadOneByID(processId, storeID);
            var updatedProcess = await process.FirstOrDefaultAsync();
            if (updatedProcess == null)
                throw new ArgumentException("There is no process with this ID.");
            else
            {
                if (storeProcessDTO.Status == null)
                    throw new ArgumentException("Status is required.");
                else
                {
                    updatedProcess.Status = storeProcessDTO.Status;
                    await storeProcessReopsitory.Update();
                    return mapper.Map<ReadStoreProcessDTO>(updatedProcess);
                }
            }
        }
    }
}
