using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DevSkill.Inventory.Web.Migrations.InventoryDb
{
    /// <inheritdoc />
    public partial class PaymentTerms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("d7b2862d-c851-47bb-9afc-f249e870adf8"));

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("eee79b4f-79e7-4b39-8121-d770f42e1880"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("33ec3bc9-08d6-4bd1-acf5-b9ab08b39eb4"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("6f11a709-f816-4ef2-b888-30826ca9ad22"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("c2ec23ee-b2c9-4e10-a1ca-bd175354c4d3"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("6f34d799-d3b4-44cb-9188-0fa0bc27fccb"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("d1f2f092-e0eb-4012-948c-0aeedf1561cc"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("f4ea485f-7171-4055-8a46-96dd99c89ed0"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("325bc763-603d-4c18-bb25-0f350d81f9a3"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("9fedd86c-6215-4687-9374-f56dceee0fba"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("f78356bd-262f-41ec-8c4f-1188713dbbc8"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("0758d4a9-39d1-4262-901a-dcfeaec4b560"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("7d5de306-c960-4339-9349-7eaf42256d35"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("a99d94e8-64fe-43e4-9ea3-abf6d4fe8f55"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("7f2a8627-2635-494d-8306-b99625498cf3"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("880e5356-a654-4631-bb1d-69625bd04436"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("ba307947-ce5b-4ed1-a98e-2823c322d904"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("138f24cd-f27a-4c20-b1f2-6748851a6ab6"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("5186843b-e2a6-4e5f-bdd2-32e48bbf3614"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("a5ca5fd9-4315-4c01-8261-d12f5dd2111f"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("ce8c1c59-53e7-4300-ba25-d98b7156f835"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("f7a814a6-50e9-469d-8233-64c2310c017d"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("7a13b17f-96ef-4d4c-ac30-f0bcb1026e99"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("8a3d639c-beb4-4578-8fe2-6dbc79e41b17"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("e3dd2b87-619c-48ae-ad9a-fb665ce85e92"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("7af20fdb-6aae-485a-ae4e-af83b1e8b2c2"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("7c247aee-9833-4a76-b944-1bce466f0543"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("c1d25621-95d8-444b-ab62-c641e856f558"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("518addf8-cb6a-4c84-a238-507bc6468d62"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("c5b4071e-44cb-4b04-a6c2-35569831b858"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("dc6ee6c3-2992-48da-b6c8-695f41e61f69"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("13d99d3a-9001-4648-98c6-6286ba2079fa"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("6881d763-29ae-4b7d-8e2c-92c01644caa2"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("ea3cfb88-8934-4e93-a891-bb38ffafeaee"));

            migrationBuilder.DropColumn(
                name: "PaymentTerms",
                table: "ProformaInvoices");

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentTermId",
                table: "ProformaInvoices",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PaymentTerms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TermName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    DueDays = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTerms", x => x.Id);
                });

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
                table: "PaymentTerms",
                columns: new[] { "Id", "Description", "DueDays", "IsActive", "TermName" },
                values: new object[,]
                {
                    { new Guid("6a1f0b2c-0001-4a1e-9f01-2b7d5c9e1001"), "Full payment is collected when the goods are handed over.", 0, true, "Cash on Delivery" },
                    { new Guid("6a1f0b2c-0002-4a1e-9f01-2b7d5c9e1002"), "Full payment is received before the goods are released.", 0, true, "Advance Payment" },
                    { new Guid("6a1f0b2c-0003-4a1e-9f01-2b7d5c9e1003"), "Half of the amount is paid up front and the rest on delivery.", 0, true, "50% Advance, 50% on Delivery" },
                    { new Guid("6a1f0b2c-0004-4a1e-9f01-2b7d5c9e1004"), "Payment is due within 7 days of the invoice date.", 7, true, "Net 7" },
                    { new Guid("6a1f0b2c-0005-4a1e-9f01-2b7d5c9e1005"), "Payment is due within 15 days of the invoice date.", 15, true, "Net 15" },
                    { new Guid("6a1f0b2c-0006-4a1e-9f01-2b7d5c9e1006"), "Payment is due within 30 days of the invoice date.", 30, true, "Net 30" },
                    { new Guid("6a1f0b2c-0007-4a1e-9f01-2b7d5c9e1007"), "Payment is due within 60 days of the invoice date.", 60, true, "Net 60" }
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

            migrationBuilder.CreateIndex(
                name: "IX_ProformaInvoices_PaymentTermId",
                table: "ProformaInvoices",
                column: "PaymentTermId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTerms_TermName",
                table: "PaymentTerms",
                column: "TermName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProformaInvoices_PaymentTerms_PaymentTermId",
                table: "ProformaInvoices",
                column: "PaymentTermId",
                principalTable: "PaymentTerms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProformaInvoices_PaymentTerms_PaymentTermId",
                table: "ProformaInvoices");

            migrationBuilder.DropTable(
                name: "PaymentTerms");

            migrationBuilder.DropIndex(
                name: "IX_ProformaInvoices_PaymentTermId",
                table: "ProformaInvoices");

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

            migrationBuilder.DropColumn(
                name: "PaymentTermId",
                table: "ProformaInvoices");

            migrationBuilder.AddColumn<string>(
                name: "PaymentTerms",
                table: "ProformaInvoices",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AdjustmentTypes",
                columns: new[] { "Id", "AdjustmentTypeName", "Description", "Sign" },
                values: new object[,]
                {
                    { new Guid("d7b2862d-c851-47bb-9afc-f249e870adf8"), "Normal", "", 0 },
                    { new Guid("eee79b4f-79e7-4b39-8121-d770f42e1880"), "Abnormal", "", 0 }
                });

            migrationBuilder.InsertData(
                table: "ApplicableTaxs",
                columns: new[] { "Id", "ApplicableTaxName", "Description", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("33ec3bc9-08d6-4bd1-acf5-b9ab08b39eb4"), "Sales Tax", "", 0m },
                    { new Guid("6f11a709-f816-4ef2-b888-30826ca9ad22"), "Fruits", "", 0m },
                    { new Guid("c2ec23ee-b2c9-4e10-a1ca-bd175354c4d3"), "Food", "", 0m }
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "BarcodeDescription", "BarcodeTypeCode", "BarcodeTypeName" },
                values: new object[,]
                {
                    { new Guid("6f34d799-d3b4-44cb-9188-0fa0bc27fccb"), "", "", "NFC" },
                    { new Guid("d1f2f092-e0eb-4012-948c-0aeedf1561cc"), "", "", "UPC" },
                    { new Guid("f4ea485f-7171-4055-8a46-96dd99c89ed0"), "", "", "QR Code" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BandOrigin", "BrandName", "Description" },
                values: new object[,]
                {
                    { new Guid("325bc763-603d-4c18-bb25-0f350d81f9a3"), "", "Sony", "" },
                    { new Guid("9fedd86c-6215-4687-9374-f56dceee0fba"), "", "Samsung", "" },
                    { new Guid("f78356bd-262f-41ec-8c4f-1188713dbbc8"), "", "Apple", "" }
                });

            migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "Address", "City", "Country", "IsActive", "LocationName", "State", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("0758d4a9-39d1-4262-901a-dcfeaec4b560"), "", "", "", true, "Warehouse B", "", "" },
                    { new Guid("7d5de306-c960-4339-9349-7eaf42256d35"), "", "", "", true, "Warehouse A", "", "" },
                    { new Guid("a99d94e8-64fe-43e4-9ea3-abf6d4fe8f55"), "", "", "", true, "Downtown Store", "", "" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryCode", "CategoryName", "Description" },
                values: new object[,]
                {
                    { new Guid("7f2a8627-2635-494d-8306-b99625498cf3"), "", "Electronics", "" },
                    { new Guid("880e5356-a654-4631-bb1d-69625bd04436"), "", "Clothing", "" },
                    { new Guid("ba307947-ce5b-4ed1-a98e-2823c322d904"), "", "Home Appliances", "" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Description", "ProductTypeCode", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("138f24cd-f27a-4c20-b1f2-6748851a6ab6"), "", "", "Clothing" },
                    { new Guid("5186843b-e2a6-4e5f-bdd2-32e48bbf3614"), "", "", "Food" },
                    { new Guid("a5ca5fd9-4315-4c01-8261-d12f5dd2111f"), "", "", "Electronics" },
                    { new Guid("ce8c1c59-53e7-4300-ba25-d98b7156f835"), "", "", "Toys" },
                    { new Guid("f7a814a6-50e9-469d-8233-64c2310c017d"), "", "", "Furniture" }
                });

            migrationBuilder.InsertData(
                table: "SellingPriceTaxes",
                columns: new[] { "Id", "Description", "SellingPriceTaxName", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("7a13b17f-96ef-4d4c-ac30-f0bcb1026e99"), "", "Exclusive", 0m },
                    { new Guid("8a3d639c-beb4-4578-8fe2-6dbc79e41b17"), "", "Inclusive", 0m },
                    { new Guid("e3dd2b87-619c-48ae-ad9a-fb665ce85e92"), "", "Zero Rate", 0m }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryCode", "Description", "SubCategoryName" },
                values: new object[,]
                {
                    { new Guid("7af20fdb-6aae-485a-ae4e-af83b1e8b2c2"), "", "", "Smartphones" },
                    { new Guid("7c247aee-9833-4a76-b944-1bce466f0543"), "", "", "Televisions" },
                    { new Guid("c1d25621-95d8-444b-ab62-c641e856f558"), "", "", "Laptops" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "AllowDecimal", "ShortName", "UnitName" },
                values: new object[,]
                {
                    { new Guid("518addf8-cb6a-4c84-a238-507bc6468d62"), 0, "", "Kilogram" },
                    { new Guid("c5b4071e-44cb-4b04-a6c2-35569831b858"), 0, "", "Piece" },
                    { new Guid("dc6ee6c3-2992-48da-b6c8-695f41e61f69"), 0, "", "Liter" }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "Description", "Name", "WarrantyDuration" },
                values: new object[,]
                {
                    { new Guid("13d99d3a-9001-4648-98c6-6286ba2079fa"), "", "", "2 Years" },
                    { new Guid("6881d763-29ae-4b7d-8e2c-92c01644caa2"), "", "", "1 Year" },
                    { new Guid("ea3cfb88-8934-4e93-a891-bb38ffafeaee"), "", "", "3 Years" }
                });
        }
    }
}
