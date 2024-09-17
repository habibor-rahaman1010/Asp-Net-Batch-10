using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevSkill.Inventory.Web.Migrations.InventoryDb
{
    /// <inheritdoc />
    public partial class ProductStoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = """
                    CREATE OR ALTER PROCEDURE ProductAdvancedSearch
                    @PageIndex int,
                    @PageSize int,
                    @OrderBy nvarchar(50),
                    @ProductName nvarchar(max) = '%',
                    @ProductType int = NULL,
                    @CategoryId uniqueidentifier = NULL,    
                    @UnitId uniqueidentifier = NULL,        
                    @BrandId uniqueidentifier = NULL,       
                    @BusinessLocationId uniqueidentifier = NULL,
                    @TaxRate int = NULL,
                    @Status int = NULL, 
                    @Total int OUTPUT,
                    @TotalDisplay int OUTPUT
                AS
                BEGIN
                    SET NOCOUNT ON;

                    -- Declare SQL strings for the query and count
                    DECLARE @sql nvarchar(MAX);
                    DECLARE @countSql nvarchar(MAX);
                    DECLARE @paramList nvarchar(MAX); 

                    -- Collecting Total (total products count without filtering)
                    SELECT @Total = COUNT(*) FROM Products;

                    -- Base SQL for counting filtered products
                    SET @countSql = N'
                        SELECT @TotalDisplay = COUNT(*) 
                        FROM Products p
                        LEFT JOIN Categories c ON p.CategoryId = c.Id
                        LEFT JOIN Subcategories sc ON p.SubcategoryId = sc.Id
                        LEFT JOIN Brands b ON p.BrandId = b.Id
                        LEFT JOIN Units u ON p.UnitId = u.Id
                        LEFT JOIN BusinessLocations bl ON p.BusinessLocationId = bl.Id
                        WHERE 1 = 1';

                    -- Add filtering conditions based on provided parameters
                    IF @ProductName IS NOT NULL AND @ProductName <> '%'
                        SET @countSql = @countSql + N' AND p.ProductName LIKE @xProductName';

                    IF @ProductType IS NOT NULL
                        SET @countSql = @countSql + N' AND p.ProductType = @xProductType';

                    IF @CategoryId IS NOT NULL
                        SET @countSql = @countSql + N' AND p.CategoryId = @xCategoryId';

                    IF @UnitId IS NOT NULL
                        SET @countSql = @countSql + N' AND p.UnitId = @xUnitId';

                    IF @BrandId IS NOT NULL
                        SET @countSql = @countSql + N' AND p.BrandId = @xBrandId';

                    IF @BusinessLocationId IS NOT NULL
                        SET @countSql = @countSql + N' AND p.BusinessLocationId = @xBusinessLocationId';

                    IF @TaxRate IS NOT NULL
                        SET @countSql = @countSql + N' AND p.Tax = @xTaxRate';

                    IF @Status IS NOT NULL
                        SET @countSql = @countSql + N' AND p.Status = @xStatus';

                    -- Prepare parameters list for counting filtered products
                    SET @paramList = N'@xProductName nvarchar(max), 
                                       @xProductType int, 
                                       @xCategoryId uniqueidentifier, 
                                       @xUnitId uniqueidentifier, 
                                       @xBrandId uniqueidentifier, 
                                       @xBusinessLocationId uniqueidentifier, 
                                       @xTaxRate decimal(18,2),
                                       @xStatus int,
                                       @TotalDisplay int OUTPUT';

                    -- Execute the count query to get filtered total
                    EXEC sp_executesql @countSql, @paramList,
                        @ProductName, @ProductType, @CategoryId, 
                        @UnitId, @BrandId, @BusinessLocationId, 
                        @TaxRate, @Status, @TotalDisplay OUTPUT;

                    -- Base SQL for retrieving paginated product data
                    SET @sql = N'
                        SELECT p.Id, p.ProductName, p.Price, p.SKU, p.Ratings, p.Created, p.Updated, 
                               c.CategoryName, sc.SubcategoryName, 
                               b.BrandName, u.UnitName, bl.LocationName
                        FROM Products p
                        LEFT JOIN Categories c ON p.CategoryId = c.Id
                        LEFT JOIN Subcategories sc ON p.SubcategoryId = sc.Id
                        LEFT JOIN Brands b ON p.BrandId = b.Id
                        LEFT JOIN Units u ON p.UnitId = u.Id
                        LEFT JOIN BusinessLocations bl ON p.BusinessLocationId = bl.Id
                        WHERE 1 = 1';

                    -- Add filtering conditions for product retrieval
                    IF @ProductName IS NOT NULL AND @ProductName <> '%'
                        SET @sql = @sql + N' AND p.ProductName LIKE @xProductName';

                    IF @ProductType IS NOT NULL
                        SET @sql = @sql + N' AND p.ProductType = @xProductType';

                    IF @CategoryId IS NOT NULL
                        SET @sql = @sql + N' AND p.CategoryId = @xCategoryId';

                    IF @UnitId IS NOT NULL
                        SET @sql = @sql + N' AND p.UnitId = @xUnitId';

                    IF @BrandId IS NOT NULL
                        SET @sql = @sql + N' AND p.BrandId = @xBrandId';

                    IF @BusinessLocationId IS NOT NULL
                        SET @sql = @sql + N' AND p.BusinessLocationId = @xBusinessLocationId';

                    IF @TaxRate IS NOT NULL
                        SET @sql = @sql + N' AND p.Tax = @xTaxRate';

                    IF @Status IS NOT NULL
                        SET @sql = @sql + N' AND p.Status = @xStatus';

                    -- Add ordering, pagination using OFFSET and FETCH
                    SET @sql = @sql + N'
                        ORDER BY ' + @OrderBy + N'
                        OFFSET (@xPageSize * (@xPageIndex - 1)) ROWS
                        FETCH NEXT @xPageSize ROWS ONLY';

                    -- Prepare parameters list for paginated product data retrieval
                    SET @paramList = N'@xProductName nvarchar(max), 
                                       @xProductType int, 
                                       @xCategoryId uniqueidentifier, 
                                       @xUnitId uniqueidentifier, 
                                       @xBrandId uniqueidentifier, 
                                       @xBusinessLocationId uniqueidentifier, 
                                       @xTaxRate decimal(18,2),
                                       @xStatus int,
                                       @xPageIndex int, 
                                       @xPageSize int';

                    -- Execute the paginated product retrieval query
                    EXEC sp_executesql @sql, @paramList,
                        @ProductName, @ProductType, @CategoryId, 
                        @UnitId, @BrandId, @BusinessLocationId, 
                        @TaxRate, @Status, @PageIndex, @PageSize;
                END;
                
                """;
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sql = "DROPE PROCEDURE [dbo].[ProductAdvancedSearch]";
            migrationBuilder.Sql(sql);
        }
    }
}
