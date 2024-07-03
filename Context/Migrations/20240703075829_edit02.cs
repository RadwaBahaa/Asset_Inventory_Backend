using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Context.Migrations
{
    /// <inheritdoc />
    public partial class edit02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetShipmentSuW_SupplierAssets_AssetID_WarehouseID_SerialNumber",
                table: "AssetShipmentSuW");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetShipmentWSt_WarehouseAssets_AssetID_WarehouseID_SerialNumber",
                table: "AssetShipmentWSt");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetShipmentSuW_SupplierAssets_AssetID_WarehouseID_SerialNumber",
                table: "AssetShipmentSuW",
                columns: new[] { "AssetID", "WarehouseID", "SerialNumber" },
                principalTable: "SupplierAssets",
                principalColumns: new[] { "AssetID", "SupplierID", "SerialNumber" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssetShipmentWSt_WarehouseAssets_AssetID_WarehouseID_SerialNumber",
                table: "AssetShipmentWSt",
                columns: new[] { "AssetID", "WarehouseID", "SerialNumber" },
                principalTable: "WarehouseAssets",
                principalColumns: new[] { "AssetID", "WarehouseID", "SerialNumber" },
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetShipmentSuW_SupplierAssets_AssetID_WarehouseID_SerialNumber",
                table: "AssetShipmentSuW");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetShipmentWSt_WarehouseAssets_AssetID_WarehouseID_SerialNumber",
                table: "AssetShipmentWSt");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetShipmentSuW_SupplierAssets_AssetID_WarehouseID_SerialNumber",
                table: "AssetShipmentSuW",
                columns: new[] { "AssetID", "WarehouseID", "SerialNumber" },
                principalTable: "SupplierAssets",
                principalColumns: new[] { "AssetID", "SupplierID", "SerialNumber" });

            migrationBuilder.AddForeignKey(
                name: "FK_AssetShipmentWSt_WarehouseAssets_AssetID_WarehouseID_SerialNumber",
                table: "AssetShipmentWSt",
                columns: new[] { "AssetID", "WarehouseID", "SerialNumber" },
                principalTable: "WarehouseAssets",
                principalColumns: new[] { "AssetID", "WarehouseID", "SerialNumber" });
        }
    }
}
