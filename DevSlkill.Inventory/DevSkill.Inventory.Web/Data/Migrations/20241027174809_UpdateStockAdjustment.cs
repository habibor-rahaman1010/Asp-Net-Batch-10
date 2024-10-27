using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DevSkill.Inventory.Web.Migrations.InventoryDb
{
    /// <inheritdoc />
    public partial class UpdateStockAdjustment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("a7131d2d-b1f7-44b9-9420-39a338d298c8"));

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("f645ab5f-ea09-4f60-bc89-968a0a9c18f0"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("489f4ea8-dbfa-4b16-842d-eebd12329ea7"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("521bcaa5-9942-4ec4-b463-5520116a5c5d"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("74dedfb2-1745-48f1-bbb5-ab84f5d7ad13"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("0cfb8134-7908-49a4-88bc-7b7f957b106d"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("82e74bd5-d873-4a29-9d3d-6e13f65e7141"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("c58f00a1-c87d-434b-a93b-f0dc932e8a12"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("51073944-a59a-460a-9ffb-363c3ff70ac3"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("d67bfa8b-6bf0-4550-94de-9ebcc2908183"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("e49dfaa9-98bd-4e39-991b-911fe1348957"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("0cc0453b-6389-404f-976b-d9f644db948e"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("6a2f127e-99a6-4360-84be-60d24d0d3125"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("cf623c49-cfb6-464a-aeea-aad9a29a702d"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("4e58d95a-61a5-46ca-b94a-8a086c34cdb0"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("75071d11-5108-40c0-a2bd-0e80460f5a38"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("fba2b3af-02d8-4499-aee5-3dcbb63f7f60"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("430fa059-3c9d-4d96-83a7-c75279de2987"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("507f69eb-e069-4882-9fa2-7422bc0e7a4e"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("87f3c54c-008d-4dbd-a82d-9d9d3c201d1d"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("94133dd6-6923-44ca-bdef-0a3a2e930371"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("b264f331-a34b-4a2e-8714-f937dada8e82"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("02bbf818-a36d-4232-b5d1-76f525993437"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("27c8b773-0fd4-440c-b45d-91e70283965a"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("9b0e1c2e-fdde-4ca0-bbfd-deeb877e9efb"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("1e5c564b-45fb-4c5c-a85f-f293e09a8d3c"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("411b1aad-8cb6-4c7d-9311-96d2c27a2627"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("777bb31d-d0fd-4c51-bee1-594daa91458b"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("78b51511-8ea1-47e6-9d9e-3529907392ff"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("7dcdc71d-b130-452b-9a03-9e7b63ace9ab"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("eb9c10f0-7b68-4bdd-9952-efe2ca9afa4f"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("20fcd225-cb0d-40a7-bf01-a9c2123656ae"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("7713531b-8b20-4ed4-b1b7-176b613d6abe"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("d9b2dd31-2c03-47a6-adf3-1ee82380666b"));

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "StockAdjustments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "StockAdjustments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "AdjustmentTypes",
                columns: new[] { "Id", "AdjustmentTypeName", "Description" },
                values: new object[,]
                {
                    { new Guid("54792645-57e8-4a94-bc80-c5931ed91228"), "Normal", "" },
                    { new Guid("fc0eec7c-3a7d-4141-ad42-1bee66037097"), "Abnormal", "" }
                });

            migrationBuilder.InsertData(
                table: "ApplicableTaxs",
                columns: new[] { "Id", "ApplicableTaxName" },
                values: new object[,]
                {
                    { new Guid("3572df64-ff01-4637-9eb0-4a0757fb8731"), "Food" },
                    { new Guid("bff23033-963c-4720-875a-3c7e9750b81d"), "Fruits" },
                    { new Guid("c5b9941a-bfba-46dd-85f7-0159baacd1d0"), "Sales Tax" }
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "BarcodeTypeName" },
                values: new object[,]
                {
                    { new Guid("762bbf50-e878-490c-b9ba-90c1ebf5a1d1"), "NFC" },
                    { new Guid("dd7cef1d-a2e0-4645-9320-c8a8e0651ef1"), "QR Code" },
                    { new Guid("e7b56a10-0e8f-47ca-a1d2-0f6885c26d8e"), "UPC" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BandOrigin", "BrandName", "Description" },
                values: new object[,]
                {
                    { new Guid("08ae359f-fdde-4cfc-b2f8-d60a29cc4612"), "", "Apple", "" },
                    { new Guid("5601e854-a462-4312-a3f6-e64e3de1fce9"), "", "Samsung", "" },
                    { new Guid("a0e2f81c-6f8b-461e-88b2-14126b184d44"), "", "Sony", "" }
                });

            migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "Address", "City", "Country", "LocationName", "State", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("263e5560-4837-43ff-aecf-2c22fa867086"), "", "", "", "Warehouse B", "", "" },
                    { new Guid("5bf3c7a8-40c0-438e-bc4c-f7697499b1b5"), "", "", "", "Downtown Store", "", "" },
                    { new Guid("b20da384-3949-4a89-814a-5188d9bae71d"), "", "", "", "Warehouse A", "", "" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryCode", "CategoryName", "Description" },
                values: new object[,]
                {
                    { new Guid("0eb9011d-273a-4fa6-9d50-9e472e20f418"), "", "Clothing", "" },
                    { new Guid("3fce2bd6-ea00-4f96-819a-e67fd8a1ffb5"), "", "Home Appliances", "" },
                    { new Guid("de407b86-9335-47c0-99dc-9f4e8b4b977f"), "", "Electronics", "" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("1a2b902e-6992-4ae3-bf0e-43e94232e8a7"), "Furniture" },
                    { new Guid("4e32b203-2676-49ec-9945-74a06f359a0c"), "Clothing" },
                    { new Guid("7a187548-be20-4a7b-b983-e818d36e466b"), "Electronics" },
                    { new Guid("a0096383-9930-4276-a632-70914631c810"), "Food" },
                    { new Guid("d8639faf-0c5a-45ca-ae48-2e5f963807fc"), "Toys" }
                });

            migrationBuilder.InsertData(
                table: "SellingPriceTaxes",
                columns: new[] { "Id", "SellingPriceTaxName" },
                values: new object[,]
                {
                    { new Guid("2949cf75-d4b1-4be4-b567-7d13bafb38c7"), "Zero Rate" },
                    { new Guid("4195024b-3e25-45d7-83a0-5f8d2a06cd98"), "Exclusive" },
                    { new Guid("64bb2ac7-88b7-4a5d-8a2a-0bb70a4000ff"), "Inclusive" }
                });

            migrationBuilder.InsertData(
                table: "Subcategories",
                columns: new[] { "Id", "SubcategoryName" },
                values: new object[,]
                {
                    { new Guid("4e1b482a-e20f-418e-b7d6-4df28b8e3ab1"), "Laptops" },
                    { new Guid("cfdff4e8-e533-42d7-a739-b2ec75cdfb8a"), "Televisions" },
                    { new Guid("fdb82154-1c68-410a-af4a-b8911b74c3db"), "Smartphones" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "AllowDecimal", "ShortName", "UnitName" },
                values: new object[,]
                {
                    { new Guid("1e4b2455-d358-4850-9567-c63208ce6d5e"), 0, "", "Kilogram" },
                    { new Guid("45fff205-d3a2-4137-bcb6-10ef8cbe1d66"), 0, "", "Piece" },
                    { new Guid("aa3bb79e-9b6d-4b93-bf3c-2205ac5b57d2"), 0, "", "Liter" }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "Description", "Name", "WarrantyDuration" },
                values: new object[,]
                {
                    { new Guid("094d79d7-dfb1-430a-8836-c0049db07b5c"), "", "", "1 Year" },
                    { new Guid("198988fb-fe32-42ff-a458-fe68f6db8f72"), "", "", "2 Years" },
                    { new Guid("7b23902d-39ff-409d-9cd7-26bd9fb4a0bc"), "", "", "3 Years" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("54792645-57e8-4a94-bc80-c5931ed91228"));

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("fc0eec7c-3a7d-4141-ad42-1bee66037097"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("3572df64-ff01-4637-9eb0-4a0757fb8731"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("bff23033-963c-4720-875a-3c7e9750b81d"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("c5b9941a-bfba-46dd-85f7-0159baacd1d0"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("762bbf50-e878-490c-b9ba-90c1ebf5a1d1"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("dd7cef1d-a2e0-4645-9320-c8a8e0651ef1"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("e7b56a10-0e8f-47ca-a1d2-0f6885c26d8e"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("08ae359f-fdde-4cfc-b2f8-d60a29cc4612"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("5601e854-a462-4312-a3f6-e64e3de1fce9"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("a0e2f81c-6f8b-461e-88b2-14126b184d44"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("263e5560-4837-43ff-aecf-2c22fa867086"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("5bf3c7a8-40c0-438e-bc4c-f7697499b1b5"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("b20da384-3949-4a89-814a-5188d9bae71d"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0eb9011d-273a-4fa6-9d50-9e472e20f418"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("3fce2bd6-ea00-4f96-819a-e67fd8a1ffb5"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("de407b86-9335-47c0-99dc-9f4e8b4b977f"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("1a2b902e-6992-4ae3-bf0e-43e94232e8a7"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("4e32b203-2676-49ec-9945-74a06f359a0c"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("7a187548-be20-4a7b-b983-e818d36e466b"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("a0096383-9930-4276-a632-70914631c810"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("d8639faf-0c5a-45ca-ae48-2e5f963807fc"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("2949cf75-d4b1-4be4-b567-7d13bafb38c7"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("4195024b-3e25-45d7-83a0-5f8d2a06cd98"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("64bb2ac7-88b7-4a5d-8a2a-0bb70a4000ff"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("4e1b482a-e20f-418e-b7d6-4df28b8e3ab1"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("cfdff4e8-e533-42d7-a739-b2ec75cdfb8a"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("fdb82154-1c68-410a-af4a-b8911b74c3db"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("1e4b2455-d358-4850-9567-c63208ce6d5e"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("45fff205-d3a2-4137-bcb6-10ef8cbe1d66"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("aa3bb79e-9b6d-4b93-bf3c-2205ac5b57d2"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("094d79d7-dfb1-430a-8836-c0049db07b5c"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("198988fb-fe32-42ff-a458-fe68f6db8f72"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("7b23902d-39ff-409d-9cd7-26bd9fb4a0bc"));

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "StockAdjustments");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "StockAdjustments");

            migrationBuilder.InsertData(
                table: "AdjustmentTypes",
                columns: new[] { "Id", "AdjustmentTypeName", "Description" },
                values: new object[,]
                {
                    { new Guid("a7131d2d-b1f7-44b9-9420-39a338d298c8"), "Abnormal", "" },
                    { new Guid("f645ab5f-ea09-4f60-bc89-968a0a9c18f0"), "Normal", "" }
                });

            migrationBuilder.InsertData(
                table: "ApplicableTaxs",
                columns: new[] { "Id", "ApplicableTaxName" },
                values: new object[,]
                {
                    { new Guid("489f4ea8-dbfa-4b16-842d-eebd12329ea7"), "Sales Tax" },
                    { new Guid("521bcaa5-9942-4ec4-b463-5520116a5c5d"), "Food" },
                    { new Guid("74dedfb2-1745-48f1-bbb5-ab84f5d7ad13"), "Fruits" }
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "BarcodeTypeName" },
                values: new object[,]
                {
                    { new Guid("0cfb8134-7908-49a4-88bc-7b7f957b106d"), "NFC" },
                    { new Guid("82e74bd5-d873-4a29-9d3d-6e13f65e7141"), "QR Code" },
                    { new Guid("c58f00a1-c87d-434b-a93b-f0dc932e8a12"), "UPC" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BandOrigin", "BrandName", "Description" },
                values: new object[,]
                {
                    { new Guid("51073944-a59a-460a-9ffb-363c3ff70ac3"), "", "Samsung", "" },
                    { new Guid("d67bfa8b-6bf0-4550-94de-9ebcc2908183"), "", "Sony", "" },
                    { new Guid("e49dfaa9-98bd-4e39-991b-911fe1348957"), "", "Apple", "" }
                });

            migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "Address", "City", "Country", "LocationName", "State", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("0cc0453b-6389-404f-976b-d9f644db948e"), "", "", "", "Warehouse A", "", "" },
                    { new Guid("6a2f127e-99a6-4360-84be-60d24d0d3125"), "", "", "", "Downtown Store", "", "" },
                    { new Guid("cf623c49-cfb6-464a-aeea-aad9a29a702d"), "", "", "", "Warehouse B", "", "" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryCode", "CategoryName", "Description" },
                values: new object[,]
                {
                    { new Guid("4e58d95a-61a5-46ca-b94a-8a086c34cdb0"), "", "Electronics", "" },
                    { new Guid("75071d11-5108-40c0-a2bd-0e80460f5a38"), "", "Clothing", "" },
                    { new Guid("fba2b3af-02d8-4499-aee5-3dcbb63f7f60"), "", "Home Appliances", "" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("430fa059-3c9d-4d96-83a7-c75279de2987"), "Clothing" },
                    { new Guid("507f69eb-e069-4882-9fa2-7422bc0e7a4e"), "Food" },
                    { new Guid("87f3c54c-008d-4dbd-a82d-9d9d3c201d1d"), "Toys" },
                    { new Guid("94133dd6-6923-44ca-bdef-0a3a2e930371"), "Furniture" },
                    { new Guid("b264f331-a34b-4a2e-8714-f937dada8e82"), "Electronics" }
                });

            migrationBuilder.InsertData(
                table: "SellingPriceTaxes",
                columns: new[] { "Id", "SellingPriceTaxName" },
                values: new object[,]
                {
                    { new Guid("02bbf818-a36d-4232-b5d1-76f525993437"), "Inclusive" },
                    { new Guid("27c8b773-0fd4-440c-b45d-91e70283965a"), "Zero Rate" },
                    { new Guid("9b0e1c2e-fdde-4ca0-bbfd-deeb877e9efb"), "Exclusive" }
                });

            migrationBuilder.InsertData(
                table: "Subcategories",
                columns: new[] { "Id", "SubcategoryName" },
                values: new object[,]
                {
                    { new Guid("1e5c564b-45fb-4c5c-a85f-f293e09a8d3c"), "Televisions" },
                    { new Guid("411b1aad-8cb6-4c7d-9311-96d2c27a2627"), "Smartphones" },
                    { new Guid("777bb31d-d0fd-4c51-bee1-594daa91458b"), "Laptops" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "AllowDecimal", "ShortName", "UnitName" },
                values: new object[,]
                {
                    { new Guid("78b51511-8ea1-47e6-9d9e-3529907392ff"), 0, "", "Piece" },
                    { new Guid("7dcdc71d-b130-452b-9a03-9e7b63ace9ab"), 0, "", "Liter" },
                    { new Guid("eb9c10f0-7b68-4bdd-9952-efe2ca9afa4f"), 0, "", "Kilogram" }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "Description", "Name", "WarrantyDuration" },
                values: new object[,]
                {
                    { new Guid("20fcd225-cb0d-40a7-bf01-a9c2123656ae"), "", "", "2 Years" },
                    { new Guid("7713531b-8b20-4ed4-b1b7-176b613d6abe"), "", "", "1 Year" },
                    { new Guid("d9b2dd31-2c03-47a6-adf3-1ee82380666b"), "", "", "3 Years" }
                });
        }
    }
}
