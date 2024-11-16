using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DevSkill.Inventory.Web.Migrations.InventoryDb
{
    /// <inheritdoc />
    public partial class ApplicationLogTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("513b18e2-0a0c-44c3-9466-8c4f6c4124c0"));

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("bb7e5b53-96a6-407f-9be9-b63ec495f6b7"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("39ab7987-3e4b-47ea-8fe1-84fc23b1aba7"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("4b16ee06-9ae2-4bd2-88c9-1c649ffda81d"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("81cd484b-c831-49b2-bda6-fee46e63277d"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("6ebd353a-3ad7-4627-9c7f-d17cc4c41ba9"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("c05a5fda-8f3f-4725-a1dc-95538ac5d425"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("edf9a774-b2bd-4693-8efb-5b927c251a02"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("b7d58fc8-776f-45d6-8fe2-2add028d5c67"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("c156ac31-3ac3-43b1-8211-5a80df2f70c3"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("c243004b-6380-4a49-abbd-d50f123e8769"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("1e775323-95f0-4d65-b3b8-81a6d4e7d1e5"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("db40c83a-5be8-4725-9c2d-5cb11c113748"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("df30d2cb-abc8-4f97-ae5d-eea419478ef4"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0ad661d2-c42e-464e-8040-d4e0f717c977"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("81b76cd2-e4b2-4dfc-b7b2-fbb3cc6b4198"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("fc86ec82-ceff-4455-89d1-698d7b494f94"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("2fb9977d-cac0-4824-a427-418a823775a1"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("674b7541-265d-4827-a0f8-c2d7336f2a74"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("776ed0ff-a123-4084-8392-049f27d3f405"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("a72d9648-eb7c-4948-99ee-b5a2fcacc4e2"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("dd97aa01-94d9-4a2b-81fc-58ecfb506bee"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("08bae7a4-567c-4593-9d77-acf91e17c9fb"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("70383614-f734-4ca0-a3bf-803718a95fb7"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("988feff6-7a31-49db-bda9-0d0ce7b3b722"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("3caa1be7-40b0-4efa-af81-99f52c1297d2"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("bafb1b45-073a-4fc8-92ca-e49b01bafebe"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("e7b7c2c6-6585-4525-b193-b1e5f03820bd"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("211849fd-c39b-4dd6-b1fc-e25d24392066"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("3041196b-bf72-462b-84e3-9074c79ea412"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("fc228ef5-b5d7-44d3-a580-a63e10f9e42f"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("447305e7-87b4-4985-8be0-e54c58794a4d"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("866db33c-5d42-49d1-ad6c-5c7feeee15a5"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("e66533e7-05bb-431f-ad16-735438f1a4df"));

            migrationBuilder.CreateTable(
                name: "ApplicationLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Exception = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationLogs", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AdjustmentTypes",
                columns: new[] { "Id", "AdjustmentTypeName", "Description" },
                values: new object[,]
                {
                    { new Guid("60affbf8-c65c-4a9b-bf49-d16de45c3f60"), "Abnormal", "" },
                    { new Guid("a194cd0d-daf6-4e4c-ade7-e31d4b4a3bb1"), "Normal", "" }
                });

            migrationBuilder.InsertData(
                table: "ApplicableTaxs",
                columns: new[] { "Id", "ApplicableTaxName", "Description", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("34f491e4-3ec2-4f70-82c1-07ca42f36c74"), "Fruits", "", 0m },
                    { new Guid("89d697c8-126a-463e-b2f8-00b012ae985f"), "Food", "", 0m },
                    { new Guid("bc7dc0ca-a444-4b92-b2ef-db0fffd5ec23"), "Sales Tax", "", 0m }
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "BarcodeDescription", "BarcodeTypeCode", "BarcodeTypeName" },
                values: new object[,]
                {
                    { new Guid("3c36b291-1b24-4b5d-9500-99de785ec6ef"), "", "", "QR Code" },
                    { new Guid("5db5bbe2-40a4-4b67-84f9-4e5b0640c494"), "", "", "NFC" },
                    { new Guid("f0c586bb-90e7-4d9a-9d6c-47e97bb5352c"), "", "", "UPC" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BandOrigin", "BrandName", "Description" },
                values: new object[,]
                {
                    { new Guid("3d6b3360-92ad-41e1-aede-5854a9b4a4c4"), "", "Apple", "" },
                    { new Guid("5565abbb-ffc1-47e5-91e7-594a7ede659f"), "", "Sony", "" },
                    { new Guid("7cbcf9fe-8b96-4b51-bdca-3e793ef010ed"), "", "Samsung", "" }
                });

            migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "Address", "City", "Country", "LocationName", "State", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("20448da1-9e9f-4394-8840-4e0a6743fac7"), "", "", "", "Warehouse A", "", "" },
                    { new Guid("7a0d15d6-33ae-4bb3-94ea-f506f2e1d01b"), "", "", "", "Downtown Store", "", "" },
                    { new Guid("91f9f47b-832f-4c30-bc13-38c3b05fc729"), "", "", "", "Warehouse B", "", "" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryCode", "CategoryName", "Description" },
                values: new object[,]
                {
                    { new Guid("66fbc1cc-c74d-4903-b247-821af97c24fb"), "", "Clothing", "" },
                    { new Guid("8cacf5c4-2890-448f-bbed-a6ff59b3ed08"), "", "Home Appliances", "" },
                    { new Guid("a75db419-a428-4db7-a33d-4b7e8ec04015"), "", "Electronics", "" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Description", "ProductTypeCode", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("09444b59-cc82-4782-95dc-2332f1e58fb0"), "", "", "Food" },
                    { new Guid("15772003-1a24-4c2d-838d-b63f9d48fccc"), "", "", "Furniture" },
                    { new Guid("95d4963f-789f-4fbc-a7ed-ff79ea48e483"), "", "", "Clothing" },
                    { new Guid("eebded05-6899-44bc-95df-abd95fcb8822"), "", "", "Toys" },
                    { new Guid("f9bb5542-23f6-4604-93cd-ac5a8e3aac71"), "", "", "Electronics" }
                });

            migrationBuilder.InsertData(
                table: "SellingPriceTaxes",
                columns: new[] { "Id", "Description", "SellingPriceTaxName", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("384d3d5f-5231-4770-80f4-9070fcc7c7fa"), "", "Zero Rate", 0m },
                    { new Guid("b056dacb-5326-4863-b5d0-2e33b9b3d04e"), "", "Inclusive", 0m },
                    { new Guid("c4c00e3e-3c1c-427b-8774-db87f7a4bdb0"), "", "Exclusive", 0m }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryCode", "Description", "SubCategoryName" },
                values: new object[,]
                {
                    { new Guid("55cfdff8-a498-4fe7-b645-dc4a1a77aaf9"), "", "", "Smartphones" },
                    { new Guid("608e7cca-d945-400c-b094-c87bba36c844"), "", "", "Laptops" },
                    { new Guid("7ea911c6-4d34-4fda-a83c-bec5c2636817"), "", "", "Televisions" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "AllowDecimal", "ShortName", "UnitName" },
                values: new object[,]
                {
                    { new Guid("16c7b776-500d-4031-8bf6-72ebbc0b78a8"), 0, "", "Kilogram" },
                    { new Guid("9654a422-7eba-4407-961b-ed1e14c2b3c6"), 0, "", "Liter" },
                    { new Guid("cd6085b0-0422-46a3-bdf3-433e6c561ce0"), 0, "", "Piece" }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "Description", "Name", "WarrantyDuration" },
                values: new object[,]
                {
                    { new Guid("11d05e11-b8d3-4921-bf60-7de55dfabccc"), "", "", "2 Years" },
                    { new Guid("37cd4955-4208-484e-9d6a-e9839ed00548"), "", "", "1 Year" },
                    { new Guid("93c90547-afab-4e3a-b39f-b01e2d051dd9"), "", "", "3 Years" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationLogs");

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("60affbf8-c65c-4a9b-bf49-d16de45c3f60"));

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("a194cd0d-daf6-4e4c-ade7-e31d4b4a3bb1"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("34f491e4-3ec2-4f70-82c1-07ca42f36c74"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("89d697c8-126a-463e-b2f8-00b012ae985f"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("bc7dc0ca-a444-4b92-b2ef-db0fffd5ec23"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("3c36b291-1b24-4b5d-9500-99de785ec6ef"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("5db5bbe2-40a4-4b67-84f9-4e5b0640c494"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("f0c586bb-90e7-4d9a-9d6c-47e97bb5352c"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("3d6b3360-92ad-41e1-aede-5854a9b4a4c4"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("5565abbb-ffc1-47e5-91e7-594a7ede659f"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("7cbcf9fe-8b96-4b51-bdca-3e793ef010ed"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("20448da1-9e9f-4394-8840-4e0a6743fac7"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("7a0d15d6-33ae-4bb3-94ea-f506f2e1d01b"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("91f9f47b-832f-4c30-bc13-38c3b05fc729"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("66fbc1cc-c74d-4903-b247-821af97c24fb"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("8cacf5c4-2890-448f-bbed-a6ff59b3ed08"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a75db419-a428-4db7-a33d-4b7e8ec04015"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("09444b59-cc82-4782-95dc-2332f1e58fb0"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("15772003-1a24-4c2d-838d-b63f9d48fccc"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("95d4963f-789f-4fbc-a7ed-ff79ea48e483"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("eebded05-6899-44bc-95df-abd95fcb8822"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("f9bb5542-23f6-4604-93cd-ac5a8e3aac71"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("384d3d5f-5231-4770-80f4-9070fcc7c7fa"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("b056dacb-5326-4863-b5d0-2e33b9b3d04e"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("c4c00e3e-3c1c-427b-8774-db87f7a4bdb0"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("55cfdff8-a498-4fe7-b645-dc4a1a77aaf9"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("608e7cca-d945-400c-b094-c87bba36c844"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("7ea911c6-4d34-4fda-a83c-bec5c2636817"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("16c7b776-500d-4031-8bf6-72ebbc0b78a8"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("9654a422-7eba-4407-961b-ed1e14c2b3c6"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("cd6085b0-0422-46a3-bdf3-433e6c561ce0"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("11d05e11-b8d3-4921-bf60-7de55dfabccc"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("37cd4955-4208-484e-9d6a-e9839ed00548"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("93c90547-afab-4e3a-b39f-b01e2d051dd9"));

            migrationBuilder.InsertData(
                table: "AdjustmentTypes",
                columns: new[] { "Id", "AdjustmentTypeName", "Description" },
                values: new object[,]
                {
                    { new Guid("513b18e2-0a0c-44c3-9466-8c4f6c4124c0"), "Normal", "" },
                    { new Guid("bb7e5b53-96a6-407f-9be9-b63ec495f6b7"), "Abnormal", "" }
                });

            migrationBuilder.InsertData(
                table: "ApplicableTaxs",
                columns: new[] { "Id", "ApplicableTaxName", "Description", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("39ab7987-3e4b-47ea-8fe1-84fc23b1aba7"), "Sales Tax", "", 0m },
                    { new Guid("4b16ee06-9ae2-4bd2-88c9-1c649ffda81d"), "Fruits", "", 0m },
                    { new Guid("81cd484b-c831-49b2-bda6-fee46e63277d"), "Food", "", 0m }
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "BarcodeDescription", "BarcodeTypeCode", "BarcodeTypeName" },
                values: new object[,]
                {
                    { new Guid("6ebd353a-3ad7-4627-9c7f-d17cc4c41ba9"), "", "", "NFC" },
                    { new Guid("c05a5fda-8f3f-4725-a1dc-95538ac5d425"), "", "", "UPC" },
                    { new Guid("edf9a774-b2bd-4693-8efb-5b927c251a02"), "", "", "QR Code" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BandOrigin", "BrandName", "Description" },
                values: new object[,]
                {
                    { new Guid("b7d58fc8-776f-45d6-8fe2-2add028d5c67"), "", "Apple", "" },
                    { new Guid("c156ac31-3ac3-43b1-8211-5a80df2f70c3"), "", "Samsung", "" },
                    { new Guid("c243004b-6380-4a49-abbd-d50f123e8769"), "", "Sony", "" }
                });

            migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "Address", "City", "Country", "LocationName", "State", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("1e775323-95f0-4d65-b3b8-81a6d4e7d1e5"), "", "", "", "Warehouse A", "", "" },
                    { new Guid("db40c83a-5be8-4725-9c2d-5cb11c113748"), "", "", "", "Downtown Store", "", "" },
                    { new Guid("df30d2cb-abc8-4f97-ae5d-eea419478ef4"), "", "", "", "Warehouse B", "", "" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryCode", "CategoryName", "Description" },
                values: new object[,]
                {
                    { new Guid("0ad661d2-c42e-464e-8040-d4e0f717c977"), "", "Clothing", "" },
                    { new Guid("81b76cd2-e4b2-4dfc-b7b2-fbb3cc6b4198"), "", "Electronics", "" },
                    { new Guid("fc86ec82-ceff-4455-89d1-698d7b494f94"), "", "Home Appliances", "" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Description", "ProductTypeCode", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("2fb9977d-cac0-4824-a427-418a823775a1"), "", "", "Food" },
                    { new Guid("674b7541-265d-4827-a0f8-c2d7336f2a74"), "", "", "Toys" },
                    { new Guid("776ed0ff-a123-4084-8392-049f27d3f405"), "", "", "Clothing" },
                    { new Guid("a72d9648-eb7c-4948-99ee-b5a2fcacc4e2"), "", "", "Electronics" },
                    { new Guid("dd97aa01-94d9-4a2b-81fc-58ecfb506bee"), "", "", "Furniture" }
                });

            migrationBuilder.InsertData(
                table: "SellingPriceTaxes",
                columns: new[] { "Id", "Description", "SellingPriceTaxName", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("08bae7a4-567c-4593-9d77-acf91e17c9fb"), "", "Exclusive", 0m },
                    { new Guid("70383614-f734-4ca0-a3bf-803718a95fb7"), "", "Inclusive", 0m },
                    { new Guid("988feff6-7a31-49db-bda9-0d0ce7b3b722"), "", "Zero Rate", 0m }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryCode", "Description", "SubCategoryName" },
                values: new object[,]
                {
                    { new Guid("3caa1be7-40b0-4efa-af81-99f52c1297d2"), "", "", "Televisions" },
                    { new Guid("bafb1b45-073a-4fc8-92ca-e49b01bafebe"), "", "", "Laptops" },
                    { new Guid("e7b7c2c6-6585-4525-b193-b1e5f03820bd"), "", "", "Smartphones" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "AllowDecimal", "ShortName", "UnitName" },
                values: new object[,]
                {
                    { new Guid("211849fd-c39b-4dd6-b1fc-e25d24392066"), 0, "", "Kilogram" },
                    { new Guid("3041196b-bf72-462b-84e3-9074c79ea412"), 0, "", "Liter" },
                    { new Guid("fc228ef5-b5d7-44d3-a580-a63e10f9e42f"), 0, "", "Piece" }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "Description", "Name", "WarrantyDuration" },
                values: new object[,]
                {
                    { new Guid("447305e7-87b4-4985-8be0-e54c58794a4d"), "", "", "1 Year" },
                    { new Guid("866db33c-5d42-49d1-ad6c-5c7feeee15a5"), "", "", "3 Years" },
                    { new Guid("e66533e7-05bb-431f-ad16-735438f1a4df"), "", "", "2 Years" }
                });
        }
    }
}
