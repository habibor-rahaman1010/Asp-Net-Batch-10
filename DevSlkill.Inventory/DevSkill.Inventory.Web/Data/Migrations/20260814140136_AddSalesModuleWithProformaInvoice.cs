using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DevSkill.Inventory.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSalesModuleWithProformaInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "ProformaInvoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProformaNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProformaDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValidUntil = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BusinessLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalespersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SalespersonName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PaymentTerms = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ItemDiscountTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalTax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GrandTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProformaInvoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProformaInvoices_BusinessLocations_BusinessLocationId",
                        column: x => x.BusinessLocationId,
                        principalTable: "BusinessLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProformaInvoices_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProformaInvoiceItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProformaInvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LineTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProformaInvoiceItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProformaInvoiceItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProformaInvoiceItems_ProformaInvoices_ProformaInvoiceId",
                        column: x => x.ProformaInvoiceId,
                        principalTable: "ProformaInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AdjustmentTypes",
                columns: new[] { "Id", "AdjustmentTypeName", "Description", "Sign" },
                values: new object[,]
                {
                    { new Guid("455403e9-9287-4b0c-a381-8ff373bdf186"), "Normal", "", 0 },
                    { new Guid("ea2de7ec-adf0-40dd-8bf7-07d38a03e9b8"), "Abnormal", "", 0 }
                });

            migrationBuilder.InsertData(
                table: "ApplicableTaxs",
                columns: new[] { "Id", "ApplicableTaxName", "Description", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("55fb5641-346f-442e-9547-ef9c4e353a0f"), "Sales Tax", "", 0m },
                    { new Guid("9253878e-2d36-40cc-807d-1cc3cba40fd9"), "Food", "", 0m },
                    { new Guid("f3ba2861-370d-41ac-841d-ad8d875b514a"), "Fruits", "", 0m }
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "BarcodeDescription", "BarcodeTypeCode", "BarcodeTypeName" },
                values: new object[,]
                {
                    { new Guid("257e96b4-3d5a-4be9-b973-f567b5330b21"), "", "", "QR Code" },
                    { new Guid("265098c9-9a38-493f-b945-b986c890282d"), "", "", "UPC" },
                    { new Guid("b665642e-5871-4df0-aedd-9c46a3c961aa"), "", "", "NFC" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BandOrigin", "BrandName", "Description" },
                values: new object[,]
                {
                    { new Guid("09ce78ba-9bad-440e-a6a4-dd43ad48bc74"), "", "Samsung", "" },
                    { new Guid("40a97774-e79e-4781-9a0c-d14bd0fa9143"), "", "Sony", "" },
                    { new Guid("89d4bc49-84ee-447d-a67f-7b85689ab4c5"), "", "Apple", "" }
                });

            migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "Address", "City", "Country", "IsActive", "LocationName", "State", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("4f56ed51-8dc9-4aa6-a352-b4530723f8d7"), "", "", "", true, "Warehouse A", "", "" },
                    { new Guid("861a8e19-3a1e-482b-b74b-6a3c553d0b25"), "", "", "", true, "Downtown Store", "", "" },
                    { new Guid("b58c331d-eeb1-4d7f-b70b-25a3ba5215fc"), "", "", "", true, "Warehouse B", "", "" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryCode", "CategoryName", "Description" },
                values: new object[,]
                {
                    { new Guid("25e5a404-db0b-4b0f-8e84-afd7f0539fb5"), "", "Home Appliances", "" },
                    { new Guid("4e3a5e18-241b-41cc-b203-3fc68bd75fd2"), "", "Clothing", "" },
                    { new Guid("bf8b3515-fa40-4db5-a549-b6faf808b99f"), "", "Electronics", "" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Description", "ProductTypeCode", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("12d1f0fe-fe0a-4da9-adac-34cbcbf4e9fb"), "", "", "Food" },
                    { new Guid("48e2cab3-6042-42ec-978a-a63feede2b20"), "", "", "Furniture" },
                    { new Guid("93482801-3942-463e-b26b-f54456daed6e"), "", "", "Electronics" },
                    { new Guid("9dc0598f-f391-4df4-abb7-8180fa6b8a0a"), "", "", "Toys" },
                    { new Guid("c59e0f9d-2ec2-4606-8a6e-b7b8a8c3d841"), "", "", "Clothing" }
                });

            migrationBuilder.InsertData(
                table: "SellingPriceTaxes",
                columns: new[] { "Id", "Description", "SellingPriceTaxName", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("5bacd899-f6d8-4b09-bff3-9b2f3d48294d"), "", "Inclusive", 0m },
                    { new Guid("647f72b5-d86d-4643-a64b-b41a83a8046b"), "", "Zero Rate", 0m },
                    { new Guid("ee07d64f-e3ba-4bc5-9cc3-2abfc61cc547"), "", "Exclusive", 0m }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryCode", "Description", "SubCategoryName" },
                values: new object[,]
                {
                    { new Guid("6c47a635-894a-4e1f-8efe-858d150c83fb"), "", "", "Televisions" },
                    { new Guid("7580bb9a-5e02-4810-a4b6-f786e776eecc"), "", "", "Smartphones" },
                    { new Guid("cde5e0f2-5488-4171-a457-85673d2ee7c5"), "", "", "Laptops" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "AllowDecimal", "ShortName", "UnitName" },
                values: new object[,]
                {
                    { new Guid("145b4a21-b679-43de-bc34-d9fb78f35ca3"), 0, "", "Kilogram" },
                    { new Guid("1ee2dec9-c63c-488c-aa29-a8e3ae4c805d"), 0, "", "Piece" },
                    { new Guid("a2415b5a-a80f-4627-9165-fdd9d6498e1f"), 0, "", "Liter" }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "Description", "Name", "WarrantyDuration" },
                values: new object[,]
                {
                    { new Guid("7840d053-4247-446a-8f3e-9b7fc3355ff1"), "", "", "2 Years" },
                    { new Guid("9b797812-ce6a-4972-8e46-c9b95f69180e"), "", "", "1 Year" },
                    { new Guid("ab5b7dcb-c308-4c6b-9225-fd36664d38a9"), "", "", "3 Years" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProformaInvoiceItems_ProductId",
                table: "ProformaInvoiceItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProformaInvoiceItems_ProformaInvoiceId",
                table: "ProformaInvoiceItems",
                column: "ProformaInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ProformaInvoices_BusinessLocationId",
                table: "ProformaInvoices",
                column: "BusinessLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProformaInvoices_CustomerId_ProformaDate",
                table: "ProformaInvoices",
                columns: new[] { "CustomerId", "ProformaDate" });

            migrationBuilder.CreateIndex(
                name: "IX_ProformaInvoices_ProformaNo",
                table: "ProformaInvoices",
                column: "ProformaNo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProformaInvoiceItems");

            migrationBuilder.DropTable(
                name: "ProformaInvoices");

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("455403e9-9287-4b0c-a381-8ff373bdf186"));

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("ea2de7ec-adf0-40dd-8bf7-07d38a03e9b8"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("55fb5641-346f-442e-9547-ef9c4e353a0f"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("9253878e-2d36-40cc-807d-1cc3cba40fd9"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("f3ba2861-370d-41ac-841d-ad8d875b514a"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("257e96b4-3d5a-4be9-b973-f567b5330b21"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("265098c9-9a38-493f-b945-b986c890282d"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("b665642e-5871-4df0-aedd-9c46a3c961aa"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("09ce78ba-9bad-440e-a6a4-dd43ad48bc74"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("40a97774-e79e-4781-9a0c-d14bd0fa9143"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("89d4bc49-84ee-447d-a67f-7b85689ab4c5"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("4f56ed51-8dc9-4aa6-a352-b4530723f8d7"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("861a8e19-3a1e-482b-b74b-6a3c553d0b25"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("b58c331d-eeb1-4d7f-b70b-25a3ba5215fc"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("25e5a404-db0b-4b0f-8e84-afd7f0539fb5"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("4e3a5e18-241b-41cc-b203-3fc68bd75fd2"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("bf8b3515-fa40-4db5-a549-b6faf808b99f"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("12d1f0fe-fe0a-4da9-adac-34cbcbf4e9fb"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("48e2cab3-6042-42ec-978a-a63feede2b20"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("93482801-3942-463e-b26b-f54456daed6e"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("9dc0598f-f391-4df4-abb7-8180fa6b8a0a"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("c59e0f9d-2ec2-4606-8a6e-b7b8a8c3d841"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("5bacd899-f6d8-4b09-bff3-9b2f3d48294d"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("647f72b5-d86d-4643-a64b-b41a83a8046b"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("ee07d64f-e3ba-4bc5-9cc3-2abfc61cc547"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("6c47a635-894a-4e1f-8efe-858d150c83fb"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("7580bb9a-5e02-4810-a4b6-f786e776eecc"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("cde5e0f2-5488-4171-a457-85673d2ee7c5"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("145b4a21-b679-43de-bc34-d9fb78f35ca3"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("1ee2dec9-c63c-488c-aa29-a8e3ae4c805d"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("a2415b5a-a80f-4627-9165-fdd9d6498e1f"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("7840d053-4247-446a-8f3e-9b7fc3355ff1"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("9b797812-ce6a-4972-8e46-c9b95f69180e"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("ab5b7dcb-c308-4c6b-9225-fd36664d38a9"));

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
        }
    }
}
