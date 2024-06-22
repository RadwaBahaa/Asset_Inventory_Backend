using AutoMapper;
using Repository.Classes;
using DTOs.DTOs.Suppliers;
using Services.Services.Interface;
using Models.Models;

namespace Services.Services.Classes
{
    public class SupplierServices : ISupplierServices
    {
        protected SupplierRepository supplierRepository { get; set; }
        protected IMapper mapper { get; set; }
        public SupplierServices(SupplierRepository supplierRepository, IMapper mapper)
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
                var newSupplier = new Supplier
                {
                    SupplierName = supplierDTO.SupplierName,
                    Location = supplierDTO.Location,
                    Address = supplierDTO.Address,
                };
                await supplierRepository.Create(newSupplier);
                return true;
            }
        }

        //_______________Read suppliers _________________ 
        public async Task<List<ReadSupplierDTO>> ReadAll()
        {
            var suppliers = await supplierRepository.Read();
            if (suppliers.Any())
            {
                throw new AggregateException("There are no suppliers.");
            }
            else
            {
                return mapper.Map<List<ReadSupplierDTO>>(suppliers);
            }
        }
        public async Task<ReadSupplierDTO> ReadByID(int SupplierID)
        {
            var supplier = await supplierRepository.ReadByID(SupplierID);
            if (supplier != null)
            {
                return mapper.Map<ReadSupplierDTO>(supplier);
            }
            else
            {
                throw new AggregateException("There are no suppliers.");
            }
        }
        //_______________Search for supplier _____________
        public async Task<List<ReadSupplierDTO>> SearchByName(string SupplierName)
        {
            var suppliersList = await supplierRepository.SearchByName(SupplierName);
            if (suppliersList.Any())
            {
                return mapper.Map<List<ReadSupplierDTO>>(suppliersList);
            }
            else
            {
                throw new AggregateException("There are no suppliers.");
            }
        }
        public async Task<List<ReadSupplierDTO>> SearchByAddress(string Address)
        {
            var suppliersList = await supplierRepository.SearchByAddress(Address);
            if (suppliersList.Any())
            {
                return mapper.Map<List<ReadSupplierDTO>>(suppliersList);
            }
            else
            {
                throw new AggregateException("There are no suppliers.");
            }
        }

        //_______________Update supplier by ID_________________ 

        public async Task<ReadSupplierDTO> Update(AddOrUpdateSupplierDTO updateSupplierDTO, int SupplierID)
        {
            var supplier = await supplierRepository.ReadByID(SupplierID);
            if (supplier == null)
            {
                throw new AggregateException("There is no supplier by this ID.");
            }
            else
            {
                supplier.SupplierName = updateSupplierDTO.SupplierName;
                supplier.Location = updateSupplierDTO.Location;
                supplier.Address = updateSupplierDTO.Address;
                await supplierRepository.Update();
                return mapper.Map<ReadSupplierDTO>(supplier);
            }
        }

        //_______________Delete supplier by ID_________________ 

        public async Task<bool> Delete(int SupplierID)
        {
            var supplier = await supplierRepository.ReadByID(SupplierID);
            if (supplier == null)
            {
                throw new AggregateException("There is no supplier by this ID.");
            }
            else
            {
                await supplierRepository.Delete(supplier);
                return true;
            }
        }
    }
}