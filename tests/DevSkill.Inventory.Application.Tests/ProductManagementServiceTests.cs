using Autofac.Extras.Moq;
using DevSkill.Inventory.Application.Services;
using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Application.Tests
{
    [ExcludeFromCodeCoverage]
    public class ProductManagementServiceTests
    {
        private AutoMock _moq;
        private IProductManagementService _productManagementService;
        private Mock<IProductRepository> _productRepositoryMock;
        private Mock<IInventoryUnitOfWork> _productUnitOfWorkMock;

        [SetUp]
        public void Setup() 
        {
            _productManagementService = _moq.Create<ProductManagementService>();
            _productRepositoryMock = _moq.Mock<IProductRepository>();
            _productUnitOfWorkMock = _moq.Mock<IInventoryUnitOfWork>();
            _productUnitOfWorkMock.Setup(x => x.ProductRepository).Returns(_productRepositoryMock.Object);
        }

        [TearDown]
        public void Teardown()
        {
            _productRepositoryMock?.Reset();
            _productUnitOfWorkMock?.Reset();
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _moq = AutoMock.GetLoose();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _moq?.Dispose();
        }


       [Test]
        public async Task CreateProduct_ShouldAddProduct_WhenTitleIsUnique()
        {
            // Arrange
            var product = new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "Unique Product",
                Price = 100
            };

            // Mock the repository to return false when checking for a duplicate title
            _productRepositoryMock.Setup(repo => repo.IsTitleDuplicateAsync(product.ProductName, null))
                .ReturnsAsync(false).Verifiable();

            // Act
            await _productManagementService.CreateProduct(product);

            // Assert
            _productRepositoryMock.VerifyAll();
            _productUnitOfWorkMock.VerifyAll();
        }

        [Test]
        public async Task CreateProduct_ShouldThrowException_WhenTitleIsDuplicate()
        {
            // Arrange
            var product = new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "Duplicate Product",
                Price = 100
            };

            // Mock the repository to return true when checking for a duplicate title
            _productRepositoryMock.Setup(repo => repo.IsTitleDuplicateAsync(product.ProductName, null))
                .ReturnsAsync(true).Verifiable();

            // Act & Assert
            var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await _productManagementService.CreateProduct(product));

            Assert.AreEqual("Product name should be unique!", exception.Message); // Verify exception message

            // Ensure AddAsync and SaveAsync are NOT called
            _productRepositoryMock.VerifyAll();
            _productUnitOfWorkMock.VerifyAll();
        }


        // Test for GetProductByIdAsync method
        [Test]
        public async Task GetProductByIdAsync_ShouldReturnProduct_WhenProductExists()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var expectedProduct = new Product
            {
                Id = productId,
                ProductName = "Sample Product",
                Price = 100
            };

            // Mock the repository to return the product for the given ID
            _productRepositoryMock.Setup(repo => repo.GetProductByIdAsync(productId))
                .ReturnsAsync(expectedProduct).Verifiable();

            // Act
            var result = await _productManagementService.GetProductByIdAsync(productId);

            // Assert
            _productRepositoryMock.VerifyAll();
            _productUnitOfWorkMock.VerifyAll();
        }

        [Test]
        public async Task GetProductByIdAsync_ShouldReturnNull_WhenProductDoesNotExist()
        {
            // Arrange
            var productId = Guid.NewGuid();

            // Mock the repository to return null when no product is found for the ID
            _productRepositoryMock.Setup(repo => repo.GetProductByIdAsync(productId))
                .ReturnsAsync((Product)null).Verifiable();

            // Act
            var result = await _productManagementService.GetProductByIdAsync(productId);

            // Assert
            _productRepositoryMock.VerifyAll();
            _productUnitOfWorkMock.VerifyAll();
        }

        // Test for GetProductsSpAsync method
        [Test]
        public async Task GetProductsSpAsync_ShouldReturnPagedProductData_WhenValidInputIsProvided()
        {
            // Arrange
            var pageIndex = 1;
            var pageSize = 10;
            var search = new ProductSearchDto { ProductName = "Sample" };
            var order = "ProductName";

            var expectedProducts = new List<ProductDto>
            {
                new ProductDto { Id = Guid.NewGuid(), ProductName = "Product1", Price = 100 },
                new ProductDto { Id = Guid.NewGuid(), ProductName = "Product2", Price = 150 }
            };

            var total = 20;
            var totalDisplay = 15;

            _productUnitOfWorkMock.Setup(uow => uow.GetPagedProductUsingSPAsync(pageIndex, pageSize, search, order))
                .ReturnsAsync((expectedProducts, total, totalDisplay));

            // Act
            var result = await _productManagementService.GetProductsSpAsync(pageIndex, pageSize, search, order);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(expectedProducts, result.data);
            Assert.AreEqual(total, result.total);
            Assert.AreEqual(totalDisplay, result.totalDisplay);
            _productUnitOfWorkMock.Verify(uow => uow.GetPagedProductUsingSPAsync(pageIndex, pageSize, search, order), Times.Once);
        }

        [Test]
        public async Task GetProductsSpAsync_ShouldReturnEmptyData_WhenNoProductsMatchSearchCriteria()
        {
            // Arrange
            var pageIndex = 1;
            var pageSize = 10;
            var search = new ProductSearchDto
            {
                ProductName = "NonExistentProduct" // No products will match this
            };
            var order = "ProductName ASC";

            var expectedProducts = new List<ProductDto>(); // No products found

            var total = 0;
            var totalDisplay = 0;

            // Mock the unit of work to return empty data
            _productUnitOfWorkMock.Setup(uow => uow.GetPagedProductUsingSPAsync(pageIndex, pageSize, search, order))
                .ReturnsAsync((expectedProducts, total, totalDisplay));

            // Act
            var result = await _productManagementService.GetProductsSpAsync(pageIndex, pageSize, search, order);

            // Assert
            Assert.NotNull(result);
            Assert.IsEmpty(result.data);
            Assert.AreEqual(total, result.total);
            Assert.AreEqual(totalDisplay, result.totalDisplay);
        }


        // Test for DeleteProductAsync method
        [Test]
        public async Task DeleteProductAsync_ShouldDeleteProduct_WhenProductExists()
        {
            // Arrange
            var productId = Guid.NewGuid();

            _productRepositoryMock.Setup(repo => repo.RemoveAsync(productId)).Verifiable();
            _productUnitOfWorkMock.Setup(z => z.SaveAsync()).Verifiable();

            // Act
            await _productManagementService.DeleteProductAsync(productId);

            // Assert
            _productRepositoryMock.VerifyAll();
            _productUnitOfWorkMock.VerifyAll();
        }

        [Test]
        public async Task DeleteProductAsync_ShouldNotCallRemoveAsync_WhenProductIdIsEmpty()
        {
            // Arrange
            var emptyProductId = Guid.Empty;
            _productRepositoryMock.Setup(repo => repo.RemoveAsync(emptyProductId)).Verifiable();
            _productUnitOfWorkMock.Setup(z => z.SaveAsync()).Verifiable();

            // Act
            await _productManagementService.DeleteProductAsync(emptyProductId);

            // Assert
            _productRepositoryMock.VerifyAll();
            _productUnitOfWorkMock.VerifyAll();
        }
    }
}
