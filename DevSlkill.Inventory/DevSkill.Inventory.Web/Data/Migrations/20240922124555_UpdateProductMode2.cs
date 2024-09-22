using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DevSkill.Inventory.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductMode2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("6e00fd96-244c-42a3-b4d4-885796c07e25"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("b335f2af-e56f-473d-a112-24d23ca79727"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("fc41c419-7b2f-4fea-8268-38c48ef5fabf"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("3ae062cd-1d42-4b9f-ae05-55bd2f6782ef"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("701239b9-7cfd-49d8-9406-b5ab4a47ecc5"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("9eae4bb4-eb42-46ba-afca-c009fe2b5528"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("27120563-2253-4d7e-b75f-9e54b232bc05"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("5dc5a503-22d2-4965-bcc5-b5abcea1a722"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("e7910ef3-6d4c-4429-a11e-c7fbb7e2f791"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("cd55d169-d873-420e-bcad-017f120af5b5"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("ce5734aa-f436-4b2f-8e57-2c26d142888a"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("d6240d7a-aa5f-4fd2-a71f-ac488ff6af9f"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0c4558a9-a18a-4a48-b110-007665045bc9"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("ad041ff8-1e82-4545-a040-9174638dd33e"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c6e6b2f2-151e-45c0-9953-0dc52846aaf8"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("04a40edd-b1b4-452d-8f2a-016e56fa094a"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("2155827e-6efb-441a-a4c4-e5dbaf9ff60d"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("2d8639f0-a3a5-47fd-a6e6-b4382960a5ba"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("68be5838-c9d8-495c-87ca-74b14bdefca1"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("cf6d3168-274b-4a0f-a632-390a554883a3"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("437e4c0f-25db-4a23-8e5c-06fae46ac27d"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("a470d2b1-18a1-494e-85bc-fbcf471aed34"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("c9055759-235a-442c-8a58-9f4d2ae4ec1d"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("24bffabd-0b2f-40a4-aa38-88cf3f263c82"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("c4c884a4-d036-4212-82f7-851ee1d393fc"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("dbbb55c0-dadf-444b-b530-f73b86fa9e68"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("1367c9ca-2c99-41ea-927a-052af2cced48"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("3ead9c40-4529-4e22-b3c2-941844e23e3f"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("e9cf6e2c-75c0-44f1-acab-8105ca05f640"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("271ca443-ccba-4cdb-84d0-8e29d0f1f46c"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("e91d720b-898d-4bc9-96f5-87e75ee77762"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("fc1f6799-9fd5-465b-9177-d1ca148f9951"));

            migrationBuilder.RenameColumn(
                name: "Possition",
                table: "Products",
                newName: "Position");

            migrationBuilder.InsertData(
                table: "ApplicableTaxs",
                columns: new[] { "Id", "ApplicableTaxName" },
                values: new object[,]
                {
                    { new Guid("3b58746b-43d4-42ec-be04-f6c9dca3aaf6"), "Food" },
                    { new Guid("68d01f25-69bc-4234-ac07-f2cbc390281e"), "Sales Tax" },
                    { new Guid("f4d071ab-052f-47ad-84d9-0aa7ddee57b7"), "Fruits" }
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "BarcodeTypeName" },
                values: new object[,]
                {
                    { new Guid("6233fda4-b367-4823-b956-5e1a42ec538b"), "QR Code" },
                    { new Guid("86400884-029e-4c8b-ac57-257741b4feec"), "UPC" },
                    { new Guid("cd426fa1-5e1b-4e77-af03-db28d3fb8f46"), "NFC" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BrandName" },
                values: new object[,]
                {
                    { new Guid("2a0bea73-91b8-4326-ab8b-21bbb43a74b4"), "Sony" },
                    { new Guid("3b92c95a-dc7c-4289-8dd8-9a38b4fb72f1"), "Samsung" },
                    { new Guid("5aae83c3-24f7-4bd0-b58c-56f3c4b5976a"), "Apple" }
                });

            migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "LocationName" },
                values: new object[,]
                {
                    { new Guid("776e90c0-9e53-41cf-8464-67512b512f62"), "Warehouse B" },
                    { new Guid("7caa505e-77ec-4ce4-8a2d-1e6e4490fa78"), "Downtown Store" },
                    { new Guid("da2e55e3-aaee-439d-a3c3-672f50257454"), "Warehouse A" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName" },
                values: new object[,]
                {
                    { new Guid("214b5211-2aa1-45f2-9950-4c1dbdd5971c"), "Electronics" },
                    { new Guid("b6604e15-1b6d-4327-afe1-6540a9007236"), "Clothing" },
                    { new Guid("fba8f656-32d0-43d2-ae57-d68ec1401318"), "Home Appliances" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("49ebd0c1-0b4e-432d-90fb-573069e93439"), "Clothing" },
                    { new Guid("587053f9-cad8-4430-b209-b5d673752745"), "Toys" },
                    { new Guid("68994fbc-9520-49bf-824a-c6d24a2a72e8"), "Furniture" },
                    { new Guid("8eab0eb1-a24d-45c3-a5a0-79304b49ad3a"), "Food" },
                    { new Guid("ba016e86-15e1-4603-95e7-801fb4407d68"), "Electronics" }
                });

            migrationBuilder.InsertData(
                table: "SellingPriceTaxes",
                columns: new[] { "Id", "SellingPriceTaxName" },
                values: new object[,]
                {
                    { new Guid("07abf964-f91a-49dc-b3b7-6709b1b3351e"), "Inclusive" },
                    { new Guid("2a095072-f0c7-4fa9-b7c8-5116241074e2"), "Zero Rate" },
                    { new Guid("6f3c00bd-c84e-419a-adec-09a230b912de"), "Exclusive" }
                });

            migrationBuilder.InsertData(
                table: "Subcategories",
                columns: new[] { "Id", "SubcategoryName" },
                values: new object[,]
                {
                    { new Guid("0d509b78-7096-44fa-99d0-34b4331d782d"), "Laptops" },
                    { new Guid("4b87703d-0342-4be2-b3f0-35b0960f1995"), "Televisions" },
                    { new Guid("98e9975e-5783-4795-9232-3b79d0ece509"), "Smartphones" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "UnitName" },
                values: new object[,]
                {
                    { new Guid("3065da95-a6f1-40c2-b4cc-92dec1a4144f"), "Piece" },
                    { new Guid("472ff1a6-3318-4833-8e1d-d136b89f8db4"), "Liter" },
                    { new Guid("b2fa0d0e-00c4-4019-9275-2c810feca7da"), "Kilogram" }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "WarrantyDuration" },
                values: new object[,]
                {
                    { new Guid("856c4cdb-8a4f-474a-a04e-6c2634aeb6b7"), "1 Year" },
                    { new Guid("b9fcae96-d1cb-48a2-b3bd-9a844d8f5535"), "3 Years" },
                    { new Guid("ff963399-7289-4479-9ef1-fa11c303a575"), "2 Years" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("3b58746b-43d4-42ec-be04-f6c9dca3aaf6"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("68d01f25-69bc-4234-ac07-f2cbc390281e"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("f4d071ab-052f-47ad-84d9-0aa7ddee57b7"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("6233fda4-b367-4823-b956-5e1a42ec538b"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("86400884-029e-4c8b-ac57-257741b4feec"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("cd426fa1-5e1b-4e77-af03-db28d3fb8f46"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("2a0bea73-91b8-4326-ab8b-21bbb43a74b4"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("3b92c95a-dc7c-4289-8dd8-9a38b4fb72f1"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("5aae83c3-24f7-4bd0-b58c-56f3c4b5976a"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("776e90c0-9e53-41cf-8464-67512b512f62"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("7caa505e-77ec-4ce4-8a2d-1e6e4490fa78"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("da2e55e3-aaee-439d-a3c3-672f50257454"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("214b5211-2aa1-45f2-9950-4c1dbdd5971c"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b6604e15-1b6d-4327-afe1-6540a9007236"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("fba8f656-32d0-43d2-ae57-d68ec1401318"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("49ebd0c1-0b4e-432d-90fb-573069e93439"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("587053f9-cad8-4430-b209-b5d673752745"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("68994fbc-9520-49bf-824a-c6d24a2a72e8"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("8eab0eb1-a24d-45c3-a5a0-79304b49ad3a"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("ba016e86-15e1-4603-95e7-801fb4407d68"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("07abf964-f91a-49dc-b3b7-6709b1b3351e"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("2a095072-f0c7-4fa9-b7c8-5116241074e2"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("6f3c00bd-c84e-419a-adec-09a230b912de"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("0d509b78-7096-44fa-99d0-34b4331d782d"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("4b87703d-0342-4be2-b3f0-35b0960f1995"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("98e9975e-5783-4795-9232-3b79d0ece509"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("3065da95-a6f1-40c2-b4cc-92dec1a4144f"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("472ff1a6-3318-4833-8e1d-d136b89f8db4"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("b2fa0d0e-00c4-4019-9275-2c810feca7da"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("856c4cdb-8a4f-474a-a04e-6c2634aeb6b7"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("b9fcae96-d1cb-48a2-b3bd-9a844d8f5535"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("ff963399-7289-4479-9ef1-fa11c303a575"));

            migrationBuilder.RenameColumn(
                name: "Position",
                table: "Products",
                newName: "Possition");

            migrationBuilder.InsertData(
                table: "ApplicableTaxs",
                columns: new[] { "Id", "ApplicableTaxName" },
                values: new object[,]
                {
                    { new Guid("6e00fd96-244c-42a3-b4d4-885796c07e25"), "Fruits" },
                    { new Guid("b335f2af-e56f-473d-a112-24d23ca79727"), "Food" },
                    { new Guid("fc41c419-7b2f-4fea-8268-38c48ef5fabf"), "Sales Tax" }
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "BarcodeTypeName" },
                values: new object[,]
                {
                    { new Guid("3ae062cd-1d42-4b9f-ae05-55bd2f6782ef"), "QR Code" },
                    { new Guid("701239b9-7cfd-49d8-9406-b5ab4a47ecc5"), "UPC" },
                    { new Guid("9eae4bb4-eb42-46ba-afca-c009fe2b5528"), "NFC" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BrandName" },
                values: new object[,]
                {
                    { new Guid("27120563-2253-4d7e-b75f-9e54b232bc05"), "Samsung" },
                    { new Guid("5dc5a503-22d2-4965-bcc5-b5abcea1a722"), "Apple" },
                    { new Guid("e7910ef3-6d4c-4429-a11e-c7fbb7e2f791"), "Sony" }
                });

            migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "LocationName" },
                values: new object[,]
                {
                    { new Guid("cd55d169-d873-420e-bcad-017f120af5b5"), "Warehouse A" },
                    { new Guid("ce5734aa-f436-4b2f-8e57-2c26d142888a"), "Downtown Store" },
                    { new Guid("d6240d7a-aa5f-4fd2-a71f-ac488ff6af9f"), "Warehouse B" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName" },
                values: new object[,]
                {
                    { new Guid("0c4558a9-a18a-4a48-b110-007665045bc9"), "Home Appliances" },
                    { new Guid("ad041ff8-1e82-4545-a040-9174638dd33e"), "Clothing" },
                    { new Guid("c6e6b2f2-151e-45c0-9953-0dc52846aaf8"), "Electronics" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("04a40edd-b1b4-452d-8f2a-016e56fa094a"), "Clothing" },
                    { new Guid("2155827e-6efb-441a-a4c4-e5dbaf9ff60d"), "Electronics" },
                    { new Guid("2d8639f0-a3a5-47fd-a6e6-b4382960a5ba"), "Toys" },
                    { new Guid("68be5838-c9d8-495c-87ca-74b14bdefca1"), "Furniture" },
                    { new Guid("cf6d3168-274b-4a0f-a632-390a554883a3"), "Food" }
                });

            migrationBuilder.InsertData(
                table: "SellingPriceTaxes",
                columns: new[] { "Id", "SellingPriceTaxName" },
                values: new object[,]
                {
                    { new Guid("437e4c0f-25db-4a23-8e5c-06fae46ac27d"), "Inclusive" },
                    { new Guid("a470d2b1-18a1-494e-85bc-fbcf471aed34"), "Zero Rate" },
                    { new Guid("c9055759-235a-442c-8a58-9f4d2ae4ec1d"), "Exclusive" }
                });

            migrationBuilder.InsertData(
                table: "Subcategories",
                columns: new[] { "Id", "SubcategoryName" },
                values: new object[,]
                {
                    { new Guid("24bffabd-0b2f-40a4-aa38-88cf3f263c82"), "Laptops" },
                    { new Guid("c4c884a4-d036-4212-82f7-851ee1d393fc"), "Smartphones" },
                    { new Guid("dbbb55c0-dadf-444b-b530-f73b86fa9e68"), "Televisions" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "UnitName" },
                values: new object[,]
                {
                    { new Guid("1367c9ca-2c99-41ea-927a-052af2cced48"), "Piece" },
                    { new Guid("3ead9c40-4529-4e22-b3c2-941844e23e3f"), "Kilogram" },
                    { new Guid("e9cf6e2c-75c0-44f1-acab-8105ca05f640"), "Liter" }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "WarrantyDuration" },
                values: new object[,]
                {
                    { new Guid("271ca443-ccba-4cdb-84d0-8e29d0f1f46c"), "1 Year" },
                    { new Guid("e91d720b-898d-4bc9-96f5-87e75ee77762"), "3 Years" },
                    { new Guid("fc1f6799-9fd5-465b-9177-d1ca148f9951"), "2 Years" }
                });
        }
    }
}
