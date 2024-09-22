using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DevSkill.Inventory.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductMode3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ApplicableTaxs_AvailableTaxId",
                table: "Products");

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
                name: "AvailableTaxId",
                table: "Products",
                newName: "ApplicableTaxId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_AvailableTaxId",
                table: "Products",
                newName: "IX_Products_ApplicableTaxId");

            migrationBuilder.InsertData(
                table: "ApplicableTaxs",
                columns: new[] { "Id", "ApplicableTaxName" },
                values: new object[,]
                {
                    { new Guid("2105a1b8-ab67-4a24-9e0e-0b4383bc8817"), "Food" },
                    { new Guid("9bc89f4a-ee38-473f-86a6-013bce11f2d0"), "Fruits" },
                    { new Guid("c6233f6b-b783-4be6-8e34-1fad3593a798"), "Sales Tax" }
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "BarcodeTypeName" },
                values: new object[,]
                {
                    { new Guid("3b8c5d58-6f53-4d70-b84f-8f8a7dc243c7"), "UPC" },
                    { new Guid("9d1c2d95-9b9d-4ad2-81ab-98bdf62d21a6"), "QR Code" },
                    { new Guid("f605f2eb-5a01-4ab1-af4f-08463f0266dc"), "NFC" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BrandName" },
                values: new object[,]
                {
                    { new Guid("782811b6-5699-486d-95ee-9d2087451277"), "Sony" },
                    { new Guid("9e1c5741-db03-4515-8d56-ebdf26cd468b"), "Apple" },
                    { new Guid("b317b48a-1306-464b-afc6-ed24cec04d0b"), "Samsung" }
                });

            migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "LocationName" },
                values: new object[,]
                {
                    { new Guid("33ccc352-dc69-42a6-9b7f-7bf5a37fb71a"), "Downtown Store" },
                    { new Guid("37dd66ac-8f84-4b3f-bad6-e533ab692167"), "Warehouse A" },
                    { new Guid("acf8b94f-86fa-4fc9-a04d-61cf1ad9d12a"), "Warehouse B" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName" },
                values: new object[,]
                {
                    { new Guid("168d6a57-6b21-492e-ac94-2495f308e8a8"), "Home Appliances" },
                    { new Guid("7fa6a871-6849-4437-939a-a01d7ebe78ee"), "Electronics" },
                    { new Guid("d0ea4ab8-e322-46bd-b6b2-fcccff76cb66"), "Clothing" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("08ae621a-83df-47ff-9ca8-4224ac795158"), "Electronics" },
                    { new Guid("4309fc0b-3072-4414-8627-5dfed3f327bb"), "Furniture" },
                    { new Guid("5f0c98bf-d034-47e6-b4d0-e871d21e5b8b"), "Clothing" },
                    { new Guid("6d8d0d81-14d3-444c-85b9-ffd3e087a0d9"), "Toys" },
                    { new Guid("b5c9275c-71b1-4bc9-8193-d7d8883bc5b8"), "Food" }
                });

            migrationBuilder.InsertData(
                table: "SellingPriceTaxes",
                columns: new[] { "Id", "SellingPriceTaxName" },
                values: new object[,]
                {
                    { new Guid("0d1f42d2-1cec-4c85-86e5-954a830c9a49"), "Zero Rate" },
                    { new Guid("716fb51f-a010-4d49-b8dd-20126d8b970f"), "Inclusive" },
                    { new Guid("e2015eed-1c56-42d3-8a10-19d0b2341d3a"), "Exclusive" }
                });

            migrationBuilder.InsertData(
                table: "Subcategories",
                columns: new[] { "Id", "SubcategoryName" },
                values: new object[,]
                {
                    { new Guid("4275a765-1fb8-4644-aef1-647b691ad839"), "Televisions" },
                    { new Guid("7158d36f-c11a-451d-93f1-257e74a9c0a3"), "Smartphones" },
                    { new Guid("d86a8736-d6cb-4ffd-9fb3-9a8a5a8ec7ba"), "Laptops" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "UnitName" },
                values: new object[,]
                {
                    { new Guid("1a5249a4-dbf3-4542-b3ae-dbb2f2021dd5"), "Liter" },
                    { new Guid("88c2dea3-05ee-45b8-b5a5-db635467ae63"), "Kilogram" },
                    { new Guid("92e57000-5b1c-45b2-b7e7-9e8abbb5ea16"), "Piece" }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "WarrantyDuration" },
                values: new object[,]
                {
                    { new Guid("2b9e9477-2f07-442b-b679-207e656f1b0a"), "2 Years" },
                    { new Guid("6afec003-b201-485a-aa71-07ea70d0d34b"), "3 Years" },
                    { new Guid("cc892fec-dd17-4db8-98e1-cff29935320a"), "1 Year" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ApplicableTaxs_ApplicableTaxId",
                table: "Products",
                column: "ApplicableTaxId",
                principalTable: "ApplicableTaxs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ApplicableTaxs_ApplicableTaxId",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("2105a1b8-ab67-4a24-9e0e-0b4383bc8817"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("9bc89f4a-ee38-473f-86a6-013bce11f2d0"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("c6233f6b-b783-4be6-8e34-1fad3593a798"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("3b8c5d58-6f53-4d70-b84f-8f8a7dc243c7"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("9d1c2d95-9b9d-4ad2-81ab-98bdf62d21a6"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("f605f2eb-5a01-4ab1-af4f-08463f0266dc"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("782811b6-5699-486d-95ee-9d2087451277"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("9e1c5741-db03-4515-8d56-ebdf26cd468b"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("b317b48a-1306-464b-afc6-ed24cec04d0b"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("33ccc352-dc69-42a6-9b7f-7bf5a37fb71a"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("37dd66ac-8f84-4b3f-bad6-e533ab692167"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("acf8b94f-86fa-4fc9-a04d-61cf1ad9d12a"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("168d6a57-6b21-492e-ac94-2495f308e8a8"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("7fa6a871-6849-4437-939a-a01d7ebe78ee"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d0ea4ab8-e322-46bd-b6b2-fcccff76cb66"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("08ae621a-83df-47ff-9ca8-4224ac795158"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("4309fc0b-3072-4414-8627-5dfed3f327bb"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("5f0c98bf-d034-47e6-b4d0-e871d21e5b8b"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("6d8d0d81-14d3-444c-85b9-ffd3e087a0d9"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("b5c9275c-71b1-4bc9-8193-d7d8883bc5b8"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("0d1f42d2-1cec-4c85-86e5-954a830c9a49"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("716fb51f-a010-4d49-b8dd-20126d8b970f"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("e2015eed-1c56-42d3-8a10-19d0b2341d3a"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("4275a765-1fb8-4644-aef1-647b691ad839"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("7158d36f-c11a-451d-93f1-257e74a9c0a3"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("d86a8736-d6cb-4ffd-9fb3-9a8a5a8ec7ba"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("1a5249a4-dbf3-4542-b3ae-dbb2f2021dd5"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("88c2dea3-05ee-45b8-b5a5-db635467ae63"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("92e57000-5b1c-45b2-b7e7-9e8abbb5ea16"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("2b9e9477-2f07-442b-b679-207e656f1b0a"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("6afec003-b201-485a-aa71-07ea70d0d34b"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("cc892fec-dd17-4db8-98e1-cff29935320a"));

            migrationBuilder.RenameColumn(
                name: "ApplicableTaxId",
                table: "Products",
                newName: "AvailableTaxId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ApplicableTaxId",
                table: "Products",
                newName: "IX_Products_AvailableTaxId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ApplicableTaxs_AvailableTaxId",
                table: "Products",
                column: "AvailableTaxId",
                principalTable: "ApplicableTaxs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
