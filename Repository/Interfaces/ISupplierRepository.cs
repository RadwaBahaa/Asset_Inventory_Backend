﻿using Models.Models;

namespace Repository.Interfaces
{
    public interface ISupplierRepository : IGenericRepository<Supplier>
    {
        public Task<Supplier> ReadByID(int id);
        public Task<Supplier> ReadByName(string name);
        public Task<Supplier> ReadByLocation(double? lon, double? lat);
        public Task<List<Supplier>> Search(string name, string address);
    }
}
