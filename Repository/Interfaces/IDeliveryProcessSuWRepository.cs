using Models.Models;

namespace Repository.Interfaces
{
    public interface IDeliveryProcessSuWRepository : IGenericRepository<DeliveryProcessSuW>
    {
        public Task<DeliveryProcessSuW> ReadByID(int ID);
        public Task<List<DeliveryProcessSuW>> Search(int? supplierID, DateTime? dateTime);
    }
}
