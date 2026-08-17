using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DevSkill.Inventory.Web.Migrations.InventoryDb
{
    /// <inheritdoc />
    public partial class PurchaesMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("0c5d206f-ccb3-4721-b925-e124def9a6ff"));

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("c5903083-8fb5-4c96-9b7b-41377e2f357b"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("4fd06986-e032-4525-97e4-472ec6bf9e04"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("9c37df19-75c1-4949-9f30-b54b07afe383"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("b3632405-c6db-44e1-859a-75d0b7c336a0"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("075589c7-370e-4759-9095-58457d32b065"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("1ef32d28-f1b1-4197-b959-2022b5b3e80e"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("bce763be-3d4d-4137-8067-66edf5548679"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("16d7046d-74df-4236-a112-b45199aaa295"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("c8115164-5d12-468e-baa2-08896d03005c"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("fdd8bbe4-affc-4af6-84e9-fe93745deb46"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("22556f10-ff3c-4c1b-bcef-b512d91b48c9"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("2fd7bd76-fbfd-44ab-97d1-ec4e43925873"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("b81c26d7-60ba-416b-9e7a-b40dc8cd39bc"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("323b3d39-7be6-4d4b-bc8d-fcb6cc4bc85d"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("59464f9e-22a9-45b8-920d-6c28c00cf9fe"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("e1112928-839f-4d59-bacd-6ca821d31fa0"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("451d9296-1035-4554-aee5-1da4be800eb9"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("4d8549f8-e66d-4c74-bbc3-aa018254120b"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("77d62a4c-9aaa-4ed0-85ab-7128266217e6"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("9b20012e-cd09-4439-b2cc-cdd3b4a547d6"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("e2ee9f5f-44c1-458d-9d78-8ce1f3786630"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("33dd4f09-aca9-4892-ae73-6665c553fde1"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("6f837744-921e-4b35-9fbc-6aac9f359db4"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("9d0c7a03-b105-4dbc-8f06-c33e4d8cbe82"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("01b263fb-4b43-4519-aabc-862adabeada3"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("5f749484-47da-4042-b3e1-d6eae79126a1"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("e3e7f4bd-c7fc-4216-a0e2-0f882975a049"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("2a6da0e6-b9cc-4243-b4ad-f52ff5884048"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("41775b21-f128-4f01-a1b2-44c11b761fbb"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("d5cb7a59-3f6b-4232-8f55-67333459a840"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("25d945fb-9b32-4b7b-bbbf-a36be6c1843e"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("4c145428-01d4-4f47-b6fe-3463f6a2551f"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("f14600ff-55d7-4731-bbda-45bfeeb21c57"));

            migrationBuilder.InsertData(
                table: "AdjustmentTypes",
                columns: new[] { "Id", "AdjustmentTypeName", "Description", "Sign" },
                values: new object[,]
                {
                    { new Guid("43283b35-3f96-4a3e-b35a-f395ea7de383"), "Abnormal", "", 0 },
                    { new Guid("43c8d526-3f8a-43d8-92c4-3ae94c6aade4"), "Normal", "", 0 }
                });

            migrationBuilder.InsertData(
                table: "ApplicableTaxs",
                columns: new[] { "Id", "ApplicableTaxName", "Description", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("55fc0b11-4bd7-4499-8cf0-dfbde9eafb7f"), "Food", "", 0m },
                    { new Guid("958bfc74-5814-4da3-bf68-ac65e2866103"), "Sales Tax", "", 0m },
                    { new Guid("ac3c3079-e076-4092-8e54-016ea8063308"), "Fruits", "", 0m }
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "BarcodeDescription", "BarcodeTypeCode", "BarcodeTypeName" },
                values: new object[,]
                {
                    { new Guid("03d28362-1525-4dfb-b46b-4656bd76dd1f"), "", "", "QR Code" },
                    { new Guid("7da178ba-0a11-4836-a55d-93ee28bfcdb1"), "", "", "NFC" },
                    { new Guid("c43bf678-8caf-46a8-900c-64e58c67b1fb"), "", "", "UPC" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BandOrigin", "BrandName", "Description" },
                values: new object[,]
                {
                    { new Guid("18c2bea8-b25e-433d-a08e-b6f7a00786ee"), "", "Sony", "" },
                    { new Guid("4aa25c30-fb48-47b0-a525-b241a55df9ee"), "", "Samsung", "" },
                    { new Guid("a0851b20-20af-427d-9d5d-a40f451f2ef2"), "", "Apple", "" }
                });

            migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "Address", "City", "Country", "IsActive", "LocationName", "State", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("648fb752-70fa-47e4-9b3f-aed1f6c33027"), "", "", "", true, "Warehouse A", "", "" },
                    { new Guid("845099d1-ee98-4be5-8551-7102645b0a81"), "", "", "", true, "Downtown Store", "", "" },
                    { new Guid("cffe7a51-7178-4d25-938f-20402c5331b4"), "", "", "", true, "Warehouse B", "", "" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryCode", "CategoryName", "Description" },
                values: new object[,]
                {
                    { new Guid("113fc42f-3eb3-46d5-ba13-e9a257983ba9"), "", "Home Appliances", "" },
                    { new Guid("6501b4d4-54e2-4276-ab33-b773e1071649"), "", "Clothing", "" },
                    { new Guid("beac8874-432a-4028-a535-5d43e9287e5f"), "", "Electronics", "" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Description", "ProductTypeCode", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("54f5ee90-adaf-4b0c-a656-57ecde14ef79"), "", "", "Clothing" },
                    { new Guid("af92f651-47ca-4dbd-bd27-c1f2b4f23ea4"), "", "", "Toys" },
                    { new Guid("b180d99d-12ff-42d9-b099-1ec17e8ec58c"), "", "", "Furniture" },
                    { new Guid("d99f73c5-4045-49c5-b3cb-511e714b2039"), "", "", "Food" },
                    { new Guid("ef208b24-e8e2-470e-8b16-08aee482a125"), "", "", "Electronics" }
                });

            migrationBuilder.InsertData(
                table: "SellingPriceTaxes",
                columns: new[] { "Id", "Description", "SellingPriceTaxName", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("2b97a364-d5b7-4a47-8407-f03f83cc7ae7"), "", "Exclusive", 0m },
                    { new Guid("4c9b1237-e114-4908-b4e7-a0a8f62eda91"), "", "Inclusive", 0m },
                    { new Guid("bca33a0a-7290-4284-adfc-4d5b25413e15"), "", "Zero Rate", 0m }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryCode", "Description", "SubCategoryName" },
                values: new object[,]
                {
                    { new Guid("17b15e1c-5063-4edc-8567-8f1e581eb494"), "", "", "Laptops" },
                    { new Guid("73f7b37d-e831-45a5-89e1-766b4e2b9ad1"), "", "", "Televisions" },
                    { new Guid("fba544f7-3086-4287-a6d0-55d39da2ede0"), "", "", "Smartphones" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "AllowDecimal", "ShortName", "UnitName" },
                values: new object[,]
                {
                    { new Guid("7f2c29e4-0db7-4654-a3fc-32a7208dcbce"), 0, "", "Kilogram" },
                    { new Guid("864f70fc-7390-44a3-b48b-66512fe1a126"), 0, "", "Liter" },
                    { new Guid("f1f55cd0-b58f-4d56-8c31-99fa87b37604"), 0, "", "Piece" }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "Description", "Name", "WarrantyDuration" },
                values: new object[,]
                {
                    { new Guid("369db434-c03b-4459-aa6d-990f9a8f2968"), "", "", "3 Years" },
                    { new Guid("40c94f14-5f6d-4d45-ba1f-82e669d85531"), "", "", "2 Years" },
                    { new Guid("9548761d-5d98-4a6e-9d57-9e36979f7008"), "", "", "1 Year" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("43283b35-3f96-4a3e-b35a-f395ea7de383"));

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("43c8d526-3f8a-43d8-92c4-3ae94c6aade4"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("55fc0b11-4bd7-4499-8cf0-dfbde9eafb7f"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("958bfc74-5814-4da3-bf68-ac65e2866103"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("ac3c3079-e076-4092-8e54-016ea8063308"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("03d28362-1525-4dfb-b46b-4656bd76dd1f"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("7da178ba-0a11-4836-a55d-93ee28bfcdb1"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("c43bf678-8caf-46a8-900c-64e58c67b1fb"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("18c2bea8-b25e-433d-a08e-b6f7a00786ee"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("4aa25c30-fb48-47b0-a525-b241a55df9ee"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("a0851b20-20af-427d-9d5d-a40f451f2ef2"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("648fb752-70fa-47e4-9b3f-aed1f6c33027"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("845099d1-ee98-4be5-8551-7102645b0a81"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("cffe7a51-7178-4d25-938f-20402c5331b4"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("113fc42f-3eb3-46d5-ba13-e9a257983ba9"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6501b4d4-54e2-4276-ab33-b773e1071649"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("beac8874-432a-4028-a535-5d43e9287e5f"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("54f5ee90-adaf-4b0c-a656-57ecde14ef79"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("af92f651-47ca-4dbd-bd27-c1f2b4f23ea4"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("b180d99d-12ff-42d9-b099-1ec17e8ec58c"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("d99f73c5-4045-49c5-b3cb-511e714b2039"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("ef208b24-e8e2-470e-8b16-08aee482a125"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("2b97a364-d5b7-4a47-8407-f03f83cc7ae7"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("4c9b1237-e114-4908-b4e7-a0a8f62eda91"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("bca33a0a-7290-4284-adfc-4d5b25413e15"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("17b15e1c-5063-4edc-8567-8f1e581eb494"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("73f7b37d-e831-45a5-89e1-766b4e2b9ad1"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("fba544f7-3086-4287-a6d0-55d39da2ede0"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("7f2c29e4-0db7-4654-a3fc-32a7208dcbce"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("864f70fc-7390-44a3-b48b-66512fe1a126"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("f1f55cd0-b58f-4d56-8c31-99fa87b37604"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("369db434-c03b-4459-aa6d-990f9a8f2968"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("40c94f14-5f6d-4d45-ba1f-82e669d85531"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("9548761d-5d98-4a6e-9d57-9e36979f7008"));

            migrationBuilder.InsertData(
                table: "AdjustmentTypes",
                columns: new[] { "Id", "AdjustmentTypeName", "Description", "Sign" },
                values: new object[,]
                {
                    { new Guid("0c5d206f-ccb3-4721-b925-e124def9a6ff"), "Normal", "", 0 },
                    { new Guid("c5903083-8fb5-4c96-9b7b-41377e2f357b"), "Abnormal", "", 0 }
                });

            migrationBuilder.InsertData(
                table: "ApplicableTaxs",
                columns: new[] { "Id", "ApplicableTaxName", "Description", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("4fd06986-e032-4525-97e4-472ec6bf9e04"), "Sales Tax", "", 0m },
                    { new Guid("9c37df19-75c1-4949-9f30-b54b07afe383"), "Fruits", "", 0m },
                    { new Guid("b3632405-c6db-44e1-859a-75d0b7c336a0"), "Food", "", 0m }
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "BarcodeDescription", "BarcodeTypeCode", "BarcodeTypeName" },
                values: new object[,]
                {
                    { new Guid("075589c7-370e-4759-9095-58457d32b065"), "", "", "NFC" },
                    { new Guid("1ef32d28-f1b1-4197-b959-2022b5b3e80e"), "", "", "UPC" },
                    { new Guid("bce763be-3d4d-4137-8067-66edf5548679"), "", "", "QR Code" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BandOrigin", "BrandName", "Description" },
                values: new object[,]
                {
                    { new Guid("16d7046d-74df-4236-a112-b45199aaa295"), "", "Sony", "" },
                    { new Guid("c8115164-5d12-468e-baa2-08896d03005c"), "", "Apple", "" },
                    { new Guid("fdd8bbe4-affc-4af6-84e9-fe93745deb46"), "", "Samsung", "" }
                });

            migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "Address", "City", "Country", "IsActive", "LocationName", "State", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("22556f10-ff3c-4c1b-bcef-b512d91b48c9"), "", "", "", true, "Downtown Store", "", "" },
                    { new Guid("2fd7bd76-fbfd-44ab-97d1-ec4e43925873"), "", "", "", true, "Warehouse B", "", "" },
                    { new Guid("b81c26d7-60ba-416b-9e7a-b40dc8cd39bc"), "", "", "", true, "Warehouse A", "", "" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryCode", "CategoryName", "Description" },
                values: new object[,]
                {
                    { new Guid("323b3d39-7be6-4d4b-bc8d-fcb6cc4bc85d"), "", "Electronics", "" },
                    { new Guid("59464f9e-22a9-45b8-920d-6c28c00cf9fe"), "", "Clothing", "" },
                    { new Guid("e1112928-839f-4d59-bacd-6ca821d31fa0"), "", "Home Appliances", "" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Description", "ProductTypeCode", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("451d9296-1035-4554-aee5-1da4be800eb9"), "", "", "Furniture" },
                    { new Guid("4d8549f8-e66d-4c74-bbc3-aa018254120b"), "", "", "Clothing" },
                    { new Guid("77d62a4c-9aaa-4ed0-85ab-7128266217e6"), "", "", "Toys" },
                    { new Guid("9b20012e-cd09-4439-b2cc-cdd3b4a547d6"), "", "", "Electronics" },
                    { new Guid("e2ee9f5f-44c1-458d-9d78-8ce1f3786630"), "", "", "Food" }
                });

            migrationBuilder.InsertData(
                table: "SellingPriceTaxes",
                columns: new[] { "Id", "Description", "SellingPriceTaxName", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("33dd4f09-aca9-4892-ae73-6665c553fde1"), "", "Inclusive", 0m },
                    { new Guid("6f837744-921e-4b35-9fbc-6aac9f359db4"), "", "Zero Rate", 0m },
                    { new Guid("9d0c7a03-b105-4dbc-8f06-c33e4d8cbe82"), "", "Exclusive", 0m }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryCode", "Description", "SubCategoryName" },
                values: new object[,]
                {
                    { new Guid("01b263fb-4b43-4519-aabc-862adabeada3"), "", "", "Laptops" },
                    { new Guid("5f749484-47da-4042-b3e1-d6eae79126a1"), "", "", "Smartphones" },
                    { new Guid("e3e7f4bd-c7fc-4216-a0e2-0f882975a049"), "", "", "Televisions" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "AllowDecimal", "ShortName", "UnitName" },
                values: new object[,]
                {
                    { new Guid("2a6da0e6-b9cc-4243-b4ad-f52ff5884048"), 0, "", "Kilogram" },
                    { new Guid("41775b21-f128-4f01-a1b2-44c11b761fbb"), 0, "", "Piece" },
                    { new Guid("d5cb7a59-3f6b-4232-8f55-67333459a840"), 0, "", "Liter" }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "Description", "Name", "WarrantyDuration" },
                values: new object[,]
                {
                    { new Guid("25d945fb-9b32-4b7b-bbbf-a36be6c1843e"), "", "", "1 Year" },
                    { new Guid("4c145428-01d4-4f47-b6fe-3463f6a2551f"), "", "", "3 Years" },
                    { new Guid("f14600ff-55d7-4731-bbda-45bfeeb21c57"), "", "", "2 Years" }
                });
        }
    }
}
