using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Context.Migrations
{
    /// <inheritdoc />
    public partial class edit01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetShipmentSuW_SupplierAssets_AssetID_SupplierID_SerialNumber",
                table: "AssetShipmentSuW");

            migrationBuilder.CreateIndex(
                name: "IX_AssetShipmentSuW_AssetID_WarehouseID_SerialNumber",
                table: "AssetShipmentSuW",
                columns: new[] { "AssetID", "WarehouseID", "SerialNumber" });

            migrationBuilder.AddForeignKey(
                name: "FK_AssetShipmentSuW_SupplierAssets_AssetID_WarehouseID_SerialNumber",
                table: "AssetShipmentSuW",
                columns: new[] { "AssetID", "WarehouseID", "SerialNumber" },
                principalTable: "SupplierAssets",
                principalColumns: new[] { "AssetID", "SupplierID", "SerialNumber" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetShipmentSuW_SupplierAssets_AssetID_WarehouseID_SerialNumber",
                table: "AssetShipmentSuW");

            migrationBuilder.DropIndex(
                name: "IX_AssetShipmentSuW_AssetID_WarehouseID_SerialNumber",
                table: "AssetShipmentSuW");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetShipmentSuW_SupplierAssets_AssetID_SupplierID_SerialNumber",
                table: "AssetShipmentSuW",
                columns: new[] { "AssetID", "SupplierID", "SerialNumber" },
                principalTable: "SupplierAssets",
                principalColumns: new[] { "AssetID", "SupplierID", "SerialNumber" });
        }
    }
}
