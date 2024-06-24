using Context.Context;
using Microsoft.EntityFrameworkCore;
using Repository.Classes;
using Repository.Interfaces;
using Services.Mapper;
using Services.Services.Classes;
using Services.Services.Interface;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();

        // Register repository interfaces and implementations
        builder.Services.AddScoped<IAssetRepository, AssetRepository>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<IDeliveryProcessSuWRepository, DeliveryProcessSuWRepository>();
        builder.Services.AddScoped<IDeliveryProcessWStRepository, DeliveryProcessWStRepository>();
        builder.Services.AddScoped<IStoreAssetRepository, StoreAssetRepository>();
        builder.Services.AddScoped<IStoreProcessRepository, StoreProcessRepository>();
        builder.Services.AddScoped<IStoreRepository, StoreRepository>();
        builder.Services.AddScoped<ISupplierAssetRepository, SupplierAssetRepository>();
        builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
        builder.Services.AddScoped<IWarehouseAssetRepository, WarehouseAssetRepository>();
        builder.Services.AddScoped<IWarehouseProcessRepository, WarehouseProcessRepository>();
        builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();

        // Register service interfaces and implementations
        builder.Services.AddScoped<IAssetServices, AssetServices>();
        builder.Services.AddScoped<ICategoryServices, CategoryServices>();
        builder.Services.AddScoped<IDeliveryProcessSuWServices, DeliveryProcessSuWServices>();
        builder.Services.AddScoped<IDeliveryProcessWStServices, DeliveryProcessWStServices>();
        builder.Services.AddScoped<IStoreAssetsServices, StoreAssetsServices>();
        builder.Services.AddScoped<IStoreProcessServices, StoreProcessServices>();
        builder.Services.AddScoped<IStoreServices, StoreServices>();
        builder.Services.AddScoped<ISupplierAssetsServices, SupplierAssetsServices>();
        builder.Services.AddScoped<ISupplierServices, SupplierServices>();
        builder.Services.AddScoped<IWarehouseAssetsServices, WarehouseAssetsServices>();
        builder.Services.AddScoped<IWarehouseProcessServices, WarehouseProcessServices>();
        builder.Services.AddScoped<IWarehouseServices, WarehouseServices>();

        builder.Services.AddAutoMapper(option =>
        {
            option.AddProfile<MapProfile>();
        });

        //builder.Services.AddIdentity<IdentityUser, IdentityRole>(option =>
        //{
        //    option.SignIn.RequireConfirmedEmail = true;
        //}).AddEntityFrameworkStores<AssetInventoryContext>();
        builder.Services.AddDbContext<AssetInventoryContext>(option =>
        {
            option.UseSqlServer(builder.Configuration.GetConnectionString("DataBase"),
            sqlServerOptions => sqlServerOptions.UseNetTopologySuite());
        });

        // Configure CORS policy
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowReactDev",
                builder =>
                {
                    builder.WithOrigins("http://localhost:3000") // Adjust with your React frontend URL
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // Enable CORS middleware
        app.UseCors("AllowReactDev");

        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
