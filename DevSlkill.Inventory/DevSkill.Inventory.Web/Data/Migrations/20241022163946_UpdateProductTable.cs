using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DevSkill.Inventory.Web.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("4a364ed1-c560-418b-be3f-6bea8db7657c"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("c9bf012b-dab4-4ae0-8d6c-bec68d68d324"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("e28d6c4d-96f3-493a-a65d-440003dd4e6a"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("1d51f2a6-fc1b-41f2-b5e9-9afeafb4f608"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("4430a385-5bfa-4fe0-a17b-923b9f12dd69"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("57c50767-7ee6-41af-996e-d22a91955647"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("0a48ae64-8702-4efb-8587-6869a0d9bfaa"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("4c55fa81-4cfc-47a8-90f9-9457b4a891eb"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("80ea14f5-0b56-4e3f-b96e-459adf684ed9"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("0f9841c7-e474-4bc0-b124-9b03e11502f9"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("15ccf6b2-d39b-478a-bde8-ce301c5175b4"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("56216776-5432-419e-a88b-3d6119c63c10"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0e063468-d1f5-4763-93bb-645d1b05801b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("3eb9b92f-4022-47d1-940c-47b895305ee2"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("665723d3-d4ba-4ccd-8615-a32a54b4997b"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("3eff3f20-cf30-4c74-acf3-4cfffac1f72a"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("4a91db4d-f625-4377-b2a5-644d7f0daa28"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("53837b60-5e79-4bdc-9abe-7baab6a35eee"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("59ea26ba-ed6f-4c2a-8733-ea973c0a783d"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("8beb7f68-7e78-4b0a-a61a-94ef75f47801"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("2b0ef55d-3612-4731-b1f6-8af4c9211a77"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("52fe2be9-4067-4712-8400-83c59c49e35c"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("ee175ad2-ff77-4f0e-b5db-7c56129197c4"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("33c16214-4aea-459f-a981-a6f519b2bc1f"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("56426fc9-838e-4a53-acf5-c2861bbfd22b"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("9a4317e8-bd58-42f4-8009-34094a744cb8"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("035eb71a-1ef7-45f4-9e2a-13a81dbff751"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("194e1c66-d7b8-43e1-93d0-24675a5e6032"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("294141a8-17ce-4965-89eb-57e4350d82a2"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("249d2cf3-2f8c-4427-99c8-32fa602a1aae"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("7d11a25e-3186-4fe4-b057-fe3aeb26bbc2"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("b0db0d75-04a8-4833-85e9-08cd4bc5d831"));

            migrationBuilder.AddColumn<int>(
                name: "CurrentStock",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "CurrentStock",
                table: "Products");

            migrationBuilder.InsertData(
                table: "ApplicableTaxs",
                columns: new[] { "Id", "ApplicableTaxName" },
                values: new object[,]
                {
                    { new Guid("4a364ed1-c560-418b-be3f-6bea8db7657c"), "Sales Tax" },
                    { new Guid("c9bf012b-dab4-4ae0-8d6c-bec68d68d324"), "Food" },
                    { new Guid("e28d6c4d-96f3-493a-a65d-440003dd4e6a"), "Fruits" }
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "BarcodeTypeName" },
                values: new object[,]
                {
                    { new Guid("1d51f2a6-fc1b-41f2-b5e9-9afeafb4f608"), "QR Code" },
                    { new Guid("4430a385-5bfa-4fe0-a17b-923b9f12dd69"), "NFC" },
                    { new Guid("57c50767-7ee6-41af-996e-d22a91955647"), "UPC" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BandOrigin", "BrandName", "Description" },
                values: new object[,]
                {
                    { new Guid("0a48ae64-8702-4efb-8587-6869a0d9bfaa"), "", "Samsung", "" },
                    { new Guid("4c55fa81-4cfc-47a8-90f9-9457b4a891eb"), "", "Apple", "" },
                    { new Guid("80ea14f5-0b56-4e3f-b96e-459adf684ed9"), "", "Sony", "" }
                });

            migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "Address", "City", "Country", "LocationName", "State", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("0f9841c7-e474-4bc0-b124-9b03e11502f9"), "", "", "", "Warehouse B", "", "" },
                    { new Guid("15ccf6b2-d39b-478a-bde8-ce301c5175b4"), "", "", "", "Downtown Store", "", "" },
                    { new Guid("56216776-5432-419e-a88b-3d6119c63c10"), "", "", "", "Warehouse A", "", "" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryCode", "CategoryName", "Description" },
                values: new object[,]
                {
                    { new Guid("0e063468-d1f5-4763-93bb-645d1b05801b"), "", "Electronics", "" },
                    { new Guid("3eb9b92f-4022-47d1-940c-47b895305ee2"), "", "Home Appliances", "" },
                    { new Guid("665723d3-d4ba-4ccd-8615-a32a54b4997b"), "", "Clothing", "" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("3eff3f20-cf30-4c74-acf3-4cfffac1f72a"), "Food" },
                    { new Guid("4a91db4d-f625-4377-b2a5-644d7f0daa28"), "Clothing" },
                    { new Guid("53837b60-5e79-4bdc-9abe-7baab6a35eee"), "Electronics" },
                    { new Guid("59ea26ba-ed6f-4c2a-8733-ea973c0a783d"), "Toys" },
                    { new Guid("8beb7f68-7e78-4b0a-a61a-94ef75f47801"), "Furniture" }
                });

            migrationBuilder.InsertData(
                table: "SellingPriceTaxes",
                columns: new[] { "Id", "SellingPriceTaxName" },
                values: new object[,]
                {
                    { new Guid("2b0ef55d-3612-4731-b1f6-8af4c9211a77"), "Zero Rate" },
                    { new Guid("52fe2be9-4067-4712-8400-83c59c49e35c"), "Inclusive" },
                    { new Guid("ee175ad2-ff77-4f0e-b5db-7c56129197c4"), "Exclusive" }
                });

            migrationBuilder.InsertData(
                table: "Subcategories",
                columns: new[] { "Id", "SubcategoryName" },
                values: new object[,]
                {
                    { new Guid("33c16214-4aea-459f-a981-a6f519b2bc1f"), "Laptops" },
                    { new Guid("56426fc9-838e-4a53-acf5-c2861bbfd22b"), "Televisions" },
                    { new Guid("9a4317e8-bd58-42f4-8009-34094a744cb8"), "Smartphones" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "AllowDecimal", "ShortName", "UnitName" },
                values: new object[,]
                {
                    { new Guid("035eb71a-1ef7-45f4-9e2a-13a81dbff751"), 0, "", "Liter" },
                    { new Guid("194e1c66-d7b8-43e1-93d0-24675a5e6032"), 0, "", "Piece" },
                    { new Guid("294141a8-17ce-4965-89eb-57e4350d82a2"), 0, "", "Kilogram" }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "Description", "Name", "WarrantyDuration" },
                values: new object[,]
                {
                    { new Guid("249d2cf3-2f8c-4427-99c8-32fa602a1aae"), "", "", "1 Year" },
                    { new Guid("7d11a25e-3186-4fe4-b057-fe3aeb26bbc2"), "", "", "2 Years" },
                    { new Guid("b0db0d75-04a8-4833-85e9-08cd4bc5d831"), "", "", "3 Years" }
                });
        }
    }
}
