using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DevSkill.Inventory.Web.Migrations.InventoryDb
{
    /// <inheritdoc />
    public partial class UpdateBarcodeTypeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "BarcodeDescription",
                table: "BarcodeTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BarcodeTypeCode",
                table: "BarcodeTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "BarcodeDescription",
                table: "BarcodeTypes");

            migrationBuilder.DropColumn(
                name: "BarcodeTypeCode",
                table: "BarcodeTypes");

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
    }
}
