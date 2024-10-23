using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DevSkill.Inventory.Web.Migrations
{
    /// <inheritdoc />
    public partial class StockAdjustmentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("50251455-b7f5-46ae-867b-1a13661e4c9b"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("9357d983-b32f-4bc9-81d2-7b84428c8c3c"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("f117b3a1-b328-4a24-addf-fdc080ae2459"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("0c4fdd03-cd9a-4196-bede-e746c6fb1fd4"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("61d6a326-e9b4-4cb9-9d27-f65125bf17b0"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("fdf7062c-510d-4214-b5bc-35f2438fdb08"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("52163bd8-6418-4210-8daa-b5df62205581"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("68a98be9-d158-44f3-98e7-cdbcea78bbc6"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("964579c6-b710-44e0-af09-c53d4da9fbdc"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("03ecb752-def8-4365-b57a-0d70c695b741"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("2b197f69-568f-423e-9f9f-64d1f794cf17"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("db24567d-dea4-46fc-b96d-0d3b11ac2d49"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("241895d6-bfe0-4e88-ad26-7bed7235b74d"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("e7af7b1d-771c-4b57-bcbe-33f33c701cb1"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("edd7c254-7c67-4aec-b0e4-c420969c1d38"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("4d00920b-bfe5-4f5e-ab24-a89058925288"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("694bc9fc-733e-4960-8aba-5813650fc00c"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("89eed6d9-d4dd-4511-8851-dde55d051e54"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("c410dd14-6be5-47e6-976e-cb4846d305eb"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("d945a2ac-622c-4a9a-afa2-60e591c422e7"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("52f1494a-5a60-4f10-bb2a-499251847003"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("d63c5056-4d8a-4eee-a999-9c3a9b726356"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("e50d9542-cd3e-4c08-a53f-63202165f9bc"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("0fc445ab-93ee-46a5-931d-9ece546f2378"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("bb0c2735-a953-4b00-b968-b1ff8bff7b6e"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("fa0b2bc3-19f8-4110-9f67-757180585570"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("af9550b8-8ff2-458b-97d3-27bf7a171349"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("b42d1751-ddcb-4713-836b-edf7b027572b"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("ef51e061-588f-4f92-b426-28ae9b1eb1e5"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("4e23a4a6-0213-4c25-b684-8cd579e4864f"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("73a5b434-2b91-45c5-a7b6-f0f4c1be94f7"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("91fd531a-6a43-49c6-8cf5-1d07d0165afe"));

            migrationBuilder.CreateTable(
                name: "StockAdjustments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdjustmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReferenceNo = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<int>(type: "int", nullable: false),
                    TotalAmountRecover = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdjustmentTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockAdjustments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockAdjustments_AdjustmentTypes_AdjustmentTypeId",
                        column: x => x.AdjustmentTypeId,
                        principalTable: "AdjustmentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockAdjustments_BusinessLocations_BusinessLocationId",
                        column: x => x.BusinessLocationId,
                        principalTable: "BusinessLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockAdjustments_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AdjustmentTypes",
                columns: new[] { "Id", "AdjustmentTypeName", "Description" },
                values: new object[,]
                {
                    { new Guid("449adae8-2eb5-4659-9759-484ee10f11fb"), "Normal", "" },
                    { new Guid("fac4a467-9e30-4bae-a60d-19ff5296b4f6"), "Abnormal", "" }
                });

            migrationBuilder.InsertData(
                table: "ApplicableTaxs",
                columns: new[] { "Id", "ApplicableTaxName" },
                values: new object[,]
                {
                    { new Guid("4f2933c5-3223-46c2-adc4-6bba64661aa5"), "Sales Tax" },
                    { new Guid("8998a696-a113-4a6e-9077-9135779b368b"), "Food" },
                    { new Guid("cb9792c8-c31e-4564-93c0-5390e2ecd55a"), "Fruits" }
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "BarcodeTypeName" },
                values: new object[,]
                {
                    { new Guid("2b6088cb-69af-4e22-b243-5c37d403e228"), "UPC" },
                    { new Guid("908d3232-5947-4723-ac8b-b82b800fd81d"), "QR Code" },
                    { new Guid("a418f1c5-e0c6-4ccf-9f51-4080e4084f14"), "NFC" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BandOrigin", "BrandName", "Description" },
                values: new object[,]
                {
                    { new Guid("3ec8fdc5-0a8b-4826-a754-931619f162cc"), "", "Samsung", "" },
                    { new Guid("993bc033-3ab4-440b-9cb8-0824d3650e36"), "", "Sony", "" },
                    { new Guid("d0e21e7e-8794-4eef-9159-ffbc2a44251a"), "", "Apple", "" }
                });

            migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "Address", "City", "Country", "LocationName", "State", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("83b19ad0-0235-4212-8f0f-e4503d9e17dc"), "", "", "", "Warehouse A", "", "" },
                    { new Guid("857c37e6-4e1b-4863-8e65-a01509b8416c"), "", "", "", "Warehouse B", "", "" },
                    { new Guid("ae21fc37-75df-41e3-be63-201af88845f3"), "", "", "", "Downtown Store", "", "" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryCode", "CategoryName", "Description" },
                values: new object[,]
                {
                    { new Guid("263f9c13-1317-4898-b3e7-469b0f609209"), "", "Home Appliances", "" },
                    { new Guid("a1f50a39-e16d-4fea-8709-955f4c7c3151"), "", "Electronics", "" },
                    { new Guid("bf8e51fb-972a-4b57-af8b-97fb96ce1993"), "", "Clothing", "" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("3e647bd7-abea-4e5c-bab9-6a8f4433372f"), "Food" },
                    { new Guid("7498fdc8-859f-41c1-ab6d-22258f39009b"), "Clothing" },
                    { new Guid("9d4c8600-f9d6-41df-bcf7-a410585a5b2d"), "Electronics" },
                    { new Guid("d752144f-3f3f-486a-8175-13be530c5421"), "Furniture" },
                    { new Guid("f828cac4-46db-43ff-980f-02c31d348360"), "Toys" }
                });

            migrationBuilder.InsertData(
                table: "SellingPriceTaxes",
                columns: new[] { "Id", "SellingPriceTaxName" },
                values: new object[,]
                {
                    { new Guid("6e5c220e-1ff8-46be-ac30-d238d54904bb"), "Exclusive" },
                    { new Guid("8c04d5cf-53b9-4900-9cb0-47cb3e4e57c8"), "Inclusive" },
                    { new Guid("8e8e53d0-6205-4d97-aa97-7a5da8f80545"), "Zero Rate" }
                });

            migrationBuilder.InsertData(
                table: "Subcategories",
                columns: new[] { "Id", "SubcategoryName" },
                values: new object[,]
                {
                    { new Guid("7dfb69b2-4b45-4013-8425-f91f2e6f6166"), "Televisions" },
                    { new Guid("9ba100dc-60a4-4bbb-8b86-0f2a1645bf69"), "Laptops" },
                    { new Guid("fb24a0a8-8c74-4b15-989c-b0160cb0accd"), "Smartphones" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "AllowDecimal", "ShortName", "UnitName" },
                values: new object[,]
                {
                    { new Guid("37d47f2f-55e0-4b82-88c8-7752e2d921a1"), 0, "", "Piece" },
                    { new Guid("9d822f80-ffed-4065-b8c2-1c560c098c45"), 0, "", "Liter" },
                    { new Guid("f406c65b-b75d-4687-bc8e-52ca31750d06"), 0, "", "Kilogram" }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "Description", "Name", "WarrantyDuration" },
                values: new object[,]
                {
                    { new Guid("0a927419-8095-45b7-8f60-21c05a8e0994"), "", "", "1 Year" },
                    { new Guid("93039997-783c-4c4c-9168-b1ad55bcd352"), "", "", "2 Years" },
                    { new Guid("ced27238-7ffa-40f9-bb30-caf6e53913f7"), "", "", "3 Years" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockAdjustments_AdjustmentTypeId",
                table: "StockAdjustments",
                column: "AdjustmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StockAdjustments_BusinessLocationId",
                table: "StockAdjustments",
                column: "BusinessLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_StockAdjustments_ProductId",
                table: "StockAdjustments",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockAdjustments");

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("449adae8-2eb5-4659-9759-484ee10f11fb"));

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("fac4a467-9e30-4bae-a60d-19ff5296b4f6"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("4f2933c5-3223-46c2-adc4-6bba64661aa5"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("8998a696-a113-4a6e-9077-9135779b368b"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("cb9792c8-c31e-4564-93c0-5390e2ecd55a"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("2b6088cb-69af-4e22-b243-5c37d403e228"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("908d3232-5947-4723-ac8b-b82b800fd81d"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("a418f1c5-e0c6-4ccf-9f51-4080e4084f14"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("3ec8fdc5-0a8b-4826-a754-931619f162cc"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("993bc033-3ab4-440b-9cb8-0824d3650e36"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("d0e21e7e-8794-4eef-9159-ffbc2a44251a"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("83b19ad0-0235-4212-8f0f-e4503d9e17dc"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("857c37e6-4e1b-4863-8e65-a01509b8416c"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("ae21fc37-75df-41e3-be63-201af88845f3"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("263f9c13-1317-4898-b3e7-469b0f609209"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1f50a39-e16d-4fea-8709-955f4c7c3151"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("bf8e51fb-972a-4b57-af8b-97fb96ce1993"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("3e647bd7-abea-4e5c-bab9-6a8f4433372f"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("7498fdc8-859f-41c1-ab6d-22258f39009b"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("9d4c8600-f9d6-41df-bcf7-a410585a5b2d"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("d752144f-3f3f-486a-8175-13be530c5421"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("f828cac4-46db-43ff-980f-02c31d348360"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("6e5c220e-1ff8-46be-ac30-d238d54904bb"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("8c04d5cf-53b9-4900-9cb0-47cb3e4e57c8"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("8e8e53d0-6205-4d97-aa97-7a5da8f80545"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("7dfb69b2-4b45-4013-8425-f91f2e6f6166"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("9ba100dc-60a4-4bbb-8b86-0f2a1645bf69"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("fb24a0a8-8c74-4b15-989c-b0160cb0accd"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("37d47f2f-55e0-4b82-88c8-7752e2d921a1"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("9d822f80-ffed-4065-b8c2-1c560c098c45"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("f406c65b-b75d-4687-bc8e-52ca31750d06"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("0a927419-8095-45b7-8f60-21c05a8e0994"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("93039997-783c-4c4c-9168-b1ad55bcd352"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("ced27238-7ffa-40f9-bb30-caf6e53913f7"));

            migrationBuilder.InsertData(
                table: "ApplicableTaxs",
                columns: new[] { "Id", "ApplicableTaxName" },
                values: new object[,]
                {
                    { new Guid("50251455-b7f5-46ae-867b-1a13661e4c9b"), "Sales Tax" },
                    { new Guid("9357d983-b32f-4bc9-81d2-7b84428c8c3c"), "Food" },
                    { new Guid("f117b3a1-b328-4a24-addf-fdc080ae2459"), "Fruits" }
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "BarcodeTypeName" },
                values: new object[,]
                {
                    { new Guid("0c4fdd03-cd9a-4196-bede-e746c6fb1fd4"), "QR Code" },
                    { new Guid("61d6a326-e9b4-4cb9-9d27-f65125bf17b0"), "UPC" },
                    { new Guid("fdf7062c-510d-4214-b5bc-35f2438fdb08"), "NFC" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BandOrigin", "BrandName", "Description" },
                values: new object[,]
                {
                    { new Guid("52163bd8-6418-4210-8daa-b5df62205581"), "", "Apple", "" },
                    { new Guid("68a98be9-d158-44f3-98e7-cdbcea78bbc6"), "", "Sony", "" },
                    { new Guid("964579c6-b710-44e0-af09-c53d4da9fbdc"), "", "Samsung", "" }
                });

            migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "Address", "City", "Country", "LocationName", "State", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("03ecb752-def8-4365-b57a-0d70c695b741"), "", "", "", "Warehouse B", "", "" },
                    { new Guid("2b197f69-568f-423e-9f9f-64d1f794cf17"), "", "", "", "Warehouse A", "", "" },
                    { new Guid("db24567d-dea4-46fc-b96d-0d3b11ac2d49"), "", "", "", "Downtown Store", "", "" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryCode", "CategoryName", "Description" },
                values: new object[,]
                {
                    { new Guid("241895d6-bfe0-4e88-ad26-7bed7235b74d"), "", "Electronics", "" },
                    { new Guid("e7af7b1d-771c-4b57-bcbe-33f33c701cb1"), "", "Home Appliances", "" },
                    { new Guid("edd7c254-7c67-4aec-b0e4-c420969c1d38"), "", "Clothing", "" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("4d00920b-bfe5-4f5e-ab24-a89058925288"), "Electronics" },
                    { new Guid("694bc9fc-733e-4960-8aba-5813650fc00c"), "Clothing" },
                    { new Guid("89eed6d9-d4dd-4511-8851-dde55d051e54"), "Furniture" },
                    { new Guid("c410dd14-6be5-47e6-976e-cb4846d305eb"), "Food" },
                    { new Guid("d945a2ac-622c-4a9a-afa2-60e591c422e7"), "Toys" }
                });

            migrationBuilder.InsertData(
                table: "SellingPriceTaxes",
                columns: new[] { "Id", "SellingPriceTaxName" },
                values: new object[,]
                {
                    { new Guid("52f1494a-5a60-4f10-bb2a-499251847003"), "Zero Rate" },
                    { new Guid("d63c5056-4d8a-4eee-a999-9c3a9b726356"), "Inclusive" },
                    { new Guid("e50d9542-cd3e-4c08-a53f-63202165f9bc"), "Exclusive" }
                });

            migrationBuilder.InsertData(
                table: "Subcategories",
                columns: new[] { "Id", "SubcategoryName" },
                values: new object[,]
                {
                    { new Guid("0fc445ab-93ee-46a5-931d-9ece546f2378"), "Televisions" },
                    { new Guid("bb0c2735-a953-4b00-b968-b1ff8bff7b6e"), "Smartphones" },
                    { new Guid("fa0b2bc3-19f8-4110-9f67-757180585570"), "Laptops" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "AllowDecimal", "ShortName", "UnitName" },
                values: new object[,]
                {
                    { new Guid("af9550b8-8ff2-458b-97d3-27bf7a171349"), 0, "", "Piece" },
                    { new Guid("b42d1751-ddcb-4713-836b-edf7b027572b"), 0, "", "Liter" },
                    { new Guid("ef51e061-588f-4f92-b426-28ae9b1eb1e5"), 0, "", "Kilogram" }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "Description", "Name", "WarrantyDuration" },
                values: new object[,]
                {
                    { new Guid("4e23a4a6-0213-4c25-b684-8cd579e4864f"), "", "", "2 Years" },
                    { new Guid("73a5b434-2b91-45c5-a7b6-f0f4c1be94f7"), "", "", "1 Year" },
                    { new Guid("91fd531a-6a43-49c6-8cf5-1d07d0165afe"), "", "", "3 Years" }
                });
        }
    }
}
