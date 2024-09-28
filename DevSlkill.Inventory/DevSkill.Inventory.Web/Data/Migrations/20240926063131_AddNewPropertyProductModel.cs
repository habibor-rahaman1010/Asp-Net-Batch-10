using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DevSkill.Inventory.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNewPropertyProductModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("1bbc6dad-febb-4c06-8982-e16ca5dcff5b"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("2ced8ddf-c61f-43f3-bafc-9f8a9e6711bc"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("7169b3c9-e792-4295-a5e4-35ae20eff422"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("05ac0168-47fe-45ce-8992-d0c14d0b17f9"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("8b7e7911-6b04-4b41-8543-950de307a422"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("e07e7533-0bcb-43d4-85fb-aecdc71296f0"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("55c6c639-9a61-4744-8abd-7acdb08c7838"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("9fe287c6-d45e-4b70-9c8b-fb03468b50c7"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("fac71f5e-6325-480a-ac90-22fb5f34a718"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("387a629c-c730-4cf8-8f67-6e8601cb7437"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("67c8c71b-4c7d-4508-b374-895e93673cda"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("a885f1d5-2c0a-416c-a83e-60565a2d5871"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("511863b0-bef7-45c1-8308-7bf6f32b5e41"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("5919db74-0f53-42cb-ab37-ca3cb6d315b1"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9e9abffa-cb1f-4da5-9034-ffb9579df891"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("00c82f51-3bf0-40d1-aad2-61032a455dd1"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("5d3efa4b-bad2-4235-93ed-b9cfbe828f15"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("d175e877-1f20-4189-9ed0-bb276bfc8dc3"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("e268d9cf-cb25-41c1-b2bd-58b4d1d7642c"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("ea140c52-fdae-4ed0-87b2-49f6b1aca0a0"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("4858aaef-c9df-4728-a5fb-e501a43b2882"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("7586d684-9317-4262-867b-fb61df1be83c"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("7db18aac-a0a4-437f-a5df-38acd04b764c"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("79ba127d-9f22-49fb-a4f0-059c8457cbbf"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("b755f881-fad7-4386-b80a-17dba807b188"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("ed7e1fa9-b494-4c17-8257-c151623a1cd2"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("696d6e03-d431-4be8-add8-3438f924e7d2"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("9abbb0b2-0dfc-49f2-901a-0a2460532853"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("d2f77d97-1e42-47a7-8c80-c1f7b9ac6cb7"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("6085ffb3-1cb5-435b-b7e5-1ce5f1269480"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("6681070d-4abf-4b9d-93d0-b80c7df9cf4c"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("d07026af-a8a2-40e6-95a3-e9e35974496c"));

            migrationBuilder.AddColumn<string>(
                name: "ProductImage",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "ApplicableTaxs",
                columns: new[] { "Id", "ApplicableTaxName" },
                values: new object[,]
                {
                    { new Guid("56ca22de-d8ce-4642-819d-b915a8abcfe2"), "Fruits" },
                    { new Guid("c68e8903-c867-477f-b159-3c40e9fc15d3"), "Sales Tax" },
                    { new Guid("cc962669-50ce-4c7b-a1e3-b810dd2c2af1"), "Food" }
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "BarcodeTypeName" },
                values: new object[,]
                {
                    { new Guid("870aa13e-b185-4fff-ab35-e0759af85c43"), "NFC" },
                    { new Guid("b20017a7-766f-4e98-914b-1b2371f84df3"), "UPC" },
                    { new Guid("b6a737d6-98fa-41ee-b6cb-74778150c978"), "QR Code" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BrandName" },
                values: new object[,]
                {
                    { new Guid("c041e676-ae07-4433-9a68-14504caf28f8"), "Apple" },
                    { new Guid("dafc99ad-405c-43a2-ad0a-5067e8fad90b"), "Samsung" },
                    { new Guid("e6becabe-d9d3-4d24-a82c-9ab928f31c5d"), "Sony" }
                });

            migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "LocationName" },
                values: new object[,]
                {
                    { new Guid("48db8dc4-4de3-426c-9c03-6ef9420d5d65"), "Warehouse A" },
                    { new Guid("9fa61ce9-86c5-401c-8702-73e6d039127e"), "Downtown Store" },
                    { new Guid("e3abab94-9bfe-4967-992e-176c7fde8e90"), "Warehouse B" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName" },
                values: new object[,]
                {
                    { new Guid("80d2234c-8a17-412f-b099-ac433e07008a"), "Electronics" },
                    { new Guid("b4ba6730-eda3-4969-8740-8568db5c3261"), "Home Appliances" },
                    { new Guid("d6088aaa-f8de-4071-b62c-4670652c889a"), "Clothing" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("17ca8ecd-7616-46bc-98a9-76106dcb55a5"), "Food" },
                    { new Guid("28a8a8c1-6356-47ff-84fb-ee84dfd29edc"), "Toys" },
                    { new Guid("8748b72b-b63e-4261-affb-c06f7c1e3f7c"), "Electronics" },
                    { new Guid("b5c164c6-91bb-4407-b5bc-9d795012fd30"), "Furniture" },
                    { new Guid("fcd4fb58-ff5c-44a1-ac90-c13fa755c8d9"), "Clothing" }
                });

            migrationBuilder.InsertData(
                table: "SellingPriceTaxes",
                columns: new[] { "Id", "SellingPriceTaxName" },
                values: new object[,]
                {
                    { new Guid("01ab0059-df18-4cc3-8dbe-1fa58fab5eff"), "Exclusive" },
                    { new Guid("8088a11d-5152-4dd0-b296-6734d249d2fc"), "Inclusive" },
                    { new Guid("a4f32a14-e76f-40e5-8913-5e733c377ddc"), "Zero Rate" }
                });

            migrationBuilder.InsertData(
                table: "Subcategories",
                columns: new[] { "Id", "SubcategoryName" },
                values: new object[,]
                {
                    { new Guid("222c8660-c1b0-4f3b-9712-d42414c8b4a8"), "Laptops" },
                    { new Guid("3b548560-921c-486c-afed-550f08ab547a"), "Televisions" },
                    { new Guid("c2113e96-49ea-4bdf-9838-324dbe89e1b2"), "Smartphones" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "UnitName" },
                values: new object[,]
                {
                    { new Guid("28c45c46-34d7-4a78-8074-db6df9a1d5c2"), "Piece" },
                    { new Guid("2be6ad91-dc8d-4bb6-a579-31c6887ce456"), "Liter" },
                    { new Guid("556c7957-c943-4957-b5a6-c418d8486ac9"), "Kilogram" }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "WarrantyDuration" },
                values: new object[,]
                {
                    { new Guid("2c3a2013-3d51-41c7-b152-4b7861154813"), "1 Year" },
                    { new Guid("2f6fbfbe-5787-4f1b-ac27-bdbc07539689"), "2 Years" },
                    { new Guid("f19ce662-6e0f-46a9-a695-5ff413ba456e"), "3 Years" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("56ca22de-d8ce-4642-819d-b915a8abcfe2"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("c68e8903-c867-477f-b159-3c40e9fc15d3"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("cc962669-50ce-4c7b-a1e3-b810dd2c2af1"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("870aa13e-b185-4fff-ab35-e0759af85c43"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("b20017a7-766f-4e98-914b-1b2371f84df3"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("b6a737d6-98fa-41ee-b6cb-74778150c978"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("c041e676-ae07-4433-9a68-14504caf28f8"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("dafc99ad-405c-43a2-ad0a-5067e8fad90b"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("e6becabe-d9d3-4d24-a82c-9ab928f31c5d"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("48db8dc4-4de3-426c-9c03-6ef9420d5d65"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("9fa61ce9-86c5-401c-8702-73e6d039127e"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("e3abab94-9bfe-4967-992e-176c7fde8e90"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("80d2234c-8a17-412f-b099-ac433e07008a"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b4ba6730-eda3-4969-8740-8568db5c3261"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d6088aaa-f8de-4071-b62c-4670652c889a"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("17ca8ecd-7616-46bc-98a9-76106dcb55a5"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("28a8a8c1-6356-47ff-84fb-ee84dfd29edc"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("8748b72b-b63e-4261-affb-c06f7c1e3f7c"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("b5c164c6-91bb-4407-b5bc-9d795012fd30"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("fcd4fb58-ff5c-44a1-ac90-c13fa755c8d9"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("01ab0059-df18-4cc3-8dbe-1fa58fab5eff"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("8088a11d-5152-4dd0-b296-6734d249d2fc"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("a4f32a14-e76f-40e5-8913-5e733c377ddc"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("222c8660-c1b0-4f3b-9712-d42414c8b4a8"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("3b548560-921c-486c-afed-550f08ab547a"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("c2113e96-49ea-4bdf-9838-324dbe89e1b2"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("28c45c46-34d7-4a78-8074-db6df9a1d5c2"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("2be6ad91-dc8d-4bb6-a579-31c6887ce456"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("556c7957-c943-4957-b5a6-c418d8486ac9"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("2c3a2013-3d51-41c7-b152-4b7861154813"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("2f6fbfbe-5787-4f1b-ac27-bdbc07539689"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("f19ce662-6e0f-46a9-a695-5ff413ba456e"));

            migrationBuilder.DropColumn(
                name: "ProductImage",
                table: "Products");

            migrationBuilder.InsertData(
                table: "ApplicableTaxs",
                columns: new[] { "Id", "ApplicableTaxName" },
                values: new object[,]
                {
                    { new Guid("1bbc6dad-febb-4c06-8982-e16ca5dcff5b"), "Food" },
                    { new Guid("2ced8ddf-c61f-43f3-bafc-9f8a9e6711bc"), "Sales Tax" },
                    { new Guid("7169b3c9-e792-4295-a5e4-35ae20eff422"), "Fruits" }
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "BarcodeTypeName" },
                values: new object[,]
                {
                    { new Guid("05ac0168-47fe-45ce-8992-d0c14d0b17f9"), "UPC" },
                    { new Guid("8b7e7911-6b04-4b41-8543-950de307a422"), "QR Code" },
                    { new Guid("e07e7533-0bcb-43d4-85fb-aecdc71296f0"), "NFC" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BrandName" },
                values: new object[,]
                {
                    { new Guid("55c6c639-9a61-4744-8abd-7acdb08c7838"), "Sony" },
                    { new Guid("9fe287c6-d45e-4b70-9c8b-fb03468b50c7"), "Apple" },
                    { new Guid("fac71f5e-6325-480a-ac90-22fb5f34a718"), "Samsung" }
                });

            migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "LocationName" },
                values: new object[,]
                {
                    { new Guid("387a629c-c730-4cf8-8f67-6e8601cb7437"), "Warehouse B" },
                    { new Guid("67c8c71b-4c7d-4508-b374-895e93673cda"), "Warehouse A" },
                    { new Guid("a885f1d5-2c0a-416c-a83e-60565a2d5871"), "Downtown Store" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName" },
                values: new object[,]
                {
                    { new Guid("511863b0-bef7-45c1-8308-7bf6f32b5e41"), "Electronics" },
                    { new Guid("5919db74-0f53-42cb-ab37-ca3cb6d315b1"), "Home Appliances" },
                    { new Guid("9e9abffa-cb1f-4da5-9034-ffb9579df891"), "Clothing" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("00c82f51-3bf0-40d1-aad2-61032a455dd1"), "Clothing" },
                    { new Guid("5d3efa4b-bad2-4235-93ed-b9cfbe828f15"), "Electronics" },
                    { new Guid("d175e877-1f20-4189-9ed0-bb276bfc8dc3"), "Food" },
                    { new Guid("e268d9cf-cb25-41c1-b2bd-58b4d1d7642c"), "Furniture" },
                    { new Guid("ea140c52-fdae-4ed0-87b2-49f6b1aca0a0"), "Toys" }
                });

            migrationBuilder.InsertData(
                table: "SellingPriceTaxes",
                columns: new[] { "Id", "SellingPriceTaxName" },
                values: new object[,]
                {
                    { new Guid("4858aaef-c9df-4728-a5fb-e501a43b2882"), "Inclusive" },
                    { new Guid("7586d684-9317-4262-867b-fb61df1be83c"), "Exclusive" },
                    { new Guid("7db18aac-a0a4-437f-a5df-38acd04b764c"), "Zero Rate" }
                });

            migrationBuilder.InsertData(
                table: "Subcategories",
                columns: new[] { "Id", "SubcategoryName" },
                values: new object[,]
                {
                    { new Guid("79ba127d-9f22-49fb-a4f0-059c8457cbbf"), "Laptops" },
                    { new Guid("b755f881-fad7-4386-b80a-17dba807b188"), "Televisions" },
                    { new Guid("ed7e1fa9-b494-4c17-8257-c151623a1cd2"), "Smartphones" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "UnitName" },
                values: new object[,]
                {
                    { new Guid("696d6e03-d431-4be8-add8-3438f924e7d2"), "Piece" },
                    { new Guid("9abbb0b2-0dfc-49f2-901a-0a2460532853"), "Kilogram" },
                    { new Guid("d2f77d97-1e42-47a7-8c80-c1f7b9ac6cb7"), "Liter" }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "WarrantyDuration" },
                values: new object[,]
                {
                    { new Guid("6085ffb3-1cb5-435b-b7e5-1ce5f1269480"), "3 Years" },
                    { new Guid("6681070d-4abf-4b9d-93d0-b80c7df9cf4c"), "2 Years" },
                    { new Guid("d07026af-a8a2-40e6-95a3-e9e35974496c"), "1 Year" }
                });
        }
    }
}
