using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DevSkill.Inventory.Web.Migrations.InventoryDb
{
    /// <inheritdoc />
    public partial class UpdateSubCategoryTable3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Subcategories",
                newName: "SubCategories"
            );

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
