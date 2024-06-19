using AutoMapper;
using DTOs.DTOs.DeliveryProcesses;
using Microsoft.EntityFrameworkCore;
using Repository.Classes;
using Services.Services.Interface;

namespace Services.Services.Classes
{
    public class WarehouseProcessServices : IWarehouseProcessServices
    {
        protected WarehouseProcessReopsitory warehouseProcessReopsitory { get; set; }
        protected IMapper mapper;
        public WarehouseProcessServices(WarehouseProcessReopsitory warehouseProcessReopsitory, IMapper mapper)
        {
            this.warehouseProcessReopsitory = warehouseProcessReopsitory;
            this.mapper = mapper;
        }
        // ___________________________ Read Processes  ___________________________
        public async Task<List<ReadWarehouseProcessDTO>> ReadAllProcess()
        {
            var processes = await warehouseProcessReopsitory.Read();
            var processesList = await processes
                .Include(p => p.AssetShipmentSuW)
                .Select(p=>mapper.Map<ReadWarehouseProcessDTO>(p))
                .ToListAsync();
            if (processesList.Any())
                throw new ArgumentException("There are no process.");
            else
                return processesList;
        }
        public async Task<ReadWarehouseProcessDTO> ReadOneProcess(int processID, int warehouseID)
        {
            var process = await warehouseProcessReopsitory.ReadOneByID(processID, warehouseID);
            var mappedProcess = await process
                .Select(p => mapper.Map<ReadWarehouseProcessDTO>(p))
                .FirstOrDefaultAsync();
            if (mappedProcess == null) 
                throw new ArgumentException("There is no process with this ID.");
            else 
                return mappedProcess;
        }
        public async Task<List<ReadWarehouseProcessDTO>> SearchByWarehouse(int warehouseID)
        {
            var processes = await warehouseProcessReopsitory.SearchByWarehouse(warehouseID);
            var processesList = await processes
                .Where(p=>p.WarehouseID==warehouseID)
                .Include(p=>p.AssetShipmentSuW)
                .Select(p=>mapper.Map<ReadWarehouseProcessDTO>(p))
                .ToListAsync();
            if (processesList.Any())
                throw new ArgumentException("There are no process to this warehouse.");
            else
                return processesList;
        }

        // _________________________ Update a Process  _________________________
        public async Task<ReadWarehouseProcessDTO> UpdateProcess(int processId, int warehouseID, UpdateWarehouseProcessDTO warehouseProcessDTO)
        {
            var process = await warehouseProcessReopsitory.ReadOneByID(processId, warehouseID);
            var updatedProcess = await process.FirstOrDefaultAsync();
            if (updatedProcess == null) 
                throw new ArgumentException("There is no process with this ID.");
            else
            {
                if (warehouseProcessDTO.Status==null)
                    throw new ArgumentException("Status is required.");
                else
                {
                    updatedProcess.Status = warehouseProcessDTO.Status;
                    await warehouseProcessReopsitory.Update();
                    return mapper.Map<ReadWarehouseProcessDTO>(updatedProcess);
                }
            }
        }
    }
}
