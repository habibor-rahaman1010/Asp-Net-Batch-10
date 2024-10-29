using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DevSkill.Inventory.Web.Migrations.InventoryDb
{
    /// <inheritdoc />
    public partial class MyAllTableUpdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ApplicableTaxs_ApplicableTaxId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_BusinessLocations_BusinessLocationId",
                table: "Products");

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
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("004edaed-73fe-49e7-a5d4-47c8e9293526"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("475840b1-5e89-495c-9ac1-4a36461238e8"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
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

            migrationBuilder.InsertData(
                table: "AdjustmentTypes",
                columns: new[] { "Id", "AdjustmentTypeName", "Description" },
                values: new object[,]
                {
                    { new Guid("a7131d2d-b1f7-44b9-9420-39a338d298c8"), "Abnormal", "" },
                    { new Guid("f645ab5f-ea09-4f60-bc89-968a0a9c18f0"), "Normal", "" }
                });

            migrationBuilder.InsertData(
                table: "ApplicableTaxs",
                columns: new[] { "Id", "ApplicableTaxName" },
                values: new object[,]
                {
                    { new Guid("489f4ea8-dbfa-4b16-842d-eebd12329ea7"), "Sales Tax" },
                    { new Guid("521bcaa5-9942-4ec4-b463-5520116a5c5d"), "Food" },
                    { new Guid("74dedfb2-1745-48f1-bbb5-ab84f5d7ad13"), "Fruits" }
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "BarcodeTypeName" },
                values: new object[,]
                {
                    { new Guid("0cfb8134-7908-49a4-88bc-7b7f957b106d"), "NFC" },
                    { new Guid("82e74bd5-d873-4a29-9d3d-6e13f65e7141"), "QR Code" },
                    { new Guid("c58f00a1-c87d-434b-a93b-f0dc932e8a12"), "UPC" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BandOrigin", "BrandName", "Description" },
                values: new object[,]
                {
                    { new Guid("51073944-a59a-460a-9ffb-363c3ff70ac3"), "", "Samsung", "" },
                    { new Guid("d67bfa8b-6bf0-4550-94de-9ebcc2908183"), "", "Sony", "" },
                    { new Guid("e49dfaa9-98bd-4e39-991b-911fe1348957"), "", "Apple", "" }
                });

            migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "Address", "City", "Country", "LocationName", "State", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("0cc0453b-6389-404f-976b-d9f644db948e"), "", "", "", "Warehouse A", "", "" },
                    { new Guid("6a2f127e-99a6-4360-84be-60d24d0d3125"), "", "", "", "Downtown Store", "", "" },
                    { new Guid("cf623c49-cfb6-464a-aeea-aad9a29a702d"), "", "", "", "Warehouse B", "", "" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryCode", "CategoryName", "Description" },
                values: new object[,]
                {
                    { new Guid("4e58d95a-61a5-46ca-b94a-8a086c34cdb0"), "", "Electronics", "" },
                    { new Guid("75071d11-5108-40c0-a2bd-0e80460f5a38"), "", "Clothing", "" },
                    { new Guid("fba2b3af-02d8-4499-aee5-3dcbb63f7f60"), "", "Home Appliances", "" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("430fa059-3c9d-4d96-83a7-c75279de2987"), "Clothing" },
                    { new Guid("507f69eb-e069-4882-9fa2-7422bc0e7a4e"), "Food" },
                    { new Guid("87f3c54c-008d-4dbd-a82d-9d9d3c201d1d"), "Toys" },
                    { new Guid("94133dd6-6923-44ca-bdef-0a3a2e930371"), "Furniture" },
                    { new Guid("b264f331-a34b-4a2e-8714-f937dada8e82"), "Electronics" }
                });

            migrationBuilder.InsertData(
                table: "SellingPriceTaxes",
                columns: new[] { "Id", "SellingPriceTaxName" },
                values: new object[,]
                {
                    { new Guid("02bbf818-a36d-4232-b5d1-76f525993437"), "Inclusive" },
                    { new Guid("27c8b773-0fd4-440c-b45d-91e70283965a"), "Zero Rate" },
                    { new Guid("9b0e1c2e-fdde-4ca0-bbfd-deeb877e9efb"), "Exclusive" }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "SubcategoryName" },
                values: new object[,]
                {
                    { new Guid("1e5c564b-45fb-4c5c-a85f-f293e09a8d3c"), "Televisions" },
                    { new Guid("411b1aad-8cb6-4c7d-9311-96d2c27a2627"), "Smartphones" },
                    { new Guid("777bb31d-d0fd-4c51-bee1-594daa91458b"), "Laptops" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "AllowDecimal", "ShortName", "UnitName" },
                values: new object[,]
                {
                    { new Guid("78b51511-8ea1-47e6-9d9e-3529907392ff"), 0, "", "Piece" },
                    { new Guid("7dcdc71d-b130-452b-9a03-9e7b63ace9ab"), 0, "", "Liter" },
                    { new Guid("eb9c10f0-7b68-4bdd-9952-efe2ca9afa4f"), 0, "", "Kilogram" }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "Description", "Name", "WarrantyDuration" },
                values: new object[,]
                {
                    { new Guid("20fcd225-cb0d-40a7-bf01-a9c2123656ae"), "", "", "2 Years" },
                    { new Guid("7713531b-8b20-4ed4-b1b7-176b613d6abe"), "", "", "1 Year" },
                    { new Guid("d9b2dd31-2c03-47a6-adf3-1ee82380666b"), "", "", "3 Years" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ApplicableTaxs_ApplicableTaxId",
                table: "Products",
                column: "ApplicableTaxId",
                principalTable: "ApplicableTaxs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_BusinessLocations_BusinessLocationId",
                table: "Products",
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
                name: "FK_Products_BusinessLocations_BusinessLocationId",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("a7131d2d-b1f7-44b9-9420-39a338d298c8"));

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("f645ab5f-ea09-4f60-bc89-968a0a9c18f0"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("489f4ea8-dbfa-4b16-842d-eebd12329ea7"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("521bcaa5-9942-4ec4-b463-5520116a5c5d"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("74dedfb2-1745-48f1-bbb5-ab84f5d7ad13"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("0cfb8134-7908-49a4-88bc-7b7f957b106d"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("82e74bd5-d873-4a29-9d3d-6e13f65e7141"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("c58f00a1-c87d-434b-a93b-f0dc932e8a12"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("51073944-a59a-460a-9ffb-363c3ff70ac3"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("d67bfa8b-6bf0-4550-94de-9ebcc2908183"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("e49dfaa9-98bd-4e39-991b-911fe1348957"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("0cc0453b-6389-404f-976b-d9f644db948e"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("6a2f127e-99a6-4360-84be-60d24d0d3125"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("cf623c49-cfb6-464a-aeea-aad9a29a702d"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("4e58d95a-61a5-46ca-b94a-8a086c34cdb0"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("75071d11-5108-40c0-a2bd-0e80460f5a38"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("fba2b3af-02d8-4499-aee5-3dcbb63f7f60"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("430fa059-3c9d-4d96-83a7-c75279de2987"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("507f69eb-e069-4882-9fa2-7422bc0e7a4e"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("87f3c54c-008d-4dbd-a82d-9d9d3c201d1d"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("94133dd6-6923-44ca-bdef-0a3a2e930371"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("b264f331-a34b-4a2e-8714-f937dada8e82"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("02bbf818-a36d-4232-b5d1-76f525993437"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("27c8b773-0fd4-440c-b45d-91e70283965a"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("9b0e1c2e-fdde-4ca0-bbfd-deeb877e9efb"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("1e5c564b-45fb-4c5c-a85f-f293e09a8d3c"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("411b1aad-8cb6-4c7d-9311-96d2c27a2627"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("777bb31d-d0fd-4c51-bee1-594daa91458b"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("78b51511-8ea1-47e6-9d9e-3529907392ff"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("7dcdc71d-b130-452b-9a03-9e7b63ace9ab"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("eb9c10f0-7b68-4bdd-9952-efe2ca9afa4f"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("20fcd225-cb0d-40a7-bf01-a9c2123656ae"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("7713531b-8b20-4ed4-b1b7-176b613d6abe"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("d9b2dd31-2c03-47a6-adf3-1ee82380666b"));

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
                table: "SubCategories",
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
                name: "FK_Products_BusinessLocations_BusinessLocationId",
                table: "Products",
                column: "BusinessLocationId",
                principalTable: "BusinessLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
