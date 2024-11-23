using Autofac.Extras.Moq;
using DevSkill.Inventory.Application.Services;
using DevSkill.Inventory.Application.ServicesContract;
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

    }
}
