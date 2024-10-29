using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DevSkill.Inventory.Web.Migrations.InventoryDb
{
    /// <inheritdoc />
    public partial class UpdateSubCategoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("4c9ec6b9-4c2d-44b2-a213-ba415d22e596"));

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("d792d357-2cb0-4be8-9eef-4b0eb5940b21"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("2ec37c31-cc19-443d-9c0c-60ff9bc92a8f"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("b91c8349-9055-40dc-bb01-c0f5cfd403ee"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("c8f6e65e-0c4b-4dca-88ec-c827aab668e7"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("0ea42e18-29a2-4548-9c34-e9d0ac2bea0f"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("1cea87b2-e68b-4eea-9c6a-8bb70e4d7310"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("ef2b421e-9828-4695-834a-49969d53e40f"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("07cb5e1e-ad9e-4c83-8879-bc685c77b2fb"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("560e25c2-b2e1-4a53-9b1c-68d2206101f3"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("f70ddace-ccc9-426e-a6e7-07a1122ab3b5"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("a5447030-fcdd-47c6-8d5b-4d586786aefc"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("ca62abf0-7b48-4d8c-ab5f-73e1085d3d2e"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("f8c7878b-6ae2-4672-a44a-7eb4a0d36a80"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("5de27ee3-f4da-4d08-b44c-5efdf534d970"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a9eb6dc3-4b2f-4dd5-a531-c35f39ab4236"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c00a9ad5-2383-4b6d-a1eb-6bb88535b66a"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("0b670a9f-f760-4426-bad6-6aabc3c008a2"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("4ee96eb1-efc5-4f95-bd8f-95d68e447814"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("6395a2b6-c5a3-46b0-bd22-b64173efec51"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("cf871616-1404-4845-bf81-cef215256cd8"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("d2518f3b-d9fb-4aa7-87bb-b2254aee7e03"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("6eb66c7a-a16e-41ac-9620-a8778a74fad2"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("934f62b0-4b1d-46d7-9d8c-30514dee4f5c"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("f76bf612-7b99-4fcb-ab45-2b0f8c9e1481"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("02b87e28-e57d-42b3-8ef5-fe692717f7cf"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("4fa51139-b54b-4712-990e-a17e1c8d65f4"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("5571fd0c-a7ca-418b-8193-994f1e3fe893"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("225071aa-166b-4770-b33c-2bdb567f754e"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("6f361be4-c316-409d-b612-3adfb6ba3a1a"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("b51e5576-d1d0-4bb7-a582-472fbc69d1fa"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("3456ab70-fbaa-4d80-8250-4df7d425587d"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("3e70c216-b710-4fea-9ab6-5c208040fb74"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("d23184a8-6e3f-435e-88d5-d9cca50b502e"));

            migrationBuilder.InsertData(
                table: "AdjustmentTypes",
                columns: new[] { "Id", "AdjustmentTypeName", "Description" },
                values: new object[,]
                {
                    { new Guid("3a4a4b24-07ac-44fd-bd0c-c5557da9b721"), "Normal", "" },
                    { new Guid("5795909e-9782-44a7-907d-25ecf281e4f5"), "Abnormal", "" }
                });

            migrationBuilder.InsertData(
                table: "ApplicableTaxs",
                columns: new[] { "Id", "ApplicableTaxName", "Description", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("17a9927d-4a25-4bcb-94a8-f38873c6c169"), "Sales Tax", "", 0m },
                    { new Guid("7587a037-dbc9-46cb-8f1a-997c82ab3829"), "Fruits", "", 0m },
                    { new Guid("d0b176e4-b591-4c2f-b667-24db1bf07bbf"), "Food", "", 0m }
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "BarcodeDescription", "BarcodeTypeCode", "BarcodeTypeName" },
                values: new object[,]
                {
                    { new Guid("24097fb3-9274-49dc-9dd9-fcdbe7dabd8e"), "", "", "NFC" },
                    { new Guid("864c7333-6cce-419c-b9cb-796482075c48"), "", "", "QR Code" },
                    { new Guid("bd9a77ae-688a-411f-9b7d-27829b16cfdd"), "", "", "UPC" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BandOrigin", "BrandName", "Description" },
                values: new object[,]
                {
                    { new Guid("24801d9d-2f99-45e6-9638-0d751b0f63ae"), "", "Apple", "" },
                    { new Guid("b6bc0013-d58a-4ff9-8332-2fcdb3c4f685"), "", "Sony", "" },
                    { new Guid("d83da2ca-6e0d-4a66-a60f-48bfe8f56fa7"), "", "Samsung", "" }
                });

            migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "Address", "City", "Country", "LocationName", "State", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("c319526b-a5f6-477e-ac5a-0e087f1afb09"), "", "", "", "Warehouse B", "", "" },
                    { new Guid("c93f5ff4-3cdc-495b-8333-21d5bd5de637"), "", "", "", "Warehouse A", "", "" },
                    { new Guid("edcb0afc-1080-465a-931f-a37c8fa5c41a"), "", "", "", "Downtown Store", "", "" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryCode", "CategoryName", "Description" },
                values: new object[,]
                {
                    { new Guid("4d53e565-8400-43c7-b6c3-6cc8738524ad"), "", "Home Appliances", "" },
                    { new Guid("7812ef7b-b3f9-45af-a130-88468e22a329"), "", "Electronics", "" },
                    { new Guid("fb79515a-e23e-483d-888a-12d0ad7cf7f7"), "", "Clothing", "" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Description", "ProductTypeCode", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("05cfbb2d-d7d4-4287-a4f3-705927e8e4ba"), "", "", "Toys" },
                    { new Guid("3c5f3ec0-e2cb-4c5e-9c23-90b955220a4a"), "", "", "Clothing" },
                    { new Guid("7f8e2edd-2a33-4c42-a2c8-2a6943067d7c"), "", "", "Food" },
                    { new Guid("c5bbc507-3579-4ce7-b34b-9a14c6ceefc6"), "", "", "Furniture" },
                    { new Guid("e5ccca87-622c-42fa-ad65-2c3b38ef9f72"), "", "", "Electronics" }
                });

            migrationBuilder.InsertData(
                table: "SellingPriceTaxes",
                columns: new[] { "Id", "Description", "SellingPriceTaxName", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("335e3440-2ed8-4e14-8920-c47ef61f2a6e"), "", "Inclusive", 0m },
                    { new Guid("35788670-619c-4d12-be04-9f16cce56f64"), "", "Zero Rate", 0m },
                    { new Guid("a1fcf8e3-d173-48e4-a6b0-8b26804ef264"), "", "Exclusive", 0m }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryCode", "Description", "SubCategoryName" },
                values: new object[,]
                {
                    { new Guid("3fdd0a46-aa26-4c61-8883-f5c1f75bc96e"), "", "", "Laptops" },
                    { new Guid("9dd377b9-a98e-41ad-975b-a4bbc1f13bcf"), "", "", "Televisions" },
                    { new Guid("abb050bd-7710-4173-b27b-e61868d73b0c"), "", "", "Smartphones" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "AllowDecimal", "ShortName", "UnitName" },
                values: new object[,]
                {
                    { new Guid("848b4f96-57dc-42f9-baca-0f6e6b8ceab5"), 0, "", "Piece" },
                    { new Guid("af7b134c-50c2-41e3-a534-013efabf0ec6"), 0, "", "Liter" },
                    { new Guid("efd2537b-66eb-4533-847d-2663932d19ff"), 0, "", "Kilogram" }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "Description", "Name", "WarrantyDuration" },
                values: new object[,]
                {
                    { new Guid("15890a3e-04ae-4e52-a57b-10cb7a7ed5ec"), "", "", "3 Years" },
                    { new Guid("d7cfa86a-d9ff-4074-9987-43287d43a93f"), "", "", "2 Years" },
                    { new Guid("db8f8d6f-7261-47f0-84d7-32641818c025"), "", "", "1 Year" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("3a4a4b24-07ac-44fd-bd0c-c5557da9b721"));

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("5795909e-9782-44a7-907d-25ecf281e4f5"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("17a9927d-4a25-4bcb-94a8-f38873c6c169"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("7587a037-dbc9-46cb-8f1a-997c82ab3829"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("d0b176e4-b591-4c2f-b667-24db1bf07bbf"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("24097fb3-9274-49dc-9dd9-fcdbe7dabd8e"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("864c7333-6cce-419c-b9cb-796482075c48"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("bd9a77ae-688a-411f-9b7d-27829b16cfdd"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("24801d9d-2f99-45e6-9638-0d751b0f63ae"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("b6bc0013-d58a-4ff9-8332-2fcdb3c4f685"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("d83da2ca-6e0d-4a66-a60f-48bfe8f56fa7"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("c319526b-a5f6-477e-ac5a-0e087f1afb09"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("c93f5ff4-3cdc-495b-8333-21d5bd5de637"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("edcb0afc-1080-465a-931f-a37c8fa5c41a"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("4d53e565-8400-43c7-b6c3-6cc8738524ad"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("7812ef7b-b3f9-45af-a130-88468e22a329"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("fb79515a-e23e-483d-888a-12d0ad7cf7f7"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("05cfbb2d-d7d4-4287-a4f3-705927e8e4ba"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("3c5f3ec0-e2cb-4c5e-9c23-90b955220a4a"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("7f8e2edd-2a33-4c42-a2c8-2a6943067d7c"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("c5bbc507-3579-4ce7-b34b-9a14c6ceefc6"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("e5ccca87-622c-42fa-ad65-2c3b38ef9f72"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("335e3440-2ed8-4e14-8920-c47ef61f2a6e"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("35788670-619c-4d12-be04-9f16cce56f64"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("a1fcf8e3-d173-48e4-a6b0-8b26804ef264"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("3fdd0a46-aa26-4c61-8883-f5c1f75bc96e"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("9dd377b9-a98e-41ad-975b-a4bbc1f13bcf"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("abb050bd-7710-4173-b27b-e61868d73b0c"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("848b4f96-57dc-42f9-baca-0f6e6b8ceab5"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("af7b134c-50c2-41e3-a534-013efabf0ec6"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("efd2537b-66eb-4533-847d-2663932d19ff"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("15890a3e-04ae-4e52-a57b-10cb7a7ed5ec"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("d7cfa86a-d9ff-4074-9987-43287d43a93f"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("db8f8d6f-7261-47f0-84d7-32641818c025"));

            migrationBuilder.InsertData(
                table: "AdjustmentTypes",
                columns: new[] { "Id", "AdjustmentTypeName", "Description" },
                values: new object[,]
                {
                    { new Guid("4c9ec6b9-4c2d-44b2-a213-ba415d22e596"), "Abnormal", "" },
                    { new Guid("d792d357-2cb0-4be8-9eef-4b0eb5940b21"), "Normal", "" }
                });

            migrationBuilder.InsertData(
                table: "ApplicableTaxs",
                columns: new[] { "Id", "ApplicableTaxName", "Description", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("2ec37c31-cc19-443d-9c0c-60ff9bc92a8f"), "Fruits", "", 0m },
                    { new Guid("b91c8349-9055-40dc-bb01-c0f5cfd403ee"), "Food", "", 0m },
                    { new Guid("c8f6e65e-0c4b-4dca-88ec-c827aab668e7"), "Sales Tax", "", 0m }
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "BarcodeDescription", "BarcodeTypeCode", "BarcodeTypeName" },
                values: new object[,]
                {
                    { new Guid("0ea42e18-29a2-4548-9c34-e9d0ac2bea0f"), "", "", "QR Code" },
                    { new Guid("1cea87b2-e68b-4eea-9c6a-8bb70e4d7310"), "", "", "UPC" },
                    { new Guid("ef2b421e-9828-4695-834a-49969d53e40f"), "", "", "NFC" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BandOrigin", "BrandName", "Description" },
                values: new object[,]
                {
                    { new Guid("07cb5e1e-ad9e-4c83-8879-bc685c77b2fb"), "", "Samsung", "" },
                    { new Guid("560e25c2-b2e1-4a53-9b1c-68d2206101f3"), "", "Sony", "" },
                    { new Guid("f70ddace-ccc9-426e-a6e7-07a1122ab3b5"), "", "Apple", "" }
                });

            migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "Address", "City", "Country", "LocationName", "State", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("a5447030-fcdd-47c6-8d5b-4d586786aefc"), "", "", "", "Downtown Store", "", "" },
                    { new Guid("ca62abf0-7b48-4d8c-ab5f-73e1085d3d2e"), "", "", "", "Warehouse A", "", "" },
                    { new Guid("f8c7878b-6ae2-4672-a44a-7eb4a0d36a80"), "", "", "", "Warehouse B", "", "" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryCode", "CategoryName", "Description" },
                values: new object[,]
                {
                    { new Guid("5de27ee3-f4da-4d08-b44c-5efdf534d970"), "", "Home Appliances", "" },
                    { new Guid("a9eb6dc3-4b2f-4dd5-a531-c35f39ab4236"), "", "Clothing", "" },
                    { new Guid("c00a9ad5-2383-4b6d-a1eb-6bb88535b66a"), "", "Electronics", "" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Description", "ProductTypeCode", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("0b670a9f-f760-4426-bad6-6aabc3c008a2"), "", "", "Toys" },
                    { new Guid("4ee96eb1-efc5-4f95-bd8f-95d68e447814"), "", "", "Food" },
                    { new Guid("6395a2b6-c5a3-46b0-bd22-b64173efec51"), "", "", "Furniture" },
                    { new Guid("cf871616-1404-4845-bf81-cef215256cd8"), "", "", "Electronics" },
                    { new Guid("d2518f3b-d9fb-4aa7-87bb-b2254aee7e03"), "", "", "Clothing" }
                });

            migrationBuilder.InsertData(
                table: "SellingPriceTaxes",
                columns: new[] { "Id", "Description", "SellingPriceTaxName", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("6eb66c7a-a16e-41ac-9620-a8778a74fad2"), "", "Zero Rate", 0m },
                    { new Guid("934f62b0-4b1d-46d7-9d8c-30514dee4f5c"), "", "Inclusive", 0m },
                    { new Guid("f76bf612-7b99-4fcb-ab45-2b0f8c9e1481"), "", "Exclusive", 0m }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryCode", "Description", "SubCategoryName" },
                values: new object[,]
                {
                    { new Guid("02b87e28-e57d-42b3-8ef5-fe692717f7cf"), "", "", "Laptops" },
                    { new Guid("4fa51139-b54b-4712-990e-a17e1c8d65f4"), "", "", "Televisions" },
                    { new Guid("5571fd0c-a7ca-418b-8193-994f1e3fe893"), "", "", "Smartphones" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "AllowDecimal", "ShortName", "UnitName" },
                values: new object[,]
                {
                    { new Guid("225071aa-166b-4770-b33c-2bdb567f754e"), 0, "", "Kilogram" },
                    { new Guid("6f361be4-c316-409d-b612-3adfb6ba3a1a"), 0, "", "Liter" },
                    { new Guid("b51e5576-d1d0-4bb7-a582-472fbc69d1fa"), 0, "", "Piece" }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "Description", "Name", "WarrantyDuration" },
                values: new object[,]
                {
                    { new Guid("3456ab70-fbaa-4d80-8250-4df7d425587d"), "", "", "2 Years" },
                    { new Guid("3e70c216-b710-4fea-9ab6-5c208040fb74"), "", "", "1 Year" },
                    { new Guid("d23184a8-6e3f-435e-88d5-d9cca50b502e"), "", "", "3 Years" }
                });
        }
    }
}
