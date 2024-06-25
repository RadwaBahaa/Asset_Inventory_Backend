using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace Context.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    StoreID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<Point>(type: "geometry", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.StoreID);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SupplierID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<Point>(type: "geometry", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SupplierID);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    WarehouseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarehouseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<Point>(type: "geometry", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.WarehouseID);
                });

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    AssetID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssetName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.AssetID);
                    table.ForeignKey(
                        name: "FK_Assets_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryProcessSuW",
                columns: table => new
                {
                    ProcessID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierID = table.Column<int>(type: "int", nullable: false),
                    TotalAssets = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryProcessSuW", x => x.ProcessID);
                    table.ForeignKey(
                        name: "FK_DeliveryProcessSuW_Suppliers_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryProcessWSt",
                columns: table => new
                {
                    ProcessID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarehouseID = table.Column<int>(type: "int", nullable: false),
                    TotalAssets = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryProcessWSt", x => x.ProcessID);
                    table.ForeignKey(
                        name: "FK_DeliveryProcessWSt_Warehouses_WarehouseID",
                        column: x => x.WarehouseID,
                        principalTable: "Warehouses",
                        principalColumn: "WarehouseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoreRequests",
                columns: table => new
                {
                    RequestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreID = table.Column<int>(type: "int", nullable: false),
                    WarehouseID = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreRequests", x => x.RequestID);
                    table.ForeignKey(
                        name: "FK_StoreRequests_Stores_StoreID",
                        column: x => x.StoreID,
                        principalTable: "Stores",
                        principalColumn: "StoreID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreRequests_Warehouses_WarehouseID",
                        column: x => x.WarehouseID,
                        principalTable: "Warehouses",
                        principalColumn: "WarehouseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseRequests",
                columns: table => new
                {
                    RequestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarehouseID = table.Column<int>(type: "int", nullable: false),
                    SupplierID = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseRequests", x => x.RequestID);
                    table.ForeignKey(
                        name: "FK_WarehouseRequests_Suppliers_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WarehouseRequests_Warehouses_WarehouseID",
                        column: x => x.WarehouseID,
                        principalTable: "Warehouses",
                        principalColumn: "WarehouseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoreAssets",
                columns: table => new
                {
                    AssetID = table.Column<int>(type: "int", nullable: false),
                    StoreID = table.Column<int>(type: "int", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductionDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreAssets", x => new { x.AssetID, x.StoreID, x.SerialNumber });
                    table.ForeignKey(
                        name: "FK_StoreAssets_Assets_AssetID",
                        column: x => x.AssetID,
                        principalTable: "Assets",
                        principalColumn: "AssetID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreAssets_Stores_StoreID",
                        column: x => x.StoreID,
                        principalTable: "Stores",
                        principalColumn: "StoreID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupplierAssets",
                columns: table => new
                {
                    AssetID = table.Column<int>(type: "int", nullable: false),
                    SupplierID = table.Column<int>(type: "int", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductionDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierAssets", x => new { x.AssetID, x.SupplierID, x.SerialNumber });
                    table.ForeignKey(
                        name: "FK_SupplierAssets_Assets_AssetID",
                        column: x => x.AssetID,
                        principalTable: "Assets",
                        principalColumn: "AssetID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupplierAssets_Suppliers_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseAssets",
                columns: table => new
                {
                    AssetID = table.Column<int>(type: "int", nullable: false),
                    WarehouseID = table.Column<int>(type: "int", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductionDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseAssets", x => new { x.AssetID, x.WarehouseID, x.SerialNumber });
                    table.ForeignKey(
                        name: "FK_WarehouseAssets_Assets_AssetID",
                        column: x => x.AssetID,
                        principalTable: "Assets",
                        principalColumn: "AssetID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WarehouseAssets_Warehouses_WarehouseID",
                        column: x => x.WarehouseID,
                        principalTable: "Warehouses",
                        principalColumn: "WarehouseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseProcesses",
                columns: table => new
                {
                    ProcessID = table.Column<int>(type: "int", nullable: false),
                    WarehouseID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseProcesses", x => new { x.ProcessID, x.WarehouseID });
                    table.ForeignKey(
                        name: "FK_WarehouseProcesses_DeliveryProcessSuW_ProcessID",
                        column: x => x.ProcessID,
                        principalTable: "DeliveryProcessSuW",
                        principalColumn: "ProcessID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WarehouseProcesses_Warehouses_WarehouseID",
                        column: x => x.WarehouseID,
                        principalTable: "Warehouses",
                        principalColumn: "WarehouseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoreProcesses",
                columns: table => new
                {
                    ProcessID = table.Column<int>(type: "int", nullable: false),
                    StoreID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreProcesses", x => new { x.ProcessID, x.StoreID });
                    table.ForeignKey(
                        name: "FK_StoreProcesses_DeliveryProcessWSt_ProcessID",
                        column: x => x.ProcessID,
                        principalTable: "DeliveryProcessWSt",
                        principalColumn: "ProcessID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreProcesses_Stores_StoreID",
                        column: x => x.StoreID,
                        principalTable: "Stores",
                        principalColumn: "StoreID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoreRequestAssets",
                columns: table => new
                {
                    RequestID = table.Column<int>(type: "int", nullable: false),
                    AsesetID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreRequestAssets", x => new { x.RequestID, x.AsesetID });
                    table.ForeignKey(
                        name: "FK_StoreRequestAssets_Assets_AsesetID",
                        column: x => x.AsesetID,
                        principalTable: "Assets",
                        principalColumn: "AssetID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreRequestAssets_StoreRequests_RequestID",
                        column: x => x.RequestID,
                        principalTable: "StoreRequests",
                        principalColumn: "RequestID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseRequestAssets",
                columns: table => new
                {
                    RequestID = table.Column<int>(type: "int", nullable: false),
                    AsesetID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseRequestAssets", x => new { x.RequestID, x.AsesetID });
                    table.ForeignKey(
                        name: "FK_WarehouseRequestAssets_Assets_AsesetID",
                        column: x => x.AsesetID,
                        principalTable: "Assets",
                        principalColumn: "AssetID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WarehouseRequestAssets_WarehouseRequests_RequestID",
                        column: x => x.RequestID,
                        principalTable: "WarehouseRequests",
                        principalColumn: "RequestID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssetShipmentSuW",
                columns: table => new
                {
                    AssetID = table.Column<int>(type: "int", nullable: false),
                    SupplierID = table.Column<int>(type: "int", nullable: false),
                    ProcessID = table.Column<int>(type: "int", nullable: false),
                    WarehouseID = table.Column<int>(type: "int", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetShipmentSuW", x => new { x.AssetID, x.SupplierID, x.SerialNumber, x.ProcessID, x.WarehouseID });
                    table.ForeignKey(
                        name: "FK_AssetShipmentSuW_SupplierAssets_AssetID_SupplierID_SerialNumber",
                        columns: x => new { x.AssetID, x.SupplierID, x.SerialNumber },
                        principalTable: "SupplierAssets",
                        principalColumns: new[] { "AssetID", "SupplierID", "SerialNumber" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssetShipmentSuW_WarehouseProcesses_ProcessID_WarehouseID",
                        columns: x => new { x.ProcessID, x.WarehouseID },
                        principalTable: "WarehouseProcesses",
                        principalColumns: new[] { "ProcessID", "WarehouseID" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssetShipmentWSt",
                columns: table => new
                {
                    AssetID = table.Column<int>(type: "int", nullable: false),
                    WarehouseID = table.Column<int>(type: "int", nullable: false),
                    ProcessID = table.Column<int>(type: "int", nullable: false),
                    StoreID = table.Column<int>(type: "int", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetShipmentWSt", x => new { x.AssetID, x.WarehouseID, x.SerialNumber, x.ProcessID, x.StoreID });
                    table.ForeignKey(
                        name: "FK_AssetShipmentWSt_StoreProcesses_ProcessID_StoreID",
                        columns: x => new { x.ProcessID, x.StoreID },
                        principalTable: "StoreProcesses",
                        principalColumns: new[] { "ProcessID", "StoreID" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetShipmentWSt_WarehouseAssets_AssetID_WarehouseID_SerialNumber",
                        columns: x => new { x.AssetID, x.WarehouseID, x.SerialNumber },
                        principalTable: "WarehouseAssets",
                        principalColumns: new[] { "AssetID", "WarehouseID", "SerialNumber" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assets_CategoryID",
                table: "Assets",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_AssetShipmentSuW_ProcessID_WarehouseID",
                table: "AssetShipmentSuW",
                columns: new[] { "ProcessID", "WarehouseID" });

            migrationBuilder.CreateIndex(
                name: "IX_AssetShipmentWSt_ProcessID_StoreID",
                table: "AssetShipmentWSt",
                columns: new[] { "ProcessID", "StoreID" });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryProcessSuW_SupplierID",
                table: "DeliveryProcessSuW",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryProcessWSt_WarehouseID",
                table: "DeliveryProcessWSt",
                column: "WarehouseID");

            migrationBuilder.CreateIndex(
                name: "IX_StoreAssets_StoreID",
                table: "StoreAssets",
                column: "StoreID");

            migrationBuilder.CreateIndex(
                name: "IX_StoreProcesses_StoreID",
                table: "StoreProcesses",
                column: "StoreID");

            migrationBuilder.CreateIndex(
                name: "IX_StoreRequestAssets_AsesetID",
                table: "StoreRequestAssets",
                column: "AsesetID");

            migrationBuilder.CreateIndex(
                name: "IX_StoreRequests_StoreID",
                table: "StoreRequests",
                column: "StoreID");

            migrationBuilder.CreateIndex(
                name: "IX_StoreRequests_WarehouseID",
                table: "StoreRequests",
                column: "WarehouseID");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierAssets_SupplierID",
                table: "SupplierAssets",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseAssets_WarehouseID",
                table: "WarehouseAssets",
                column: "WarehouseID");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseProcesses_WarehouseID",
                table: "WarehouseProcesses",
                column: "WarehouseID");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseRequestAssets_AsesetID",
                table: "WarehouseRequestAssets",
                column: "AsesetID");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseRequests_SupplierID",
                table: "WarehouseRequests",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseRequests_WarehouseID",
                table: "WarehouseRequests",
                column: "WarehouseID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetShipmentSuW");

            migrationBuilder.DropTable(
                name: "AssetShipmentWSt");

            migrationBuilder.DropTable(
                name: "StoreAssets");

            migrationBuilder.DropTable(
                name: "StoreRequestAssets");

            migrationBuilder.DropTable(
                name: "WarehouseRequestAssets");

            migrationBuilder.DropTable(
                name: "SupplierAssets");

            migrationBuilder.DropTable(
                name: "WarehouseProcesses");

            migrationBuilder.DropTable(
                name: "StoreProcesses");

            migrationBuilder.DropTable(
                name: "WarehouseAssets");

            migrationBuilder.DropTable(
                name: "StoreRequests");

            migrationBuilder.DropTable(
                name: "WarehouseRequests");

            migrationBuilder.DropTable(
                name: "DeliveryProcessSuW");

            migrationBuilder.DropTable(
                name: "DeliveryProcessWSt");

            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
