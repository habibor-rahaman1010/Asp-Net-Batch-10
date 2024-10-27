using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DevSkill.Inventory.Web.Migrations.InventoryDb
{
    /// <inheritdoc />
    public partial class UpdateStockAdjustment2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("54792645-57e8-4a94-bc80-c5931ed91228"));

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("fc0eec7c-3a7d-4141-ad42-1bee66037097"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("3572df64-ff01-4637-9eb0-4a0757fb8731"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("bff23033-963c-4720-875a-3c7e9750b81d"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("c5b9941a-bfba-46dd-85f7-0159baacd1d0"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("762bbf50-e878-490c-b9ba-90c1ebf5a1d1"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("dd7cef1d-a2e0-4645-9320-c8a8e0651ef1"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("e7b56a10-0e8f-47ca-a1d2-0f6885c26d8e"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("08ae359f-fdde-4cfc-b2f8-d60a29cc4612"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("5601e854-a462-4312-a3f6-e64e3de1fce9"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("a0e2f81c-6f8b-461e-88b2-14126b184d44"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("263e5560-4837-43ff-aecf-2c22fa867086"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("5bf3c7a8-40c0-438e-bc4c-f7697499b1b5"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("b20da384-3949-4a89-814a-5188d9bae71d"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0eb9011d-273a-4fa6-9d50-9e472e20f418"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("3fce2bd6-ea00-4f96-819a-e67fd8a1ffb5"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("de407b86-9335-47c0-99dc-9f4e8b4b977f"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("1a2b902e-6992-4ae3-bf0e-43e94232e8a7"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("4e32b203-2676-49ec-9945-74a06f359a0c"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("7a187548-be20-4a7b-b983-e818d36e466b"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("a0096383-9930-4276-a632-70914631c810"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("d8639faf-0c5a-45ca-ae48-2e5f963807fc"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("2949cf75-d4b1-4be4-b567-7d13bafb38c7"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("4195024b-3e25-45d7-83a0-5f8d2a06cd98"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("64bb2ac7-88b7-4a5d-8a2a-0bb70a4000ff"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("4e1b482a-e20f-418e-b7d6-4df28b8e3ab1"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("cfdff4e8-e533-42d7-a739-b2ec75cdfb8a"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("fdb82154-1c68-410a-af4a-b8911b74c3db"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("1e4b2455-d358-4850-9567-c63208ce6d5e"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("45fff205-d3a2-4137-bcb6-10ef8cbe1d66"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("aa3bb79e-9b6d-4b93-bf3c-2205ac5b57d2"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("094d79d7-dfb1-430a-8836-c0049db07b5c"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("198988fb-fe32-42ff-a458-fe68f6db8f72"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("7b23902d-39ff-409d-9cd7-26bd9fb4a0bc"));

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "StockAdjustments",
                newName: "AdjustmentQuantity");

            migrationBuilder.InsertData(
                table: "AdjustmentTypes",
                columns: new[] { "Id", "AdjustmentTypeName", "Description" },
                values: new object[,]
                {
                    { new Guid("cd66911e-1e6e-4cc4-82dd-c84bc1461816"), "Abnormal", "" },
                    { new Guid("ebf2ec90-3724-4018-b3e0-70cf09fda857"), "Normal", "" }
                });

            migrationBuilder.InsertData(
                table: "ApplicableTaxs",
                columns: new[] { "Id", "ApplicableTaxName" },
                values: new object[,]
                {
                    { new Guid("09307dd9-7f42-4353-a4a7-3e8db0b2d024"), "Food" },
                    { new Guid("231e22bd-c47b-411d-8723-d7f310d92599"), "Sales Tax" },
                    { new Guid("5074ea0c-3e41-4252-9196-89d117c1f0dc"), "Fruits" }
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "BarcodeTypeName" },
                values: new object[,]
                {
                    { new Guid("46ed0d78-088a-419e-91b9-f94d99255021"), "UPC" },
                    { new Guid("8d388b79-3bc1-41a4-8e61-0fdb85c9427f"), "NFC" },
                    { new Guid("c6efb2c8-ee49-4b6b-acd5-563b841a932c"), "QR Code" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BandOrigin", "BrandName", "Description" },
                values: new object[,]
                {
                    { new Guid("1ba16274-7018-4be5-adb9-bdd7ac66082c"), "", "Apple", "" },
                    { new Guid("601fd9c7-7569-4b8a-9ef6-88c33f2d2a6d"), "", "Sony", "" },
                    { new Guid("a3b37722-b7f2-4f66-9ec3-7c07e80bbb85"), "", "Samsung", "" }
                });

            migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "Address", "City", "Country", "LocationName", "State", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("5df26722-63d8-4b15-aa6a-e0a1924cf75c"), "", "", "", "Downtown Store", "", "" },
                    { new Guid("b5b137dd-d3f4-4e68-bbbb-c802f5ac25be"), "", "", "", "Warehouse A", "", "" },
                    { new Guid("dd18b58e-ce05-48d7-99ed-3afad023141e"), "", "", "", "Warehouse B", "", "" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryCode", "CategoryName", "Description" },
                values: new object[,]
                {
                    { new Guid("753f76f1-98bf-4c47-bdfc-2868fac8ca3a"), "", "Home Appliances", "" },
                    { new Guid("aba5b3ae-5ba0-4e01-9e09-6b7f28973e37"), "", "Electronics", "" },
                    { new Guid("f05a076e-377d-4cf7-a149-ccb96cc4e43c"), "", "Clothing", "" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("34123722-096c-42cf-8742-e9a80682cb8c"), "Clothing" },
                    { new Guid("4888284c-1458-4909-99d8-64981174ce8e"), "Food" },
                    { new Guid("6f3c913c-753e-4e8e-9a91-4c3d3d6880a8"), "Furniture" },
                    { new Guid("80066a04-a1ee-427f-9df5-da47705b7da5"), "Toys" },
                    { new Guid("da7602fd-a642-4de3-9763-031784116a1e"), "Electronics" }
                });

            migrationBuilder.InsertData(
                table: "SellingPriceTaxes",
                columns: new[] { "Id", "SellingPriceTaxName" },
                values: new object[,]
                {
                    { new Guid("6e94f51a-6574-4671-9cd8-1696213cd0ef"), "Zero Rate" },
                    { new Guid("ba2ea601-45a8-4488-936f-f1afa3286f88"), "Inclusive" },
                    { new Guid("dbd0c99e-5f01-4b43-b3f4-2caef913c165"), "Exclusive" }
                });

            migrationBuilder.InsertData(
                table: "Subcategories",
                columns: new[] { "Id", "SubcategoryName" },
                values: new object[,]
                {
                    { new Guid("123a49b7-64d1-45c9-8553-52535d9360e0"), "Televisions" },
                    { new Guid("12a9f322-b263-4d17-81ee-56c4201c4d21"), "Smartphones" },
                    { new Guid("95f4d772-1c91-4c07-ba32-551180083070"), "Laptops" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "AllowDecimal", "ShortName", "UnitName" },
                values: new object[,]
                {
                    { new Guid("39ae2860-10cf-47d9-a8d2-6b9dec9d9222"), 0, "", "Piece" },
                    { new Guid("df9123bf-c00c-426c-9e5b-26e47c864dc2"), 0, "", "Kilogram" },
                    { new Guid("f2015fae-b2cb-4beb-a593-6d0b1725f45a"), 0, "", "Liter" }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "Description", "Name", "WarrantyDuration" },
                values: new object[,]
                {
                    { new Guid("78b258a8-8155-4f1c-9e81-0c4af2b130ce"), "", "", "1 Year" },
                    { new Guid("aba2a488-ecff-406f-be3b-efd030055afc"), "", "", "2 Years" },
                    { new Guid("d119e8e8-c41c-4e7c-89a8-42e39a85a4ed"), "", "", "3 Years" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("cd66911e-1e6e-4cc4-82dd-c84bc1461816"));

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("ebf2ec90-3724-4018-b3e0-70cf09fda857"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("09307dd9-7f42-4353-a4a7-3e8db0b2d024"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("231e22bd-c47b-411d-8723-d7f310d92599"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("5074ea0c-3e41-4252-9196-89d117c1f0dc"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("46ed0d78-088a-419e-91b9-f94d99255021"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("8d388b79-3bc1-41a4-8e61-0fdb85c9427f"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("c6efb2c8-ee49-4b6b-acd5-563b841a932c"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("1ba16274-7018-4be5-adb9-bdd7ac66082c"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("601fd9c7-7569-4b8a-9ef6-88c33f2d2a6d"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("a3b37722-b7f2-4f66-9ec3-7c07e80bbb85"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("5df26722-63d8-4b15-aa6a-e0a1924cf75c"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("b5b137dd-d3f4-4e68-bbbb-c802f5ac25be"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("dd18b58e-ce05-48d7-99ed-3afad023141e"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("753f76f1-98bf-4c47-bdfc-2868fac8ca3a"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("aba5b3ae-5ba0-4e01-9e09-6b7f28973e37"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("f05a076e-377d-4cf7-a149-ccb96cc4e43c"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("34123722-096c-42cf-8742-e9a80682cb8c"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("4888284c-1458-4909-99d8-64981174ce8e"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("6f3c913c-753e-4e8e-9a91-4c3d3d6880a8"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("80066a04-a1ee-427f-9df5-da47705b7da5"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("da7602fd-a642-4de3-9763-031784116a1e"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("6e94f51a-6574-4671-9cd8-1696213cd0ef"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("ba2ea601-45a8-4488-936f-f1afa3286f88"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("dbd0c99e-5f01-4b43-b3f4-2caef913c165"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("123a49b7-64d1-45c9-8553-52535d9360e0"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("12a9f322-b263-4d17-81ee-56c4201c4d21"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("95f4d772-1c91-4c07-ba32-551180083070"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("39ae2860-10cf-47d9-a8d2-6b9dec9d9222"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("df9123bf-c00c-426c-9e5b-26e47c864dc2"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("f2015fae-b2cb-4beb-a593-6d0b1725f45a"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("78b258a8-8155-4f1c-9e81-0c4af2b130ce"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("aba2a488-ecff-406f-be3b-efd030055afc"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("d119e8e8-c41c-4e7c-89a8-42e39a85a4ed"));

            migrationBuilder.RenameColumn(
                name: "AdjustmentQuantity",
                table: "StockAdjustments",
                newName: "Quantity");

            migrationBuilder.InsertData(
                table: "AdjustmentTypes",
                columns: new[] { "Id", "AdjustmentTypeName", "Description" },
                values: new object[,]
                {
                    { new Guid("54792645-57e8-4a94-bc80-c5931ed91228"), "Normal", "" },
                    { new Guid("fc0eec7c-3a7d-4141-ad42-1bee66037097"), "Abnormal", "" }
                });

            migrationBuilder.InsertData(
                table: "ApplicableTaxs",
                columns: new[] { "Id", "ApplicableTaxName" },
                values: new object[,]
                {
                    { new Guid("3572df64-ff01-4637-9eb0-4a0757fb8731"), "Food" },
                    { new Guid("bff23033-963c-4720-875a-3c7e9750b81d"), "Fruits" },
                    { new Guid("c5b9941a-bfba-46dd-85f7-0159baacd1d0"), "Sales Tax" }
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "BarcodeTypeName" },
                values: new object[,]
                {
                    { new Guid("762bbf50-e878-490c-b9ba-90c1ebf5a1d1"), "NFC" },
                    { new Guid("dd7cef1d-a2e0-4645-9320-c8a8e0651ef1"), "QR Code" },
                    { new Guid("e7b56a10-0e8f-47ca-a1d2-0f6885c26d8e"), "UPC" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BandOrigin", "BrandName", "Description" },
                values: new object[,]
                {
                    { new Guid("08ae359f-fdde-4cfc-b2f8-d60a29cc4612"), "", "Apple", "" },
                    { new Guid("5601e854-a462-4312-a3f6-e64e3de1fce9"), "", "Samsung", "" },
                    { new Guid("a0e2f81c-6f8b-461e-88b2-14126b184d44"), "", "Sony", "" }
                });

            migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "Address", "City", "Country", "LocationName", "State", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("263e5560-4837-43ff-aecf-2c22fa867086"), "", "", "", "Warehouse B", "", "" },
                    { new Guid("5bf3c7a8-40c0-438e-bc4c-f7697499b1b5"), "", "", "", "Downtown Store", "", "" },
                    { new Guid("b20da384-3949-4a89-814a-5188d9bae71d"), "", "", "", "Warehouse A", "", "" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryCode", "CategoryName", "Description" },
                values: new object[,]
                {
                    { new Guid("0eb9011d-273a-4fa6-9d50-9e472e20f418"), "", "Clothing", "" },
                    { new Guid("3fce2bd6-ea00-4f96-819a-e67fd8a1ffb5"), "", "Home Appliances", "" },
                    { new Guid("de407b86-9335-47c0-99dc-9f4e8b4b977f"), "", "Electronics", "" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("1a2b902e-6992-4ae3-bf0e-43e94232e8a7"), "Furniture" },
                    { new Guid("4e32b203-2676-49ec-9945-74a06f359a0c"), "Clothing" },
                    { new Guid("7a187548-be20-4a7b-b983-e818d36e466b"), "Electronics" },
                    { new Guid("a0096383-9930-4276-a632-70914631c810"), "Food" },
                    { new Guid("d8639faf-0c5a-45ca-ae48-2e5f963807fc"), "Toys" }
                });

            migrationBuilder.InsertData(
                table: "SellingPriceTaxes",
                columns: new[] { "Id", "SellingPriceTaxName" },
                values: new object[,]
                {
                    { new Guid("2949cf75-d4b1-4be4-b567-7d13bafb38c7"), "Zero Rate" },
                    { new Guid("4195024b-3e25-45d7-83a0-5f8d2a06cd98"), "Exclusive" },
                    { new Guid("64bb2ac7-88b7-4a5d-8a2a-0bb70a4000ff"), "Inclusive" }
                });

            migrationBuilder.InsertData(
                table: "Subcategories",
                columns: new[] { "Id", "SubcategoryName" },
                values: new object[,]
                {
                    { new Guid("4e1b482a-e20f-418e-b7d6-4df28b8e3ab1"), "Laptops" },
                    { new Guid("cfdff4e8-e533-42d7-a739-b2ec75cdfb8a"), "Televisions" },
                    { new Guid("fdb82154-1c68-410a-af4a-b8911b74c3db"), "Smartphones" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "AllowDecimal", "ShortName", "UnitName" },
                values: new object[,]
                {
                    { new Guid("1e4b2455-d358-4850-9567-c63208ce6d5e"), 0, "", "Kilogram" },
                    { new Guid("45fff205-d3a2-4137-bcb6-10ef8cbe1d66"), 0, "", "Piece" },
                    { new Guid("aa3bb79e-9b6d-4b93-bf3c-2205ac5b57d2"), 0, "", "Liter" }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "Description", "Name", "WarrantyDuration" },
                values: new object[,]
                {
                    { new Guid("094d79d7-dfb1-430a-8836-c0049db07b5c"), "", "", "1 Year" },
                    { new Guid("198988fb-fe32-42ff-a458-fe68f6db8f72"), "", "", "2 Years" },
                    { new Guid("7b23902d-39ff-409d-9cd7-26bd9fb4a0bc"), "", "", "3 Years" }
                });
        }
    }
}
