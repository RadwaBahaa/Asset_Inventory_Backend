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

        //________________ Create a new Supplier ______________
        public async Task<bool> CreateSupplier(AddOrUpdateSupplierDTO createSupplierDTO)
        {
            if (string.IsNullOrEmpty(createSupplierDTO.SupplierName))
            {
                throw new ArgumentException("Supplier name cannot be empty!");
            }

            var newSupplier = new Supplier
            {
                SupplierName = createSupplierDTO.SupplierName,
                Location = createSupplierDTO.Location,
                Address = createSupplierDTO.Address,

            };
            await supplierRepository.Create(newSupplier);

            return true; 
        }

        //_______________Read all Suppliers _________________ 

        public async Task<List<ReadSupplierDTO>> GetAllSuppliers()
        {
            var suppliers = await supplierRepository.Read();
            var readSupplierDTO = mapper.Map<List<ReadSupplierDTO>>(suppliers);
            return readSupplierDTO;
        }


        //_______________Read Supplier by ID_________________ 
        public async Task<ReadSupplierDTO> GetSupplierByID(int SupplierID)
        {
            var supplier = await supplierRepository.GetOneByID(SupplierID);
            if (supplier == null)
            {
                throw new KeyNotFoundException("Supplier not found");
            }

            var readSupplierDTO = mapper.Map<ReadSupplierDTO>(supplier);
            return readSupplierDTO;
        }

        //_______________Update Supplier by ID_________________ 

        public async Task<ReadSupplierDTO> UpdateSupplier(AddOrUpdateSupplierDTO updateSupplierDTO, int SupplierID)
        {
            var Supplier = await supplierRepository.GetOneByID(SupplierID);
            if (Supplier == null)
            {
                throw new KeyNotFoundException("Supplier not found");
            }

            if (string.IsNullOrEmpty(updateSupplierDTO.SupplierName))
            {
                throw new ArgumentException("Supplier name cannot be empty!");
            }

            Supplier.SupplierName = updateSupplierDTO.SupplierName;
            Supplier.Location = updateSupplierDTO.Location;
            Supplier.Address = updateSupplierDTO.Address;

            await supplierRepository.Update();

            var readSupplierDTO = mapper.Map<ReadSupplierDTO>(Supplier);
            return readSupplierDTO;
        }

        //_______________Delete Supplier by ID_________________ 

        public async Task<bool> DeleteSupplier(int SupplierID)
        {
            var Supplier = await supplierRepository.GetOneByID(SupplierID);
            if (Supplier == null)
            {
                throw new KeyNotFoundException("Supplier not found");
            }

            await supplierRepository.Delete(Supplier);
            return true;
        }
    }
}