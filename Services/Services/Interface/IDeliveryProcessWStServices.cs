﻿using DTOs.DTOs.DeliveryProcesses;

namespace Services.Services.Interface
{
    public interface IDeliveryProcessWStServices
    {
        public Task<bool> Create(AddDeliveryProcessWStDTO deliveryProcess, int warehouseID);
        public Task<List<ReadDeliveryProcessWStDTO>> ReadAll();
        public Task<ReadDeliveryProcessWStDTO> ReadByID(int ID);
        public Task<List<ReadDeliveryProcessWStDTO>> ReadByWarehouse(int warehouseID);
        public Task<List<ReadDeliveryProcessWStDTO>> ReadByStore(int storeID);
        public Task<List<ReadDeliveryProcessWStDTO>> Search(DateTime? date);
        public Task<bool> DeleteProcess(int processID);
    }
}

