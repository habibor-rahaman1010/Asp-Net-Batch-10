using Autofac.Extras.Moq;
using DevSkill.Inventory.Application.Services;
using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain;
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
    public class ProductTypeManagementServiceTests
    {
        private AutoMock _moq;
        private IProductTypeManagementService _productTypeManagementService;
        private Mock<IProductTypeRepository> _productTypeRepositoryMock;
        private Mock<IInventoryUnitOfWork> _productTypeUnitOfWorkMock;


        [SetUp]
        public void Setup()
        {
            _productTypeManagementService = _moq.Create<ProductTypeManagementService>();
            _productTypeRepositoryMock = _moq.Mock<IProductTypeRepository>();
            _productTypeUnitOfWorkMock = _moq.Mock<IInventoryUnitOfWork>();
            _productTypeUnitOfWorkMock.Setup(x => x.ProductTypeRepository).Returns(_productTypeRepositoryMock.Object);
        }

        [TearDown]
        public void Teardown()
        {
            _productTypeUnitOfWorkMock?.Reset();
            _productTypeRepositoryMock?.Reset();
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
        public async Task AddProductTypeAsync_ShouldCallAddAndSave_WhenProductTypeIsValid()
        {
            // Arrange
            var productType = new ProductType
            {
                Id = Guid.NewGuid(),
                ProductTypeName = "Electronics",
                Description = "Devices and gadgets",
                ProductTypeCode = "PT78216"
            };

            _productTypeRepositoryMock.Setup(repo => repo.AddAsync(productType)).Verifiable();
            _productTypeUnitOfWorkMock.Setup(uow => uow.SaveAsync()).Verifiable();

            // Act
            await _productTypeManagementService.AddProductTypeAsync(productType);

            // Assert
            _productTypeRepositoryMock.VerifyAll();
            _productTypeUnitOfWorkMock.VerifyAll();
        }


        [Test]
        public async Task GetAllProductTypeAsync_ShouldReturnPagedProductTypeData_WhenCalledWithValidInputs()
        {
            // Arrange
            var pageIndex = 1;
            var pageSize = 10;
            var search = new DataTablesSearch { Regex = false, Value = "Electronics" };
            var order = "ProductTypeName";

            var expectedData = new List<ProductType>
            {
                new ProductType { Id = Guid.NewGuid(), ProductTypeName = "Electronics", Description = "Electronic devices" },
                new ProductType { Id = Guid.NewGuid(), ProductTypeName = "Furniture", Description = "Home furniture" }
            };
            var total = 20;
            var totalDisplay = 15;

            // Mock the repository to return the expected paged data
            _productTypeRepositoryMock.Setup(repo => repo.GetPagedProductTypesAsync(pageIndex, pageSize, search, order))
                .ReturnsAsync((expectedData, total, totalDisplay))
                .Verifiable();

            // Act
            var result = await _productTypeManagementService.GetAllProductTypeAsync(pageIndex, pageSize, search, order);

            // Assert
            _productTypeRepositoryMock.VerifyAll();
            _productTypeUnitOfWorkMock.VerifyAll();
        }


        [Test]
        public async Task GetProductTypeIdAsync_ShouldReturnProductType_WhenIdIsValid()
        {
            // Arrange
            var productTypeId = Guid.NewGuid();
            var expectedProductType = new ProductType
            {
                Id = productTypeId,
                ProductTypeName = "Electronics",
                Description = "Electronic devices",
                ProductTypeCode = "PTC3782"
            };

            // Mock the repository to return the expected product type
            _productTypeRepositoryMock.Setup(repo => repo.GetByIdAsync(productTypeId))
                .ReturnsAsync(expectedProductType).Verifiable();

            // Act
            var result = await _productTypeManagementService.GetProductTypeIdAsync(productTypeId);

            // Assert
            _productTypeRepositoryMock.VerifyAll();
            _productTypeUnitOfWorkMock.VerifyAll();
        }


        [Test]
        public async Task GetProductTypesAsync_ShouldReturnAllProductTypes_WhenCalled()
        {
            // Arrange
            var expectedProductTypes = new List<ProductType>
            {
                new ProductType { Id = Guid.NewGuid(), ProductTypeName = "Electronics", Description = "Electronic devices" },
                new ProductType { Id = Guid.NewGuid(), ProductTypeName = "Furniture", Description = "Home furniture" }
            };

            // Mock the repository to return the expected product types
            _productTypeRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(expectedProductTypes).Verifiable();

            // Act
            var result = await _productTypeManagementService.GetProductTypesAsync();

            // Assert
            _productTypeRepositoryMock.VerifyAll();
            _productTypeUnitOfWorkMock.VerifyAll();
        }


        [Test]
        public async Task DeleteProductTypeAsync_ShouldCallRemoveAndSave_WhenIdIsValid()
        {
            // Arrange
            var validProductTypeId = Guid.NewGuid();
            var productType = new ProductType { Id = validProductTypeId, ProductTypeName = "Test Product Type" };

            // Mock GetByIdAsync to return a valid product type
            _productTypeRepositoryMock.Setup(repo => repo.GetByIdAsync(validProductTypeId))
                .ReturnsAsync(productType).Verifiable();

            // Mock RemoveAsync and SaveAsync
            _productTypeRepositoryMock.Setup(repo => repo.RemoveAsync(validProductTypeId)).Verifiable();

            _productTypeUnitOfWorkMock.Setup(uow => uow.SaveAsync()).Verifiable();

            // Act
            await _productTypeManagementService.DeleteProductTypeAsync(validProductTypeId);

            // Assert
            _productTypeRepositoryMock.VerifyAll();
            _productTypeUnitOfWorkMock.VerifyAll();
        }

        [Test]
        public async Task DeleteProductTypeAsync_ShouldThrowKeyNotFoundException_WhenIdIsInvalid()
        {
            // Arrange
            var invalidProductTypeId = Guid.NewGuid();

            // Mock the GetByIdAsync method to return null for the invalid ID
            _productTypeRepositoryMock.Setup(repo => repo.GetByIdAsync(invalidProductTypeId))
                .ReturnsAsync((ProductType?)null).Verifiable();

            // Act
            var exception = Assert.ThrowsAsync<KeyNotFoundException>(
                async () => await _productTypeManagementService.DeleteProductTypeAsync(invalidProductTypeId)
            );  

            //Assert
            Assert.AreEqual($"Product type with ID {invalidProductTypeId} not found.", exception.Message);

            _productTypeRepositoryMock.VerifyAll();
            _productTypeUnitOfWorkMock.VerifyAll();
        }

        [Test]
        public async Task UpdateProductTypeAsync_ShouldCallEditAndSave_WhenValidProductTypeIsProvided()
        {
            // Arrange
            var validProductType = new ProductType
            {
                Id = Guid.NewGuid(),
                ProductTypeName = "Updated Product Type",
                ProductTypeCode = "PT378",
                Description = "Description",
            };

            // Mock EditAsync to simulate the update operation
            _productTypeRepositoryMock.Setup(repo => repo.EditAsync(validProductType)).Verifiable();

            // Mock SaveAsync to ensure it's called
            _productTypeUnitOfWorkMock.Setup(uow => uow.SaveAsync()).Verifiable();

            // Act
            await _productTypeManagementService.UpdateProductTypeAsync(validProductType);

            // Assert
            _productTypeRepositoryMock.VerifyAll();
            _productTypeUnitOfWorkMock.VerifyAll();
        }

    }
}
