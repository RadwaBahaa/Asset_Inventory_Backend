using AutoMapper;
using DTOs.DTOs.Suppliers;
using Services.Services.Interface;
using Models.Models;
using Repository.Interfaces;
using NetTopologySuite.Geometries;
using Microsoft.EntityFrameworkCore;
using Models.DTOs;

namespace Services.Services.Classes
{
    public class SupplierServices : ISupplierServices
    {
        protected ISupplierRepository supplierRepository { get; set; }
        protected IMapper mapper { get; set; }
        public SupplierServices(ISupplierRepository supplierRepository, IMapper mapper)
        {
            this.supplierRepository = supplierRepository;
            this.mapper = mapper;
        }

        //________________ Create supplier ______________
        public async Task<bool> Create(AddOrUpdateSupplierDTO supplierDTO)
        {

            if (supplierDTO == null)
            {
                throw new ArgumentException("Supplier name cannot be empty!");
            }
            else
            {
                var findSupplierByName = await supplierRepository.ReadByName(supplierDTO.SupplierName);
                var findSupplierByLocation = await supplierRepository.ReadByLocation(supplierDTO.Longitude, supplierDTO.Latitude);
                if (findSupplierByName != null || findSupplierByLocation != null)
                {
                    throw new AggregateException("This Supplier already exists.");
                }
                else
                {
                    var newSupplier = new Supplier
                    {
                        SupplierName = supplierDTO.SupplierName,
                        Location = new Point(supplierDTO.Longitude.Value, supplierDTO.Latitude.Value) { SRID = 4326 },
                        Address = supplierDTO.Address,
                    };
                    await supplierRepository.Create(newSupplier);
                    return true;
                }
            }
        }

        //_______________Read suppliers _________________ 
        public async Task<List<ReadSupplierDTO>> ReadAll()
        {
            var suppliers = await supplierRepository.Read();
            var allSuppliers = await suppliers
                .Include(s => s.SupplierAssets)
                .ToListAsync();
            return mapper.Map<List<ReadSupplierDTO>>(allSuppliers);
        }
        public async Task<List<SupplierGeoJsonDTO>> ReadAllSuppliersAsGeoJson()
        {
            var suppliers = await supplierRepository.Read();
            var allSuppliers = await suppliers
                .Include(s => s.SupplierAssets)
                .Select(supplier => new SupplierGeoJsonDTO(supplier))
                .ToListAsync();
            return allSuppliers;
        }
        public async Task<ReadSupplierDTO> ReadByID(int supplierID)
        {
            var supplier = await supplierRepository.ReadByID(supplierID);
            return mapper.Map<ReadSupplierDTO>(supplier);
        }
        public async Task<SupplierGeoJsonDTO> ReadSupplierAsGeoJson(int id)
        {
            var supplier = await supplierRepository.ReadByID(id);
            return new SupplierGeoJsonDTO(supplier);
        }

        //_______________Search for supplier _____________
        public async Task<List<ReadSupplierDTO>> SearchByName(string supplierName)
        {
            var suppliersList = await supplierRepository.SearchByName(supplierName);
            return mapper.Map<List<ReadSupplierDTO>>(suppliersList);
        }
        public async Task<List<ReadSupplierDTO>> SearchByAddress(string Address)
        {
            var suppliersList = await supplierRepository.SearchByAddress(Address);
            return mapper.Map<List<ReadSupplierDTO>>(suppliersList);
        }

        //_______________Update supplier by ID_________________ 
        public async Task<ReadSupplierDTO> Update(AddOrUpdateSupplierDTO supplierDTO, int supplierID)
        {
            var supplier = await supplierRepository.ReadByID(supplierID);
            if (supplier == null)
            {
                throw new KeyNotFoundException("There is no supplier by this ID.");
            }
            else
            {
                supplier.SupplierName = supplierDTO.SupplierName;
                supplier.Location = new Point(supplierDTO.Longitude.Value, supplierDTO.Latitude.Value) { SRID = 4326 };
                supplier.Address = supplierDTO.Address;
                await supplierRepository.Update();
                return mapper.Map<ReadSupplierDTO>(supplier);
            }
        }

        //_______________Delete supplier by ID_________________ 
        public async Task<bool> Delete(int supplierID)
        {
            var supplier = await supplierRepository.ReadByID(supplierID);
            if (supplier == null)
            {
                throw new KeyNotFoundException("There is no supplier by this ID.");
            }
            else
            {
                await supplierRepository.Delete(supplier);
                return true;
            }
        }
    }
}