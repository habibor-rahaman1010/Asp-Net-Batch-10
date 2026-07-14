using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DevSkill.Inventory.Web.Migrations.InventoryDb
{
    /// <inheritdoc />
    public partial class updateStockAdjustment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockAdjustments_Products_ProductId",
                table: "StockAdjustments");

            migrationBuilder.DropIndex(
                name: "IX_StockAdjustments_ProductId",
                table: "StockAdjustments");

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("9bdd4f6d-eefd-4d7e-b8a9-2c28e6e21441"));

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("d7e491cb-e8bd-4bd5-84cd-cd3fa580f17b"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("7abad6bf-826c-4d7b-9933-c806cdba7c66"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("7e1dacd6-d128-485d-9668-b500965ba366"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("c068f13b-b831-4f9e-948e-08d31b400050"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("66011cbb-7de8-4baa-a347-5be00b6178bf"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("91b50023-2ee1-4bb8-9d40-26769c205005"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("b886eb72-f495-471e-8a32-c8d80721e938"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("0f520da9-1196-4e97-a82a-71c1a036037a"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("414e69a9-9e11-4cc5-b65f-7b7a3ee7bb52"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("cdd913cc-40b8-4c18-95ea-a0dce3e9e32e"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("3ff68f26-ece4-4af0-8403-a79ec36c2ef1"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("abafd160-1078-41e2-84f3-d3e2a763355b"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("cf20deba-99b9-4dc9-9990-4ca16048dab9"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("50c25fe0-3b61-49cf-abc0-f7bd14a7d637"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b9346764-a829-4f0c-9cdc-d69d138e36de"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("e4ce9f5a-2c6b-4fef-a25f-ea072bb86036"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("175c061c-a624-4ab3-b4bb-d4b78c3c666a"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("1e8562ba-bca7-47ac-b1b2-df08c8c5046c"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("92e08a38-a365-454e-8f2c-3501d15ac709"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("ce9fc1a2-b7be-4fd2-8f9b-560898dd0704"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("e9191739-2ffe-4d94-9c0f-163db8d4857e"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("282df0e8-a72d-4785-9c42-6da70330e156"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("c8ee138a-9c03-41e3-abae-3d771961d6f9"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("e2de71cb-267b-4214-ab2d-c7646c2c5acd"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("26f09b3e-4b0e-4a1e-affc-af401877c6e3"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("422bb16f-c2e1-4417-b655-75a96fa4eda3"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("d58bebe8-c2e2-4ff9-a64f-486a4991ad3f"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("28863918-0fd0-45a4-ae64-37dd99a64506"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("8caf1fe0-5a5f-4817-ab5f-248adbf774b8"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("f3439779-9b8b-4dc3-a2ac-5cff2acb86b3"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("1d0591a3-1492-4d3c-a57b-481852c6c41b"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("1ee65c73-2cf3-418d-82b5-967d96f68517"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("70a84413-b924-4a04-ab76-cf51e021d1f4"));

            migrationBuilder.DropColumn(
                name: "AdjustmentQuantity",
                table: "StockAdjustments");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "StockAdjustments");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "StockAdjustments");

            migrationBuilder.CreateTable(
                name: "StockAdjustmentItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StockAdjustmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdjustmentQuantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockAdjustmentItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockAdjustmentItem_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockAdjustmentItem_StockAdjustments_StockAdjustmentId",
                        column: x => x.StockAdjustmentId,
                        principalTable: "StockAdjustments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AdjustmentTypes",
                columns: new[] { "Id", "AdjustmentTypeName", "Description", "Sign" },
                values: new object[,]
                {
                    { new Guid("197900f9-cf1d-4360-853d-06ca60008c3d"), "Abnormal", "", 0 },
                    { new Guid("386e962a-a29a-400e-b884-16392b839ecb"), "Normal", "", 0 }
                });

            migrationBuilder.InsertData(
                table: "ApplicableTaxs",
                columns: new[] { "Id", "ApplicableTaxName", "Description", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("333228c5-662a-4175-a4da-d413b01a0f09"), "Fruits", "", 0m },
                    { new Guid("9ceeb1e5-c98f-4e96-8185-d397777598df"), "Food", "", 0m },
                    { new Guid("b094b23c-c2d0-4de3-a607-a7dbb903f96a"), "Sales Tax", "", 0m }
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "BarcodeDescription", "BarcodeTypeCode", "BarcodeTypeName" },
                values: new object[,]
                {
                    { new Guid("9125fd9e-076b-4a7c-a160-480d0b910dc5"), "", "", "QR Code" },
                    { new Guid("be790ab1-bce5-4da8-bb63-3528dffd7163"), "", "", "NFC" },
                    { new Guid("d867d922-e2dc-41af-a8c3-df00342c1b6b"), "", "", "UPC" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BandOrigin", "BrandName", "Description" },
                values: new object[,]
                {
                    { new Guid("047ef026-fc67-406a-addf-024c0a56503a"), "", "Apple", "" },
                    { new Guid("62f50ffa-b6f9-42a2-9c46-0991e7b92cb7"), "", "Sony", "" },
                    { new Guid("e5fd942d-4822-48ab-a13f-b604b82ba78a"), "", "Samsung", "" }
                });

            migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "Address", "City", "Country", "LocationName", "State", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("38b0b0fb-1f3f-4316-94df-de3f43de913d"), "", "", "", "Warehouse A", "", "" },
                    { new Guid("8daaec4d-c5db-4466-b2fc-ad7d4eb3833e"), "", "", "", "Downtown Store", "", "" },
                    { new Guid("e7fb16b3-9732-4d38-aa77-1dbba7ba7c12"), "", "", "", "Warehouse B", "", "" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryCode", "CategoryName", "Description" },
                values: new object[,]
                {
                    { new Guid("22f83f46-055c-4dc6-8f42-d68c59b35290"), "", "Electronics", "" },
                    { new Guid("c4fa5ba1-170b-4ade-a979-40457b11279f"), "", "Home Appliances", "" },
                    { new Guid("f1e9bff5-9aaa-407f-8286-09a0627ee82b"), "", "Clothing", "" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Description", "ProductTypeCode", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("19dbec03-132d-4a80-8f37-2d03ac4ddf9c"), "", "", "Clothing" },
                    { new Guid("3fbd1f8e-3591-43e3-9dc5-651c80bb15cd"), "", "", "Food" },
                    { new Guid("529a7c4a-4160-45f0-a0bb-d71b56e3fd5f"), "", "", "Electronics" },
                    { new Guid("5c06f4dc-0a1e-4938-8ed8-3eb8693c3b43"), "", "", "Toys" },
                    { new Guid("bcbe84c8-0ca1-4d04-a1c3-f642c8222c92"), "", "", "Furniture" }
                });

            migrationBuilder.InsertData(
                table: "SellingPriceTaxes",
                columns: new[] { "Id", "Description", "SellingPriceTaxName", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("2cd4b077-f8c3-4b06-a5c5-4220b1688820"), "", "Exclusive", 0m },
                    { new Guid("7f8fa9ea-e6be-4eaa-9908-8debd8f7d28b"), "", "Zero Rate", 0m },
                    { new Guid("806a05f1-3743-43d9-9e80-c287733c2aa9"), "", "Inclusive", 0m }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryCode", "Description", "SubCategoryName" },
                values: new object[,]
                {
                    { new Guid("55cb826f-d45f-4d77-be87-5d94d0e8b925"), "", "", "Smartphones" },
                    { new Guid("77dac59b-c7fc-40c8-928b-162e71a030e2"), "", "", "Laptops" },
                    { new Guid("fbaaa515-871c-469e-a034-7d903e438a70"), "", "", "Televisions" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "AllowDecimal", "ShortName", "UnitName" },
                values: new object[,]
                {
                    { new Guid("03919421-bb7c-4687-acfe-deeb4fc4b4c3"), 0, "", "Liter" },
                    { new Guid("c6189ea2-eafd-44ee-a1eb-3efe8421ef17"), 0, "", "Kilogram" },
                    { new Guid("e5fc0748-09f3-4bdc-ab1d-8a5e74dd63f0"), 0, "", "Piece" }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "Description", "Name", "WarrantyDuration" },
                values: new object[,]
                {
                    { new Guid("41d508cc-d00f-40ad-8f54-96fa229cf272"), "", "", "2 Years" },
                    { new Guid("994e41c0-8a2d-43b4-bd73-e42d01a3ebd6"), "", "", "3 Years" },
                    { new Guid("f4a80656-890a-4ea3-be13-6c0674a36e67"), "", "", "1 Year" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockTransfers_FromWarehouseId",
                table: "StockTransfers",
                column: "FromWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransfers_ToWarehouseId",
                table: "StockTransfers",
                column: "ToWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_StockAdjustmentItem_ProductId",
                table: "StockAdjustmentItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StockAdjustmentItem_StockAdjustmentId",
                table: "StockAdjustmentItem",
                column: "StockAdjustmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransfers_BusinessLocations_FromWarehouseId",
                table: "StockTransfers",
                column: "FromWarehouseId",
                principalTable: "BusinessLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransfers_BusinessLocations_ToWarehouseId",
                table: "StockTransfers",
                column: "ToWarehouseId",
                principalTable: "BusinessLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockTransfers_BusinessLocations_FromWarehouseId",
                table: "StockTransfers");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransfers_BusinessLocations_ToWarehouseId",
                table: "StockTransfers");

            migrationBuilder.DropTable(
                name: "StockAdjustmentItem");

            migrationBuilder.DropIndex(
                name: "IX_StockTransfers_FromWarehouseId",
                table: "StockTransfers");

            migrationBuilder.DropIndex(
                name: "IX_StockTransfers_ToWarehouseId",
                table: "StockTransfers");

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("197900f9-cf1d-4360-853d-06ca60008c3d"));

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("386e962a-a29a-400e-b884-16392b839ecb"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("333228c5-662a-4175-a4da-d413b01a0f09"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("9ceeb1e5-c98f-4e96-8185-d397777598df"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("b094b23c-c2d0-4de3-a607-a7dbb903f96a"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("9125fd9e-076b-4a7c-a160-480d0b910dc5"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("be790ab1-bce5-4da8-bb63-3528dffd7163"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("d867d922-e2dc-41af-a8c3-df00342c1b6b"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("047ef026-fc67-406a-addf-024c0a56503a"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("62f50ffa-b6f9-42a2-9c46-0991e7b92cb7"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("e5fd942d-4822-48ab-a13f-b604b82ba78a"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("38b0b0fb-1f3f-4316-94df-de3f43de913d"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("8daaec4d-c5db-4466-b2fc-ad7d4eb3833e"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("e7fb16b3-9732-4d38-aa77-1dbba7ba7c12"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22f83f46-055c-4dc6-8f42-d68c59b35290"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c4fa5ba1-170b-4ade-a979-40457b11279f"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("f1e9bff5-9aaa-407f-8286-09a0627ee82b"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("19dbec03-132d-4a80-8f37-2d03ac4ddf9c"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("3fbd1f8e-3591-43e3-9dc5-651c80bb15cd"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("529a7c4a-4160-45f0-a0bb-d71b56e3fd5f"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("5c06f4dc-0a1e-4938-8ed8-3eb8693c3b43"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("bcbe84c8-0ca1-4d04-a1c3-f642c8222c92"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("2cd4b077-f8c3-4b06-a5c5-4220b1688820"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("7f8fa9ea-e6be-4eaa-9908-8debd8f7d28b"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("806a05f1-3743-43d9-9e80-c287733c2aa9"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("55cb826f-d45f-4d77-be87-5d94d0e8b925"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("77dac59b-c7fc-40c8-928b-162e71a030e2"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("fbaaa515-871c-469e-a034-7d903e438a70"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("03919421-bb7c-4687-acfe-deeb4fc4b4c3"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("c6189ea2-eafd-44ee-a1eb-3efe8421ef17"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("e5fc0748-09f3-4bdc-ab1d-8a5e74dd63f0"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("41d508cc-d00f-40ad-8f54-96fa229cf272"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("994e41c0-8a2d-43b4-bd73-e42d01a3ebd6"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("f4a80656-890a-4ea3-be13-6c0674a36e67"));

            migrationBuilder.AddColumn<int>(
                name: "AdjustmentQuantity",
                table: "StockAdjustments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "StockAdjustments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "StockAdjustments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "AdjustmentTypes",
                columns: new[] { "Id", "AdjustmentTypeName", "Description", "Sign" },
                values: new object[,]
                {
                    { new Guid("9bdd4f6d-eefd-4d7e-b8a9-2c28e6e21441"), "Abnormal", "", 0 },
                    { new Guid("d7e491cb-e8bd-4bd5-84cd-cd3fa580f17b"), "Normal", "", 0 }
                });

            migrationBuilder.InsertData(
                table: "ApplicableTaxs",
                columns: new[] { "Id", "ApplicableTaxName", "Description", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("7abad6bf-826c-4d7b-9933-c806cdba7c66"), "Food", "", 0m },
                    { new Guid("7e1dacd6-d128-485d-9668-b500965ba366"), "Fruits", "", 0m },
                    { new Guid("c068f13b-b831-4f9e-948e-08d31b400050"), "Sales Tax", "", 0m }
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "BarcodeDescription", "BarcodeTypeCode", "BarcodeTypeName" },
                values: new object[,]
                {
                    { new Guid("66011cbb-7de8-4baa-a347-5be00b6178bf"), "", "", "UPC" },
                    { new Guid("91b50023-2ee1-4bb8-9d40-26769c205005"), "", "", "NFC" },
                    { new Guid("b886eb72-f495-471e-8a32-c8d80721e938"), "", "", "QR Code" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BandOrigin", "BrandName", "Description" },
                values: new object[,]
                {
                    { new Guid("0f520da9-1196-4e97-a82a-71c1a036037a"), "", "Apple", "" },
                    { new Guid("414e69a9-9e11-4cc5-b65f-7b7a3ee7bb52"), "", "Sony", "" },
                    { new Guid("cdd913cc-40b8-4c18-95ea-a0dce3e9e32e"), "", "Samsung", "" }
                });

            migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "Address", "City", "Country", "LocationName", "State", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("3ff68f26-ece4-4af0-8403-a79ec36c2ef1"), "", "", "", "Downtown Store", "", "" },
                    { new Guid("abafd160-1078-41e2-84f3-d3e2a763355b"), "", "", "", "Warehouse B", "", "" },
                    { new Guid("cf20deba-99b9-4dc9-9990-4ca16048dab9"), "", "", "", "Warehouse A", "", "" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryCode", "CategoryName", "Description" },
                values: new object[,]
                {
                    { new Guid("50c25fe0-3b61-49cf-abc0-f7bd14a7d637"), "", "Electronics", "" },
                    { new Guid("b9346764-a829-4f0c-9cdc-d69d138e36de"), "", "Home Appliances", "" },
                    { new Guid("e4ce9f5a-2c6b-4fef-a25f-ea072bb86036"), "", "Clothing", "" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Description", "ProductTypeCode", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("175c061c-a624-4ab3-b4bb-d4b78c3c666a"), "", "", "Toys" },
                    { new Guid("1e8562ba-bca7-47ac-b1b2-df08c8c5046c"), "", "", "Furniture" },
                    { new Guid("92e08a38-a365-454e-8f2c-3501d15ac709"), "", "", "Electronics" },
                    { new Guid("ce9fc1a2-b7be-4fd2-8f9b-560898dd0704"), "", "", "Food" },
                    { new Guid("e9191739-2ffe-4d94-9c0f-163db8d4857e"), "", "", "Clothing" }
                });

            migrationBuilder.InsertData(
                table: "SellingPriceTaxes",
                columns: new[] { "Id", "Description", "SellingPriceTaxName", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("282df0e8-a72d-4785-9c42-6da70330e156"), "", "Zero Rate", 0m },
                    { new Guid("c8ee138a-9c03-41e3-abae-3d771961d6f9"), "", "Inclusive", 0m },
                    { new Guid("e2de71cb-267b-4214-ab2d-c7646c2c5acd"), "", "Exclusive", 0m }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryCode", "Description", "SubCategoryName" },
                values: new object[,]
                {
                    { new Guid("26f09b3e-4b0e-4a1e-affc-af401877c6e3"), "", "", "Laptops" },
                    { new Guid("422bb16f-c2e1-4417-b655-75a96fa4eda3"), "", "", "Smartphones" },
                    { new Guid("d58bebe8-c2e2-4ff9-a64f-486a4991ad3f"), "", "", "Televisions" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "AllowDecimal", "ShortName", "UnitName" },
                values: new object[,]
                {
                    { new Guid("28863918-0fd0-45a4-ae64-37dd99a64506"), 0, "", "Liter" },
                    { new Guid("8caf1fe0-5a5f-4817-ab5f-248adbf774b8"), 0, "", "Kilogram" },
                    { new Guid("f3439779-9b8b-4dc3-a2ac-5cff2acb86b3"), 0, "", "Piece" }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "Description", "Name", "WarrantyDuration" },
                values: new object[,]
                {
                    { new Guid("1d0591a3-1492-4d3c-a57b-481852c6c41b"), "", "", "3 Years" },
                    { new Guid("1ee65c73-2cf3-418d-82b5-967d96f68517"), "", "", "2 Years" },
                    { new Guid("70a84413-b924-4a04-ab76-cf51e021d1f4"), "", "", "1 Year" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockAdjustments_ProductId",
                table: "StockAdjustments",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockAdjustments_Products_ProductId",
                table: "StockAdjustments",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
