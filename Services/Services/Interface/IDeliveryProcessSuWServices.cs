using AutoMapper;
using DTOs.DTOs.DeliveryProcesses;
using Models.Models;
using Repository.Classes;

namespace Services.Services.Interface
{
    public interface IDeliveryProcessSuWServices
    {
        public Task<bool> Create(AddDeliveryProcessSuWDTO addDeliveryProcessSuWDTO, int supplierID);
        public Task<List<ReadDeliveryProcessSuWDTO>> ReadAllProcess();
        public Task<ReadDeliveryProcessSuWDTO> ReadOneByID(int id);
        public Task<List<ReadDeliveryProcessSuWDTO>> SearchBySupplier(int supplierID);
        public Task<List<ReadDeliveryProcessSuWDTO>> SearchByDate(DateTime date);
        public Task<bool> DeleteProcess(int processID);
    }
}

