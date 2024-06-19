﻿using AutoMapper;
using DTOs.DTOs.Assets;
using DTOs.DTOs.Categories;
using DTOs.DTOs.DeliveryProcesses;
using DTOs.DTOs.Requests;
using DTOs.DTOs.Stores;
using DTOs.DTOs.Suppliers;
using DTOs.DTOs.Warehouses;
using Models.Models;

namespace Services.Mapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            // Mapping Asset Model______________________________________________
            CreateMap<AddOrUpdateAssetDTO, Asset>().ReverseMap();
            CreateMap<ReadAssetDTO, Asset>().ReverseMap();

            // Mapping Category Model______________________________________________
            CreateMap<AddOrUpdateCategoryDTO, Category>().ReverseMap();
            CreateMap<ReadCategoryDTO, Category>().ReverseMap();

            // Mapping DeliveryProcessSuW Model______________________________________________
            CreateMap<AddDeliveryProcessSuWDTO, DeliveryProcessSuW>().ReverseMap();
            CreateMap<ReadDeliveryProcessSuWDTO, DeliveryProcessSuW>().ReverseMap();

            // Mapping DeliveryProcessWSt Model______________________________________________
            CreateMap<AddDeliveryProcessWStDTO, DeliveryProcessWSt>().ReverseMap();
            CreateMap<ReadDeliveryProcessWStDTO, DeliveryProcessWSt>().ReverseMap();

            // Mapping StoreProcess Model______________________________________________
            CreateMap<AddStoreProcessDTO, StoreProcess>().ReverseMap();
            CreateMap<ReadStoreProcessDTO, StoreProcess>().ReverseMap();
            CreateMap<UpdateStoreProcessDTO, StoreProcess>().ReverseMap();

            // Mapping WarehouseProcess Model______________________________________________
            CreateMap<AddWarehouseProcessDTO, WarehouseProcess>().ReverseMap();
            CreateMap<ReadWarehouseProcessDTO, WarehouseProcess>().ReverseMap();
            CreateMap<UpdateWarehouseProcessDTO, WarehouseProcess>().ReverseMap();

            // Mapping AddStoreRequestAsset Model______________________________________________
            CreateMap<AddStoreRequestAssetsDTO, StoreRequestAsset>().ReverseMap();
            CreateMap<ReadStoreRequestAssetsDTO, StoreRequestAsset>().ReverseMap();

            // Mapping WarehouseRequestAsset Model______________________________________________
            CreateMap<AddWarehouseRequestAssetsDTO, WarehouseRequestAsset>().ReverseMap();
            CreateMap<ReadWarehouseRequestAssetsDTO, WarehouseRequestAsset>().ReverseMap();

            // Mapping StoreRequest Model______________________________________________
            CreateMap<AddStoreRequestDTO, StoreRequest>().ReverseMap();
            CreateMap<UpdateStoreRequestDTO, StoreRequest>().ReverseMap();
            CreateMap<ReadStoreRequestDTO, StoreRequest>().ReverseMap();

            // Mapping WarehouseRequest Model______________________________________________
            CreateMap<AddWarehouseRequestDTO, WarehouseRequest>().ReverseMap();
            CreateMap<UpdateWarehouseRequestDTO, WarehouseRequest>().ReverseMap();
            CreateMap<ReadWarehouseRequestDTO, WarehouseRequest>().ReverseMap();

            // Mapping Store Model______________________________________________
            CreateMap<ReadStoreDTO,Store>().ReverseMap();
            CreateMap<AddOrUpdateStoreDTO, Store>().ReverseMap();

            // Mapping StoreAssets Model______________________________________________
            CreateMap<ReadStoreAssetsDTO, StoreAsset>().ReverseMap();
            CreateMap<AddOrUpdateStoreAssetsDTO,StoreAsset>().ReverseMap();

            // Mapping Supplier Model______________________________________________
            CreateMap<ReadSupplierDTO, Supplier>().ReverseMap();
            CreateMap<AddOrUpdateSupplierDTO, Supplier>().ReverseMap();

            // Mapping SupplierAssets Model______________________________________________
            CreateMap<ReadSupplierAssetsDTO, SupplierAsset>().ReverseMap();
            CreateMap<AddOrUpdateSupplierAssetsDTO, SupplierAsset>().ReverseMap();

            // Mapping Warehouse Model______________________________________________
            CreateMap<ReadWarehouseDTO, Warehouse>().ReverseMap();
            CreateMap<AddOrUpdateWarehouseDTO, Warehouse>().ReverseMap();

            // Mapping WarehouseAssets Model______________________________________________
            CreateMap<ReadWarehouseAssetsDTO, WarehouseAsset>().ReverseMap();
            CreateMap<AddOrUpdateWarehouseAssetsDTO, WarehouseAsset>().ReverseMap();
        }
    }
}
