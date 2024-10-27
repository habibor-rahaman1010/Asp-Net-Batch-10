using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DevSkill.Inventory.Web.Migrations.InventoryDb
{
    /// <inheritdoc />
    public partial class MyAllTableUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ApplicableTaxs_ApplicableTaxId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_BarcodeTypes_BarcodeTypeId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_BusinessLocations_BusinessLocationId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_SellingPriceTaxes_SellingPriceTaxId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Subcategories_SubcategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Units_UnitId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Warranties_WarrantyId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_StockAdjustments_BusinessLocations_BusinessLocationId",
                table: "StockAdjustments");

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("019d9193-08d7-434a-8ac4-c5af7eb58a00"));

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("fbefbd69-6b34-40e8-a23a-9aac1904d70d"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("c264b3f9-7a62-4ebd-9ef0-1cf1541cf8c4"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("c6b5273b-9892-49d5-96c2-1b551c884934"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("cf90a96a-5386-4dd0-b297-79a4b038c652"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("28628019-c539-484f-8641-ae9e0f2dd888"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("2a021909-9ae8-42c8-8fe7-e67e02940999"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("b848c430-96f5-4bd2-877a-8f5b6e7cc3fa"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("3144a24a-9871-4d36-87ec-594304dfc5a5"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("5f6d7589-ebe2-4172-8bd2-a01ac423f933"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("e995cab2-bd88-4597-ae01-31a713e2c2af"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("3da84706-747f-4b61-9a91-92e1b45e9202"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("8283ddb8-e5c6-4b8a-9899-c7960d6e792b"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("f6a53bdd-304f-4e75-ba96-483a6a7fa68e"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("17a9d308-d905-4e51-abaf-d5af1deb7968"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a8a93ef7-e00d-4e03-803b-a15687db8d08"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("e4503a07-4e9f-4ae1-a9cc-707f6dd029c6"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("48e71b16-8973-4a47-acc9-b56174c5b474"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("6bf35d50-ddc1-401b-9663-247f16969897"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("86a77b47-35ff-457b-80f6-92723487effa"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("8915f6f3-345b-427b-aeee-8f3f5f527aa1"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("e1609561-adcb-4fa4-9f42-8f3f8f09b08c"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("24835120-9ab3-4156-b139-dc6d4c47e8cb"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("43e09a64-f917-40df-abec-3b8315101fbd"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("d531544d-af73-4e96-8b81-ca96f1af8811"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("482f7c94-0a89-41f1-8328-d15f9858d1c4"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("4c9e0bf1-39f8-4e24-bc84-02eb0e06299d"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("d0bd1ed4-09af-47b2-900e-3850682f8365"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("01f4bd14-cd2a-44b8-8af2-1022a671c9c7"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("4fcaf20e-4753-4b91-93d5-c52b7f510b1b"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("c594b92f-38ab-42b5-bedc-814a51ea5aad"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("3071b0d8-6904-4763-8a01-96ccce260ad3"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("bed9bf32-e1ba-4b97-b88b-78bcd3aa127e"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("d525efd7-0fdd-4142-8b6d-f6a5ce1fc75a"));

            migrationBuilder.AlterColumn<Guid>(
                name: "SubcategoryId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "BusinessLocationId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ApplicableTaxId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "AdjustmentTypes",
                columns: new[] { "Id", "AdjustmentTypeName", "Description" },
                values: new object[,]
                {
                    { new Guid("626cfb94-01cb-4570-937d-81e0931316f1"), "Abnormal", "" },
                    { new Guid("bea030e1-201b-49c4-9859-bdb3ba328562"), "Normal", "" }
                });

            migrationBuilder.InsertData(
                table: "ApplicableTaxs",
                columns: new[] { "Id", "ApplicableTaxName" },
                values: new object[,]
                {
                    { new Guid("4ce1dfcc-d890-4f9b-b4e9-336639078d51"), "Sales Tax" },
                    { new Guid("62b38f04-553e-417f-badd-9b0c2c5c9996"), "Fruits" },
                    { new Guid("978b50c7-62dc-4a79-828f-24dee0d8b98e"), "Food" }
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "BarcodeTypeName" },
                values: new object[,]
                {
                    { new Guid("0ac349e8-b214-4c9a-9788-5ff6f687adf0"), "UPC" },
                    { new Guid("6cbf21d8-c4ce-4930-a1cb-fd4952abe17d"), "NFC" },
                    { new Guid("caf13dbf-175b-4f1d-a09b-b873e4718aba"), "QR Code" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BandOrigin", "BrandName", "Description" },
                values: new object[,]
                {
                    { new Guid("22e36e94-944c-4c93-a1ec-558c10a7acec"), "", "Samsung", "" },
                    { new Guid("42398c66-1f62-4b67-a45e-36480ee0d31f"), "", "Apple", "" },
                    { new Guid("afc1c6e8-b057-4f59-bfba-78098becc4e9"), "", "Sony", "" }
                });

            migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "Address", "City", "Country", "LocationName", "State", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("4232f60f-695d-4343-a5c5-3ce0f1bb46b9"), "", "", "", "Warehouse A", "", "" },
                    { new Guid("e6ffe3bc-28f7-41dc-a0b8-ee2d24d95e23"), "", "", "", "Warehouse B", "", "" },
                    { new Guid("e704b6c3-1a93-4616-9768-4dfb3c5bc2a5"), "", "", "", "Downtown Store", "", "" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryCode", "CategoryName", "Description" },
                values: new object[,]
                {
                    { new Guid("6545e1bd-585f-4147-8168-516a9f2633ae"), "", "Home Appliances", "" },
                    { new Guid("a1fe9107-1c30-42a0-8b61-596d727d0925"), "", "Electronics", "" },
                    { new Guid("b79ec226-9535-4121-a81f-f91620a800ae"), "", "Clothing", "" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("0ea0e95b-9865-4c81-8ecf-89427d993d7f"), "Clothing" },
                    { new Guid("7cf69625-ac59-4be3-bb3d-755b57919287"), "Toys" },
                    { new Guid("835c7f98-d81f-4164-8d5e-311466fdc65d"), "Furniture" },
                    { new Guid("a75d3d68-2ac9-4c51-bf3e-7221399b1996"), "Food" },
                    { new Guid("db7558b2-722c-4d62-b44a-d4eb4398e081"), "Electronics" }
                });

            migrationBuilder.InsertData(
                table: "SellingPriceTaxes",
                columns: new[] { "Id", "SellingPriceTaxName" },
                values: new object[,]
                {
                    { new Guid("196d9f9d-7e02-47e4-b7f5-b93bf920cefe"), "Exclusive" },
                    { new Guid("da5fbc58-c3c9-4c9d-9012-c8e7b520e71e"), "Zero Rate" },
                    { new Guid("ee4874cf-30c8-475c-adfe-cc199e463867"), "Inclusive" }
                });

            migrationBuilder.InsertData(
                table: "Subcategories",
                columns: new[] { "Id", "SubcategoryName" },
                values: new object[,]
                {
                    { new Guid("004edaed-73fe-49e7-a5d4-47c8e9293526"), "Televisions" },
                    { new Guid("475840b1-5e89-495c-9ac1-4a36461238e8"), "Laptops" },
                    { new Guid("ed5f6a26-ebcd-479d-ba98-4d74d5b1eda0"), "Smartphones" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "AllowDecimal", "ShortName", "UnitName" },
                values: new object[,]
                {
                    { new Guid("9a54035f-13a6-4218-bf4a-d4550236ba20"), 0, "", "Kilogram" },
                    { new Guid("aa432a84-eaf4-472f-8ff4-4f658783e8db"), 0, "", "Liter" },
                    { new Guid("e6aba29c-8444-4706-8680-300778d23fee"), 0, "", "Piece" }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "Description", "Name", "WarrantyDuration" },
                values: new object[,]
                {
                    { new Guid("3b8f40cc-e5d6-463b-aa97-9d53ea3ec4b5"), "", "", "3 Years" },
                    { new Guid("8973b3ff-8630-4420-a8d3-86f84025008f"), "", "", "2 Years" },
                    { new Guid("ad31eb84-68d0-4207-b2b2-a532a2e8769d"), "", "", "1 Year" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ApplicableTaxs_ApplicableTaxId",
                table: "Products",
                column: "ApplicableTaxId",
                principalTable: "ApplicableTaxs",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_BarcodeTypes_BarcodeTypeId",
                table: "Products",
                column: "BarcodeTypeId",
                principalTable: "BarcodeTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_BusinessLocations_BusinessLocationId",
                table: "Products",
                column: "BusinessLocationId",
                principalTable: "BusinessLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeId",
                table: "Products",
                column: "ProductTypeId",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SellingPriceTaxes_SellingPriceTaxId",
                table: "Products",
                column: "SellingPriceTaxId",
                principalTable: "SellingPriceTaxes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Subcategories_SubcategoryId",
                table: "Products",
                column: "SubcategoryId",
                principalTable: "Subcategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Units_UnitId",
                table: "Products",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Warranties_WarrantyId",
                table: "Products",
                column: "WarrantyId",
                principalTable: "Warranties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StockAdjustments_BusinessLocations_BusinessLocationId",
                table: "StockAdjustments",
                column: "BusinessLocationId",
                principalTable: "BusinessLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ApplicableTaxs_ApplicableTaxId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_BarcodeTypes_BarcodeTypeId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_BusinessLocations_BusinessLocationId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_SellingPriceTaxes_SellingPriceTaxId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Subcategories_SubcategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Units_UnitId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Warranties_WarrantyId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_StockAdjustments_BusinessLocations_BusinessLocationId",
                table: "StockAdjustments");

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("626cfb94-01cb-4570-937d-81e0931316f1"));

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("bea030e1-201b-49c4-9859-bdb3ba328562"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("4ce1dfcc-d890-4f9b-b4e9-336639078d51"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("62b38f04-553e-417f-badd-9b0c2c5c9996"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("978b50c7-62dc-4a79-828f-24dee0d8b98e"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("0ac349e8-b214-4c9a-9788-5ff6f687adf0"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("6cbf21d8-c4ce-4930-a1cb-fd4952abe17d"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("caf13dbf-175b-4f1d-a09b-b873e4718aba"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("22e36e94-944c-4c93-a1ec-558c10a7acec"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("42398c66-1f62-4b67-a45e-36480ee0d31f"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("afc1c6e8-b057-4f59-bfba-78098becc4e9"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("4232f60f-695d-4343-a5c5-3ce0f1bb46b9"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("e6ffe3bc-28f7-41dc-a0b8-ee2d24d95e23"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("e704b6c3-1a93-4616-9768-4dfb3c5bc2a5"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6545e1bd-585f-4147-8168-516a9f2633ae"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1fe9107-1c30-42a0-8b61-596d727d0925"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b79ec226-9535-4121-a81f-f91620a800ae"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("0ea0e95b-9865-4c81-8ecf-89427d993d7f"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("7cf69625-ac59-4be3-bb3d-755b57919287"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("835c7f98-d81f-4164-8d5e-311466fdc65d"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("a75d3d68-2ac9-4c51-bf3e-7221399b1996"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("db7558b2-722c-4d62-b44a-d4eb4398e081"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("196d9f9d-7e02-47e4-b7f5-b93bf920cefe"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("da5fbc58-c3c9-4c9d-9012-c8e7b520e71e"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("ee4874cf-30c8-475c-adfe-cc199e463867"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("004edaed-73fe-49e7-a5d4-47c8e9293526"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("475840b1-5e89-495c-9ac1-4a36461238e8"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("ed5f6a26-ebcd-479d-ba98-4d74d5b1eda0"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("9a54035f-13a6-4218-bf4a-d4550236ba20"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("aa432a84-eaf4-472f-8ff4-4f658783e8db"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("e6aba29c-8444-4706-8680-300778d23fee"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("3b8f40cc-e5d6-463b-aa97-9d53ea3ec4b5"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("8973b3ff-8630-4420-a8d3-86f84025008f"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("ad31eb84-68d0-4207-b2b2-a532a2e8769d"));

            migrationBuilder.AlterColumn<Guid>(
                name: "SubcategoryId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "BusinessLocationId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ApplicableTaxId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AdjustmentTypes",
                columns: new[] { "Id", "AdjustmentTypeName", "Description" },
                values: new object[,]
                {
                    { new Guid("019d9193-08d7-434a-8ac4-c5af7eb58a00"), "Normal", "" },
                    { new Guid("fbefbd69-6b34-40e8-a23a-9aac1904d70d"), "Abnormal", "" }
                });

            migrationBuilder.InsertData(
                table: "ApplicableTaxs",
                columns: new[] { "Id", "ApplicableTaxName" },
                values: new object[,]
                {
                    { new Guid("c264b3f9-7a62-4ebd-9ef0-1cf1541cf8c4"), "Food" },
                    { new Guid("c6b5273b-9892-49d5-96c2-1b551c884934"), "Fruits" },
                    { new Guid("cf90a96a-5386-4dd0-b297-79a4b038c652"), "Sales Tax" }
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "BarcodeTypeName" },
                values: new object[,]
                {
                    { new Guid("28628019-c539-484f-8641-ae9e0f2dd888"), "UPC" },
                    { new Guid("2a021909-9ae8-42c8-8fe7-e67e02940999"), "QR Code" },
                    { new Guid("b848c430-96f5-4bd2-877a-8f5b6e7cc3fa"), "NFC" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BandOrigin", "BrandName", "Description" },
                values: new object[,]
                {
                    { new Guid("3144a24a-9871-4d36-87ec-594304dfc5a5"), "", "Samsung", "" },
                    { new Guid("5f6d7589-ebe2-4172-8bd2-a01ac423f933"), "", "Sony", "" },
                    { new Guid("e995cab2-bd88-4597-ae01-31a713e2c2af"), "", "Apple", "" }
                });

            migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "Address", "City", "Country", "LocationName", "State", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("3da84706-747f-4b61-9a91-92e1b45e9202"), "", "", "", "Warehouse B", "", "" },
                    { new Guid("8283ddb8-e5c6-4b8a-9899-c7960d6e792b"), "", "", "", "Downtown Store", "", "" },
                    { new Guid("f6a53bdd-304f-4e75-ba96-483a6a7fa68e"), "", "", "", "Warehouse A", "", "" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryCode", "CategoryName", "Description" },
                values: new object[,]
                {
                    { new Guid("17a9d308-d905-4e51-abaf-d5af1deb7968"), "", "Electronics", "" },
                    { new Guid("a8a93ef7-e00d-4e03-803b-a15687db8d08"), "", "Clothing", "" },
                    { new Guid("e4503a07-4e9f-4ae1-a9cc-707f6dd029c6"), "", "Home Appliances", "" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("48e71b16-8973-4a47-acc9-b56174c5b474"), "Furniture" },
                    { new Guid("6bf35d50-ddc1-401b-9663-247f16969897"), "Electronics" },
                    { new Guid("86a77b47-35ff-457b-80f6-92723487effa"), "Clothing" },
                    { new Guid("8915f6f3-345b-427b-aeee-8f3f5f527aa1"), "Food" },
                    { new Guid("e1609561-adcb-4fa4-9f42-8f3f8f09b08c"), "Toys" }
                });

            migrationBuilder.InsertData(
                table: "SellingPriceTaxes",
                columns: new[] { "Id", "SellingPriceTaxName" },
                values: new object[,]
                {
                    { new Guid("24835120-9ab3-4156-b139-dc6d4c47e8cb"), "Zero Rate" },
                    { new Guid("43e09a64-f917-40df-abec-3b8315101fbd"), "Exclusive" },
                    { new Guid("d531544d-af73-4e96-8b81-ca96f1af8811"), "Inclusive" }
                });

            migrationBuilder.InsertData(
                table: "Subcategories",
                columns: new[] { "Id", "SubcategoryName" },
                values: new object[,]
                {
                    { new Guid("482f7c94-0a89-41f1-8328-d15f9858d1c4"), "Televisions" },
                    { new Guid("4c9e0bf1-39f8-4e24-bc84-02eb0e06299d"), "Laptops" },
                    { new Guid("d0bd1ed4-09af-47b2-900e-3850682f8365"), "Smartphones" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "AllowDecimal", "ShortName", "UnitName" },
                values: new object[,]
                {
                    { new Guid("01f4bd14-cd2a-44b8-8af2-1022a671c9c7"), 0, "", "Kilogram" },
                    { new Guid("4fcaf20e-4753-4b91-93d5-c52b7f510b1b"), 0, "", "Liter" },
                    { new Guid("c594b92f-38ab-42b5-bedc-814a51ea5aad"), 0, "", "Piece" }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "Description", "Name", "WarrantyDuration" },
                values: new object[,]
                {
                    { new Guid("3071b0d8-6904-4763-8a01-96ccce260ad3"), "", "", "3 Years" },
                    { new Guid("bed9bf32-e1ba-4b97-b88b-78bcd3aa127e"), "", "", "2 Years" },
                    { new Guid("d525efd7-0fdd-4142-8b6d-f6a5ce1fc75a"), "", "", "1 Year" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ApplicableTaxs_ApplicableTaxId",
                table: "Products",
                column: "ApplicableTaxId",
                principalTable: "ApplicableTaxs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_BarcodeTypes_BarcodeTypeId",
                table: "Products",
                column: "BarcodeTypeId",
                principalTable: "BarcodeTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_BusinessLocations_BusinessLocationId",
                table: "Products",
                column: "BusinessLocationId",
                principalTable: "BusinessLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeId",
                table: "Products",
                column: "ProductTypeId",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SellingPriceTaxes_SellingPriceTaxId",
                table: "Products",
                column: "SellingPriceTaxId",
                principalTable: "SellingPriceTaxes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Subcategories_SubcategoryId",
                table: "Products",
                column: "SubcategoryId",
                principalTable: "Subcategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Units_UnitId",
                table: "Products",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Warranties_WarrantyId",
                table: "Products",
                column: "WarrantyId",
                principalTable: "Warranties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockAdjustments_BusinessLocations_BusinessLocationId",
                table: "StockAdjustments",
                column: "BusinessLocationId",
                principalTable: "BusinessLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
