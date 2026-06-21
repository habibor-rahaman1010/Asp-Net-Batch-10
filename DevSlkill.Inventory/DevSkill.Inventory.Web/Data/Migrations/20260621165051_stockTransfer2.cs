using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DevSkill.Inventory.Web.Migrations.InventoryDb
{
    /// <inheritdoc />
    public partial class stockTransfer2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("3619e230-3841-4353-b3b2-4d9cddf8a4fb"));

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("50aa52e1-9082-4f8f-9db0-2a928985d34d"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("2d3fa4f7-afa6-405e-8cd2-a6c8fe4aaad5"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("bd3d3760-62dc-4955-abc4-36ff257a86da"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("e4146b73-7038-4e6f-99c4-0d8e8a3fb160"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("2b5d28b5-2428-4909-82bd-e2bdcb33e336"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("7eb2b715-1372-4b4d-8d74-202758ebf4a2"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("a1d0685b-5165-4e9b-a01b-7f2b014e8f9b"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("7d00e500-b9a3-4708-981c-8b35ef7b10b6"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("8b8d36ae-86e0-49b8-a2a6-121bb377313f"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("a2d2984a-ba4b-4c28-90b1-f5111b35fc23"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("ab37af84-9785-48b3-bd3b-28cfd07fbb48"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("c0888df4-d2f1-40c8-be2b-694edd3b0de8"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("f944fdc2-3618-4321-8740-0d4262d16b2e"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("5d73daa5-3552-4d20-a600-56923d583eb2"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("7355f789-b445-4020-8281-a547b248cd7e"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a93b21ef-f28b-4aaa-8672-ba13ddb91cc2"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("5d3a0fba-08b2-4e82-a66c-5dc0b92f361b"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("64e48cfb-6ac2-4df8-b7d6-6e436d394647"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("6ea75a62-0d99-4322-b89e-6396964b4f72"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("a3a11941-4c10-40d3-aea7-3e41114723e9"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("b4316f5e-2a48-402b-b52c-a7ff198e5473"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("2f082bd7-4408-4bf2-8ab9-666dbcf45925"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("e6d01860-0d90-449c-bfe1-52c9ff53826a"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("ed4c5986-dc7a-4c7d-b2a2-6f03a18ca2f7"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("033f24d1-1a84-4501-8980-123478e201bc"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("0aec21b2-e7b0-4f72-9662-2dc4f24f3cd1"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("306def13-f160-478c-9d11-16e2c5bfbef5"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("1cad0678-a34e-498c-be84-2657159f9514"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("6f013067-df40-4da9-a3d8-2e3c841515a1"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("722082f3-3ec8-4d8d-a408-e4d63c639f42"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("788546b0-245f-41af-ada7-d50ec7ecab02"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("9880549f-dfcd-489c-8bc6-442a563cb6ec"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("f09630bc-c98b-47c2-9539-77927deb473e"));

            migrationBuilder.CreateTable(
                name: "StockTransfers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransferNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransferDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FromWarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToWarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTransfers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockTransferItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StockTransferId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTransferItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockTransferItem_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockTransferItem_StockTransfers_StockTransferId",
                        column: x => x.StockTransferId,
                        principalTable: "StockTransfers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AdjustmentTypes",
                columns: new[] { "Id", "AdjustmentTypeName", "Description", "Sign" },
                values: new object[,]
                {
                    { new Guid("0fcd8d02-609b-4afc-8f41-2fe5653c09d1"), "Abnormal", "", 0 },
                    { new Guid("4095c4aa-0f0c-406c-9c68-7cf54123b628"), "Normal", "", 0 }
                });

            migrationBuilder.InsertData(
                table: "ApplicableTaxs",
                columns: new[] { "Id", "ApplicableTaxName", "Description", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("074a518f-6579-428b-9b28-420735e603cf"), "Food", "", 0m },
                    { new Guid("51b67a8a-2145-4510-8449-07aabdcbe6d9"), "Fruits", "", 0m },
                    { new Guid("65bec287-491e-4049-913b-9f89a6902477"), "Sales Tax", "", 0m }
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "BarcodeDescription", "BarcodeTypeCode", "BarcodeTypeName" },
                values: new object[,]
                {
                    { new Guid("1105b21b-5d98-41a5-a5af-ab2ef3ee9812"), "", "", "NFC" },
                    { new Guid("60f1519a-0abd-462e-adf6-03adf4246aa8"), "", "", "UPC" },
                    { new Guid("aa022281-d204-4576-bdaa-8620c52b6f60"), "", "", "QR Code" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BandOrigin", "BrandName", "Description" },
                values: new object[,]
                {
                    { new Guid("08a94d1a-e513-44f4-af63-920de1946599"), "", "Apple", "" },
                    { new Guid("ae29428a-2664-4965-8f7a-02c213e39a94"), "", "Samsung", "" },
                    { new Guid("d0688470-aabc-41d7-a41e-281ca55a9c9a"), "", "Sony", "" }
                });

            migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "Address", "City", "Country", "LocationName", "State", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("363f9a86-a5da-4d06-a988-5480bf6c9ccf"), "", "", "", "Downtown Store", "", "" },
                    { new Guid("405624dc-eafa-4d2f-bca9-35f42f7c3a9c"), "", "", "", "Warehouse B", "", "" },
                    { new Guid("eaabb893-8ab6-4209-9668-8671d245fd78"), "", "", "", "Warehouse A", "", "" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryCode", "CategoryName", "Description" },
                values: new object[,]
                {
                    { new Guid("4850b835-1630-4294-bd9d-217f3d04e8c3"), "", "Electronics", "" },
                    { new Guid("50d6edae-0c51-4748-af99-683bd0bf061e"), "", "Home Appliances", "" },
                    { new Guid("d7256850-fae9-43a1-8880-bbf10c44c859"), "", "Clothing", "" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Description", "ProductTypeCode", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("00e331b0-8aa1-41ab-a503-fa681c74667f"), "", "", "Toys" },
                    { new Guid("161821a0-b62b-487d-b314-7cee51d828df"), "", "", "Furniture" },
                    { new Guid("85e924d8-77d6-41b3-be6a-b4e5960c343d"), "", "", "Food" },
                    { new Guid("ad117cc3-fc4b-48ab-b698-216d9b2ffe64"), "", "", "Clothing" },
                    { new Guid("b9910dd4-edd9-4441-aa97-4b68bfc5265d"), "", "", "Electronics" }
                });

            migrationBuilder.InsertData(
                table: "SellingPriceTaxes",
                columns: new[] { "Id", "Description", "SellingPriceTaxName", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("b86d9049-2d05-426a-abec-42b88d88a731"), "", "Exclusive", 0m },
                    { new Guid("d7deb087-0936-4a2f-a11a-6b4c8553141e"), "", "Inclusive", 0m },
                    { new Guid("e1907d5c-60d1-420b-9e1a-901ad0b03313"), "", "Zero Rate", 0m }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryCode", "Description", "SubCategoryName" },
                values: new object[,]
                {
                    { new Guid("090cd748-7832-4149-9a69-6cc9683d5fdc"), "", "", "Laptops" },
                    { new Guid("4a048301-32c4-4107-9889-74826d1fc3b6"), "", "", "Televisions" },
                    { new Guid("cc1a41cb-ac8e-4cd3-942d-0224253e333a"), "", "", "Smartphones" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "AllowDecimal", "ShortName", "UnitName" },
                values: new object[,]
                {
                    { new Guid("21fe0b5e-c885-49d2-9e93-d7d2456d05b7"), 0, "", "Piece" },
                    { new Guid("811ec4f9-23d2-4ba1-88ea-3af0a5a2e5fa"), 0, "", "Liter" },
                    { new Guid("9cfa179f-aef3-490d-871f-c868a85aa32d"), 0, "", "Kilogram" }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "Description", "Name", "WarrantyDuration" },
                values: new object[,]
                {
                    { new Guid("11739037-7797-4f8e-b1d2-41ed2dfd7e4c"), "", "", "1 Year" },
                    { new Guid("267ec9ab-5405-4fda-a988-513131166334"), "", "", "2 Years" },
                    { new Guid("e65d4f83-cf7d-422d-a9a5-78caa56a0eb5"), "", "", "3 Years" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockTransferItem_ProductId",
                table: "StockTransferItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransferItem_StockTransferId",
                table: "StockTransferItem",
                column: "StockTransferId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockTransferItem");

            migrationBuilder.DropTable(
                name: "StockTransfers");

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("0fcd8d02-609b-4afc-8f41-2fe5653c09d1"));

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("4095c4aa-0f0c-406c-9c68-7cf54123b628"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("074a518f-6579-428b-9b28-420735e603cf"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("51b67a8a-2145-4510-8449-07aabdcbe6d9"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("65bec287-491e-4049-913b-9f89a6902477"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("1105b21b-5d98-41a5-a5af-ab2ef3ee9812"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("60f1519a-0abd-462e-adf6-03adf4246aa8"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("aa022281-d204-4576-bdaa-8620c52b6f60"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("08a94d1a-e513-44f4-af63-920de1946599"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("ae29428a-2664-4965-8f7a-02c213e39a94"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("d0688470-aabc-41d7-a41e-281ca55a9c9a"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("363f9a86-a5da-4d06-a988-5480bf6c9ccf"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("405624dc-eafa-4d2f-bca9-35f42f7c3a9c"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("eaabb893-8ab6-4209-9668-8671d245fd78"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("4850b835-1630-4294-bd9d-217f3d04e8c3"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("50d6edae-0c51-4748-af99-683bd0bf061e"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d7256850-fae9-43a1-8880-bbf10c44c859"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("00e331b0-8aa1-41ab-a503-fa681c74667f"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("161821a0-b62b-487d-b314-7cee51d828df"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("85e924d8-77d6-41b3-be6a-b4e5960c343d"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("ad117cc3-fc4b-48ab-b698-216d9b2ffe64"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("b9910dd4-edd9-4441-aa97-4b68bfc5265d"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("b86d9049-2d05-426a-abec-42b88d88a731"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("d7deb087-0936-4a2f-a11a-6b4c8553141e"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("e1907d5c-60d1-420b-9e1a-901ad0b03313"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("090cd748-7832-4149-9a69-6cc9683d5fdc"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("4a048301-32c4-4107-9889-74826d1fc3b6"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("cc1a41cb-ac8e-4cd3-942d-0224253e333a"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("21fe0b5e-c885-49d2-9e93-d7d2456d05b7"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("811ec4f9-23d2-4ba1-88ea-3af0a5a2e5fa"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("9cfa179f-aef3-490d-871f-c868a85aa32d"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("11739037-7797-4f8e-b1d2-41ed2dfd7e4c"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("267ec9ab-5405-4fda-a988-513131166334"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("e65d4f83-cf7d-422d-a9a5-78caa56a0eb5"));

            migrationBuilder.InsertData(
                table: "AdjustmentTypes",
                columns: new[] { "Id", "AdjustmentTypeName", "Description", "Sign" },
                values: new object[,]
                {
                    { new Guid("3619e230-3841-4353-b3b2-4d9cddf8a4fb"), "Normal", "", 0 },
                    { new Guid("50aa52e1-9082-4f8f-9db0-2a928985d34d"), "Abnormal", "", 0 }
                });

            migrationBuilder.InsertData(
                table: "ApplicableTaxs",
                columns: new[] { "Id", "ApplicableTaxName", "Description", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("2d3fa4f7-afa6-405e-8cd2-a6c8fe4aaad5"), "Fruits", "", 0m },
                    { new Guid("bd3d3760-62dc-4955-abc4-36ff257a86da"), "Food", "", 0m },
                    { new Guid("e4146b73-7038-4e6f-99c4-0d8e8a3fb160"), "Sales Tax", "", 0m }
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "BarcodeDescription", "BarcodeTypeCode", "BarcodeTypeName" },
                values: new object[,]
                {
                    { new Guid("2b5d28b5-2428-4909-82bd-e2bdcb33e336"), "", "", "QR Code" },
                    { new Guid("7eb2b715-1372-4b4d-8d74-202758ebf4a2"), "", "", "NFC" },
                    { new Guid("a1d0685b-5165-4e9b-a01b-7f2b014e8f9b"), "", "", "UPC" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BandOrigin", "BrandName", "Description" },
                values: new object[,]
                {
                    { new Guid("7d00e500-b9a3-4708-981c-8b35ef7b10b6"), "", "Apple", "" },
                    { new Guid("8b8d36ae-86e0-49b8-a2a6-121bb377313f"), "", "Sony", "" },
                    { new Guid("a2d2984a-ba4b-4c28-90b1-f5111b35fc23"), "", "Samsung", "" }
                });

            migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "Address", "City", "Country", "LocationName", "State", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("ab37af84-9785-48b3-bd3b-28cfd07fbb48"), "", "", "", "Downtown Store", "", "" },
                    { new Guid("c0888df4-d2f1-40c8-be2b-694edd3b0de8"), "", "", "", "Warehouse A", "", "" },
                    { new Guid("f944fdc2-3618-4321-8740-0d4262d16b2e"), "", "", "", "Warehouse B", "", "" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryCode", "CategoryName", "Description" },
                values: new object[,]
                {
                    { new Guid("5d73daa5-3552-4d20-a600-56923d583eb2"), "", "Electronics", "" },
                    { new Guid("7355f789-b445-4020-8281-a547b248cd7e"), "", "Home Appliances", "" },
                    { new Guid("a93b21ef-f28b-4aaa-8672-ba13ddb91cc2"), "", "Clothing", "" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Description", "ProductTypeCode", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("5d3a0fba-08b2-4e82-a66c-5dc0b92f361b"), "", "", "Furniture" },
                    { new Guid("64e48cfb-6ac2-4df8-b7d6-6e436d394647"), "", "", "Food" },
                    { new Guid("6ea75a62-0d99-4322-b89e-6396964b4f72"), "", "", "Clothing" },
                    { new Guid("a3a11941-4c10-40d3-aea7-3e41114723e9"), "", "", "Toys" },
                    { new Guid("b4316f5e-2a48-402b-b52c-a7ff198e5473"), "", "", "Electronics" }
                });

            migrationBuilder.InsertData(
                table: "SellingPriceTaxes",
                columns: new[] { "Id", "Description", "SellingPriceTaxName", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("2f082bd7-4408-4bf2-8ab9-666dbcf45925"), "", "Zero Rate", 0m },
                    { new Guid("e6d01860-0d90-449c-bfe1-52c9ff53826a"), "", "Exclusive", 0m },
                    { new Guid("ed4c5986-dc7a-4c7d-b2a2-6f03a18ca2f7"), "", "Inclusive", 0m }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryCode", "Description", "SubCategoryName" },
                values: new object[,]
                {
                    { new Guid("033f24d1-1a84-4501-8980-123478e201bc"), "", "", "Televisions" },
                    { new Guid("0aec21b2-e7b0-4f72-9662-2dc4f24f3cd1"), "", "", "Smartphones" },
                    { new Guid("306def13-f160-478c-9d11-16e2c5bfbef5"), "", "", "Laptops" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "AllowDecimal", "ShortName", "UnitName" },
                values: new object[,]
                {
                    { new Guid("1cad0678-a34e-498c-be84-2657159f9514"), 0, "", "Liter" },
                    { new Guid("6f013067-df40-4da9-a3d8-2e3c841515a1"), 0, "", "Piece" },
                    { new Guid("722082f3-3ec8-4d8d-a408-e4d63c639f42"), 0, "", "Kilogram" }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "Description", "Name", "WarrantyDuration" },
                values: new object[,]
                {
                    { new Guid("788546b0-245f-41af-ada7-d50ec7ecab02"), "", "", "1 Year" },
                    { new Guid("9880549f-dfcd-489c-8bc6-442a563cb6ec"), "", "", "2 Years" },
                    { new Guid("f09630bc-c98b-47c2-9539-77927deb473e"), "", "", "3 Years" }
                });
        }
    }
}
