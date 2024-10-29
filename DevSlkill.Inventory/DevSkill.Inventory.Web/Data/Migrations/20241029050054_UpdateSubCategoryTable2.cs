using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DevSkill.Inventory.Web.Migrations.InventoryDb
{
    /// <inheritdoc />
    public partial class UpdateSubCategoryTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("3a4a4b24-07ac-44fd-bd0c-c5557da9b721"));

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("5795909e-9782-44a7-907d-25ecf281e4f5"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("17a9927d-4a25-4bcb-94a8-f38873c6c169"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("7587a037-dbc9-46cb-8f1a-997c82ab3829"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("d0b176e4-b591-4c2f-b667-24db1bf07bbf"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("24097fb3-9274-49dc-9dd9-fcdbe7dabd8e"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("864c7333-6cce-419c-b9cb-796482075c48"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("bd9a77ae-688a-411f-9b7d-27829b16cfdd"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("24801d9d-2f99-45e6-9638-0d751b0f63ae"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("b6bc0013-d58a-4ff9-8332-2fcdb3c4f685"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("d83da2ca-6e0d-4a66-a60f-48bfe8f56fa7"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("c319526b-a5f6-477e-ac5a-0e087f1afb09"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("c93f5ff4-3cdc-495b-8333-21d5bd5de637"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("edcb0afc-1080-465a-931f-a37c8fa5c41a"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("4d53e565-8400-43c7-b6c3-6cc8738524ad"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("7812ef7b-b3f9-45af-a130-88468e22a329"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("fb79515a-e23e-483d-888a-12d0ad7cf7f7"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("05cfbb2d-d7d4-4287-a4f3-705927e8e4ba"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("3c5f3ec0-e2cb-4c5e-9c23-90b955220a4a"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("7f8e2edd-2a33-4c42-a2c8-2a6943067d7c"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("c5bbc507-3579-4ce7-b34b-9a14c6ceefc6"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("e5ccca87-622c-42fa-ad65-2c3b38ef9f72"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("335e3440-2ed8-4e14-8920-c47ef61f2a6e"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("35788670-619c-4d12-be04-9f16cce56f64"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("a1fcf8e3-d173-48e4-a6b0-8b26804ef264"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("3fdd0a46-aa26-4c61-8883-f5c1f75bc96e"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("9dd377b9-a98e-41ad-975b-a4bbc1f13bcf"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("abb050bd-7710-4173-b27b-e61868d73b0c"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("848b4f96-57dc-42f9-baca-0f6e6b8ceab5"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("af7b134c-50c2-41e3-a534-013efabf0ec6"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("efd2537b-66eb-4533-847d-2663932d19ff"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("15890a3e-04ae-4e52-a57b-10cb7a7ed5ec"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("d7cfa86a-d9ff-4074-9987-43287d43a93f"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("db8f8d6f-7261-47f0-84d7-32641818c025"));

            migrationBuilder.InsertData(
                table: "AdjustmentTypes",
                columns: new[] { "Id", "AdjustmentTypeName", "Description" },
                values: new object[,]
                {
                    { new Guid("30f53248-5513-488e-8def-6154e100fab6"), "Abnormal", "" },
                    { new Guid("c4e409cc-87cf-4f09-9468-3ea62df4bca4"), "Normal", "" }
                });

            migrationBuilder.InsertData(
                table: "ApplicableTaxs",
                columns: new[] { "Id", "ApplicableTaxName", "Description", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("410aabb1-830d-49dd-9be2-b31bf10fe6c1"), "Food", "", 0m },
                    { new Guid("7290abff-7bd8-4dd0-a2f8-5d6dd282ffe0"), "Fruits", "", 0m },
                    { new Guid("ee5e4a55-dbbb-47d1-b92b-4d29307805b5"), "Sales Tax", "", 0m }
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "BarcodeDescription", "BarcodeTypeCode", "BarcodeTypeName" },
                values: new object[,]
                {
                    { new Guid("49059193-b136-41fb-a231-ff8d8f7fc6c3"), "", "", "NFC" },
                    { new Guid("c562ab7b-bf15-4df0-b0ee-0d0db37a8ef5"), "", "", "UPC" },
                    { new Guid("d78810e1-bb89-48ba-90bb-560f601d53bf"), "", "", "QR Code" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BandOrigin", "BrandName", "Description" },
                values: new object[,]
                {
                    { new Guid("0b905c20-831b-4eb6-a5fc-b2db0c382a03"), "", "Apple", "" },
                    { new Guid("a5f1ef82-79e3-458e-8cfb-2ce7f05b63c7"), "", "Samsung", "" },
                    { new Guid("d56dde26-fc4d-49bc-bbd9-8c706b79754d"), "", "Sony", "" }
                });

            migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "Address", "City", "Country", "LocationName", "State", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("325649fb-32e4-49aa-93e3-03c93feb7400"), "", "", "", "Warehouse A", "", "" },
                    { new Guid("701dded1-8169-42c1-9b3b-28308d4d5068"), "", "", "", "Downtown Store", "", "" },
                    { new Guid("92504eb0-4459-4c32-88dd-8c6223831f2d"), "", "", "", "Warehouse B", "", "" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryCode", "CategoryName", "Description" },
                values: new object[,]
                {
                    { new Guid("27200225-c3a8-43d1-8623-5108bfa4a1b8"), "", "Home Appliances", "" },
                    { new Guid("2f5f409c-1209-45b8-8361-512440215c0b"), "", "Electronics", "" },
                    { new Guid("338392f3-7bfa-47e6-bf68-916e36ca6aca"), "", "Clothing", "" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Description", "ProductTypeCode", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("67a33f07-86b1-49a2-a30e-7a54aaa87b95"), "", "", "Toys" },
                    { new Guid("7fb85054-c336-4ad4-8ba2-3559bc96b7de"), "", "", "Food" },
                    { new Guid("8d0956ac-8827-486a-9441-896948037da0"), "", "", "Electronics" },
                    { new Guid("9ebf8871-edae-435f-8070-6d516dea7c40"), "", "", "Clothing" },
                    { new Guid("e93b2821-ba73-4201-82c4-c7999df692c4"), "", "", "Furniture" }
                });

            migrationBuilder.InsertData(
                table: "SellingPriceTaxes",
                columns: new[] { "Id", "Description", "SellingPriceTaxName", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("6398bfe0-c2ac-4e57-9dab-e130cf711eed"), "", "Exclusive", 0m },
                    { new Guid("ad1b3f3a-ae16-430a-93d5-039b998a7952"), "", "Zero Rate", 0m },
                    { new Guid("dcec2154-2746-4bf7-9674-631faa6cd0f8"), "", "Inclusive", 0m }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryCode", "Description", "SubCategoryName" },
                values: new object[,]
                {
                    { new Guid("016d201c-8f31-4af3-9034-b5d39c965070"), "", "", "Smartphones" },
                    { new Guid("8ba249ee-b0b3-43af-9549-cd1b54a4eb7d"), "", "", "Laptops" },
                    { new Guid("f1e5b490-5306-42dd-9b05-8e10bcf55563"), "", "", "Televisions" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "AllowDecimal", "ShortName", "UnitName" },
                values: new object[,]
                {
                    { new Guid("4ad7aec6-27ce-4824-b942-b408208bdc6c"), 0, "", "Piece" },
                    { new Guid("88a6daec-88b7-4b33-85db-7b0ce41c87b3"), 0, "", "Kilogram" },
                    { new Guid("eec9f64e-5404-4d02-a9c4-8e68b003392f"), 0, "", "Liter" }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "Description", "Name", "WarrantyDuration" },
                values: new object[,]
                {
                    { new Guid("77b14dd3-af25-4114-a8e1-c36b955da6ef"), "", "", "1 Year" },
                    { new Guid("ad5b53e7-fd3a-4c70-8bcf-d7b78e85f76e"), "", "", "2 Years" },
                    { new Guid("de0a7bcf-d762-45bf-92cf-5da3a3e459e3"), "", "", "3 Years" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("30f53248-5513-488e-8def-6154e100fab6"));

            migrationBuilder.DeleteData(
                table: "AdjustmentTypes",
                keyColumn: "Id",
                keyValue: new Guid("c4e409cc-87cf-4f09-9468-3ea62df4bca4"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("410aabb1-830d-49dd-9be2-b31bf10fe6c1"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("7290abff-7bd8-4dd0-a2f8-5d6dd282ffe0"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("ee5e4a55-dbbb-47d1-b92b-4d29307805b5"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("49059193-b136-41fb-a231-ff8d8f7fc6c3"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("c562ab7b-bf15-4df0-b0ee-0d0db37a8ef5"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("d78810e1-bb89-48ba-90bb-560f601d53bf"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("0b905c20-831b-4eb6-a5fc-b2db0c382a03"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("a5f1ef82-79e3-458e-8cfb-2ce7f05b63c7"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("d56dde26-fc4d-49bc-bbd9-8c706b79754d"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("325649fb-32e4-49aa-93e3-03c93feb7400"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("701dded1-8169-42c1-9b3b-28308d4d5068"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("92504eb0-4459-4c32-88dd-8c6223831f2d"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("27200225-c3a8-43d1-8623-5108bfa4a1b8"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("2f5f409c-1209-45b8-8361-512440215c0b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("338392f3-7bfa-47e6-bf68-916e36ca6aca"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("67a33f07-86b1-49a2-a30e-7a54aaa87b95"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("7fb85054-c336-4ad4-8ba2-3559bc96b7de"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("8d0956ac-8827-486a-9441-896948037da0"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("9ebf8871-edae-435f-8070-6d516dea7c40"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("e93b2821-ba73-4201-82c4-c7999df692c4"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("6398bfe0-c2ac-4e57-9dab-e130cf711eed"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("ad1b3f3a-ae16-430a-93d5-039b998a7952"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("dcec2154-2746-4bf7-9674-631faa6cd0f8"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("016d201c-8f31-4af3-9034-b5d39c965070"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("8ba249ee-b0b3-43af-9549-cd1b54a4eb7d"));

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: new Guid("f1e5b490-5306-42dd-9b05-8e10bcf55563"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("4ad7aec6-27ce-4824-b942-b408208bdc6c"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("88a6daec-88b7-4b33-85db-7b0ce41c87b3"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("eec9f64e-5404-4d02-a9c4-8e68b003392f"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("77b14dd3-af25-4114-a8e1-c36b955da6ef"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("ad5b53e7-fd3a-4c70-8bcf-d7b78e85f76e"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("de0a7bcf-d762-45bf-92cf-5da3a3e459e3"));

            migrationBuilder.InsertData(
                table: "AdjustmentTypes",
                columns: new[] { "Id", "AdjustmentTypeName", "Description" },
                values: new object[,]
                {
                    { new Guid("3a4a4b24-07ac-44fd-bd0c-c5557da9b721"), "Normal", "" },
                    { new Guid("5795909e-9782-44a7-907d-25ecf281e4f5"), "Abnormal", "" }
                });

            migrationBuilder.InsertData(
                table: "ApplicableTaxs",
                columns: new[] { "Id", "ApplicableTaxName", "Description", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("17a9927d-4a25-4bcb-94a8-f38873c6c169"), "Sales Tax", "", 0m },
                    { new Guid("7587a037-dbc9-46cb-8f1a-997c82ab3829"), "Fruits", "", 0m },
                    { new Guid("d0b176e4-b591-4c2f-b667-24db1bf07bbf"), "Food", "", 0m }
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "BarcodeDescription", "BarcodeTypeCode", "BarcodeTypeName" },
                values: new object[,]
                {
                    { new Guid("24097fb3-9274-49dc-9dd9-fcdbe7dabd8e"), "", "", "NFC" },
                    { new Guid("864c7333-6cce-419c-b9cb-796482075c48"), "", "", "QR Code" },
                    { new Guid("bd9a77ae-688a-411f-9b7d-27829b16cfdd"), "", "", "UPC" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BandOrigin", "BrandName", "Description" },
                values: new object[,]
                {
                    { new Guid("24801d9d-2f99-45e6-9638-0d751b0f63ae"), "", "Apple", "" },
                    { new Guid("b6bc0013-d58a-4ff9-8332-2fcdb3c4f685"), "", "Sony", "" },
                    { new Guid("d83da2ca-6e0d-4a66-a60f-48bfe8f56fa7"), "", "Samsung", "" }
                });

            migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "Address", "City", "Country", "LocationName", "State", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("c319526b-a5f6-477e-ac5a-0e087f1afb09"), "", "", "", "Warehouse B", "", "" },
                    { new Guid("c93f5ff4-3cdc-495b-8333-21d5bd5de637"), "", "", "", "Warehouse A", "", "" },
                    { new Guid("edcb0afc-1080-465a-931f-a37c8fa5c41a"), "", "", "", "Downtown Store", "", "" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryCode", "CategoryName", "Description" },
                values: new object[,]
                {
                    { new Guid("4d53e565-8400-43c7-b6c3-6cc8738524ad"), "", "Home Appliances", "" },
                    { new Guid("7812ef7b-b3f9-45af-a130-88468e22a329"), "", "Electronics", "" },
                    { new Guid("fb79515a-e23e-483d-888a-12d0ad7cf7f7"), "", "Clothing", "" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Description", "ProductTypeCode", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("05cfbb2d-d7d4-4287-a4f3-705927e8e4ba"), "", "", "Toys" },
                    { new Guid("3c5f3ec0-e2cb-4c5e-9c23-90b955220a4a"), "", "", "Clothing" },
                    { new Guid("7f8e2edd-2a33-4c42-a2c8-2a6943067d7c"), "", "", "Food" },
                    { new Guid("c5bbc507-3579-4ce7-b34b-9a14c6ceefc6"), "", "", "Furniture" },
                    { new Guid("e5ccca87-622c-42fa-ad65-2c3b38ef9f72"), "", "", "Electronics" }
                });

            migrationBuilder.InsertData(
                table: "SellingPriceTaxes",
                columns: new[] { "Id", "Description", "SellingPriceTaxName", "TaxRate" },
                values: new object[,]
                {
                    { new Guid("335e3440-2ed8-4e14-8920-c47ef61f2a6e"), "", "Inclusive", 0m },
                    { new Guid("35788670-619c-4d12-be04-9f16cce56f64"), "", "Zero Rate", 0m },
                    { new Guid("a1fcf8e3-d173-48e4-a6b0-8b26804ef264"), "", "Exclusive", 0m }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryCode", "Description", "SubCategoryName" },
                values: new object[,]
                {
                    { new Guid("3fdd0a46-aa26-4c61-8883-f5c1f75bc96e"), "", "", "Laptops" },
                    { new Guid("9dd377b9-a98e-41ad-975b-a4bbc1f13bcf"), "", "", "Televisions" },
                    { new Guid("abb050bd-7710-4173-b27b-e61868d73b0c"), "", "", "Smartphones" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "AllowDecimal", "ShortName", "UnitName" },
                values: new object[,]
                {
                    { new Guid("848b4f96-57dc-42f9-baca-0f6e6b8ceab5"), 0, "", "Piece" },
                    { new Guid("af7b134c-50c2-41e3-a534-013efabf0ec6"), 0, "", "Liter" },
                    { new Guid("efd2537b-66eb-4533-847d-2663932d19ff"), 0, "", "Kilogram" }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "Description", "Name", "WarrantyDuration" },
                values: new object[,]
                {
                    { new Guid("15890a3e-04ae-4e52-a57b-10cb7a7ed5ec"), "", "", "3 Years" },
                    { new Guid("d7cfa86a-d9ff-4074-9987-43287d43a93f"), "", "", "2 Years" },
                    { new Guid("db8f8d6f-7261-47f0-84d7-32641818c025"), "", "", "1 Year" }
                });
        }
    }
}
