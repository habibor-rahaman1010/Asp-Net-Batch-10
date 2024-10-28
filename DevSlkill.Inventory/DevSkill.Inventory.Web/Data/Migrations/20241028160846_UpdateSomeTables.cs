using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DevSkill.Inventory.Web.Migrations.InventoryDb
{
    /// <inheritdoc />
    public partial class UpdateSomeTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("067be132-5681-48c4-a8a9-aa70f41b57c2"));

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("e1d43912-78d9-4119-a849-2228db9f3ad9"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("1a7f92cf-74aa-47f7-84b0-444394ffefe0"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("763e0239-34b1-46c3-999c-93827001395d"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("7cd4f52b-a6bd-4057-9338-bed8d5f55b12"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("5ad0ef6a-690c-4d03-8ed8-8d457df61310"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("a88c9a30-9768-4f79-a19a-99f5d78a5f65"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("ecd53c88-0335-4f8f-9e53-c918e9c397f1"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("4359d807-4e38-4549-8701-9c2cd93ebdff"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("5e7e92dc-672e-4587-8fcc-dc76965693e9"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("89d70d76-17ae-4b5f-a48c-c4587ee532da"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("0ffd3212-96e8-4de7-8323-54762ee1e33d"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("9a197091-704e-470c-9a5e-88e4462028d9"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("d76767f8-50b4-40e1-92fe-a97c5078fde8"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0fe165b9-a3ac-4a33-aaa7-7f3db18f6d62"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("3a57f127-1ee3-425e-b544-f9dc6a4e217b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("534dd9cb-0fdc-4219-b383-73387f17e3f4"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("8e5118df-0d91-4564-9a79-e6aa1f9fa133"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("c20edcdc-6272-4b2f-aa72-f44d6020a53f"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("d6432c9f-79d1-43df-9134-49a9e37ea70e"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("ead7daea-36fd-4b0d-a8fe-1b4c7ed3b690"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("fa1f9255-8fed-4b25-8a1c-9609711c633a"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("1a9a2295-0959-43a8-9673-cf2e74883a35"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("9c13d4d1-ae57-4727-8cdf-8ab429277e7c"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("eaf70cf2-9675-4ac6-8520-9f683f586ae7"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("1c498c3d-0096-42bc-a2b3-5bc16e4236e2"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("21dcf7db-831d-4951-9388-aec6e1d7e4ee"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("3f11b4c1-342f-42bb-a026-5f49bbc4e1bf"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("9a8ea287-ba44-498c-8b3b-0009c8468bba"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("cf35fabe-2b1d-4c4e-a9cc-4feeaeaf2748"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("df3c7417-b235-4fb2-a56e-83b0e7bcb872"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("28c859ca-c0dd-4d67-b329-e6623085f043"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("b882e46a-a5a4-4338-906e-e933555ad7f4"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("d3d2ba45-10ed-4407-aa49-12872b85bb2d"));

            migrationBuilder.RenameColumn(
                name: "SubcategoryName",
                table: "Subcategories",
                newName: "SubCategoryName");

            migrationBuilder.AddColumn<string>(
                name: "CategoryCode",
                table: "Subcategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Subcategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "SellingPriceTaxes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "TaxRate",
                table: "SellingPriceTaxes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ProductTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductTypeCode",
                table: "ProductTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ApplicableTaxs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "TaxRate",
                table: "ApplicableTaxs",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

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
                table: "Subcategories",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("02b87e28-e57d-42b3-8ef5-fe692717f7cf"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("4fa51139-b54b-4712-990e-a17e1c8d65f4"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
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

            migrationBuilder.DropColumn(
                name: "CategoryCode",
                table: "Subcategories");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Subcategories");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "SellingPriceTaxes");

            migrationBuilder.DropColumn(
                name: "TaxRate",
                table: "SellingPriceTaxes");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ProductTypes");

            migrationBuilder.DropColumn(
                name: "ProductTypeCode",
                table: "ProductTypes");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ApplicableTaxs");

            migrationBuilder.DropColumn(
                name: "TaxRate",
                table: "ApplicableTaxs");

            migrationBuilder.RenameColumn(
                name: "SubCategoryName",
                table: "Subcategories",
                newName: "SubcategoryName");

            migrationBuilder.InsertData(
                table: "AdjustmentTypes",
                columns: new[] { "Id", "AdjustmentTypeName", "Description" },
                values: new object[,]
                {
                    { new Guid("067be132-5681-48c4-a8a9-aa70f41b57c2"), "Abnormal", "" },
                    { new Guid("e1d43912-78d9-4119-a849-2228db9f3ad9"), "Normal", "" }
                });

            migrationBuilder.InsertData(
                table: "ApplicableTaxs",
                columns: new[] { "Id", "ApplicableTaxName" },
                values: new object[,]
                {
                    { new Guid("1a7f92cf-74aa-47f7-84b0-444394ffefe0"), "Food" },
                    { new Guid("763e0239-34b1-46c3-999c-93827001395d"), "Sales Tax" },
                    { new Guid("7cd4f52b-a6bd-4057-9338-bed8d5f55b12"), "Fruits" }
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "BarcodeDescription", "BarcodeTypeCode", "BarcodeTypeName" },
                values: new object[,]
                {
                    { new Guid("5ad0ef6a-690c-4d03-8ed8-8d457df61310"), "", "", "NFC" },
                    { new Guid("a88c9a30-9768-4f79-a19a-99f5d78a5f65"), "", "", "QR Code" },
                    { new Guid("ecd53c88-0335-4f8f-9e53-c918e9c397f1"), "", "", "UPC" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BandOrigin", "BrandName", "Description" },
                values: new object[,]
                {
                    { new Guid("4359d807-4e38-4549-8701-9c2cd93ebdff"), "", "Samsung", "" },
                    { new Guid("5e7e92dc-672e-4587-8fcc-dc76965693e9"), "", "Apple", "" },
                    { new Guid("89d70d76-17ae-4b5f-a48c-c4587ee532da"), "", "Sony", "" }
                });

            migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "Address", "City", "Country", "LocationName", "State", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("0ffd3212-96e8-4de7-8323-54762ee1e33d"), "", "", "", "Warehouse A", "", "" },
                    { new Guid("9a197091-704e-470c-9a5e-88e4462028d9"), "", "", "", "Downtown Store", "", "" },
                    { new Guid("d76767f8-50b4-40e1-92fe-a97c5078fde8"), "", "", "", "Warehouse B", "", "" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryCode", "CategoryName", "Description" },
                values: new object[,]
                {
                    { new Guid("0fe165b9-a3ac-4a33-aaa7-7f3db18f6d62"), "", "Home Appliances", "" },
                    { new Guid("3a57f127-1ee3-425e-b544-f9dc6a4e217b"), "", "Electronics", "" },
                    { new Guid("534dd9cb-0fdc-4219-b383-73387f17e3f4"), "", "Clothing", "" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("8e5118df-0d91-4564-9a79-e6aa1f9fa133"), "Clothing" },
                    { new Guid("c20edcdc-6272-4b2f-aa72-f44d6020a53f"), "Furniture" },
                    { new Guid("d6432c9f-79d1-43df-9134-49a9e37ea70e"), "Electronics" },
                    { new Guid("ead7daea-36fd-4b0d-a8fe-1b4c7ed3b690"), "Toys" },
                    { new Guid("fa1f9255-8fed-4b25-8a1c-9609711c633a"), "Food" }
                });

            migrationBuilder.InsertData(
                table: "SellingPriceTaxes",
                columns: new[] { "Id", "SellingPriceTaxName" },
                values: new object[,]
                {
                    { new Guid("1a9a2295-0959-43a8-9673-cf2e74883a35"), "Inclusive" },
                    { new Guid("9c13d4d1-ae57-4727-8cdf-8ab429277e7c"), "Zero Rate" },
                    { new Guid("eaf70cf2-9675-4ac6-8520-9f683f586ae7"), "Exclusive" }
                });

            migrationBuilder.InsertData(
                table: "Subcategories",
                columns: new[] { "Id", "SubcategoryName" },
                values: new object[,]
                {
                    { new Guid("1c498c3d-0096-42bc-a2b3-5bc16e4236e2"), "Televisions" },
                    { new Guid("21dcf7db-831d-4951-9388-aec6e1d7e4ee"), "Laptops" },
                    { new Guid("3f11b4c1-342f-42bb-a026-5f49bbc4e1bf"), "Smartphones" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "AllowDecimal", "ShortName", "UnitName" },
                values: new object[,]
                {
                    { new Guid("9a8ea287-ba44-498c-8b3b-0009c8468bba"), 0, "", "Kilogram" },
                    { new Guid("cf35fabe-2b1d-4c4e-a9cc-4feeaeaf2748"), 0, "", "Liter" },
                    { new Guid("df3c7417-b235-4fb2-a56e-83b0e7bcb872"), 0, "", "Piece" }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "Description", "Name", "WarrantyDuration" },
                values: new object[,]
                {
                    { new Guid("28c859ca-c0dd-4d67-b329-e6623085f043"), "", "", "2 Years" },
                    { new Guid("b882e46a-a5a4-4338-906e-e933555ad7f4"), "", "", "1 Year" },
                    { new Guid("d3d2ba45-10ed-4407-aa49-12872b85bb2d"), "", "", "3 Years" }
                });
        }
    }
}
