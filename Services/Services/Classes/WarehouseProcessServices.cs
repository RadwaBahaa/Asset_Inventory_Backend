using AutoMapper;
using DTOs.DTOs.DeliveryProcesses;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using Services.Services.Interface;

namespace Services.Services.Classes
{
    public class WarehouseProcessServices : IWarehouseProcessServices
    {
        protected IWarehouseProcessRepository warehouseProcessReopsitory { get; set; }
        protected IMapper mapper;
        public WarehouseProcessServices(IWarehouseProcessRepository warehouseProcessReopsitory, IMapper mapper)
        {
            this.warehouseProcessReopsitory = warehouseProcessReopsitory;
            this.mapper = mapper;
        }
        // ___________________________ Read Processes  ___________________________
        public async Task<List<ReadWarehouseProcessDTO>> ReadAll()
        {
            var processes = await warehouseProcessReopsitory.Read();
            var processesList = await processes
                .Include(p => p.AssetShipmentSuWs)
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
        public async Task<List<ReadWarehouseProcessDTO>> SearchByWarehouse(int warehouseID)
        {
            var processes = await warehouseProcessReopsitory.SearchByWarehouse(warehouseID);
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
        public async Task<ReadWarehouseProcessDTO> Update(int processId, int warehouseID, UpdateWarehouseProcessDTO warehouseProcessDTO)
        {
            var process = await warehouseProcessReopsitory.ReadByID(processId, warehouseID);
            if (process == null)
                throw new ArgumentException("There is no process with this ID.");
            else
            {
                if (warehouseProcessDTO.Status == null)
                    throw new ArgumentException("Status is required.");
                else
                {
                    process.Status = warehouseProcessDTO.Status;
                    await warehouseProcessReopsitory.Update();
                    return mapper.Map<ReadWarehouseProcessDTO>(process);
                }
            }
        }
    }
}
