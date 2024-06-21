using Context.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Mapper;

namespace Asset_Inventory_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddScoped<ISupplierServices, SupplierServices>();
            builder.Services.AddScoped<ISupplierAssetServices, SupplierAssetServices>();

            builder.Services.AddScoped<IWarehouseServices, WarehouseServices>();
            builder.Services.AddScoped<IWarehouseAssetServices, WarehouseAssetServices>();
           
            builder.Services.AddScoped<IStoreServices, StoreServices>();
            builder.Services.AddScoped<IStoreAssetServices, StoreAssetServices>();

        
            



            builder.Services.AddAutoMapper(option =>
            {
                option.AddProfile<MapProfile>();
            });

            builder.Services.AddIdentity<IdentityUser, IdentityRole>(option =>
            {
                option.SignIn.RequireConfirmedEmail = true;
            }).AddEntityFrameworkStores<AssetInventoryContext>();
            builder.Services.AddDbContext<AssetInventoryContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DataBase"),
                    sqlServerOptions => sqlServerOptions.UseNetTopologySuite());
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
