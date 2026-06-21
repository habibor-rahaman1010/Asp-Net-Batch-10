using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DevSkill.Inventory.Web.Migrations.InventoryDb
{
    /// <inheritdoc />
    public partial class stockTransfer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("0e95c8a8-56d8-4b6d-aa0e-0399b02dfd68"));

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("94552087-4787-4ee1-961b-0ec173672f64"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("14ebbc47-26b5-4eb4-b0e2-01fb1b420c5d"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("2e8ea20b-ea15-49ad-a4c6-b8952dd8cc29"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("424e16a3-a54b-4cc3-8de3-757c3d431cfc"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("3e410890-e992-4477-b437-e085ce0a72dd"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("450c3e3e-9aa6-425f-a6d6-68c5322990f4"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("7fb72fad-0be3-426f-a37f-1af1b777d4de"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("874b8737-f318-46f4-ae22-e011cc4bcf81"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("9b594b20-add3-4a79-ab53-5a02acf2bf44"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("b1b7ecc8-bcfe-4973-9934-76ddfb0548f2"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("2b34b2f4-ae14-4dd8-aa13-0660789c0677"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("39c70e6c-98ce-4376-a771-9809f16d5c73"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("496c7748-81be-4c28-8dea-6b4ddfed28ae"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("37f5ce09-d1fc-4ea0-9687-60e2fdb037ae"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("96e3a93f-e8dd-4f09-8cec-bc9159b2936d"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c334fd05-99f3-4195-a8ef-96d3d57ef2f6"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("800b233c-dbb5-4476-a083-f6a75e0e2a8f"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("a220ff47-13c9-42be-a446-b48c94687df9"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("a602aa60-5bc3-4c29-b765-f641d93bc3e6"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("fb7b1581-2cdd-44d7-bd41-890fb6aaedfd"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("fe7c0a48-917b-4dc7-ad27-229205ac5a9e"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("29da6c47-2468-418c-b855-f6805a20a444"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("95e9c725-47c5-4f00-be99-22c5a1f374b9"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("e5c97a11-be49-4689-a149-8c27c51f6452"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("1bf86a14-8455-4e60-9565-f5cc672f344a"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("4dcce627-cbdb-4cc7-bf81-d92eaa767677"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("efd97eae-90ac-425b-8277-d3b9b929a65a"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("5fb7be51-af4a-4fc8-8e5c-54dbd6219dc3"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("b846a1d1-1466-43a8-83d8-866f4476ba52"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("f75b9b6f-5e3d-4273-b325-9c68a9c6e534"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("5ca6e1cd-98c6-4a20-8cdd-8e2bc503c94d"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("9f435e79-95d1-471b-bb09-4efaa8825655"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("b6463d97-3216-43bb-aec5-41bf119a894a"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AdjustmentTypes",
                columns: new[] { "Id", "AdjustmentTypeName", "Description", "Sign" },
                values: new object[,]
                {
                    { new Guid("0e95c8a8-56d8-4b6d-aa0e-0399b02dfd68"), "Abnormal", "", 0 },
                    { new Guid("94552087-4787-4ee1-961b-0ec173672f64"), "Normal", "", 0 }
                });

            migrationBuilder.InsertData(
                table: "ApplicableTaxs",
                columns: new[] { "Id", "ApplicableTaxName", "Description", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("14ebbc47-26b5-4eb4-b0e2-01fb1b420c5d"), "Fruits", "", 0m },
                    { new Guid("2e8ea20b-ea15-49ad-a4c6-b8952dd8cc29"), "Sales Tax", "", 0m },
                    { new Guid("424e16a3-a54b-4cc3-8de3-757c3d431cfc"), "Food", "", 0m }
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "BarcodeDescription", "BarcodeTypeCode", "BarcodeTypeName" },
                values: new object[,]
                {
                    { new Guid("3e410890-e992-4477-b437-e085ce0a72dd"), "", "", "NFC" },
                    { new Guid("450c3e3e-9aa6-425f-a6d6-68c5322990f4"), "", "", "QR Code" },
                    { new Guid("7fb72fad-0be3-426f-a37f-1af1b777d4de"), "", "", "UPC" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BandOrigin", "BrandName", "Description" },
                values: new object[,]
                {
                    { new Guid("874b8737-f318-46f4-ae22-e011cc4bcf81"), "", "Sony", "" },
                    { new Guid("9b594b20-add3-4a79-ab53-5a02acf2bf44"), "", "Apple", "" },
                    { new Guid("b1b7ecc8-bcfe-4973-9934-76ddfb0548f2"), "", "Samsung", "" }
                });

            migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "Address", "City", "Country", "LocationName", "State", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("2b34b2f4-ae14-4dd8-aa13-0660789c0677"), "", "", "", "Warehouse B", "", "" },
                    { new Guid("39c70e6c-98ce-4376-a771-9809f16d5c73"), "", "", "", "Downtown Store", "", "" },
                    { new Guid("496c7748-81be-4c28-8dea-6b4ddfed28ae"), "", "", "", "Warehouse A", "", "" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryCode", "CategoryName", "Description" },
                values: new object[,]
                {
                    { new Guid("37f5ce09-d1fc-4ea0-9687-60e2fdb037ae"), "", "Home Appliances", "" },
                    { new Guid("96e3a93f-e8dd-4f09-8cec-bc9159b2936d"), "", "Electronics", "" },
                    { new Guid("c334fd05-99f3-4195-a8ef-96d3d57ef2f6"), "", "Clothing", "" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Description", "ProductTypeCode", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("800b233c-dbb5-4476-a083-f6a75e0e2a8f"), "", "", "Toys" },
                    { new Guid("a220ff47-13c9-42be-a446-b48c94687df9"), "", "", "Electronics" },
                    { new Guid("a602aa60-5bc3-4c29-b765-f641d93bc3e6"), "", "", "Clothing" },
                    { new Guid("fb7b1581-2cdd-44d7-bd41-890fb6aaedfd"), "", "", "Food" },
                    { new Guid("fe7c0a48-917b-4dc7-ad27-229205ac5a9e"), "", "", "Furniture" }
                });

            migrationBuilder.InsertData(
                table: "SellingPriceTaxes",
                columns: new[] { "Id", "Description", "SellingPriceTaxName", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("29da6c47-2468-418c-b855-f6805a20a444"), "", "Inclusive", 0m },
                    { new Guid("95e9c725-47c5-4f00-be99-22c5a1f374b9"), "", "Exclusive", 0m },
                    { new Guid("e5c97a11-be49-4689-a149-8c27c51f6452"), "", "Zero Rate", 0m }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryCode", "Description", "SubCategoryName" },
                values: new object[,]
                {
                    { new Guid("1bf86a14-8455-4e60-9565-f5cc672f344a"), "", "", "Smartphones" },
                    { new Guid("4dcce627-cbdb-4cc7-bf81-d92eaa767677"), "", "", "Laptops" },
                    { new Guid("efd97eae-90ac-425b-8277-d3b9b929a65a"), "", "", "Televisions" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "AllowDecimal", "ShortName", "UnitName" },
                values: new object[,]
                {
                    { new Guid("5fb7be51-af4a-4fc8-8e5c-54dbd6219dc3"), 0, "", "Liter" },
                    { new Guid("b846a1d1-1466-43a8-83d8-866f4476ba52"), 0, "", "Piece" },
                    { new Guid("f75b9b6f-5e3d-4273-b325-9c68a9c6e534"), 0, "", "Kilogram" }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "Description", "Name", "WarrantyDuration" },
                values: new object[,]
                {
                    { new Guid("5ca6e1cd-98c6-4a20-8cdd-8e2bc503c94d"), "", "", "2 Years" },
                    { new Guid("9f435e79-95d1-471b-bb09-4efaa8825655"), "", "", "3 Years" },
                    { new Guid("b6463d97-3216-43bb-aec5-41bf119a894a"), "", "", "1 Year" }
                });
        }
    }
}
