using Context.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repository.Classes;
using Repository.Interfaces;
using Services.Mapper;
using Services.Services.Classes;
using Services.Services.Interface;
using System.Text;

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


        builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AssetInventoryContext>();

        builder.Services.AddDbContext<AssetInventoryContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase"),
                sqlServerOptions => sqlServerOptions.UseNetTopologySuite());
        }, ServiceLifetime.Scoped);

        // Configure CORS policy
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowReactDev",
                builder =>
                {
                    builder.WithOrigins("http://localhost:3000")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
        });
        builder.Services.AddAuthentication(op =>
        {
            op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(op =>
        {
            op.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
                ValidateIssuerSigningKey = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidIssuer = builder.Configuration["JWT:Issuer"],
                ValidAudience = builder.Configuration["JWT:Audience"],
            };
        });
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

            // Define the OAuth2.0 scheme that's in use (i.e., Implicit, Password, Application and AccessCode)
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
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
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
