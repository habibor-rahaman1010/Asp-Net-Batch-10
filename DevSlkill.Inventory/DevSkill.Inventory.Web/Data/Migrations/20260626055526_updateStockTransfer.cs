using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DevSkill.Inventory.Web.Migrations.InventoryDb
{
    /// <inheritdoc />
    public partial class updateStockTransfer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockTransferItem_Products_ProductId",
                table: "StockTransferItem");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransferItem_StockTransfers_StockTransferId",
                table: "StockTransferItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockTransferItem",
                table: "StockTransferItem");

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

            migrationBuilder.RenameTable(
                name: "StockTransferItem",
                newName: "StockTransferItems");

            migrationBuilder.RenameIndex(
                name: "IX_StockTransferItem_StockTransferId",
                table: "StockTransferItems",
                newName: "IX_StockTransferItems_StockTransferId");

            migrationBuilder.RenameIndex(
                name: "IX_StockTransferItem_ProductId",
                table: "StockTransferItems",
                newName: "IX_StockTransferItems_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockTransferItems",
                table: "StockTransferItems",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AdjustmentTypes",
                columns: new[] { "Id", "AdjustmentTypeName", "Description", "Sign" },
                values: new object[,]
                {
                    { new Guid("9bdd4f6d-eefd-4d7e-b8a9-2c28e6e21441"), "Abnormal", "", 0 },
                    { new Guid("d7e491cb-e8bd-4bd5-84cd-cd3fa580f17b"), "Normal", "", 0 }
                });

            migrationBuilder.InsertData(
                table: "ApplicableTaxs",
                columns: new[] { "Id", "ApplicableTaxName", "Description", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("7abad6bf-826c-4d7b-9933-c806cdba7c66"), "Food", "", 0m },
                    { new Guid("7e1dacd6-d128-485d-9668-b500965ba366"), "Fruits", "", 0m },
                    { new Guid("c068f13b-b831-4f9e-948e-08d31b400050"), "Sales Tax", "", 0m }
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "BarcodeDescription", "BarcodeTypeCode", "BarcodeTypeName" },
                values: new object[,]
                {
                    { new Guid("66011cbb-7de8-4baa-a347-5be00b6178bf"), "", "", "UPC" },
                    { new Guid("91b50023-2ee1-4bb8-9d40-26769c205005"), "", "", "NFC" },
                    { new Guid("b886eb72-f495-471e-8a32-c8d80721e938"), "", "", "QR Code" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BandOrigin", "BrandName", "Description" },
                values: new object[,]
                {
                    { new Guid("0f520da9-1196-4e97-a82a-71c1a036037a"), "", "Apple", "" },
                    { new Guid("414e69a9-9e11-4cc5-b65f-7b7a3ee7bb52"), "", "Sony", "" },
                    { new Guid("cdd913cc-40b8-4c18-95ea-a0dce3e9e32e"), "", "Samsung", "" }
                });

            migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "Address", "City", "Country", "LocationName", "State", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("3ff68f26-ece4-4af0-8403-a79ec36c2ef1"), "", "", "", "Downtown Store", "", "" },
                    { new Guid("abafd160-1078-41e2-84f3-d3e2a763355b"), "", "", "", "Warehouse B", "", "" },
                    { new Guid("cf20deba-99b9-4dc9-9990-4ca16048dab9"), "", "", "", "Warehouse A", "", "" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryCode", "CategoryName", "Description" },
                values: new object[,]
                {
                    { new Guid("50c25fe0-3b61-49cf-abc0-f7bd14a7d637"), "", "Electronics", "" },
                    { new Guid("b9346764-a829-4f0c-9cdc-d69d138e36de"), "", "Home Appliances", "" },
                    { new Guid("e4ce9f5a-2c6b-4fef-a25f-ea072bb86036"), "", "Clothing", "" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Description", "ProductTypeCode", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("175c061c-a624-4ab3-b4bb-d4b78c3c666a"), "", "", "Toys" },
                    { new Guid("1e8562ba-bca7-47ac-b1b2-df08c8c5046c"), "", "", "Furniture" },
                    { new Guid("92e08a38-a365-454e-8f2c-3501d15ac709"), "", "", "Electronics" },
                    { new Guid("ce9fc1a2-b7be-4fd2-8f9b-560898dd0704"), "", "", "Food" },
                    { new Guid("e9191739-2ffe-4d94-9c0f-163db8d4857e"), "", "", "Clothing" }
                });

            migrationBuilder.InsertData(
                table: "SellingPriceTaxes",
                columns: new[] { "Id", "Description", "SellingPriceTaxName", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("282df0e8-a72d-4785-9c42-6da70330e156"), "", "Zero Rate", 0m },
                    { new Guid("c8ee138a-9c03-41e3-abae-3d771961d6f9"), "", "Inclusive", 0m },
                    { new Guid("e2de71cb-267b-4214-ab2d-c7646c2c5acd"), "", "Exclusive", 0m }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryCode", "Description", "SubCategoryName" },
                values: new object[,]
                {
                    { new Guid("26f09b3e-4b0e-4a1e-affc-af401877c6e3"), "", "", "Laptops" },
                    { new Guid("422bb16f-c2e1-4417-b655-75a96fa4eda3"), "", "", "Smartphones" },
                    { new Guid("d58bebe8-c2e2-4ff9-a64f-486a4991ad3f"), "", "", "Televisions" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "AllowDecimal", "ShortName", "UnitName" },
                values: new object[,]
                {
                    { new Guid("28863918-0fd0-45a4-ae64-37dd99a64506"), 0, "", "Liter" },
                    { new Guid("8caf1fe0-5a5f-4817-ab5f-248adbf774b8"), 0, "", "Kilogram" },
                    { new Guid("f3439779-9b8b-4dc3-a2ac-5cff2acb86b3"), 0, "", "Piece" }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "Description", "Name", "WarrantyDuration" },
                values: new object[,]
                {
                    { new Guid("1d0591a3-1492-4d3c-a57b-481852c6c41b"), "", "", "3 Years" },
                    { new Guid("1ee65c73-2cf3-418d-82b5-967d96f68517"), "", "", "2 Years" },
                    { new Guid("70a84413-b924-4a04-ab76-cf51e021d1f4"), "", "", "1 Year" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransferItems_Products_ProductId",
                table: "StockTransferItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransferItems_StockTransfers_StockTransferId",
                table: "StockTransferItems",
                column: "StockTransferId",
                principalTable: "StockTransfers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockTransferItems_Products_ProductId",
                table: "StockTransferItems");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransferItems_StockTransfers_StockTransferId",
                table: "StockTransferItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockTransferItems",
                table: "StockTransferItems");

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("9bdd4f6d-eefd-4d7e-b8a9-2c28e6e21441"));

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("d7e491cb-e8bd-4bd5-84cd-cd3fa580f17b"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("7abad6bf-826c-4d7b-9933-c806cdba7c66"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("7e1dacd6-d128-485d-9668-b500965ba366"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("c068f13b-b831-4f9e-948e-08d31b400050"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("66011cbb-7de8-4baa-a347-5be00b6178bf"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("91b50023-2ee1-4bb8-9d40-26769c205005"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("b886eb72-f495-471e-8a32-c8d80721e938"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("0f520da9-1196-4e97-a82a-71c1a036037a"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("414e69a9-9e11-4cc5-b65f-7b7a3ee7bb52"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("cdd913cc-40b8-4c18-95ea-a0dce3e9e32e"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("3ff68f26-ece4-4af0-8403-a79ec36c2ef1"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("abafd160-1078-41e2-84f3-d3e2a763355b"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("cf20deba-99b9-4dc9-9990-4ca16048dab9"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("50c25fe0-3b61-49cf-abc0-f7bd14a7d637"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b9346764-a829-4f0c-9cdc-d69d138e36de"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("e4ce9f5a-2c6b-4fef-a25f-ea072bb86036"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("175c061c-a624-4ab3-b4bb-d4b78c3c666a"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("1e8562ba-bca7-47ac-b1b2-df08c8c5046c"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("92e08a38-a365-454e-8f2c-3501d15ac709"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("ce9fc1a2-b7be-4fd2-8f9b-560898dd0704"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("e9191739-2ffe-4d94-9c0f-163db8d4857e"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("282df0e8-a72d-4785-9c42-6da70330e156"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("c8ee138a-9c03-41e3-abae-3d771961d6f9"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("e2de71cb-267b-4214-ab2d-c7646c2c5acd"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("26f09b3e-4b0e-4a1e-affc-af401877c6e3"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("422bb16f-c2e1-4417-b655-75a96fa4eda3"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("d58bebe8-c2e2-4ff9-a64f-486a4991ad3f"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("28863918-0fd0-45a4-ae64-37dd99a64506"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("8caf1fe0-5a5f-4817-ab5f-248adbf774b8"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("f3439779-9b8b-4dc3-a2ac-5cff2acb86b3"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("1d0591a3-1492-4d3c-a57b-481852c6c41b"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("1ee65c73-2cf3-418d-82b5-967d96f68517"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("70a84413-b924-4a04-ab76-cf51e021d1f4"));

            migrationBuilder.RenameTable(
                name: "StockTransferItems",
                newName: "StockTransferItem");

            migrationBuilder.RenameIndex(
                name: "IX_StockTransferItems_StockTransferId",
                table: "StockTransferItem",
                newName: "IX_StockTransferItem_StockTransferId");

            migrationBuilder.RenameIndex(
                name: "IX_StockTransferItems_ProductId",
                table: "StockTransferItem",
                newName: "IX_StockTransferItem_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockTransferItem",
                table: "StockTransferItem",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransferItem_Products_ProductId",
                table: "StockTransferItem",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransferItem_StockTransfers_StockTransferId",
                table: "StockTransferItem",
                column: "StockTransferId",
                principalTable: "StockTransfers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
