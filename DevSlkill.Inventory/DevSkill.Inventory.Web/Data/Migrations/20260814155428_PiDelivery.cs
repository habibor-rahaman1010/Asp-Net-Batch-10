using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DevSkill.Inventory.Web.Migrations.InventoryDb
{
    /// <inheritdoc />
    public partial class PiDelivery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("d1b0119b-e970-4522-b47d-daa77f1de7ee"));

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("ee6a58fe-ef4f-429c-9087-88de565b0d91"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("58a7753c-7f6c-4219-a87b-c5df5db6fb8c"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("cd5f6d2b-c2cf-4a3d-b2b1-46debabd08a7"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("f4b17fc4-cf27-40f6-aea6-9eff97ebb561"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("05036279-2e69-4313-ac7b-dbf3c121cab5"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("593550c8-6a1f-466b-b791-d24efdfdf383"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("e5e1f398-641e-4e5a-a026-09af094ddc9d"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("09c00d16-7ec8-49d6-8900-2ca6fc75b5fe"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("b4f6d3d1-27f9-4381-ac2e-21345efe95d0"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("f794904c-4dfe-45a5-99c4-9fa16da0facc"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("2e93ca75-406b-4705-9cec-841e562f4183"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("66831a9a-e067-4c25-aefe-5baae9f4d66a"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("ce9b9c28-7943-4792-8bb4-260dc94c925c"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("019267fd-0f06-46f5-86b0-fa1df227f002"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("966c57a4-3659-4a89-a5e1-0cc41c125405"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9a26dcef-f874-4d06-978c-c27fbe1b3a77"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("24df87e1-364d-4d60-bd4b-f5af17eb74d3"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("53aea96a-d6f6-486e-a5b7-22ac0832f897"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("6c26b06d-1583-4252-beab-2c02ff4b3796"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("8b5d3b50-854f-4a6c-b4c9-f12b86386c98"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("def9d70d-de51-447f-893f-abce882f2107"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("4f575bce-932a-4ae1-b0d9-0e0ddc143a00"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("b8f9eae2-d872-4a5d-9b36-769cd157c660"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("ed8b3bc0-10f9-4482-9494-a99b26eacdb4"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("1799c2f5-86f6-4fc9-aca6-be0a194c1b47"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("1b1c4823-4a07-4723-8883-b18acab8baa4"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("92b251c4-2894-40b9-87cf-52de7a41232d"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("263aa468-3920-4300-a9c8-dee5d173db91"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("8dedc4e0-3cab-4b6f-bbaa-b4ff5b1e8118"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("db815e15-8b2d-4585-96a1-7585152db5a4"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("0bb32c13-648f-4de6-a2b5-3bf6e10b56f4"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("1db3452f-005c-4ac2-bbe8-118ce624194a"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("87a0b384-e753-4d5e-b36a-5f1d14e6ea20"));

            migrationBuilder.CreateTable(
                name: "Deliveries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeliveryNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProformaInvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BusinessLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ReceivedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliveries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deliveries_BusinessLocations_BusinessLocationId",
                        column: x => x.BusinessLocationId,
                        principalTable: "BusinessLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Deliveries_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Deliveries_ProformaInvoices_ProformaInvoiceId",
                        column: x => x.ProformaInvoiceId,
                        principalTable: "ProformaInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeliveryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderedQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeliveredQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryItems_Deliveries_DeliveryId",
                        column: x => x.DeliveryId,
                        principalTable: "Deliveries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliveryItems_Products_ProductId",
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
                    { new Guid("81a4a276-5a50-45b4-a989-87c60e2355d5"), "Abnormal", "", 0 },
                    { new Guid("cefc62d6-036b-4a82-91d5-56190b73fe90"), "Normal", "", 0 }
                });

            migrationBuilder.InsertData(
                table: "ApplicableTaxs",
                columns: new[] { "Id", "ApplicableTaxName", "Description", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("203e2e10-8b9d-4109-870b-e8cb781ea1be"), "Sales Tax", "", 0m },
                    { new Guid("c131bf40-058a-4c00-a68b-bf13d8fbf907"), "Food", "", 0m },
                    { new Guid("c8498195-b188-43b3-add3-7f4c9e9b2d7a"), "Fruits", "", 0m }
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "BarcodeDescription", "BarcodeTypeCode", "BarcodeTypeName" },
                values: new object[,]
                {
                    { new Guid("4dcbdbe1-183d-4828-83c9-273871ce3d7d"), "", "", "QR Code" },
                    { new Guid("6cdf39af-3d83-4ef3-8b02-ae6c64a6f523"), "", "", "UPC" },
                    { new Guid("c65ed4ea-6ea2-4efb-b1c7-e67a7ab5d070"), "", "", "NFC" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BandOrigin", "BrandName", "Description" },
                values: new object[,]
                {
                    { new Guid("02e880eb-2788-452f-9e8a-43e6060c5ab3"), "", "Sony", "" },
                    { new Guid("74b9a5ef-d2f3-4539-984c-100fd5091a8c"), "", "Apple", "" },
                    { new Guid("c752f202-3076-4931-927b-d1427d8a28d5"), "", "Samsung", "" }
                });

            migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "Address", "City", "Country", "IsActive", "LocationName", "State", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("31949655-046e-420e-9da4-56885861fd98"), "", "", "", true, "Downtown Store", "", "" },
                    { new Guid("4e074ecc-ce90-49e4-98ae-d8bfaefe2169"), "", "", "", true, "Warehouse A", "", "" },
                    { new Guid("d5a51edf-3bf8-48ac-8c84-6bb11d678fe6"), "", "", "", true, "Warehouse B", "", "" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryCode", "CategoryName", "Description" },
                values: new object[,]
                {
                    { new Guid("0527ecad-9a84-4433-8b03-9b68f78d7791"), "", "Home Appliances", "" },
                    { new Guid("6c7ef205-b37f-44c8-9e52-89516cd4a679"), "", "Electronics", "" },
                    { new Guid("9c20e045-e9c7-4aee-b1db-b10f07ad0265"), "", "Clothing", "" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Description", "ProductTypeCode", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("2640a352-5cb5-4668-b84a-d46b38c2ab78"), "", "", "Clothing" },
                    { new Guid("88db5d0b-6c3f-4420-86d9-aef564ff18ac"), "", "", "Toys" },
                    { new Guid("c555db2a-fbcd-45dc-a89a-c189092dbc44"), "", "", "Furniture" },
                    { new Guid("d80ecc1c-9053-4f44-bdc6-85b09ea1a87e"), "", "", "Electronics" },
                    { new Guid("e7e5f2f7-da8c-4f4f-8e39-48faac11281e"), "", "", "Food" }
                });

            migrationBuilder.InsertData(
                table: "SellingPriceTaxes",
                columns: new[] { "Id", "Description", "SellingPriceTaxName", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("434581e0-22ab-42a4-81f3-cedf3854531a"), "", "Zero Rate", 0m },
                    { new Guid("7e2b4ca2-3531-4ba0-bbb7-e6a36c224be2"), "", "Inclusive", 0m },
                    { new Guid("b22d9b11-097d-44ea-8143-15adfefd8775"), "", "Exclusive", 0m }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryCode", "Description", "SubCategoryName" },
                values: new object[,]
                {
                    { new Guid("97331e14-0134-47c4-b7dc-7db73ebd6dca"), "", "", "Laptops" },
                    { new Guid("b1058bef-e8d3-4820-9cd9-277dc02459e7"), "", "", "Smartphones" },
                    { new Guid("bdd1fdb9-8385-4bff-bf69-5c6eade7765b"), "", "", "Televisions" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "AllowDecimal", "ShortName", "UnitName" },
                values: new object[,]
                {
                    { new Guid("16772479-531a-4fa7-b93e-7d3409620906"), 0, "", "Liter" },
                    { new Guid("81e63006-a7f1-4dd7-968e-5da6538ac01f"), 0, "", "Piece" },
                    { new Guid("df3165eb-1d90-4ef0-8960-bb5e00b5ddae"), 0, "", "Kilogram" }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "Description", "Name", "WarrantyDuration" },
                values: new object[,]
                {
                    { new Guid("52d8f437-e62e-4cdf-bb04-b1e319fabb8c"), "", "", "2 Years" },
                    { new Guid("6c22e21a-d313-40fb-8ce8-70b09b204e1a"), "", "", "1 Year" },
                    { new Guid("d8137a0e-dfa8-4627-9c32-aa0203116f36"), "", "", "3 Years" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_BusinessLocationId",
                table: "Deliveries",
                column: "BusinessLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_CustomerId_DeliveryDate",
                table: "Deliveries",
                columns: new[] { "CustomerId", "DeliveryDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_DeliveryNo",
                table: "Deliveries",
                column: "DeliveryNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_ProformaInvoiceId",
                table: "Deliveries",
                column: "ProformaInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryItems_DeliveryId_ProductId",
                table: "DeliveryItems",
                columns: new[] { "DeliveryId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryItems_ProductId",
                table: "DeliveryItems",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliveryItems");

            migrationBuilder.DropTable(
                name: "Deliveries");

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("81a4a276-5a50-45b4-a989-87c60e2355d5"));

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("cefc62d6-036b-4a82-91d5-56190b73fe90"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("203e2e10-8b9d-4109-870b-e8cb781ea1be"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("c131bf40-058a-4c00-a68b-bf13d8fbf907"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("c8498195-b188-43b3-add3-7f4c9e9b2d7a"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("4dcbdbe1-183d-4828-83c9-273871ce3d7d"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("6cdf39af-3d83-4ef3-8b02-ae6c64a6f523"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("c65ed4ea-6ea2-4efb-b1c7-e67a7ab5d070"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("02e880eb-2788-452f-9e8a-43e6060c5ab3"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("74b9a5ef-d2f3-4539-984c-100fd5091a8c"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("c752f202-3076-4931-927b-d1427d8a28d5"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("31949655-046e-420e-9da4-56885861fd98"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("4e074ecc-ce90-49e4-98ae-d8bfaefe2169"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("d5a51edf-3bf8-48ac-8c84-6bb11d678fe6"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0527ecad-9a84-4433-8b03-9b68f78d7791"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6c7ef205-b37f-44c8-9e52-89516cd4a679"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9c20e045-e9c7-4aee-b1db-b10f07ad0265"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("2640a352-5cb5-4668-b84a-d46b38c2ab78"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("88db5d0b-6c3f-4420-86d9-aef564ff18ac"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("c555db2a-fbcd-45dc-a89a-c189092dbc44"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("d80ecc1c-9053-4f44-bdc6-85b09ea1a87e"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("e7e5f2f7-da8c-4f4f-8e39-48faac11281e"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("434581e0-22ab-42a4-81f3-cedf3854531a"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("7e2b4ca2-3531-4ba0-bbb7-e6a36c224be2"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("b22d9b11-097d-44ea-8143-15adfefd8775"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("97331e14-0134-47c4-b7dc-7db73ebd6dca"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("b1058bef-e8d3-4820-9cd9-277dc02459e7"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("bdd1fdb9-8385-4bff-bf69-5c6eade7765b"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("16772479-531a-4fa7-b93e-7d3409620906"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("81e63006-a7f1-4dd7-968e-5da6538ac01f"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("df3165eb-1d90-4ef0-8960-bb5e00b5ddae"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("52d8f437-e62e-4cdf-bb04-b1e319fabb8c"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("6c22e21a-d313-40fb-8ce8-70b09b204e1a"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("d8137a0e-dfa8-4627-9c32-aa0203116f36"));

            migrationBuilder.InsertData(
                table: "AdjustmentTypes",
                columns: new[] { "Id", "AdjustmentTypeName", "Description", "Sign" },
                values: new object[,]
                {
                    { new Guid("d1b0119b-e970-4522-b47d-daa77f1de7ee"), "Normal", "", 0 },
                    { new Guid("ee6a58fe-ef4f-429c-9087-88de565b0d91"), "Abnormal", "", 0 }
                });

            migrationBuilder.InsertData(
                table: "ApplicableTaxs",
                columns: new[] { "Id", "ApplicableTaxName", "Description", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("58a7753c-7f6c-4219-a87b-c5df5db6fb8c"), "Food", "", 0m },
                    { new Guid("cd5f6d2b-c2cf-4a3d-b2b1-46debabd08a7"), "Sales Tax", "", 0m },
                    { new Guid("f4b17fc4-cf27-40f6-aea6-9eff97ebb561"), "Fruits", "", 0m }
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "BarcodeDescription", "BarcodeTypeCode", "BarcodeTypeName" },
                values: new object[,]
                {
                    { new Guid("05036279-2e69-4313-ac7b-dbf3c121cab5"), "", "", "NFC" },
                    { new Guid("593550c8-6a1f-466b-b791-d24efdfdf383"), "", "", "QR Code" },
                    { new Guid("e5e1f398-641e-4e5a-a026-09af094ddc9d"), "", "", "UPC" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BandOrigin", "BrandName", "Description" },
                values: new object[,]
                {
                    { new Guid("09c00d16-7ec8-49d6-8900-2ca6fc75b5fe"), "", "Samsung", "" },
                    { new Guid("b4f6d3d1-27f9-4381-ac2e-21345efe95d0"), "", "Apple", "" },
                    { new Guid("f794904c-4dfe-45a5-99c4-9fa16da0facc"), "", "Sony", "" }
                });

            migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "Address", "City", "Country", "IsActive", "LocationName", "State", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("2e93ca75-406b-4705-9cec-841e562f4183"), "", "", "", true, "Warehouse B", "", "" },
                    { new Guid("66831a9a-e067-4c25-aefe-5baae9f4d66a"), "", "", "", true, "Downtown Store", "", "" },
                    { new Guid("ce9b9c28-7943-4792-8bb4-260dc94c925c"), "", "", "", true, "Warehouse A", "", "" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryCode", "CategoryName", "Description" },
                values: new object[,]
                {
                    { new Guid("019267fd-0f06-46f5-86b0-fa1df227f002"), "", "Clothing", "" },
                    { new Guid("966c57a4-3659-4a89-a5e1-0cc41c125405"), "", "Home Appliances", "" },
                    { new Guid("9a26dcef-f874-4d06-978c-c27fbe1b3a77"), "", "Electronics", "" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Description", "ProductTypeCode", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("24df87e1-364d-4d60-bd4b-f5af17eb74d3"), "", "", "Furniture" },
                    { new Guid("53aea96a-d6f6-486e-a5b7-22ac0832f897"), "", "", "Toys" },
                    { new Guid("6c26b06d-1583-4252-beab-2c02ff4b3796"), "", "", "Food" },
                    { new Guid("8b5d3b50-854f-4a6c-b4c9-f12b86386c98"), "", "", "Electronics" },
                    { new Guid("def9d70d-de51-447f-893f-abce882f2107"), "", "", "Clothing" }
                });

            migrationBuilder.InsertData(
                table: "SellingPriceTaxes",
                columns: new[] { "Id", "Description", "SellingPriceTaxName", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("4f575bce-932a-4ae1-b0d9-0e0ddc143a00"), "", "Exclusive", 0m },
                    { new Guid("b8f9eae2-d872-4a5d-9b36-769cd157c660"), "", "Inclusive", 0m },
                    { new Guid("ed8b3bc0-10f9-4482-9494-a99b26eacdb4"), "", "Zero Rate", 0m }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryCode", "Description", "SubCategoryName" },
                values: new object[,]
                {
                    { new Guid("1799c2f5-86f6-4fc9-aca6-be0a194c1b47"), "", "", "Televisions" },
                    { new Guid("1b1c4823-4a07-4723-8883-b18acab8baa4"), "", "", "Smartphones" },
                    { new Guid("92b251c4-2894-40b9-87cf-52de7a41232d"), "", "", "Laptops" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "AllowDecimal", "ShortName", "UnitName" },
                values: new object[,]
                {
                    { new Guid("263aa468-3920-4300-a9c8-dee5d173db91"), 0, "", "Kilogram" },
                    { new Guid("8dedc4e0-3cab-4b6f-bbaa-b4ff5b1e8118"), 0, "", "Liter" },
                    { new Guid("db815e15-8b2d-4585-96a1-7585152db5a4"), 0, "", "Piece" }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "Description", "Name", "WarrantyDuration" },
                values: new object[,]
                {
                    { new Guid("0bb32c13-648f-4de6-a2b5-3bf6e10b56f4"), "", "", "3 Years" },
                    { new Guid("1db3452f-005c-4ac2-bbe8-118ce624194a"), "", "", "1 Year" },
                    { new Guid("87a0b384-e753-4d5e-b36a-5f1d14e6ea20"), "", "", "2 Years" }
                });
        }
    }
}
