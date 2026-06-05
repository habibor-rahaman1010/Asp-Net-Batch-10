using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DevSkill.Inventory.Web.Migrations.InventoryDb
{
    /// <inheritdoc />
    public partial class UserActivityTracking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("0a6a6159-9974-4927-a43c-3ea8341d3daa"));

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("6f38e288-0b02-4e68-911a-ae934694cf2b"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("2cf6f6e2-6c9f-475e-aad7-52442b8c3ec3"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("659f2999-0db7-4bd0-ae82-6b38f92b071c"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("a434cafb-f465-4b0c-96e1-81d44603ecf3"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("5a8b5cef-b19e-450d-83bc-c46a1596f6d8"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("6999843f-0da9-47c0-9d16-7b5a6fa88f5f"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("8c2534fb-bc9a-4f09-9022-077cfa2ddad3"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("743856b5-f162-4ede-8bd4-73e8abe5cd9c"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("98ff8513-e1e0-4653-af6c-3d9eb8c30855"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("e6a7e925-3c1b-4393-8df6-ee4c711f573c"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("61a54343-50a1-447f-84ef-5317c3495969"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("7425d474-1a1d-4067-bcd3-feb3ebe78791"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("b3465ffa-44a3-4f4e-bffc-0ef66338be7f"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("10739c00-8267-4753-9c45-8542c1f12094"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("53c0a7e9-16fb-4803-8dfb-3fa5dfa00394"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("5c2f5f16-933b-4109-ad6c-bd11176a5e2e"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("3aa524ee-6c80-4ea3-99d8-54cbff9bc665"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("3fe17aed-4000-46d7-ac18-56061d237ac6"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("949a58eb-96f0-41b1-9811-5d4d852cf247"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("9c96094b-972d-4b60-a2da-2bb82d2bf35f"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("a9d3905f-def2-4ba6-8330-f10fe8b52f19"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("16d707b2-94c4-48eb-a864-60e3fd79cf74"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("37488668-ba0a-4550-8c51-ea806660a4a5"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("dad90976-363f-4758-a9d9-39643f690984"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("18fc1147-ba46-4333-a0ab-65fa2f32ea46"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("98372d66-f2b5-4d88-8f82-9a5ec7d26814"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("e946c61f-c4ff-4ddb-81b6-021f223d25e8"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("3ccc6a19-75ea-4657-a686-6c01c04ae155"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("93fc5d07-6779-4271-96e5-acd0c3b351e7"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("943c9da4-45ab-42a0-8f73-2ffae5e589e9"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("1daca30e-23cb-476f-9401-737bde36a98d"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("701d1505-4a0d-4e3b-ba7f-8d596a3f8978"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("e4c224f6-9515-4461-a0fd-4bb61d2cdfa6"));

            migrationBuilder.CreateTable(
                name: "UserActivities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PageVisited = table.Column<int>(type: "int", nullable: false),
                    ControllerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Browser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastActivityTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoginTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LogoutTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActionCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserActivities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserActivityLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ControllerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HttpMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Browser = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserActivityLogs", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AdjustmentTypes",
                columns: new[] { "Id", "AdjustmentTypeName", "Description" },
                values: new object[,]
                {
                    { new Guid("15debf80-fec9-4580-bcee-a32855d3e969"), "Normal", "" },
                    { new Guid("fe128d77-806b-420b-8b56-07afdf5896fc"), "Abnormal", "" }
                });

            migrationBuilder.InsertData(
                table: "ApplicableTaxs",
                columns: new[] { "Id", "ApplicableTaxName", "Description", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("8278ae62-7ada-428e-845b-4dc85b983df4"), "Food", "", 0m },
                    { new Guid("a584bc2c-9cc9-4591-849b-168604d0f7a4"), "Sales Tax", "", 0m },
                    { new Guid("a60ed174-ea04-40f3-9a2a-71ad13c01eea"), "Fruits", "", 0m }
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "BarcodeDescription", "BarcodeTypeCode", "BarcodeTypeName" },
                values: new object[,]
                {
                    { new Guid("499e6fd8-5846-4bc1-8c45-72fd73917aef"), "", "", "NFC" },
                    { new Guid("a8e1ae1c-a30c-412b-9415-321cea2a6663"), "", "", "UPC" },
                    { new Guid("fd5fa9e7-e522-4198-8d7e-4f9264bd2519"), "", "", "QR Code" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BandOrigin", "BrandName", "Description" },
                values: new object[,]
                {
                    { new Guid("0158d0bb-d526-42c4-accf-d9b87f3512de"), "", "Sony", "" },
                    { new Guid("2c2c3ed1-b21e-4af9-87d4-c69703c3f0da"), "", "Samsung", "" },
                    { new Guid("3d25c2a0-cd98-4b8c-9fe6-6cd0c7a28159"), "", "Apple", "" }
                });

            migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "Address", "City", "Country", "LocationName", "State", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("33f415a0-c5c2-4e1c-ae13-5744f5870637"), "", "", "", "Warehouse A", "", "" },
                    { new Guid("3e641efd-f889-4aee-b3d9-b9147af7991b"), "", "", "", "Warehouse B", "", "" },
                    { new Guid("6d1db533-7019-40f5-abce-420cb0b0b21c"), "", "", "", "Downtown Store", "", "" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryCode", "CategoryName", "Description" },
                values: new object[,]
                {
                    { new Guid("38c818ad-2fc4-49b6-96dc-8e467aed7135"), "", "Home Appliances", "" },
                    { new Guid("49623036-76dc-4826-a6c2-5d3bb21a5029"), "", "Clothing", "" },
                    { new Guid("8b75f574-a66d-46ff-a467-a23c89e7994b"), "", "Electronics", "" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Description", "ProductTypeCode", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("182d30b8-59fe-4274-a1d2-2914a995c2dd"), "", "", "Electronics" },
                    { new Guid("27c2a37f-c260-44f4-ac90-763599b65a56"), "", "", "Food" },
                    { new Guid("aa4ed8ed-bac7-4e59-8c12-5a53fc07eae8"), "", "", "Furniture" },
                    { new Guid("c59c430b-6c6c-48d0-bfa9-2397506357de"), "", "", "Toys" },
                    { new Guid("ddab800b-8daf-42bd-ae7e-f314519d8255"), "", "", "Clothing" }
                });

            migrationBuilder.InsertData(
                table: "SellingPriceTaxes",
                columns: new[] { "Id", "Description", "SellingPriceTaxName", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("7ba138a8-7ed1-4e31-a2ac-54154c532c40"), "", "Inclusive", 0m },
                    { new Guid("a88e347f-8fba-44ed-b8b0-8df3b301ffe2"), "", "Zero Rate", 0m },
                    { new Guid("e742260d-9dee-41db-80d7-bdc8f7b145d3"), "", "Exclusive", 0m }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryCode", "Description", "SubCategoryName" },
                values: new object[,]
                {
                    { new Guid("30cd2468-bf7e-4fcb-b455-883c628a5561"), "", "", "Laptops" },
                    { new Guid("ca244c8d-3869-41c5-b16f-cd631b552622"), "", "", "Televisions" },
                    { new Guid("d2e6c033-2b8e-4c66-935c-7b825502af55"), "", "", "Smartphones" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "AllowDecimal", "ShortName", "UnitName" },
                values: new object[,]
                {
                    { new Guid("51c23730-bd5f-46d2-8130-f8dbd353f4e5"), 0, "", "Kilogram" },
                    { new Guid("a531bad5-b9df-4c1f-b472-44f55cf52386"), 0, "", "Liter" },
                    { new Guid("de79a95c-ca31-44d9-9c03-69114c0c67b6"), 0, "", "Piece" }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "Description", "Name", "WarrantyDuration" },
                values: new object[,]
                {
                    { new Guid("32d4cc19-b652-487c-8bde-75fbdbc95d0c"), "", "", "2 Years" },
                    { new Guid("78640e14-80e0-4d82-9947-d41ca946cf0c"), "", "", "1 Year" },
                    { new Guid("eb30528c-6bc8-456d-ae09-237709752a12"), "", "", "3 Years" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserActivities");

            migrationBuilder.DropTable(
                name: "UserActivityLogs");

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("15debf80-fec9-4580-bcee-a32855d3e969"));

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("fe128d77-806b-420b-8b56-07afdf5896fc"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("8278ae62-7ada-428e-845b-4dc85b983df4"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("a584bc2c-9cc9-4591-849b-168604d0f7a4"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("a60ed174-ea04-40f3-9a2a-71ad13c01eea"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("499e6fd8-5846-4bc1-8c45-72fd73917aef"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("a8e1ae1c-a30c-412b-9415-321cea2a6663"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("fd5fa9e7-e522-4198-8d7e-4f9264bd2519"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("0158d0bb-d526-42c4-accf-d9b87f3512de"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("2c2c3ed1-b21e-4af9-87d4-c69703c3f0da"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("3d25c2a0-cd98-4b8c-9fe6-6cd0c7a28159"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("33f415a0-c5c2-4e1c-ae13-5744f5870637"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("3e641efd-f889-4aee-b3d9-b9147af7991b"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("6d1db533-7019-40f5-abce-420cb0b0b21c"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("38c818ad-2fc4-49b6-96dc-8e467aed7135"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("49623036-76dc-4826-a6c2-5d3bb21a5029"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("8b75f574-a66d-46ff-a467-a23c89e7994b"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("182d30b8-59fe-4274-a1d2-2914a995c2dd"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("27c2a37f-c260-44f4-ac90-763599b65a56"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("aa4ed8ed-bac7-4e59-8c12-5a53fc07eae8"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("c59c430b-6c6c-48d0-bfa9-2397506357de"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("ddab800b-8daf-42bd-ae7e-f314519d8255"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("7ba138a8-7ed1-4e31-a2ac-54154c532c40"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("a88e347f-8fba-44ed-b8b0-8df3b301ffe2"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("e742260d-9dee-41db-80d7-bdc8f7b145d3"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("30cd2468-bf7e-4fcb-b455-883c628a5561"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("ca244c8d-3869-41c5-b16f-cd631b552622"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("d2e6c033-2b8e-4c66-935c-7b825502af55"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("51c23730-bd5f-46d2-8130-f8dbd353f4e5"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("a531bad5-b9df-4c1f-b472-44f55cf52386"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("de79a95c-ca31-44d9-9c03-69114c0c67b6"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("32d4cc19-b652-487c-8bde-75fbdbc95d0c"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("78640e14-80e0-4d82-9947-d41ca946cf0c"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("eb30528c-6bc8-456d-ae09-237709752a12"));

            migrationBuilder.InsertData(
                table: "AdjustmentTypes",
                columns: new[] { "Id", "AdjustmentTypeName", "Description" },
                values: new object[,]
                {
                    { new Guid("0a6a6159-9974-4927-a43c-3ea8341d3daa"), "Normal", "" },
                    { new Guid("6f38e288-0b02-4e68-911a-ae934694cf2b"), "Abnormal", "" }
                });

            migrationBuilder.InsertData(
                table: "ApplicableTaxs",
                columns: new[] { "Id", "ApplicableTaxName", "Description", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("2cf6f6e2-6c9f-475e-aad7-52442b8c3ec3"), "Sales Tax", "", 0m },
                    { new Guid("659f2999-0db7-4bd0-ae82-6b38f92b071c"), "Fruits", "", 0m },
                    { new Guid("a434cafb-f465-4b0c-96e1-81d44603ecf3"), "Food", "", 0m }
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "BarcodeDescription", "BarcodeTypeCode", "BarcodeTypeName" },
                values: new object[,]
                {
                    { new Guid("5a8b5cef-b19e-450d-83bc-c46a1596f6d8"), "", "", "QR Code" },
                    { new Guid("6999843f-0da9-47c0-9d16-7b5a6fa88f5f"), "", "", "NFC" },
                    { new Guid("8c2534fb-bc9a-4f09-9022-077cfa2ddad3"), "", "", "UPC" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BandOrigin", "BrandName", "Description" },
                values: new object[,]
                {
                    { new Guid("743856b5-f162-4ede-8bd4-73e8abe5cd9c"), "", "Sony", "" },
                    { new Guid("98ff8513-e1e0-4653-af6c-3d9eb8c30855"), "", "Apple", "" },
                    { new Guid("e6a7e925-3c1b-4393-8df6-ee4c711f573c"), "", "Samsung", "" }
                });

            migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "Address", "City", "Country", "LocationName", "State", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("61a54343-50a1-447f-84ef-5317c3495969"), "", "", "", "Warehouse B", "", "" },
                    { new Guid("7425d474-1a1d-4067-bcd3-feb3ebe78791"), "", "", "", "Warehouse A", "", "" },
                    { new Guid("b3465ffa-44a3-4f4e-bffc-0ef66338be7f"), "", "", "", "Downtown Store", "", "" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryCode", "CategoryName", "Description" },
                values: new object[,]
                {
                    { new Guid("10739c00-8267-4753-9c45-8542c1f12094"), "", "Electronics", "" },
                    { new Guid("53c0a7e9-16fb-4803-8dfb-3fa5dfa00394"), "", "Clothing", "" },
                    { new Guid("5c2f5f16-933b-4109-ad6c-bd11176a5e2e"), "", "Home Appliances", "" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Description", "ProductTypeCode", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("3aa524ee-6c80-4ea3-99d8-54cbff9bc665"), "", "", "Food" },
                    { new Guid("3fe17aed-4000-46d7-ac18-56061d237ac6"), "", "", "Furniture" },
                    { new Guid("949a58eb-96f0-41b1-9811-5d4d852cf247"), "", "", "Toys" },
                    { new Guid("9c96094b-972d-4b60-a2da-2bb82d2bf35f"), "", "", "Clothing" },
                    { new Guid("a9d3905f-def2-4ba6-8330-f10fe8b52f19"), "", "", "Electronics" }
                });

            migrationBuilder.InsertData(
                table: "SellingPriceTaxes",
                columns: new[] { "Id", "Description", "SellingPriceTaxName", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("16d707b2-94c4-48eb-a864-60e3fd79cf74"), "", "Exclusive", 0m },
                    { new Guid("37488668-ba0a-4550-8c51-ea806660a4a5"), "", "Inclusive", 0m },
                    { new Guid("dad90976-363f-4758-a9d9-39643f690984"), "", "Zero Rate", 0m }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryCode", "Description", "SubCategoryName" },
                values: new object[,]
                {
                    { new Guid("18fc1147-ba46-4333-a0ab-65fa2f32ea46"), "", "", "Laptops" },
                    { new Guid("98372d66-f2b5-4d88-8f82-9a5ec7d26814"), "", "", "Smartphones" },
                    { new Guid("e946c61f-c4ff-4ddb-81b6-021f223d25e8"), "", "", "Televisions" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "AllowDecimal", "ShortName", "UnitName" },
                values: new object[,]
                {
                    { new Guid("3ccc6a19-75ea-4657-a686-6c01c04ae155"), 0, "", "Piece" },
                    { new Guid("93fc5d07-6779-4271-96e5-acd0c3b351e7"), 0, "", "Liter" },
                    { new Guid("943c9da4-45ab-42a0-8f73-2ffae5e589e9"), 0, "", "Kilogram" }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "Description", "Name", "WarrantyDuration" },
                values: new object[,]
                {
                    { new Guid("1daca30e-23cb-476f-9401-737bde36a98d"), "", "", "1 Year" },
                    { new Guid("701d1505-4a0d-4e3b-ba7f-8d596a3f8978"), "", "", "3 Years" },
                    { new Guid("e4c224f6-9515-4461-a0fd-4bb61d2cdfa6"), "", "", "2 Years" }
                });
        }
    }
}
