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
                    Exception = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Properties = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    { new Guid("2db176b0-9518-48bf-93ec-a7d8d85a0d7e"), "Abnormal", "" },
                    { new Guid("c2cb93df-fd8e-4057-b569-4602c18b3e77"), "Normal", "" }
                });

            migrationBuilder.InsertData(
                table: "ApplicableTaxs",
                columns: new[] { "Id", "ApplicableTaxName", "Description", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("10f08e97-d1a4-49cb-95ce-1bb7f85f6740"), "Food", "", 0m },
                    { new Guid("6763dc86-c30e-42bf-8892-40d57351db42"), "Sales Tax", "", 0m },
                    { new Guid("b34f7c4b-93b8-4ce3-ab57-33a4de07ad09"), "Fruits", "", 0m }
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "BarcodeDescription", "BarcodeTypeCode", "BarcodeTypeName" },
                values: new object[,]
                {
                    { new Guid("5052f7c1-c664-4038-8053-e60466b16750"), "", "", "UPC" },
                    { new Guid("61585250-67c5-4dff-be66-3f9c98f67c93"), "", "", "NFC" },
                    { new Guid("dbae37e7-765e-4f66-aae2-4b8a22c1b1b3"), "", "", "QR Code" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BandOrigin", "BrandName", "Description" },
                values: new object[,]
                {
                    { new Guid("09f2f38e-968f-44a4-a7a3-5e0fdb815500"), "", "Apple", "" },
                    { new Guid("21daed38-28fd-4400-bbf4-9b348577a147"), "", "Sony", "" },
                    { new Guid("9948ec7e-a5d9-49e6-91bb-c41085f2a2d6"), "", "Samsung", "" }
                });

            migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "Address", "City", "Country", "LocationName", "State", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("2f00a44e-7eb2-423b-8954-cdb54b8ee26d"), "", "", "", "Warehouse A", "", "" },
                    { new Guid("3a0d2063-435f-401c-849b-bb714d0abac8"), "", "", "", "Downtown Store", "", "" },
                    { new Guid("3f6db187-6085-4369-ae5b-402195d5a5a1"), "", "", "", "Warehouse B", "", "" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryCode", "CategoryName", "Description" },
                values: new object[,]
                {
                    { new Guid("04598c5a-3cba-4850-bb38-285236e0f171"), "", "Home Appliances", "" },
                    { new Guid("43d5c5fb-093e-4cb1-8143-1bd55e3d815f"), "", "Electronics", "" },
                    { new Guid("fa27e962-c9c0-4ba4-86e9-4efde5861390"), "", "Clothing", "" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Description", "ProductTypeCode", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("09a20cd9-23ef-427f-b2c6-a35933a281ec"), "", "", "Furniture" },
                    { new Guid("17e88774-7c3e-43b6-aa13-7b7a63be56be"), "", "", "Electronics" },
                    { new Guid("6ca6c5f9-d0f1-467b-b7ae-898d4db3a208"), "", "", "Toys" },
                    { new Guid("a33d6676-b4cf-42a6-8a6a-509cd8905357"), "", "", "Clothing" },
                    { new Guid("b098e548-1854-46aa-bd65-e6153e674d23"), "", "", "Food" }
                });

            migrationBuilder.InsertData(
                table: "SellingPriceTaxes",
                columns: new[] { "Id", "Description", "SellingPriceTaxName", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("7af3d891-411c-4658-9893-efe4e692771f"), "", "Exclusive", 0m },
                    { new Guid("bd9eb1ff-4fcb-4701-b7bf-16f8bb5ef53f"), "", "Inclusive", 0m },
                    { new Guid("cb61a02f-7ce1-4537-b555-68d648182c6c"), "", "Zero Rate", 0m }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryCode", "Description", "SubCategoryName" },
                values: new object[,]
                {
                    { new Guid("24d5e704-6613-47c2-8018-a2f9442ff852"), "", "", "Smartphones" },
                    { new Guid("9c5012f5-bd00-418d-8284-e273a6979672"), "", "", "Televisions" },
                    { new Guid("e97ad49f-cef0-4d3d-bb52-8e55a2feb904"), "", "", "Laptops" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "AllowDecimal", "ShortName", "UnitName" },
                values: new object[,]
                {
                    { new Guid("168590df-03d7-4139-a258-319661d11169"), 0, "", "Liter" },
                    { new Guid("cdb2b94a-67f2-4302-979c-4dffe1dd3099"), 0, "", "Piece" },
                    { new Guid("f4b51600-8fde-484d-b629-babe5afd50cb"), 0, "", "Kilogram" }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "Description", "Name", "WarrantyDuration" },
                values: new object[,]
                {
                    { new Guid("af392588-2bbd-4559-9f99-79e31c635d30"), "", "", "2 Years" },
                    { new Guid("b61ee5f3-dfd2-4c9a-aa85-e15c8cbaf815"), "", "", "1 Year" },
                    { new Guid("d7bbbffe-7a19-4601-b76d-eb6cdfef1276"), "", "", "3 Years" }
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
                keyValue: new Guid("2db176b0-9518-48bf-93ec-a7d8d85a0d7e"));

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("c2cb93df-fd8e-4057-b569-4602c18b3e77"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("10f08e97-d1a4-49cb-95ce-1bb7f85f6740"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("6763dc86-c30e-42bf-8892-40d57351db42"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("b34f7c4b-93b8-4ce3-ab57-33a4de07ad09"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("5052f7c1-c664-4038-8053-e60466b16750"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("61585250-67c5-4dff-be66-3f9c98f67c93"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("dbae37e7-765e-4f66-aae2-4b8a22c1b1b3"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("09f2f38e-968f-44a4-a7a3-5e0fdb815500"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("21daed38-28fd-4400-bbf4-9b348577a147"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("9948ec7e-a5d9-49e6-91bb-c41085f2a2d6"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("2f00a44e-7eb2-423b-8954-cdb54b8ee26d"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("3a0d2063-435f-401c-849b-bb714d0abac8"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("3f6db187-6085-4369-ae5b-402195d5a5a1"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("04598c5a-3cba-4850-bb38-285236e0f171"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("43d5c5fb-093e-4cb1-8143-1bd55e3d815f"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("fa27e962-c9c0-4ba4-86e9-4efde5861390"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("09a20cd9-23ef-427f-b2c6-a35933a281ec"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("17e88774-7c3e-43b6-aa13-7b7a63be56be"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("6ca6c5f9-d0f1-467b-b7ae-898d4db3a208"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("a33d6676-b4cf-42a6-8a6a-509cd8905357"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("b098e548-1854-46aa-bd65-e6153e674d23"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("7af3d891-411c-4658-9893-efe4e692771f"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("bd9eb1ff-4fcb-4701-b7bf-16f8bb5ef53f"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("cb61a02f-7ce1-4537-b555-68d648182c6c"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("24d5e704-6613-47c2-8018-a2f9442ff852"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("9c5012f5-bd00-418d-8284-e273a6979672"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("e97ad49f-cef0-4d3d-bb52-8e55a2feb904"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("168590df-03d7-4139-a258-319661d11169"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("cdb2b94a-67f2-4302-979c-4dffe1dd3099"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("f4b51600-8fde-484d-b629-babe5afd50cb"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("af392588-2bbd-4559-9f99-79e31c635d30"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("b61ee5f3-dfd2-4c9a-aa85-e15c8cbaf815"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("d7bbbffe-7a19-4601-b76d-eb6cdfef1276"));

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
