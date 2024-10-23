using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DevSkill.Inventory.Web.Migrations
{
    /// <inheritdoc />
    public partial class AdjustmentTypesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("7226f718-2d19-4fb2-9bee-586281257df4"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("90a51ecd-95f4-4e92-9efb-09b31a1109a4"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("911cdef1-6ed6-4350-a917-380cf91db201"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("29b75b7c-5fd8-44c5-9145-a8b2ee554c69"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("79aecfcf-1f84-4a3d-a8c7-83ad6fd71915"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("8e57fb64-edd0-46d1-822e-23a9c4c43007"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("04a402d6-9b94-4abb-b4cd-572c5980ad5c"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("8f8ee098-f7ba-496c-94b9-b89527289150"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("a3e7eae3-7119-47d6-9929-1d161cb936b0"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("4faa4a52-6b9a-4e16-aeea-eca6f6415cf5"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("a1205f00-10f0-470c-b8bb-afdb8752846a"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("e0d5f778-e519-461d-af18-828444fc9383"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("19753583-7641-4763-ae49-bfb725bb22ab"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("5d2c6a3e-d22f-4997-a11c-0c224331f8fa"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("7c6bde57-245f-4b6d-9322-6aa392766714"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("0868600f-12e7-4fac-80bd-25000c73fc6c"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("1a04f119-b91c-46d7-966a-7139d497810a"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("37f52d88-0f86-4a92-a84d-13e0777d496a"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("9a83f754-7410-4248-ad5d-ef75ca92d5f6"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("c9980811-73e1-4869-b942-e66ec16bf0fc"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("526b2298-ecac-432c-bcc8-896da6df4826"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("78910528-3642-41af-96e3-87c96028e905"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("cb30dd16-06f0-457a-b536-bad78a04ca0a"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("40fce42a-91a9-4975-85dc-6bde96d7ac49"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("725273db-f0a2-468e-af8b-6d8fe208d283"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("b73a6fe3-dc27-4b00-8f80-af1960f9e6c6"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("75e260e7-26c7-43c1-a091-dcd5a1b6a457"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("8b2267d3-c73f-4084-8f12-841d911628ba"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("ba7b41ea-d486-4052-a9fa-74b105e179d1"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("a91af553-7cd7-4864-ad95-ed054af0ed6d"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("d04851da-eed6-4676-a187-2ccc43b14a28"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("dd0e8aa6-7e8c-40d5-a995-774b866c4f6a"));

            migrationBuilder.CreateTable(
                name: "AdjustmentTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdjustmentTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdjustmentTypes", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdjustmentTypes");

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

            migrationBuilder.InsertData(
                table: "ApplicableTaxs",
                columns: new[] { "Id", "ApplicableTaxName" },
                values: new object[,]
                {
                    { new Guid("7226f718-2d19-4fb2-9bee-586281257df4"), "Fruits" },
                    { new Guid("90a51ecd-95f4-4e92-9efb-09b31a1109a4"), "Food" },
                    { new Guid("911cdef1-6ed6-4350-a917-380cf91db201"), "Sales Tax" }
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "BarcodeTypeName" },
                values: new object[,]
                {
                    { new Guid("29b75b7c-5fd8-44c5-9145-a8b2ee554c69"), "QR Code" },
                    { new Guid("79aecfcf-1f84-4a3d-a8c7-83ad6fd71915"), "NFC" },
                    { new Guid("8e57fb64-edd0-46d1-822e-23a9c4c43007"), "UPC" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BandOrigin", "BrandName", "Description" },
                values: new object[,]
                {
                    { new Guid("04a402d6-9b94-4abb-b4cd-572c5980ad5c"), "", "Samsung", "" },
                    { new Guid("8f8ee098-f7ba-496c-94b9-b89527289150"), "", "Apple", "" },
                    { new Guid("a3e7eae3-7119-47d6-9929-1d161cb936b0"), "", "Sony", "" }
                });

            migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "Address", "City", "Country", "LocationName", "State", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("4faa4a52-6b9a-4e16-aeea-eca6f6415cf5"), "", "", "", "Downtown Store", "", "" },
                    { new Guid("a1205f00-10f0-470c-b8bb-afdb8752846a"), "", "", "", "Warehouse A", "", "" },
                    { new Guid("e0d5f778-e519-461d-af18-828444fc9383"), "", "", "", "Warehouse B", "", "" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryCode", "CategoryName", "Description" },
                values: new object[,]
                {
                    { new Guid("19753583-7641-4763-ae49-bfb725bb22ab"), "", "Clothing", "" },
                    { new Guid("5d2c6a3e-d22f-4997-a11c-0c224331f8fa"), "", "Home Appliances", "" },
                    { new Guid("7c6bde57-245f-4b6d-9322-6aa392766714"), "", "Electronics", "" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("0868600f-12e7-4fac-80bd-25000c73fc6c"), "Food" },
                    { new Guid("1a04f119-b91c-46d7-966a-7139d497810a"), "Electronics" },
                    { new Guid("37f52d88-0f86-4a92-a84d-13e0777d496a"), "Toys" },
                    { new Guid("9a83f754-7410-4248-ad5d-ef75ca92d5f6"), "Furniture" },
                    { new Guid("c9980811-73e1-4869-b942-e66ec16bf0fc"), "Clothing" }
                });

            migrationBuilder.InsertData(
                table: "SellingPriceTaxes",
                columns: new[] { "Id", "SellingPriceTaxName" },
                values: new object[,]
                {
                    { new Guid("526b2298-ecac-432c-bcc8-896da6df4826"), "Exclusive" },
                    { new Guid("78910528-3642-41af-96e3-87c96028e905"), "Zero Rate" },
                    { new Guid("cb30dd16-06f0-457a-b536-bad78a04ca0a"), "Inclusive" }
                });

            migrationBuilder.InsertData(
                table: "Subcategories",
                columns: new[] { "Id", "SubcategoryName" },
                values: new object[,]
                {
                    { new Guid("40fce42a-91a9-4975-85dc-6bde96d7ac49"), "Televisions" },
                    { new Guid("725273db-f0a2-468e-af8b-6d8fe208d283"), "Smartphones" },
                    { new Guid("b73a6fe3-dc27-4b00-8f80-af1960f9e6c6"), "Laptops" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "AllowDecimal", "ShortName", "UnitName" },
                values: new object[,]
                {
                    { new Guid("75e260e7-26c7-43c1-a091-dcd5a1b6a457"), 0, "", "Liter" },
                    { new Guid("8b2267d3-c73f-4084-8f12-841d911628ba"), 0, "", "Piece" },
                    { new Guid("ba7b41ea-d486-4052-a9fa-74b105e179d1"), 0, "", "Kilogram" }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "Description", "Name", "WarrantyDuration" },
                values: new object[,]
                {
                    { new Guid("a91af553-7cd7-4864-ad95-ed054af0ed6d"), "", "", "3 Years" },
                    { new Guid("d04851da-eed6-4676-a187-2ccc43b14a28"), "", "", "1 Year" },
                    { new Guid("dd0e8aa6-7e8c-40d5-a995-774b866c4f6a"), "", "", "2 Years" }
                });
        }
    }
}
