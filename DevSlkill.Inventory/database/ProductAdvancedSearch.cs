USE [DevSkillInventory]
GO
/****** Object:  StoredProcedure [dbo].[ProductAdvancedSearch]    Script Date: 05-Jun-26 7:12:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE OR ALTER PROCEDURE [dbo].[ProductAdvancedSearch]
    @PageIndex int,
    @PageSize int,
    @OrderBy nvarchar(50),
    @ProductName nvarchar(max) = '%',
    @Description nvarchar(max) = '%',
    @CategoryId uniqueidentifier = NULL,    
    @UnitId uniqueidentifier = NULL,   
    @BrandId uniqueidentifier = NULL,       
    @BusinessLocationId uniqueidentifier = NULL,
    @Status int = NULL,
    @SellingPriceTaxId uniqueidentifier = NULL,
    @ProductTypeId uniqueidentifier = NULL,
    @ApplicableTaxId uniqueidentifier = NULL,
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
    SET @countSql = 'SELECT @TotalDisplay = COUNT(*) 
        FROM Products p
        INNER JOIN Categories c ON p.CategoryId = c.Id
        INNER JOIN Subcategories sc ON p.SubcategoryId = sc.Id
        INNER JOIN Brands b ON p.BrandId = b.Id
        INNER JOIN Units u ON p.UnitId = u.Id
        INNER JOIN BusinessLocations bl ON p.BusinessLocationId = bl.Id
        WHERE 1 = 1';

    -- Add filter for ProductName OR Description
    SET @countSql = @countSql + ' AND (p.ProductName LIKE ''%'' + @xSearchText + ''%'' 
                                     OR p.Description LIKE ''%'' + @xSearchText + ''%'')';

    -- Add other filters (ProductType, Category, etc.)
    IF @CategoryId IS NOT NULL
        SET @countSql = @countSql + ' AND p.CategoryId = @xCategoryId';

    IF @UnitId IS NOT NULL
        SET @countSql = @countSql + ' AND p.UnitId = @xUnitId';

    IF @BrandId IS NOT NULL
        SET @countSql = @countSql + ' AND p.BrandId = @xBrandId';

    IF @BusinessLocationId IS NOT NULL
        SET @countSql = @countSql + ' AND p.BusinessLocationId = @xBusinessLocationId';

    IF @Status IS NOT NULL
        SET @countSql = @countSql + ' AND p.Status = @xStatus';

    IF @SellingPriceTaxId IS NOT NULL
        SET @countSql = @countSql + ' AND p.SellingPriceTaxId = @xSellingPriceTaxId';

    IF @ProductTypeId IS NOT NULL
        SET @countSql = @countSql + ' AND p.ProductTypeId = @xProductTypeId';

    IF @ApplicableTaxId IS NOT NULL
        SET @countSql = @countSql + ' AND p.ApplicableTaxId = @xApplicableTaxId';

    -- Prepare parameter list for count query
    SELECT @paramList = '@xSearchText nvarchar(max),
        @xCategoryId uniqueidentifier,
        @xUnitId uniqueidentifier,
        @xBrandId uniqueidentifier,
        @xBusinessLocationId uniqueidentifier,
        @xStatus int,
        @xSellingPriceTaxId uniqueidentifier,
        @xProductTypeId uniqueidentifier,
        @xApplicableTaxId uniqueidentifier,
        @TotalDisplay int OUTPUT';

    -- Execute the count query
    EXEC sp_executesql @countSql, @paramList,
        @xSearchText = @ProductName,
        @xCategoryId = @CategoryId,
        @xUnitId = @UnitId,
        @xBrandId = @BrandId,
        @xBusinessLocationId = @BusinessLocationId,
        @xStatus = @Status,
        @xSellingPriceTaxId = @SellingPriceTaxId,
        @xProductTypeId = @ProductTypeId,
        @xApplicableTaxId = @ApplicableTaxId,
        @TotalDisplay = @TotalDisplay OUTPUT;

    -- Base SQL for retrieving paginated product data
    SET @sql = 'SELECT p.Id, p.ProductImage, p.ProductName, p.Description, p.Price, p.SKU, p.Ratings, p.CurrentStock, p.Created, p.Updated, p.SellingPrice,
               c.CategoryName, sc.SubcategoryName, 
               b.BrandName, u.UnitName, bl.LocationName, pt.ProductTypeName, at.ApplicableTaxName
        FROM Products p
        INNER JOIN Categories c ON p.CategoryId = c.Id
        INNER JOIN Subcategories sc ON p.SubcategoryId = sc.Id
        INNER JOIN Brands b ON p.BrandId = b.Id
        INNER JOIN Units u ON p.UnitId = u.Id
        INNER JOIN BusinessLocations bl ON p.BusinessLocationId = bl.Id
        INNER JOIN ProductTypes pt ON p.ProductTypeId = pt.Id
        INNER JOIN ApplicableTaxs at ON p.ApplicableTaxId = at.Id
        WHERE 1 = 1';

    -- Add filter for ProductName OR Description
    SET @sql = @sql + ' AND (p.ProductName LIKE ''%'' + @xSearchText + ''%'' 
                         OR p.Description LIKE ''%'' + @xSearchText + ''%'')';

    -- Add other filters (ProductType, Category, etc.)
    IF @CategoryId IS NOT NULL
        SET @sql = @sql + ' AND p.CategoryId = @xCategoryId';

    IF @UnitId IS NOT NULL
        SET @sql = @sql + ' AND p.UnitId = @xUnitId';

    IF @BrandId IS NOT NULL
        SET @sql = @sql + ' AND p.BrandId = @xBrandId';

    IF @BusinessLocationId IS NOT NULL
        SET @sql = @sql + ' AND p.BusinessLocationId = @xBusinessLocationId';

    IF @Status IS NOT NULL
        SET @sql = @sql + ' AND p.Status = @xStatus';

    IF @SellingPriceTaxId IS NOT NULL
        SET @sql = @sql + ' AND p.SellingPriceTaxId = @xSellingPriceTaxId';

    IF @ProductTypeId IS NOT NULL
        SET @sql = @sql + ' AND p.ProductTypeId = @xProductTypeId';

    IF @ApplicableTaxId IS NOT NULL
        SET @sql = @sql + ' AND p.ApplicableTaxId = @xApplicableTaxId';

    -- Add ordering, pagination using OFFSET and FETCH
    SET @sql = @sql + ' ORDER BY ' + @OrderBy + 
        ' OFFSET @PageSize * (@PageIndex - 1) ROWS 
        FETCH NEXT @PageSize ROWS ONLY';

    -- Prepare parameters list for paginated product data retrieval
    SELECT @paramList = '@xSearchText nvarchar(max),
        @xCategoryId uniqueidentifier,
        @xUnitId uniqueidentifier,
        @xBrandId uniqueidentifier,
        @xBusinessLocationId uniqueidentifier,
        @xStatus int,
        @xSellingPriceTaxId uniqueidentifier,
        @xProductTypeId uniqueidentifier,
        @xApplicableTaxId uniqueidentifier,
        @PageIndex int,
        @PageSize int';

    -- Execute the paginated product retrieval query
    EXEC sp_executesql @sql, @paramList,
        @xSearchText = @ProductName,
        @xCategoryId = @CategoryId,
        @xUnitId = @UnitId,
        @xBrandId = @BrandId,
        @xBusinessLocationId = @BusinessLocationId,
        @xStatus = @Status,
        @xSellingPriceTaxId = @SellingPriceTaxId,
        @xProductTypeId = @ProductTypeId,
        @xApplicableTaxId = @ApplicableTaxId,
        @PageIndex = @PageIndex,
        @PageSize = @PageSize;

    PRINT @sql;
    PRINT @countSql;
END;