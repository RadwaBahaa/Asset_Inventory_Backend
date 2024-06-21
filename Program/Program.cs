using Context.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Mapper;
using Services.Services.Classes;
using Services.Services.Interface;

namespace Asset_Inventory_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddScoped<IAssetServices, AssetServices>();
            builder.Services.AddScoped<ICategoryServices, CategoryServices>();
            builder.Services.AddScoped<IAssetServices, AssetServices>();
            builder.Services.AddScoped<ICategoryServices, CategoryServices>();
            builder.Services.AddScoped<IDeliveryProcessSuWServices, DeliveryProcessSuWServices>();
            builder.Services.AddScoped<IDeliveryProcessWStServices, DeliveryProcessWStServices>();
            builder.Services.AddScoped<IWarehouseProcessServices, WarehouseProcessServices>();
            builder.Services.AddScoped<IStoreProcessServices, StoreProcessServices>();
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
