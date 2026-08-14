using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DevSkill.Inventory.Web.Migrations.InventoryDb
{
    /// <inheritdoc />
    public partial class PiCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("455403e9-9287-4b0c-a381-8ff373bdf186"));

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("ea2de7ec-adf0-40dd-8bf7-07d38a03e9b8"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("55fb5641-346f-442e-9547-ef9c4e353a0f"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("9253878e-2d36-40cc-807d-1cc3cba40fd9"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("f3ba2861-370d-41ac-841d-ad8d875b514a"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("257e96b4-3d5a-4be9-b973-f567b5330b21"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("265098c9-9a38-493f-b945-b986c890282d"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("b665642e-5871-4df0-aedd-9c46a3c961aa"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("09ce78ba-9bad-440e-a6a4-dd43ad48bc74"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("40a97774-e79e-4781-9a0c-d14bd0fa9143"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("89d4bc49-84ee-447d-a67f-7b85689ab4c5"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("4f56ed51-8dc9-4aa6-a352-b4530723f8d7"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("861a8e19-3a1e-482b-b74b-6a3c553d0b25"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("b58c331d-eeb1-4d7f-b70b-25a3ba5215fc"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("25e5a404-db0b-4b0f-8e84-afd7f0539fb5"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("4e3a5e18-241b-41cc-b203-3fc68bd75fd2"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("bf8b3515-fa40-4db5-a549-b6faf808b99f"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("12d1f0fe-fe0a-4da9-adac-34cbcbf4e9fb"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("48e2cab3-6042-42ec-978a-a63feede2b20"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("93482801-3942-463e-b26b-f54456daed6e"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("9dc0598f-f391-4df4-abb7-8180fa6b8a0a"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("c59e0f9d-2ec2-4606-8a6e-b7b8a8c3d841"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("5bacd899-f6d8-4b09-bff3-9b2f3d48294d"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("647f72b5-d86d-4643-a64b-b41a83a8046b"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("ee07d64f-e3ba-4bc5-9cc3-2abfc61cc547"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("6c47a635-894a-4e1f-8efe-858d150c83fb"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("7580bb9a-5e02-4810-a4b6-f786e776eecc"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("cde5e0f2-5488-4171-a457-85673d2ee7c5"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("145b4a21-b679-43de-bc34-d9fb78f35ca3"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("1ee2dec9-c63c-488c-aa29-a8e3ae4c805d"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("a2415b5a-a80f-4627-9165-fdd9d6498e1f"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("7840d053-4247-446a-8f3e-9b7fc3355ff1"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("9b797812-ce6a-4972-8e46-c9b95f69180e"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("ab5b7dcb-c308-4c6b-9225-fd36664d38a9"));

            migrationBuilder.InsertData(
                table: "AdjustmentTypes",
                columns: new[] { "Id", "AdjustmentTypeName", "Description", "Sign" },
                values: new object[,]
                {
                    { new Guid("d7b2862d-c851-47bb-9afc-f249e870adf8"), "Normal", "", 0 },
                    { new Guid("eee79b4f-79e7-4b39-8121-d770f42e1880"), "Abnormal", "", 0 }
                });

            migrationBuilder.InsertData(
                table: "ApplicableTaxs",
                columns: new[] { "Id", "ApplicableTaxName", "Description", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("33ec3bc9-08d6-4bd1-acf5-b9ab08b39eb4"), "Sales Tax", "", 0m },
                    { new Guid("6f11a709-f816-4ef2-b888-30826ca9ad22"), "Fruits", "", 0m },
                    { new Guid("c2ec23ee-b2c9-4e10-a1ca-bd175354c4d3"), "Food", "", 0m }
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "BarcodeDescription", "BarcodeTypeCode", "BarcodeTypeName" },
                values: new object[,]
                {
                    { new Guid("6f34d799-d3b4-44cb-9188-0fa0bc27fccb"), "", "", "NFC" },
                    { new Guid("d1f2f092-e0eb-4012-948c-0aeedf1561cc"), "", "", "UPC" },
                    { new Guid("f4ea485f-7171-4055-8a46-96dd99c89ed0"), "", "", "QR Code" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BandOrigin", "BrandName", "Description" },
                values: new object[,]
                {
                    { new Guid("325bc763-603d-4c18-bb25-0f350d81f9a3"), "", "Sony", "" },
                    { new Guid("9fedd86c-6215-4687-9374-f56dceee0fba"), "", "Samsung", "" },
                    { new Guid("f78356bd-262f-41ec-8c4f-1188713dbbc8"), "", "Apple", "" }
                });

            migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "Address", "City", "Country", "IsActive", "LocationName", "State", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("0758d4a9-39d1-4262-901a-dcfeaec4b560"), "", "", "", true, "Warehouse B", "", "" },
                    { new Guid("7d5de306-c960-4339-9349-7eaf42256d35"), "", "", "", true, "Warehouse A", "", "" },
                    { new Guid("a99d94e8-64fe-43e4-9ea3-abf6d4fe8f55"), "", "", "", true, "Downtown Store", "", "" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryCode", "CategoryName", "Description" },
                values: new object[,]
                {
                    { new Guid("7f2a8627-2635-494d-8306-b99625498cf3"), "", "Electronics", "" },
                    { new Guid("880e5356-a654-4631-bb1d-69625bd04436"), "", "Clothing", "" },
                    { new Guid("ba307947-ce5b-4ed1-a98e-2823c322d904"), "", "Home Appliances", "" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Description", "ProductTypeCode", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("138f24cd-f27a-4c20-b1f2-6748851a6ab6"), "", "", "Clothing" },
                    { new Guid("5186843b-e2a6-4e5f-bdd2-32e48bbf3614"), "", "", "Food" },
                    { new Guid("a5ca5fd9-4315-4c01-8261-d12f5dd2111f"), "", "", "Electronics" },
                    { new Guid("ce8c1c59-53e7-4300-ba25-d98b7156f835"), "", "", "Toys" },
                    { new Guid("f7a814a6-50e9-469d-8233-64c2310c017d"), "", "", "Furniture" }
                });

            migrationBuilder.InsertData(
                table: "SellingPriceTaxes",
                columns: new[] { "Id", "Description", "SellingPriceTaxName", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("7a13b17f-96ef-4d4c-ac30-f0bcb1026e99"), "", "Exclusive", 0m },
                    { new Guid("8a3d639c-beb4-4578-8fe2-6dbc79e41b17"), "", "Inclusive", 0m },
                    { new Guid("e3dd2b87-619c-48ae-ad9a-fb665ce85e92"), "", "Zero Rate", 0m }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryCode", "Description", "SubCategoryName" },
                values: new object[,]
                {
                    { new Guid("7af20fdb-6aae-485a-ae4e-af83b1e8b2c2"), "", "", "Smartphones" },
                    { new Guid("7c247aee-9833-4a76-b944-1bce466f0543"), "", "", "Televisions" },
                    { new Guid("c1d25621-95d8-444b-ab62-c641e856f558"), "", "", "Laptops" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "AllowDecimal", "ShortName", "UnitName" },
                values: new object[,]
                {
                    { new Guid("518addf8-cb6a-4c84-a238-507bc6468d62"), 0, "", "Kilogram" },
                    { new Guid("c5b4071e-44cb-4b04-a6c2-35569831b858"), 0, "", "Piece" },
                    { new Guid("dc6ee6c3-2992-48da-b6c8-695f41e61f69"), 0, "", "Liter" }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "Description", "Name", "WarrantyDuration" },
                values: new object[,]
                {
                    { new Guid("13d99d3a-9001-4648-98c6-6286ba2079fa"), "", "", "2 Years" },
                    { new Guid("6881d763-29ae-4b7d-8e2c-92c01644caa2"), "", "", "1 Year" },
                    { new Guid("ea3cfb88-8934-4e93-a891-bb38ffafeaee"), "", "", "3 Years" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("d7b2862d-c851-47bb-9afc-f249e870adf8"));

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("eee79b4f-79e7-4b39-8121-d770f42e1880"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("33ec3bc9-08d6-4bd1-acf5-b9ab08b39eb4"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("6f11a709-f816-4ef2-b888-30826ca9ad22"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("c2ec23ee-b2c9-4e10-a1ca-bd175354c4d3"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("6f34d799-d3b4-44cb-9188-0fa0bc27fccb"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("d1f2f092-e0eb-4012-948c-0aeedf1561cc"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("f4ea485f-7171-4055-8a46-96dd99c89ed0"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("325bc763-603d-4c18-bb25-0f350d81f9a3"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("9fedd86c-6215-4687-9374-f56dceee0fba"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("f78356bd-262f-41ec-8c4f-1188713dbbc8"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("0758d4a9-39d1-4262-901a-dcfeaec4b560"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("7d5de306-c960-4339-9349-7eaf42256d35"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("a99d94e8-64fe-43e4-9ea3-abf6d4fe8f55"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("7f2a8627-2635-494d-8306-b99625498cf3"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("880e5356-a654-4631-bb1d-69625bd04436"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("ba307947-ce5b-4ed1-a98e-2823c322d904"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("138f24cd-f27a-4c20-b1f2-6748851a6ab6"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("5186843b-e2a6-4e5f-bdd2-32e48bbf3614"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("a5ca5fd9-4315-4c01-8261-d12f5dd2111f"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("ce8c1c59-53e7-4300-ba25-d98b7156f835"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("f7a814a6-50e9-469d-8233-64c2310c017d"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("7a13b17f-96ef-4d4c-ac30-f0bcb1026e99"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("8a3d639c-beb4-4578-8fe2-6dbc79e41b17"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("e3dd2b87-619c-48ae-ad9a-fb665ce85e92"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("7af20fdb-6aae-485a-ae4e-af83b1e8b2c2"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("7c247aee-9833-4a76-b944-1bce466f0543"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("c1d25621-95d8-444b-ab62-c641e856f558"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("518addf8-cb6a-4c84-a238-507bc6468d62"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("c5b4071e-44cb-4b04-a6c2-35569831b858"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("dc6ee6c3-2992-48da-b6c8-695f41e61f69"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("13d99d3a-9001-4648-98c6-6286ba2079fa"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("6881d763-29ae-4b7d-8e2c-92c01644caa2"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("ea3cfb88-8934-4e93-a891-bb38ffafeaee"));

            migrationBuilder.InsertData(
                table: "AdjustmentTypes",
                columns: new[] { "Id", "AdjustmentTypeName", "Description", "Sign" },
                values: new object[,]
                {
                    { new Guid("455403e9-9287-4b0c-a381-8ff373bdf186"), "Normal", "", 0 },
                    { new Guid("ea2de7ec-adf0-40dd-8bf7-07d38a03e9b8"), "Abnormal", "", 0 }
                });

            migrationBuilder.InsertData(
                table: "ApplicableTaxs",
                columns: new[] { "Id", "ApplicableTaxName", "Description", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("55fb5641-346f-442e-9547-ef9c4e353a0f"), "Sales Tax", "", 0m },
                    { new Guid("9253878e-2d36-40cc-807d-1cc3cba40fd9"), "Food", "", 0m },
                    { new Guid("f3ba2861-370d-41ac-841d-ad8d875b514a"), "Fruits", "", 0m }
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "BarcodeDescription", "BarcodeTypeCode", "BarcodeTypeName" },
                values: new object[,]
                {
                    { new Guid("257e96b4-3d5a-4be9-b973-f567b5330b21"), "", "", "QR Code" },
                    { new Guid("265098c9-9a38-493f-b945-b986c890282d"), "", "", "UPC" },
                    { new Guid("b665642e-5871-4df0-aedd-9c46a3c961aa"), "", "", "NFC" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BandOrigin", "BrandName", "Description" },
                values: new object[,]
                {
                    { new Guid("09ce78ba-9bad-440e-a6a4-dd43ad48bc74"), "", "Samsung", "" },
                    { new Guid("40a97774-e79e-4781-9a0c-d14bd0fa9143"), "", "Sony", "" },
                    { new Guid("89d4bc49-84ee-447d-a67f-7b85689ab4c5"), "", "Apple", "" }
                });

            migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "Address", "City", "Country", "IsActive", "LocationName", "State", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("4f56ed51-8dc9-4aa6-a352-b4530723f8d7"), "", "", "", true, "Warehouse A", "", "" },
                    { new Guid("861a8e19-3a1e-482b-b74b-6a3c553d0b25"), "", "", "", true, "Downtown Store", "", "" },
                    { new Guid("b58c331d-eeb1-4d7f-b70b-25a3ba5215fc"), "", "", "", true, "Warehouse B", "", "" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryCode", "CategoryName", "Description" },
                values: new object[,]
                {
                    { new Guid("25e5a404-db0b-4b0f-8e84-afd7f0539fb5"), "", "Home Appliances", "" },
                    { new Guid("4e3a5e18-241b-41cc-b203-3fc68bd75fd2"), "", "Clothing", "" },
                    { new Guid("bf8b3515-fa40-4db5-a549-b6faf808b99f"), "", "Electronics", "" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Description", "ProductTypeCode", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("12d1f0fe-fe0a-4da9-adac-34cbcbf4e9fb"), "", "", "Food" },
                    { new Guid("48e2cab3-6042-42ec-978a-a63feede2b20"), "", "", "Furniture" },
                    { new Guid("93482801-3942-463e-b26b-f54456daed6e"), "", "", "Electronics" },
                    { new Guid("9dc0598f-f391-4df4-abb7-8180fa6b8a0a"), "", "", "Toys" },
                    { new Guid("c59e0f9d-2ec2-4606-8a6e-b7b8a8c3d841"), "", "", "Clothing" }
                });

            migrationBuilder.InsertData(
                table: "SellingPriceTaxes",
                columns: new[] { "Id", "Description", "SellingPriceTaxName", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("5bacd899-f6d8-4b09-bff3-9b2f3d48294d"), "", "Inclusive", 0m },
                    { new Guid("647f72b5-d86d-4643-a64b-b41a83a8046b"), "", "Zero Rate", 0m },
                    { new Guid("ee07d64f-e3ba-4bc5-9cc3-2abfc61cc547"), "", "Exclusive", 0m }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryCode", "Description", "SubCategoryName" },
                values: new object[,]
                {
                    { new Guid("6c47a635-894a-4e1f-8efe-858d150c83fb"), "", "", "Televisions" },
                    { new Guid("7580bb9a-5e02-4810-a4b6-f786e776eecc"), "", "", "Smartphones" },
                    { new Guid("cde5e0f2-5488-4171-a457-85673d2ee7c5"), "", "", "Laptops" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "AllowDecimal", "ShortName", "UnitName" },
                values: new object[,]
                {
                    { new Guid("145b4a21-b679-43de-bc34-d9fb78f35ca3"), 0, "", "Kilogram" },
                    { new Guid("1ee2dec9-c63c-488c-aa29-a8e3ae4c805d"), 0, "", "Piece" },
                    { new Guid("a2415b5a-a80f-4627-9165-fdd9d6498e1f"), 0, "", "Liter" }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "Description", "Name", "WarrantyDuration" },
                values: new object[,]
                {
                    { new Guid("7840d053-4247-446a-8f3e-9b7fc3355ff1"), "", "", "2 Years" },
                    { new Guid("9b797812-ce6a-4972-8e46-c9b95f69180e"), "", "", "1 Year" },
                    { new Guid("ab5b7dcb-c308-4c6b-9225-fd36664d38a9"), "", "", "3 Years" }
                });
        }
    }
}
