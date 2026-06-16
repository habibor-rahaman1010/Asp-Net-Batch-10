using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DevSkill.Inventory.Web.Migrations.InventoryDb
{
    /// <inheritdoc />
    public partial class updateAdjustmentType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("15debf80-fec9-4580-bcee-a32855d3e969"));

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("fe128d77-806b-420b-8b56-07afdf5896fc"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("8278ae62-7ada-428e-845b-4dc85b983df4"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("a584bc2c-9cc9-4591-849b-168604d0f7a4"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("a60ed174-ea04-40f3-9a2a-71ad13c01eea"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("499e6fd8-5846-4bc1-8c45-72fd73917aef"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("a8e1ae1c-a30c-412b-9415-321cea2a6663"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("fd5fa9e7-e522-4198-8d7e-4f9264bd2519"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("0158d0bb-d526-42c4-accf-d9b87f3512de"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("2c2c3ed1-b21e-4af9-87d4-c69703c3f0da"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("3d25c2a0-cd98-4b8c-9fe6-6cd0c7a28159"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("33f415a0-c5c2-4e1c-ae13-5744f5870637"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("3e641efd-f889-4aee-b3d9-b9147af7991b"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("6d1db533-7019-40f5-abce-420cb0b0b21c"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("38c818ad-2fc4-49b6-96dc-8e467aed7135"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("49623036-76dc-4826-a6c2-5d3bb21a5029"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("8b75f574-a66d-46ff-a467-a23c89e7994b"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("182d30b8-59fe-4274-a1d2-2914a995c2dd"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("27c2a37f-c260-44f4-ac90-763599b65a56"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("aa4ed8ed-bac7-4e59-8c12-5a53fc07eae8"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("c59c430b-6c6c-48d0-bfa9-2397506357de"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("ddab800b-8daf-42bd-ae7e-f314519d8255"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("7ba138a8-7ed1-4e31-a2ac-54154c532c40"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("a88e347f-8fba-44ed-b8b0-8df3b301ffe2"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("e742260d-9dee-41db-80d7-bdc8f7b145d3"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("30cd2468-bf7e-4fcb-b455-883c628a5561"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("ca244c8d-3869-41c5-b16f-cd631b552622"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("d2e6c033-2b8e-4c66-935c-7b825502af55"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("51c23730-bd5f-46d2-8130-f8dbd353f4e5"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("a531bad5-b9df-4c1f-b472-44f55cf52386"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("de79a95c-ca31-44d9-9c03-69114c0c67b6"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("32d4cc19-b652-487c-8bde-75fbdbc95d0c"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("78640e14-80e0-4d82-9947-d41ca946cf0c"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("eb30528c-6bc8-456d-ae09-237709752a12"));

            migrationBuilder.AddColumn<int>(
                name: "Sign",
                table: "AdjustmentTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AdjustmentTypes",
                columns: new[] { "Id", "AdjustmentTypeName", "Description", "Sign" },
                values: new object[,]
                {
                    { new Guid("0e95c8a8-56d8-4b6d-aa0e-0399b02dfd68"), "Abnormal", "", 0 },
                    { new Guid("94552087-4787-4ee1-961b-0ec173672f64"), "Normal", "", 0 }
                });

            migrationBuilder.InsertData(
                table: "ApplicableTaxs",
                columns: new[] { "Id", "ApplicableTaxName", "Description", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("14ebbc47-26b5-4eb4-b0e2-01fb1b420c5d"), "Fruits", "", 0m },
                    { new Guid("2e8ea20b-ea15-49ad-a4c6-b8952dd8cc29"), "Sales Tax", "", 0m },
                    { new Guid("424e16a3-a54b-4cc3-8de3-757c3d431cfc"), "Food", "", 0m }
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "BarcodeDescription", "BarcodeTypeCode", "BarcodeTypeName" },
                values: new object[,]
                {
                    { new Guid("3e410890-e992-4477-b437-e085ce0a72dd"), "", "", "NFC" },
                    { new Guid("450c3e3e-9aa6-425f-a6d6-68c5322990f4"), "", "", "QR Code" },
                    { new Guid("7fb72fad-0be3-426f-a37f-1af1b777d4de"), "", "", "UPC" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BandOrigin", "BrandName", "Description" },
                values: new object[,]
                {
                    { new Guid("874b8737-f318-46f4-ae22-e011cc4bcf81"), "", "Sony", "" },
                    { new Guid("9b594b20-add3-4a79-ab53-5a02acf2bf44"), "", "Apple", "" },
                    { new Guid("b1b7ecc8-bcfe-4973-9934-76ddfb0548f2"), "", "Samsung", "" }
                });

            migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "Address", "City", "Country", "LocationName", "State", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("2b34b2f4-ae14-4dd8-aa13-0660789c0677"), "", "", "", "Warehouse B", "", "" },
                    { new Guid("39c70e6c-98ce-4376-a771-9809f16d5c73"), "", "", "", "Downtown Store", "", "" },
                    { new Guid("496c7748-81be-4c28-8dea-6b4ddfed28ae"), "", "", "", "Warehouse A", "", "" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryCode", "CategoryName", "Description" },
                values: new object[,]
                {
                    { new Guid("37f5ce09-d1fc-4ea0-9687-60e2fdb037ae"), "", "Home Appliances", "" },
                    { new Guid("96e3a93f-e8dd-4f09-8cec-bc9159b2936d"), "", "Electronics", "" },
                    { new Guid("c334fd05-99f3-4195-a8ef-96d3d57ef2f6"), "", "Clothing", "" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Description", "ProductTypeCode", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("800b233c-dbb5-4476-a083-f6a75e0e2a8f"), "", "", "Toys" },
                    { new Guid("a220ff47-13c9-42be-a446-b48c94687df9"), "", "", "Electronics" },
                    { new Guid("a602aa60-5bc3-4c29-b765-f641d93bc3e6"), "", "", "Clothing" },
                    { new Guid("fb7b1581-2cdd-44d7-bd41-890fb6aaedfd"), "", "", "Food" },
                    { new Guid("fe7c0a48-917b-4dc7-ad27-229205ac5a9e"), "", "", "Furniture" }
                });

            migrationBuilder.InsertData(
                table: "SellingPriceTaxes",
                columns: new[] { "Id", "Description", "SellingPriceTaxName", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("29da6c47-2468-418c-b855-f6805a20a444"), "", "Inclusive", 0m },
                    { new Guid("95e9c725-47c5-4f00-be99-22c5a1f374b9"), "", "Exclusive", 0m },
                    { new Guid("e5c97a11-be49-4689-a149-8c27c51f6452"), "", "Zero Rate", 0m }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryCode", "Description", "SubCategoryName" },
                values: new object[,]
                {
                    { new Guid("1bf86a14-8455-4e60-9565-f5cc672f344a"), "", "", "Smartphones" },
                    { new Guid("4dcce627-cbdb-4cc7-bf81-d92eaa767677"), "", "", "Laptops" },
                    { new Guid("efd97eae-90ac-425b-8277-d3b9b929a65a"), "", "", "Televisions" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "AllowDecimal", "ShortName", "UnitName" },
                values: new object[,]
                {
                    { new Guid("5fb7be51-af4a-4fc8-8e5c-54dbd6219dc3"), 0, "", "Liter" },
                    { new Guid("b846a1d1-1466-43a8-83d8-866f4476ba52"), 0, "", "Piece" },
                    { new Guid("f75b9b6f-5e3d-4273-b325-9c68a9c6e534"), 0, "", "Kilogram" }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "Description", "Name", "WarrantyDuration" },
                values: new object[,]
                {
                    { new Guid("5ca6e1cd-98c6-4a20-8cdd-8e2bc503c94d"), "", "", "2 Years" },
                    { new Guid("9f435e79-95d1-471b-bb09-4efaa8825655"), "", "", "3 Years" },
                    { new Guid("b6463d97-3216-43bb-aec5-41bf119a894a"), "", "", "1 Year" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("0e95c8a8-56d8-4b6d-aa0e-0399b02dfd68"));

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("94552087-4787-4ee1-961b-0ec173672f64"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("14ebbc47-26b5-4eb4-b0e2-01fb1b420c5d"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("2e8ea20b-ea15-49ad-a4c6-b8952dd8cc29"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("424e16a3-a54b-4cc3-8de3-757c3d431cfc"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("3e410890-e992-4477-b437-e085ce0a72dd"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("450c3e3e-9aa6-425f-a6d6-68c5322990f4"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("7fb72fad-0be3-426f-a37f-1af1b777d4de"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("874b8737-f318-46f4-ae22-e011cc4bcf81"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("9b594b20-add3-4a79-ab53-5a02acf2bf44"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("b1b7ecc8-bcfe-4973-9934-76ddfb0548f2"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("2b34b2f4-ae14-4dd8-aa13-0660789c0677"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("39c70e6c-98ce-4376-a771-9809f16d5c73"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("496c7748-81be-4c28-8dea-6b4ddfed28ae"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("37f5ce09-d1fc-4ea0-9687-60e2fdb037ae"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("96e3a93f-e8dd-4f09-8cec-bc9159b2936d"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c334fd05-99f3-4195-a8ef-96d3d57ef2f6"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("800b233c-dbb5-4476-a083-f6a75e0e2a8f"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("a220ff47-13c9-42be-a446-b48c94687df9"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("a602aa60-5bc3-4c29-b765-f641d93bc3e6"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("fb7b1581-2cdd-44d7-bd41-890fb6aaedfd"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("fe7c0a48-917b-4dc7-ad27-229205ac5a9e"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("29da6c47-2468-418c-b855-f6805a20a444"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("95e9c725-47c5-4f00-be99-22c5a1f374b9"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("e5c97a11-be49-4689-a149-8c27c51f6452"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("1bf86a14-8455-4e60-9565-f5cc672f344a"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("4dcce627-cbdb-4cc7-bf81-d92eaa767677"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("efd97eae-90ac-425b-8277-d3b9b929a65a"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("5fb7be51-af4a-4fc8-8e5c-54dbd6219dc3"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("b846a1d1-1466-43a8-83d8-866f4476ba52"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("f75b9b6f-5e3d-4273-b325-9c68a9c6e534"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("5ca6e1cd-98c6-4a20-8cdd-8e2bc503c94d"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("9f435e79-95d1-471b-bb09-4efaa8825655"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("b6463d97-3216-43bb-aec5-41bf119a894a"));

            migrationBuilder.DropColumn(
                name: "Sign",
                table: "AdjustmentTypes");

            migrationBuilder.InsertData(
                table: "AdjustmentTypes",
                columns: new[] { "Id", "AdjustmentTypeName", "Description" },
                values: new object[,]
                {
                    { new Guid("15debf80-fec9-4580-bcee-a32855d3e969"), "Normal", "" },
                    { new Guid("fe128d77-806b-420b-8b56-07afdf5896fc"), "Abnormal", "" }
                });

            migrationBuilder.InsertData(
                table: "ApplicableTaxs",
                columns: new[] { "Id", "ApplicableTaxName", "Description", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("8278ae62-7ada-428e-845b-4dc85b983df4"), "Food", "", 0m },
                    { new Guid("a584bc2c-9cc9-4591-849b-168604d0f7a4"), "Sales Tax", "", 0m },
                    { new Guid("a60ed174-ea04-40f3-9a2a-71ad13c01eea"), "Fruits", "", 0m }
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "BarcodeDescription", "BarcodeTypeCode", "BarcodeTypeName" },
                values: new object[,]
                {
                    { new Guid("499e6fd8-5846-4bc1-8c45-72fd73917aef"), "", "", "NFC" },
                    { new Guid("a8e1ae1c-a30c-412b-9415-321cea2a6663"), "", "", "UPC" },
                    { new Guid("fd5fa9e7-e522-4198-8d7e-4f9264bd2519"), "", "", "QR Code" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BandOrigin", "BrandName", "Description" },
                values: new object[,]
                {
                    { new Guid("0158d0bb-d526-42c4-accf-d9b87f3512de"), "", "Sony", "" },
                    { new Guid("2c2c3ed1-b21e-4af9-87d4-c69703c3f0da"), "", "Samsung", "" },
                    { new Guid("3d25c2a0-cd98-4b8c-9fe6-6cd0c7a28159"), "", "Apple", "" }
                });

            migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "Address", "City", "Country", "LocationName", "State", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("33f415a0-c5c2-4e1c-ae13-5744f5870637"), "", "", "", "Warehouse A", "", "" },
                    { new Guid("3e641efd-f889-4aee-b3d9-b9147af7991b"), "", "", "", "Warehouse B", "", "" },
                    { new Guid("6d1db533-7019-40f5-abce-420cb0b0b21c"), "", "", "", "Downtown Store", "", "" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryCode", "CategoryName", "Description" },
                values: new object[,]
                {
                    { new Guid("38c818ad-2fc4-49b6-96dc-8e467aed7135"), "", "Home Appliances", "" },
                    { new Guid("49623036-76dc-4826-a6c2-5d3bb21a5029"), "", "Clothing", "" },
                    { new Guid("8b75f574-a66d-46ff-a467-a23c89e7994b"), "", "Electronics", "" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Description", "ProductTypeCode", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("182d30b8-59fe-4274-a1d2-2914a995c2dd"), "", "", "Electronics" },
                    { new Guid("27c2a37f-c260-44f4-ac90-763599b65a56"), "", "", "Food" },
                    { new Guid("aa4ed8ed-bac7-4e59-8c12-5a53fc07eae8"), "", "", "Furniture" },
                    { new Guid("c59c430b-6c6c-48d0-bfa9-2397506357de"), "", "", "Toys" },
                    { new Guid("ddab800b-8daf-42bd-ae7e-f314519d8255"), "", "", "Clothing" }
                });

            migrationBuilder.InsertData(
                table: "SellingPriceTaxes",
                columns: new[] { "Id", "Description", "SellingPriceTaxName", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("7ba138a8-7ed1-4e31-a2ac-54154c532c40"), "", "Inclusive", 0m },
                    { new Guid("a88e347f-8fba-44ed-b8b0-8df3b301ffe2"), "", "Zero Rate", 0m },
                    { new Guid("e742260d-9dee-41db-80d7-bdc8f7b145d3"), "", "Exclusive", 0m }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryCode", "Description", "SubCategoryName" },
                values: new object[,]
                {
                    { new Guid("30cd2468-bf7e-4fcb-b455-883c628a5561"), "", "", "Laptops" },
                    { new Guid("ca244c8d-3869-41c5-b16f-cd631b552622"), "", "", "Televisions" },
                    { new Guid("d2e6c033-2b8e-4c66-935c-7b825502af55"), "", "", "Smartphones" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "AllowDecimal", "ShortName", "UnitName" },
                values: new object[,]
                {
                    { new Guid("51c23730-bd5f-46d2-8130-f8dbd353f4e5"), 0, "", "Kilogram" },
                    { new Guid("a531bad5-b9df-4c1f-b472-44f55cf52386"), 0, "", "Liter" },
                    { new Guid("de79a95c-ca31-44d9-9c03-69114c0c67b6"), 0, "", "Piece" }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "Description", "Name", "WarrantyDuration" },
                values: new object[,]
                {
                    { new Guid("32d4cc19-b652-487c-8bde-75fbdbc95d0c"), "", "", "2 Years" },
                    { new Guid("78640e14-80e0-4d82-9947-d41ca946cf0c"), "", "", "1 Year" },
                    { new Guid("eb30528c-6bc8-456d-ae09-237709752a12"), "", "", "3 Years" }
                });
        }
    }
}
