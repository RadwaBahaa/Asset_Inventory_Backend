using AutoMapper;
using DTOs.DTOs.DeliveryProcesses;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using Services.Services.Interface;

namespace Services.Services.Classes
{
    public class StoreProcessServices : IStoreProcessServices
    {
        protected IStoreProcessRepository storeProcessReopsitory { get; set; }
        protected IMapper mapper;
        public StoreProcessServices(IStoreProcessRepository storeProcessReopsitory, IMapper mapper)
        {
            this.storeProcessReopsitory = storeProcessReopsitory;
            this.mapper = mapper;
        }
        // ___________________________ Read Processes  ___________________________
        public async Task<List<ReadStoreProcessDTO>> ReadAll()
        {
            var processes = await storeProcessReopsitory.Read();
            var processesList = await processes
                    .Include(p => p.AssetShipmentWSts)
                        .ThenInclude(ash=>ash.WarehouseAsset)
                            .ThenInclude(wa=>wa.Asset)
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
        public async Task<List<ReadStoreProcessDTO>> SearchByStore(int storeID)
        {
            var processes = await storeProcessReopsitory.SearchByStore(storeID);
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
        public async Task<ReadStoreProcessDTO> Update(int processId, int storeID, UpdateStoreProcessDTO storeProcessDTO)
        {
            var process = await storeProcessReopsitory.ReadByID(processId, storeID);
            if (process == null)
                throw new ArgumentException("There is no process with this ID.");
            else
            {
                if (storeProcessDTO.Status == null)
                    throw new ArgumentException("Status is required.");
                else
                {
                    process.Status = storeProcessDTO.Status;
                    await storeProcessReopsitory.Update();
                    return mapper.Map<ReadStoreProcessDTO>(process);
                }
            }
        }
    }
}
