using System;
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
                name: "FK_AssetShipmentSuW_SupplierAssets_AssetID_SupplierID_AssetCreationDate",
                table: "AssetShipmentSuW");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetShipmentWSt_WarehouseAssets_AssetID_WarehouseID_AssetCreationDate",
                table: "AssetShipmentWSt");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WarehouseAssets",
                table: "WarehouseAssets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplierAssets",
                table: "SupplierAssets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StoreAssets",
                table: "StoreAssets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssetShipmentWSt",
                table: "AssetShipmentWSt");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssetShipmentSuW",
                table: "AssetShipmentSuW");

            migrationBuilder.DropColumn(
                name: "AssetCreationDate",
                table: "AssetShipmentWSt");

            migrationBuilder.DropColumn(
                name: "AssetCreationDate",
                table: "AssetShipmentSuW");

            migrationBuilder.DropColumn(
                name: "SerialNumber",
                table: "Assets");

            migrationBuilder.RenameColumn(
                name: "AssetCreationDate",
                table: "WarehouseAssets",
                newName: "ProductionDate");

            migrationBuilder.RenameColumn(
                name: "AssetCreationDate",
                table: "SupplierAssets",
                newName: "ProductionDate");

            migrationBuilder.RenameColumn(
                name: "AssetCreationDate",
                table: "StoreAssets",
                newName: "ProductionDate");

            migrationBuilder.AddColumn<string>(
                name: "SerialNo",
                table: "WarehouseAssets",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SerialNo",
                table: "SupplierAssets",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SerialNo",
                table: "StoreAssets",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SerialNo",
                table: "AssetShipmentWSt",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SerialNo",
                table: "AssetShipmentSuW",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "Assets",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Assets",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "AssetName",
                table: "Assets",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarehouseAssets",
                table: "WarehouseAssets",
                columns: new[] { "AssetID", "WarehouseID", "SerialNo" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupplierAssets",
                table: "SupplierAssets",
                columns: new[] { "AssetID", "SupplierID", "SerialNo" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_StoreAssets",
                table: "StoreAssets",
                columns: new[] { "AssetID", "StoreID", "SerialNo" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssetShipmentWSt",
                table: "AssetShipmentWSt",
                columns: new[] { "AssetID", "WarehouseID", "SerialNo", "ProcessID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssetShipmentSuW",
                table: "AssetShipmentSuW",
                columns: new[] { "AssetID", "SupplierID", "SerialNo", "ProcessID" });

            migrationBuilder.AddForeignKey(
                name: "FK_AssetShipmentSuW_SupplierAssets_AssetID_SupplierID_SerialNo",
                table: "AssetShipmentSuW",
                columns: new[] { "AssetID", "SupplierID", "SerialNo" },
                principalTable: "SupplierAssets",
                principalColumns: new[] { "AssetID", "SupplierID", "SerialNo" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssetShipmentWSt_WarehouseAssets_AssetID_WarehouseID_SerialNo",
                table: "AssetShipmentWSt",
                columns: new[] { "AssetID", "WarehouseID", "SerialNo" },
                principalTable: "WarehouseAssets",
                principalColumns: new[] { "AssetID", "WarehouseID", "SerialNo" },
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetShipmentSuW_SupplierAssets_AssetID_SupplierID_SerialNo",
                table: "AssetShipmentSuW");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetShipmentWSt_WarehouseAssets_AssetID_WarehouseID_SerialNo",
                table: "AssetShipmentWSt");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WarehouseAssets",
                table: "WarehouseAssets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplierAssets",
                table: "SupplierAssets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StoreAssets",
                table: "StoreAssets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssetShipmentWSt",
                table: "AssetShipmentWSt");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssetShipmentSuW",
                table: "AssetShipmentSuW");

            migrationBuilder.DropColumn(
                name: "SerialNo",
                table: "WarehouseAssets");

            migrationBuilder.DropColumn(
                name: "SerialNo",
                table: "SupplierAssets");

            migrationBuilder.DropColumn(
                name: "SerialNo",
                table: "StoreAssets");

            migrationBuilder.DropColumn(
                name: "SerialNo",
                table: "AssetShipmentWSt");

            migrationBuilder.DropColumn(
                name: "SerialNo",
                table: "AssetShipmentSuW");

            migrationBuilder.RenameColumn(
                name: "ProductionDate",
                table: "WarehouseAssets",
                newName: "AssetCreationDate");

            migrationBuilder.RenameColumn(
                name: "ProductionDate",
                table: "SupplierAssets",
                newName: "AssetCreationDate");

            migrationBuilder.RenameColumn(
                name: "ProductionDate",
                table: "StoreAssets",
                newName: "AssetCreationDate");

            migrationBuilder.AddColumn<DateOnly>(
                name: "AssetCreationDate",
                table: "AssetShipmentWSt",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "AssetCreationDate",
                table: "AssetShipmentSuW",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "Assets",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "AssetName",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "SerialNumber",
                table: "Assets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarehouseAssets",
                table: "WarehouseAssets",
                columns: new[] { "AssetID", "WarehouseID", "AssetCreationDate" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupplierAssets",
                table: "SupplierAssets",
                columns: new[] { "AssetID", "SupplierID", "AssetCreationDate" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_StoreAssets",
                table: "StoreAssets",
                columns: new[] { "AssetID", "StoreID", "AssetCreationDate" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssetShipmentWSt",
                table: "AssetShipmentWSt",
                columns: new[] { "AssetID", "WarehouseID", "AssetCreationDate", "ProcessID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssetShipmentSuW",
                table: "AssetShipmentSuW",
                columns: new[] { "AssetID", "SupplierID", "AssetCreationDate", "ProcessID" });

            migrationBuilder.AddForeignKey(
                name: "FK_AssetShipmentSuW_SupplierAssets_AssetID_SupplierID_AssetCreationDate",
                table: "AssetShipmentSuW",
                columns: new[] { "AssetID", "SupplierID", "AssetCreationDate" },
                principalTable: "SupplierAssets",
                principalColumns: new[] { "AssetID", "SupplierID", "AssetCreationDate" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssetShipmentWSt_WarehouseAssets_AssetID_WarehouseID_AssetCreationDate",
                table: "AssetShipmentWSt",
                columns: new[] { "AssetID", "WarehouseID", "AssetCreationDate" },
                principalTable: "WarehouseAssets",
                principalColumns: new[] { "AssetID", "WarehouseID", "AssetCreationDate" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
