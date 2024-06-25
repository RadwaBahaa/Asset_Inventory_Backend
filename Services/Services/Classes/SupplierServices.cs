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
        public async Task<bool> CreateByData(AddOrUpdateSupplierDTO supplierDTO)
        {

            if (supplierDTO == null)
            {
                throw new ArgumentException("Supplier data cannot be empty!");
            }

            var findSupplierByName = await supplierRepository.ReadByName(supplierDTO.SupplierName);
            var findSupplierByLocation = await supplierRepository.ReadByLocation(supplierDTO.Longitude, supplierDTO.Latitude);
            if (findSupplierByName != null)
            {
                throw new ArgumentException("A supplier with this name already exists.");
            }

            if (findSupplierByLocation != null)
            {
                throw new ArgumentException("A supplier at this location already exists.");
            }

            var newSupplier = new Supplier
            {
                SupplierName = supplierDTO.SupplierName,
                Location = new Point(supplierDTO.Longitude, supplierDTO.Latitude) { SRID = 4326 },
                Address = supplierDTO.Address,
            };
            await supplierRepository.Create(newSupplier);
            return true;
        }
        public async Task<bool> CreateByGeoJSON(AddSupplierGeoJsonDTO supplierDTO)
        {
            if (supplierDTO == null)
            { throw new ArgumentException("Supplier data cannot be empty!"); }
            var findSupplierByName = await supplierRepository.ReadByName(supplierDTO.properties.supplierName);
            var findSupplierByLocation = await supplierRepository.ReadByLocation(supplierDTO.geometry.coordinates[0], supplierDTO.geometry.coordinates[1]);
            if (findSupplierByName != null)
            {
                throw new ArgumentException("A supplier with this name already exists.");
            }

            if (findSupplierByLocation != null)
            {
                throw new ArgumentException("A supplier at this location already exists.");
            }

            var newSupplier = new Supplier
            {
                SupplierName = supplierDTO.properties.supplierName,
                Location = new Point(supplierDTO.geometry.coordinates[0], supplierDTO.geometry.coordinates[1]) { SRID = 4326 },
                Address = supplierDTO.properties.address,
            };
            await supplierRepository.Create(newSupplier);
            return true;
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
        public async Task<List<ReadSupplierGeoJsonDTO>> ReadAllSuppliersAsGeoJson()
        {
            var suppliers = await supplierRepository.Read();
            var allSuppliers = await suppliers
                .Include(s => s.SupplierAssets)
                .Select(supplier => new ReadSupplierGeoJsonDTO(supplier))
                .ToListAsync();
            return allSuppliers;
        }
        public async Task<ReadSupplierDTO> ReadByID(int supplierID)
        {
            var supplier = await supplierRepository.ReadByID(supplierID);
            return mapper.Map<ReadSupplierDTO>(supplier);
        }
        public async Task<ReadSupplierGeoJsonDTO> ReadSupplierAsGeoJson(int id)
        {
            var supplier = await supplierRepository.ReadByID(id);
            return new ReadSupplierGeoJsonDTO(supplier);
        }

        //_______________Search for supplier _____________
        public async Task<List<ReadSupplierDTO>> Search(string name, string address)
        {
            var suppliersList = await supplierRepository.Search(name, address);
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

            supplier.SupplierName = supplierDTO.SupplierName;
            supplier.Location = new Point(supplierDTO.Longitude, supplierDTO.Latitude) { SRID = 4326 };
            supplier.Address = supplierDTO.Address;
            await supplierRepository.Update();
            return mapper.Map<ReadSupplierDTO>(supplier);
        }

        //_______________Delete supplier by ID_________________ 
        public async Task<bool> Delete(int supplierID)
        {
            var supplier = await supplierRepository.ReadByID(supplierID);
            if (supplier == null)
            {
                throw new KeyNotFoundException("There is no supplier by this ID.");
            }

            await supplierRepository.Delete(supplier);
            return true;
        }
    }
}