using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DevSkill.Inventory.Web.Migrations.InventoryDb
{
    /// <inheritdoc />
    public partial class SalesMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockAdjustmentItems_StockAdjustments_StockAdjustmentId",
                table: "StockAdjustmentItems");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransferItems_StockTransfers_StockTransferId",
                table: "StockTransferItems");

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("0a9fdb59-8976-4470-b399-91ff3aab9ba5"));

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("f02d91a8-a485-4d72-9ab5-603dc6d8a15a"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("25f90c9e-8075-40a4-a7c6-1ef73d6f3db3"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("85870ef8-4a5d-4800-afcc-0f5696781315"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("c6cb321b-984a-4ddc-a56a-1eb51e819cf8"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("02191cb6-76a8-453c-9448-dde3b7d8f3db"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("3d7e6611-66d3-4eb1-95f7-b6476cf8105e"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("bac35c3f-e99f-41a4-89f2-3bc96bdcc0ff"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("26ae02a8-c057-4d00-b39f-5061625390e1"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("26bbc91d-b814-48a1-84d0-86ff56fa8dc1"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("9ac5c6a6-25ec-4125-b4c1-169a6f1a1eb2"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("862f754a-3cdf-49cd-8e49-ed8cb536b18f"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("94283cb7-e503-4abe-9dba-c43fffdd05f4"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("979ecd93-3339-48ba-9e1f-9c97e212cfa1"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("4418f3b9-65c5-4d90-a19e-40cf6372ac87"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("46c7c5f7-1a44-4933-b91c-f7e52ef3cf1a"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("94264134-ff76-4e54-bbec-badea5e57ee9"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("11f24b0e-ede4-42e6-9648-67541abb379b"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("512915f3-b4c7-4c5f-b42c-84464d54350a"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("8a06e575-67ad-4f03-8818-b8773e72d17c"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("c14ab857-d4ed-479d-97b9-af19ddbf243c"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("fffa76c6-b427-42f3-885b-dd980eff388b"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("24f92514-0455-408e-9eab-c075e703c6bf"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("8141d6e4-e8b9-4c87-b83c-3981d5c1361c"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("d495f52a-218a-4cc8-bd9a-c92dc8d3afef"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("1523ebf1-88d1-4b04-ac5e-3ca4896b1ae7"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("3039edb8-8408-4b32-a950-84244eeaf676"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("faf047e0-767a-45eb-b695-d18b958dca84"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("8e3c2296-3858-4179-9010-6164fc9d9cf3"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("cf2f5f62-eeb8-4a87-b52d-d479d8a16f89"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("f8ece393-6c36-41b5-bc16-8367725cbf6a"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("1eea7a84-56a1-4d62-bdc3-6ed3b5ed3988"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("929ec7f2-df60-41b4-addb-adcff62e6034"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("ce02a806-878d-4bc0-820c-b673b3a0a677"));

            migrationBuilder.AddColumn<int>(
                name: "ReservedStock",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "BusinessLocations",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CustomerCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CustomerType = table.Column<int>(type: "int", nullable: false),
                    CreditLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OpeningBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrentOutstanding = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriceLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PriceListName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CustomerType = table.Column<int>(type: "int", nullable: false),
                    EffectiveFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EffectiveTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiscountRules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RuleName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Scope = table.Column<int>(type: "int", nullable: false),
                    DiscountType = table.Column<int>(type: "int", nullable: false),
                    DiscountValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CustomerType = table.Column<int>(type: "int", nullable: true),
                    MinQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinOrderAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxDiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EffectiveFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EffectiveTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiscountRules_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiscountRules_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PriceListItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PriceListId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceListItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceListItems_PriceLists_PriceListId",
                        column: x => x.PriceListId,
                        principalTable: "PriceLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PriceListItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AdjustmentTypes",
                columns: new[] { "Id", "AdjustmentTypeName", "Description", "Sign" },
                values: new object[,]
                {
                    { new Guid("3e70dc4f-9680-48ca-bc5d-d9f7494c349e"), "Abnormal", "", 0 },
                    { new Guid("ab70b561-1ef6-4d51-812c-5466c208fea8"), "Normal", "", 0 }
                });

            migrationBuilder.InsertData(
                table: "ApplicableTaxs",
                columns: new[] { "Id", "ApplicableTaxName", "Description", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("287beec2-dbe6-4ee7-9fee-6c5463824170"), "Food", "", 0m },
                    { new Guid("37b90f39-eb93-4036-9158-d1c08dac69dd"), "Sales Tax", "", 0m },
                    { new Guid("ee256d77-e4e0-469c-a4fe-89734c8f9e21"), "Fruits", "", 0m }
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "BarcodeDescription", "BarcodeTypeCode", "BarcodeTypeName" },
                values: new object[,]
                {
                    { new Guid("5e2d3ed8-51c9-4e5c-9124-2f5f71c953e5"), "", "", "UPC" },
                    { new Guid("cd33c814-bec3-441e-98f0-8412cab10bb5"), "", "", "QR Code" },
                    { new Guid("ed276e3c-f725-4aef-b400-6b3cd8e13fb6"), "", "", "NFC" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BandOrigin", "BrandName", "Description" },
                values: new object[,]
                {
                    { new Guid("1df566f2-d014-47ab-870d-f6af82864e78"), "", "Apple", "" },
                    { new Guid("5da93915-c330-49f4-b521-e0f3c649e204"), "", "Samsung", "" },
                    { new Guid("f32fdcf7-f77f-48b1-a36c-663bd0314f0d"), "", "Sony", "" }
                });

            migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "Address", "City", "Country", "IsActive", "LocationName", "State", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("1a7b5656-82c8-4a09-89bd-02ce07c70d0e"), "", "", "", true, "Warehouse B", "", "" },
                    { new Guid("2879a892-0634-4319-b245-0a2ff7d7ab0d"), "", "", "", true, "Warehouse A", "", "" },
                    { new Guid("a904fb5b-42e1-44da-a761-33bc7c904eba"), "", "", "", true, "Downtown Store", "", "" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryCode", "CategoryName", "Description" },
                values: new object[,]
                {
                    { new Guid("3069d9af-edf9-439f-beff-ec1fbc2e8ed4"), "", "Clothing", "" },
                    { new Guid("83b36b8f-cb84-4f75-851d-f130e8a1d493"), "", "Electronics", "" },
                    { new Guid("a974b9a9-93a7-4c37-aff0-b81e833175ff"), "", "Home Appliances", "" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Description", "ProductTypeCode", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("73a6919f-216a-44d6-9ed9-a66a1023df42"), "", "", "Furniture" },
                    { new Guid("859c118b-071f-4368-bae2-81af9d3d5485"), "", "", "Food" },
                    { new Guid("9e4b5031-e86b-41c0-8798-88dcc37cf320"), "", "", "Clothing" },
                    { new Guid("a23f0d6b-e7f4-40c6-b30c-96b9f4102175"), "", "", "Toys" },
                    { new Guid("ef868e07-203d-4ae3-9496-e7745943381a"), "", "", "Electronics" }
                });

            migrationBuilder.InsertData(
                table: "SellingPriceTaxes",
                columns: new[] { "Id", "Description", "SellingPriceTaxName", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("0e302f28-89d5-4864-8520-3255139fd12a"), "", "Exclusive", 0m },
                    { new Guid("ca2138a4-85ef-4936-aab0-3bf1ecf0d854"), "", "Zero Rate", 0m },
                    { new Guid("fbc0cc5b-91ef-4085-8329-bcb4ddd532a4"), "", "Inclusive", 0m }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryCode", "Description", "SubCategoryName" },
                values: new object[,]
                {
                    { new Guid("5af791c3-1731-45f5-a869-7d2217804a15"), "", "", "Laptops" },
                    { new Guid("db70fa81-b1e0-4259-972a-545d0d42307e"), "", "", "Televisions" },
                    { new Guid("f0e65aea-c50a-4961-8431-0f29c1b1f088"), "", "", "Smartphones" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "AllowDecimal", "ShortName", "UnitName" },
                values: new object[,]
                {
                    { new Guid("0e50d16d-d2af-49a9-aa81-64769d351027"), 0, "", "Liter" },
                    { new Guid("64ba624f-a468-457d-94d3-98801e188ed2"), 0, "", "Kilogram" },
                    { new Guid("e504227b-0ba0-4b99-aed8-c52adfcecb65"), 0, "", "Piece" }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "Description", "Name", "WarrantyDuration" },
                values: new object[,]
                {
                    { new Guid("4ef0ae70-1d33-492a-809d-47649a831747"), "", "", "2 Years" },
                    { new Guid("934927e6-127f-485b-91d8-67f7b7c1e972"), "", "", "3 Years" },
                    { new Guid("b3824851-3f38-4993-a904-2490c1bd5142"), "", "", "1 Year" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerCode",
                table: "Customers",
                column: "CustomerCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerName",
                table: "Customers",
                column: "CustomerName");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountRules_CustomerId",
                table: "DiscountRules",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountRules_ProductId",
                table: "DiscountRules",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountRules_Scope_IsActive_EffectiveFrom",
                table: "DiscountRules",
                columns: new[] { "Scope", "IsActive", "EffectiveFrom" });

            migrationBuilder.CreateIndex(
                name: "IX_PriceListItems_PriceListId_ProductId_MinQuantity",
                table: "PriceListItems",
                columns: new[] { "PriceListId", "ProductId", "MinQuantity" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PriceListItems_ProductId",
                table: "PriceListItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceLists_CustomerType_IsActive_EffectiveFrom",
                table: "PriceLists",
                columns: new[] { "CustomerType", "IsActive", "EffectiveFrom" });

            migrationBuilder.CreateIndex(
                name: "IX_PriceLists_PriceListName",
                table: "PriceLists",
                column: "PriceListName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StockAdjustmentItems_StockAdjustments_StockAdjustmentId",
                table: "StockAdjustmentItems",
                column: "StockAdjustmentId",
                principalTable: "StockAdjustments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransferItems_StockTransfers_StockTransferId",
                table: "StockTransferItems",
                column: "StockTransferId",
                principalTable: "StockTransfers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockAdjustmentItems_StockAdjustments_StockAdjustmentId",
                table: "StockAdjustmentItems");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransferItems_StockTransfers_StockTransferId",
                table: "StockTransferItems");

            migrationBuilder.DropTable(
                name: "DiscountRules");

            migrationBuilder.DropTable(
                name: "PriceListItems");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "PriceLists");

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("3e70dc4f-9680-48ca-bc5d-d9f7494c349e"));

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("ab70b561-1ef6-4d51-812c-5466c208fea8"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("287beec2-dbe6-4ee7-9fee-6c5463824170"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("37b90f39-eb93-4036-9158-d1c08dac69dd"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("ee256d77-e4e0-469c-a4fe-89734c8f9e21"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("5e2d3ed8-51c9-4e5c-9124-2f5f71c953e5"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("cd33c814-bec3-441e-98f0-8412cab10bb5"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("ed276e3c-f725-4aef-b400-6b3cd8e13fb6"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("1df566f2-d014-47ab-870d-f6af82864e78"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("5da93915-c330-49f4-b521-e0f3c649e204"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("f32fdcf7-f77f-48b1-a36c-663bd0314f0d"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("1a7b5656-82c8-4a09-89bd-02ce07c70d0e"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("2879a892-0634-4319-b245-0a2ff7d7ab0d"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("a904fb5b-42e1-44da-a761-33bc7c904eba"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("3069d9af-edf9-439f-beff-ec1fbc2e8ed4"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("83b36b8f-cb84-4f75-851d-f130e8a1d493"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a974b9a9-93a7-4c37-aff0-b81e833175ff"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("73a6919f-216a-44d6-9ed9-a66a1023df42"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("859c118b-071f-4368-bae2-81af9d3d5485"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("9e4b5031-e86b-41c0-8798-88dcc37cf320"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("a23f0d6b-e7f4-40c6-b30c-96b9f4102175"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("ef868e07-203d-4ae3-9496-e7745943381a"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("0e302f28-89d5-4864-8520-3255139fd12a"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("ca2138a4-85ef-4936-aab0-3bf1ecf0d854"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("fbc0cc5b-91ef-4085-8329-bcb4ddd532a4"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("5af791c3-1731-45f5-a869-7d2217804a15"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("db70fa81-b1e0-4259-972a-545d0d42307e"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("f0e65aea-c50a-4961-8431-0f29c1b1f088"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("0e50d16d-d2af-49a9-aa81-64769d351027"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("64ba624f-a468-457d-94d3-98801e188ed2"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("e504227b-0ba0-4b99-aed8-c52adfcecb65"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("4ef0ae70-1d33-492a-809d-47649a831747"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("934927e6-127f-485b-91d8-67f7b7c1e972"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("b3824851-3f38-4993-a904-2490c1bd5142"));

            migrationBuilder.DropColumn(
                name: "ReservedStock",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "BusinessLocations");

            migrationBuilder.InsertData(
                table: "AdjustmentTypes",
                columns: new[] { "Id", "AdjustmentTypeName", "Description", "Sign" },
                values: new object[,]
                {
                    { new Guid("0a9fdb59-8976-4470-b399-91ff3aab9ba5"), "Abnormal", "", 0 },
                    { new Guid("f02d91a8-a485-4d72-9ab5-603dc6d8a15a"), "Normal", "", 0 }
                });

            migrationBuilder.InsertData(
                table: "ApplicableTaxs",
                columns: new[] { "Id", "ApplicableTaxName", "Description", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("25f90c9e-8075-40a4-a7c6-1ef73d6f3db3"), "Fruits", "", 0m },
                    { new Guid("85870ef8-4a5d-4800-afcc-0f5696781315"), "Food", "", 0m },
                    { new Guid("c6cb321b-984a-4ddc-a56a-1eb51e819cf8"), "Sales Tax", "", 0m }
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "BarcodeDescription", "BarcodeTypeCode", "BarcodeTypeName" },
                values: new object[,]
                {
                    { new Guid("02191cb6-76a8-453c-9448-dde3b7d8f3db"), "", "", "NFC" },
                    { new Guid("3d7e6611-66d3-4eb1-95f7-b6476cf8105e"), "", "", "UPC" },
                    { new Guid("bac35c3f-e99f-41a4-89f2-3bc96bdcc0ff"), "", "", "QR Code" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BandOrigin", "BrandName", "Description" },
                values: new object[,]
                {
                    { new Guid("26ae02a8-c057-4d00-b39f-5061625390e1"), "", "Samsung", "" },
                    { new Guid("26bbc91d-b814-48a1-84d0-86ff56fa8dc1"), "", "Sony", "" },
                    { new Guid("9ac5c6a6-25ec-4125-b4c1-169a6f1a1eb2"), "", "Apple", "" }
                });

            migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "Address", "City", "Country", "LocationName", "State", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("862f754a-3cdf-49cd-8e49-ed8cb536b18f"), "", "", "", "Downtown Store", "", "" },
                    { new Guid("94283cb7-e503-4abe-9dba-c43fffdd05f4"), "", "", "", "Warehouse B", "", "" },
                    { new Guid("979ecd93-3339-48ba-9e1f-9c97e212cfa1"), "", "", "", "Warehouse A", "", "" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryCode", "CategoryName", "Description" },
                values: new object[,]
                {
                    { new Guid("4418f3b9-65c5-4d90-a19e-40cf6372ac87"), "", "Clothing", "" },
                    { new Guid("46c7c5f7-1a44-4933-b91c-f7e52ef3cf1a"), "", "Home Appliances", "" },
                    { new Guid("94264134-ff76-4e54-bbec-badea5e57ee9"), "", "Electronics", "" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Description", "ProductTypeCode", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("11f24b0e-ede4-42e6-9648-67541abb379b"), "", "", "Clothing" },
                    { new Guid("512915f3-b4c7-4c5f-b42c-84464d54350a"), "", "", "Food" },
                    { new Guid("8a06e575-67ad-4f03-8818-b8773e72d17c"), "", "", "Electronics" },
                    { new Guid("c14ab857-d4ed-479d-97b9-af19ddbf243c"), "", "", "Toys" },
                    { new Guid("fffa76c6-b427-42f3-885b-dd980eff388b"), "", "", "Furniture" }
                });

            migrationBuilder.InsertData(
                table: "SellingPriceTaxes",
                columns: new[] { "Id", "Description", "SellingPriceTaxName", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("24f92514-0455-408e-9eab-c075e703c6bf"), "", "Inclusive", 0m },
                    { new Guid("8141d6e4-e8b9-4c87-b83c-3981d5c1361c"), "", "Zero Rate", 0m },
                    { new Guid("d495f52a-218a-4cc8-bd9a-c92dc8d3afef"), "", "Exclusive", 0m }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryCode", "Description", "SubCategoryName" },
                values: new object[,]
                {
                    { new Guid("1523ebf1-88d1-4b04-ac5e-3ca4896b1ae7"), "", "", "Televisions" },
                    { new Guid("3039edb8-8408-4b32-a950-84244eeaf676"), "", "", "Smartphones" },
                    { new Guid("faf047e0-767a-45eb-b695-d18b958dca84"), "", "", "Laptops" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "AllowDecimal", "ShortName", "UnitName" },
                values: new object[,]
                {
                    { new Guid("8e3c2296-3858-4179-9010-6164fc9d9cf3"), 0, "", "Liter" },
                    { new Guid("cf2f5f62-eeb8-4a87-b52d-d479d8a16f89"), 0, "", "Piece" },
                    { new Guid("f8ece393-6c36-41b5-bc16-8367725cbf6a"), 0, "", "Kilogram" }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "Description", "Name", "WarrantyDuration" },
                values: new object[,]
                {
                    { new Guid("1eea7a84-56a1-4d62-bdc3-6ed3b5ed3988"), "", "", "1 Year" },
                    { new Guid("929ec7f2-df60-41b4-addb-adcff62e6034"), "", "", "2 Years" },
                    { new Guid("ce02a806-878d-4bc0-820c-b673b3a0a677"), "", "", "3 Years" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_StockAdjustmentItems_StockAdjustments_StockAdjustmentId",
                table: "StockAdjustmentItems",
                column: "StockAdjustmentId",
                principalTable: "StockAdjustments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransferItems_StockTransfers_StockTransferId",
                table: "StockTransferItems",
                column: "StockTransferId",
                principalTable: "StockTransfers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
