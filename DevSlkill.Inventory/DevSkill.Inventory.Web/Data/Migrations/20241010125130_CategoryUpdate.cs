using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DevSkill.Inventory.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class CategoryUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("56ca22de-d8ce-4642-819d-b915a8abcfe2"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("c68e8903-c867-477f-b159-3c40e9fc15d3"));

            migrationBuilder.DeleteData(
                table: "ApplicableTaxs",
                keyColumn: "Id",
                keyValue: new Guid("cc962669-50ce-4c7b-a1e3-b810dd2c2af1"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("870aa13e-b185-4fff-ab35-e0759af85c43"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("b20017a7-766f-4e98-914b-1b2371f84df3"));

            migrationBuilder.DeleteData(
                table: "BarcodeTypes",
                keyColumn: "Id",
                keyValue: new Guid("b6a737d6-98fa-41ee-b6cb-74778150c978"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("c041e676-ae07-4433-9a68-14504caf28f8"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("dafc99ad-405c-43a2-ad0a-5067e8fad90b"));

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: new Guid("e6becabe-d9d3-4d24-a82c-9ab928f31c5d"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("48db8dc4-4de3-426c-9c03-6ef9420d5d65"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("9fa61ce9-86c5-401c-8702-73e6d039127e"));

            migrationBuilder.DeleteData(
                table: "BusinessLocations",
                keyColumn: "Id",
                keyValue: new Guid("e3abab94-9bfe-4967-992e-176c7fde8e90"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("80d2234c-8a17-412f-b099-ac433e07008a"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b4ba6730-eda3-4969-8740-8568db5c3261"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d6088aaa-f8de-4071-b62c-4670652c889a"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("17ca8ecd-7616-46bc-98a9-76106dcb55a5"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("28a8a8c1-6356-47ff-84fb-ee84dfd29edc"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("8748b72b-b63e-4261-affb-c06f7c1e3f7c"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("b5c164c6-91bb-4407-b5bc-9d795012fd30"));

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("fcd4fb58-ff5c-44a1-ac90-c13fa755c8d9"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("01ab0059-df18-4cc3-8dbe-1fa58fab5eff"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("8088a11d-5152-4dd0-b296-6734d249d2fc"));

            migrationBuilder.DeleteData(
                table: "SellingPriceTaxes",
                keyColumn: "Id",
                keyValue: new Guid("a4f32a14-e76f-40e5-8913-5e733c377ddc"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("222c8660-c1b0-4f3b-9712-d42414c8b4a8"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("3b548560-921c-486c-afed-550f08ab547a"));

            migrationBuilder.DeleteData(
                table: "Subcategories",
                keyColumn: "Id",
                keyValue: new Guid("c2113e96-49ea-4bdf-9838-324dbe89e1b2"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("28c45c46-34d7-4a78-8074-db6df9a1d5c2"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("2be6ad91-dc8d-4bb6-a579-31c6887ce456"));

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: new Guid("556c7957-c943-4957-b5a6-c418d8486ac9"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("2c3a2013-3d51-41c7-b152-4b7861154813"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("2f6fbfbe-5787-4f1b-ac27-bdbc07539689"));

            migrationBuilder.DeleteData(
                table: "Warranties",
                keyColumn: "Id",
                keyValue: new Guid("f19ce662-6e0f-46a9-a695-5ff413ba456e"));

            migrationBuilder.AddColumn<string>(
                name: "CategoryCode",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "CategoryCode",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Categories");

            migrationBuilder.InsertData(
                table: "ApplicableTaxs",
                columns: new[] { "Id", "ApplicableTaxName" },
                values: new object[,]
                {
                    { new Guid("56ca22de-d8ce-4642-819d-b915a8abcfe2"), "Fruits" },
                    { new Guid("c68e8903-c867-477f-b159-3c40e9fc15d3"), "Sales Tax" },
                    { new Guid("cc962669-50ce-4c7b-a1e3-b810dd2c2af1"), "Food" }
                });

            migrationBuilder.InsertData(
                table: "BarcodeTypes",
                columns: new[] { "Id", "BarcodeTypeName" },
                values: new object[,]
                {
                    { new Guid("870aa13e-b185-4fff-ab35-e0759af85c43"), "NFC" },
                    { new Guid("b20017a7-766f-4e98-914b-1b2371f84df3"), "UPC" },
                    { new Guid("b6a737d6-98fa-41ee-b6cb-74778150c978"), "QR Code" }
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "BrandName" },
                values: new object[,]
                {
                    { new Guid("c041e676-ae07-4433-9a68-14504caf28f8"), "Apple" },
                    { new Guid("dafc99ad-405c-43a2-ad0a-5067e8fad90b"), "Samsung" },
                    { new Guid("e6becabe-d9d3-4d24-a82c-9ab928f31c5d"), "Sony" }
                });

            migrationBuilder.InsertData(
                table: "BusinessLocations",
                columns: new[] { "Id", "LocationName" },
                values: new object[,]
                {
                    { new Guid("48db8dc4-4de3-426c-9c03-6ef9420d5d65"), "Warehouse A" },
                    { new Guid("9fa61ce9-86c5-401c-8702-73e6d039127e"), "Downtown Store" },
                    { new Guid("e3abab94-9bfe-4967-992e-176c7fde8e90"), "Warehouse B" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName" },
                values: new object[,]
                {
                    { new Guid("80d2234c-8a17-412f-b099-ac433e07008a"), "Electronics" },
                    { new Guid("b4ba6730-eda3-4969-8740-8568db5c3261"), "Home Appliances" },
                    { new Guid("d6088aaa-f8de-4071-b62c-4670652c889a"), "Clothing" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "ProductTypeName" },
                values: new object[,]
                {
                    { new Guid("17ca8ecd-7616-46bc-98a9-76106dcb55a5"), "Food" },
                    { new Guid("28a8a8c1-6356-47ff-84fb-ee84dfd29edc"), "Toys" },
                    { new Guid("8748b72b-b63e-4261-affb-c06f7c1e3f7c"), "Electronics" },
                    { new Guid("b5c164c6-91bb-4407-b5bc-9d795012fd30"), "Furniture" },
                    { new Guid("fcd4fb58-ff5c-44a1-ac90-c13fa755c8d9"), "Clothing" }
                });

            migrationBuilder.InsertData(
                table: "SellingPriceTaxes",
                columns: new[] { "Id", "SellingPriceTaxName" },
                values: new object[,]
                {
                    { new Guid("01ab0059-df18-4cc3-8dbe-1fa58fab5eff"), "Exclusive" },
                    { new Guid("8088a11d-5152-4dd0-b296-6734d249d2fc"), "Inclusive" },
                    { new Guid("a4f32a14-e76f-40e5-8913-5e733c377ddc"), "Zero Rate" }
                });

            migrationBuilder.InsertData(
                table: "Subcategories",
                columns: new[] { "Id", "SubcategoryName" },
                values: new object[,]
                {
                    { new Guid("222c8660-c1b0-4f3b-9712-d42414c8b4a8"), "Laptops" },
                    { new Guid("3b548560-921c-486c-afed-550f08ab547a"), "Televisions" },
                    { new Guid("c2113e96-49ea-4bdf-9838-324dbe89e1b2"), "Smartphones" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "UnitName" },
                values: new object[,]
                {
                    { new Guid("28c45c46-34d7-4a78-8074-db6df9a1d5c2"), "Piece" },
                    { new Guid("2be6ad91-dc8d-4bb6-a579-31c6887ce456"), "Liter" },
                    { new Guid("556c7957-c943-4957-b5a6-c418d8486ac9"), "Kilogram" }
                });

            migrationBuilder.InsertData(
                table: "Warranties",
                columns: new[] { "Id", "WarrantyDuration" },
                values: new object[,]
                {
                    { new Guid("2c3a2013-3d51-41c7-b152-4b7861154813"), "1 Year" },
                    { new Guid("2f6fbfbe-5787-4f1b-ac27-bdbc07539689"), "2 Years" },
                    { new Guid("f19ce662-6e0f-46a9-a695-5ff413ba456e"), "3 Years" }
                });
        }
    }
}
