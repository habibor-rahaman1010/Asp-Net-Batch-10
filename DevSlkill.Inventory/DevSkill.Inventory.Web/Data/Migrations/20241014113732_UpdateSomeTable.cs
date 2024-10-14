using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DevSkill.Inventory.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSomeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("4afe1554-e358-4d39-a57c-cdf8e816c936"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("4ec785cf-290d-4db0-b250-a2b61f467b53"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("ff817b80-5048-43b5-b670-0b9aee7685fa"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("795ba420-33e6-448b-9036-546c3c65aa7e"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("b39a22b8-937a-4239-8bc6-3b66e8741983"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("f2ddd2d7-ddfc-4557-9b84-7f14fd74a446"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("10a0b9db-c548-4e35-9474-4ff50f0e6e99"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("4ae2faa4-42fe-412f-8af3-bf6cefb720d9"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("5e06240a-3cc9-4e5e-b435-01f858f96a8f"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("385b987a-3a07-41b9-85f9-b4145f768a39"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("4ad8b60c-4c8b-41fd-abe1-d80bbdf56e8f"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("d8a2afef-bb89-486e-8e5c-f9f250387a85"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0226cf1a-3edc-4739-ad75-b84e0eb4dde5"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("630b6c02-ae63-4b1d-a2fc-501d35e605f9"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6a2c74de-7807-41e3-b703-90c82e9a9a6e"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("2e86f058-786d-4836-ab14-410dd33318f5"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("6965adb6-f506-483d-8287-5226065389b2"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("82bece7a-4620-48da-901b-49d4e7bb3466"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("db93eb49-3ae0-4d9e-bf67-9092528a5dc3"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("e60229c9-7df2-4d2b-8dc2-d146d0b9331b"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("3fc6e1f0-5b0c-4c72-8338-5a37b661b82c"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("bd18c221-b5af-4e8f-bdd5-491255215661"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("e5dc99e8-0572-43d3-b367-8e4e49034dd9"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("1b76cf16-c41d-4c45-8e6d-175d0d87048f"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("bf682955-08bc-4772-a082-54e41d849c36"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("d0b066f0-210e-4b58-9fd7-28fc819991a9"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("43c7473a-d771-418f-ba64-b00a225db52d"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("4e7bd394-b32a-4f70-88ac-0433a5e5961d"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("a4e3fbd2-dae9-4403-bf23-70a7edd80d4e"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("380bb88c-a0d5-4cf7-84b6-0d87d0ae610d"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("a28bd299-9b28-4893-be9b-9ab202c09932"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("f9339dd6-632a-4c7c-8df3-b3d7cb84ed05"));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Warranties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Warranties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AllowDecimal",
                table: "Units",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SortName",
                table: "Units",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BandOrigin",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "ApplicableTaxs",
                columns: new[] { "Id", "ApplicableTaxName" },
                values: new object[,]
                {
                    { new Guid("00778b75-c55b-44f0-a3ec-e00664df41f0"), "Food" },
                    { new Guid("8bc64b6f-2f29-4231-961d-a5f60f7923aa"), "Fruits" },
                    { new Guid("b395daf8-96cd-4839-a737-b8292608e88e"), "Sales Tax" }
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "BarcodeTypeName" },
                values: new object[,]
                {
                    { new Guid("12c29ecd-cc08-4b4a-b7be-4f505a0a9c6b"), "NFC" },
                    { new Guid("23a38c7c-6181-4edd-bf98-fecce1094824"), "UPC" },
                    { new Guid("76946c35-d735-4b0d-bebf-4cbfb5a92ba0"), "QR Code" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BandOrigin", "BrandName", "Description" },
                values: new object[,]
                {
                    { new Guid("3e655575-643f-48f1-ab96-ac8c90b10678"), "", "Apple", "" },
                    { new Guid("be1a5152-942e-4577-985e-3cb7fd83efc8"), "", "Samsung", "" },
                    { new Guid("c9708e78-3e63-46f3-81df-a346a02d438c"), "", "Sony", "" }
                });

            migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "LocationName" },
                values: new object[,]
                {
                    { new Guid("7d131168-312a-4cd5-b3aa-469325d62719"), "Warehouse A" },
                    { new Guid("afa688c1-ab42-4f83-8604-7b27fde44d5d"), "Downtown Store" },
                    { new Guid("ec04bbfa-1967-4ea7-8dac-c7ed7d329c4e"), "Warehouse B" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryCode", "CategoryName", "Description" },
                values: new object[,]
                {
                    { new Guid("57945a8a-efbc-4b0c-93d5-66f6bf8d96a6"), "", "Clothing", "" },
                    { new Guid("687f4235-3a63-4be1-973b-d9ac2c51e968"), "", "Electronics", "" },
                    { new Guid("8d2c3ea2-3613-4ff4-abc9-3750c9df6a4b"), "", "Home Appliances", "" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("0dbd9584-de00-41fb-8baa-a4cae40958b6"), "Clothing" },
                    { new Guid("1413ce4a-207c-4408-a1d2-7324db0a9359"), "Furniture" },
                    { new Guid("1b73141d-c183-4079-8e68-e0c6864d814c"), "Food" },
                    { new Guid("410e787c-26be-4e04-a345-5ff20cad34f0"), "Electronics" },
                    { new Guid("66fe68d2-7d01-4bb1-8dd0-d7cbf076a47d"), "Toys" }
                });

            migrationBuilder.InsertData(
                table: "SellingPriceTaxes",
                columns: new[] { "Id", "SellingPriceTaxName" },
                values: new object[,]
                {
                    { new Guid("079c4d7c-5c9b-49c1-b8dc-02afadfc101f"), "Exclusive" },
                    { new Guid("3153d0ac-cc6d-44bb-959d-d30614cb65f5"), "Inclusive" },
                    { new Guid("7739a583-553d-42e3-aa6e-548d5a9b8fa6"), "Zero Rate" }
                });

            migrationBuilder.InsertData(
                table: "Subcategories",
                columns: new[] { "Id", "SubcategoryName" },
                values: new object[,]
                {
                    { new Guid("e666aff6-6253-4c7a-be2e-052143dc0f53"), "Laptops" },
                    { new Guid("ea5e49ea-f24c-4560-b9ea-aa421b537586"), "Smartphones" },
                    { new Guid("f226aa03-514b-4af8-b565-f88764a2edaf"), "Televisions" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "AllowDecimal", "SortName", "UnitName" },
                values: new object[,]
                {
                    { new Guid("5544454b-d26a-4268-b98f-48b0e41d1e74"), "", "", "Kilogram" },
                    { new Guid("60c5571c-a011-4455-9146-5c1127f9bfab"), "", "", "Liter" },
                    { new Guid("b86364a0-b191-418d-914b-a956e4cfc920"), "", "", "Piece" }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "Description", "Name", "WarrantyDuration" },
                values: new object[,]
                {
                    { new Guid("10925397-6105-4542-9d60-5454283a15cd"), "", "", "3 Years" },
                    { new Guid("438e8f27-e9c1-420b-9029-68c8dc0e848b"), "", "", "1 Year" },
                    { new Guid("de3aa74b-a98e-469b-a62f-c4d3842c2dd1"), "", "", "2 Years" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("00778b75-c55b-44f0-a3ec-e00664df41f0"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("8bc64b6f-2f29-4231-961d-a5f60f7923aa"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("b395daf8-96cd-4839-a737-b8292608e88e"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("12c29ecd-cc08-4b4a-b7be-4f505a0a9c6b"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("23a38c7c-6181-4edd-bf98-fecce1094824"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("76946c35-d735-4b0d-bebf-4cbfb5a92ba0"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("3e655575-643f-48f1-ab96-ac8c90b10678"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("be1a5152-942e-4577-985e-3cb7fd83efc8"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("c9708e78-3e63-46f3-81df-a346a02d438c"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("7d131168-312a-4cd5-b3aa-469325d62719"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("afa688c1-ab42-4f83-8604-7b27fde44d5d"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("ec04bbfa-1967-4ea7-8dac-c7ed7d329c4e"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("57945a8a-efbc-4b0c-93d5-66f6bf8d96a6"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("687f4235-3a63-4be1-973b-d9ac2c51e968"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("8d2c3ea2-3613-4ff4-abc9-3750c9df6a4b"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("0dbd9584-de00-41fb-8baa-a4cae40958b6"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("1413ce4a-207c-4408-a1d2-7324db0a9359"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("1b73141d-c183-4079-8e68-e0c6864d814c"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("410e787c-26be-4e04-a345-5ff20cad34f0"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("66fe68d2-7d01-4bb1-8dd0-d7cbf076a47d"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("079c4d7c-5c9b-49c1-b8dc-02afadfc101f"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("3153d0ac-cc6d-44bb-959d-d30614cb65f5"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("7739a583-553d-42e3-aa6e-548d5a9b8fa6"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("e666aff6-6253-4c7a-be2e-052143dc0f53"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("ea5e49ea-f24c-4560-b9ea-aa421b537586"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("f226aa03-514b-4af8-b565-f88764a2edaf"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("5544454b-d26a-4268-b98f-48b0e41d1e74"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("60c5571c-a011-4455-9146-5c1127f9bfab"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("b86364a0-b191-418d-914b-a956e4cfc920"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("10925397-6105-4542-9d60-5454283a15cd"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("438e8f27-e9c1-420b-9029-68c8dc0e848b"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("de3aa74b-a98e-469b-a62f-c4d3842c2dd1"));

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Warranties");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Warranties");

            migrationBuilder.DropColumn(
                name: "AllowDecimal",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "SortName",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "BandOrigin",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Brands");

            migrationBuilder.InsertData(
                table: "ApplicableTaxs",
                columns: new[] { "Id", "ApplicableTaxName" },
                values: new object[,]
                {
                    { new Guid("4afe1554-e358-4d39-a57c-cdf8e816c936"), "Sales Tax" },
                    { new Guid("4ec785cf-290d-4db0-b250-a2b61f467b53"), "Fruits" },
                    { new Guid("ff817b80-5048-43b5-b670-0b9aee7685fa"), "Food" }
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "BarcodeTypeName" },
                values: new object[,]
                {
                    { new Guid("795ba420-33e6-448b-9036-546c3c65aa7e"), "NFC" },
                    { new Guid("b39a22b8-937a-4239-8bc6-3b66e8741983"), "QR Code" },
                    { new Guid("f2ddd2d7-ddfc-4557-9b84-7f14fd74a446"), "UPC" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BrandName" },
                values: new object[,]
                {
                    { new Guid("10a0b9db-c548-4e35-9474-4ff50f0e6e99"), "Apple" },
                    { new Guid("4ae2faa4-42fe-412f-8af3-bf6cefb720d9"), "Samsung" },
                    { new Guid("5e06240a-3cc9-4e5e-b435-01f858f96a8f"), "Sony" }
                });

            migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "LocationName" },
                values: new object[,]
                {
                    { new Guid("385b987a-3a07-41b9-85f9-b4145f768a39"), "Downtown Store" },
                    { new Guid("4ad8b60c-4c8b-41fd-abe1-d80bbdf56e8f"), "Warehouse B" },
                    { new Guid("d8a2afef-bb89-486e-8e5c-f9f250387a85"), "Warehouse A" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryCode", "CategoryName", "Description" },
                values: new object[,]
                {
                    { new Guid("0226cf1a-3edc-4739-ad75-b84e0eb4dde5"), "", "Clothing", "" },
                    { new Guid("630b6c02-ae63-4b1d-a2fc-501d35e605f9"), "", "Home Appliances", "" },
                    { new Guid("6a2c74de-7807-41e3-b703-90c82e9a9a6e"), "", "Electronics", "" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("2e86f058-786d-4836-ab14-410dd33318f5"), "Electronics" },
                    { new Guid("6965adb6-f506-483d-8287-5226065389b2"), "Furniture" },
                    { new Guid("82bece7a-4620-48da-901b-49d4e7bb3466"), "Food" },
                    { new Guid("db93eb49-3ae0-4d9e-bf67-9092528a5dc3"), "Clothing" },
                    { new Guid("e60229c9-7df2-4d2b-8dc2-d146d0b9331b"), "Toys" }
                });

            migrationBuilder.InsertData(
                table: "SellingPriceTaxes",
                columns: new[] { "Id", "SellingPriceTaxName" },
                values: new object[,]
                {
                    { new Guid("3fc6e1f0-5b0c-4c72-8338-5a37b661b82c"), "Zero Rate" },
                    { new Guid("bd18c221-b5af-4e8f-bdd5-491255215661"), "Inclusive" },
                    { new Guid("e5dc99e8-0572-43d3-b367-8e4e49034dd9"), "Exclusive" }
                });

            migrationBuilder.InsertData(
                table: "Subcategories",
                columns: new[] { "Id", "SubcategoryName" },
                values: new object[,]
                {
                    { new Guid("1b76cf16-c41d-4c45-8e6d-175d0d87048f"), "Laptops" },
                    { new Guid("bf682955-08bc-4772-a082-54e41d849c36"), "Smartphones" },
                    { new Guid("d0b066f0-210e-4b58-9fd7-28fc819991a9"), "Televisions" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "UnitName" },
                values: new object[,]
                {
                    { new Guid("43c7473a-d771-418f-ba64-b00a225db52d"), "Kilogram" },
                    { new Guid("4e7bd394-b32a-4f70-88ac-0433a5e5961d"), "Piece" },
                    { new Guid("a4e3fbd2-dae9-4403-bf23-70a7edd80d4e"), "Liter" }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "WarrantyDuration" },
                values: new object[,]
                {
                    { new Guid("380bb88c-a0d5-4cf7-84b6-0d87d0ae610d"), "2 Years" },
                    { new Guid("a28bd299-9b28-4893-be9b-9ab202c09932"), "3 Years" },
                    { new Guid("f9339dd6-632a-4c7c-8df3-b3d7cb84ed05"), "1 Year" }
                });
        }
    }
}
