using Autofac.Extras.Moq;
using DevSkill.Inventory.Application.Services;
using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities.StockAdjustmentEntites;
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
    public class StockAdjustmentManagementServiceTests
    {
        private AutoMock _moq;
        private IStockAdjustmentManagementService _stockAdjustmentManagementService;
        private Mock<IStockAdjustmentRepository> _stockAdjustmentRepositoryMock;
        private Mock<IInventoryUnitOfWork> _stockAdjustmentUnitOfWorkMock;


        [SetUp]
        public void Setup()
        {
            _stockAdjustmentManagementService = _moq.Create<StockAdjustmentManagementService>();
            _stockAdjustmentRepositoryMock = _moq.Mock<IStockAdjustmentRepository>();
            _stockAdjustmentUnitOfWorkMock = _moq.Mock<IInventoryUnitOfWork>();
            _stockAdjustmentUnitOfWorkMock.Setup(x => x.StockAdjustmentRepository).Returns(_stockAdjustmentRepositoryMock.Object);
        }

        [TearDown]
        public void Teardown()
        {
            _stockAdjustmentRepositoryMock?.Reset();
            _stockAdjustmentUnitOfWorkMock?.Reset();
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
        public async Task AddStockAdjustmentAsync_ShouldCallAddAndSave_WhenValidStockAdjustmentIsProvided()
        {
            // Arrange
            var validStockAdjustment = new StockAdjustment
            {
                Id = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
                AdjustmentQuantity = 10,
                Reason = "Inventory correction"
            };

            _stockAdjustmentRepositoryMock.Setup(repo => repo.AddAsync(validStockAdjustment)).Verifiable();
            _stockAdjustmentUnitOfWorkMock.Setup(uow => uow.SaveAsync()).Verifiable();

            // Act
            await _stockAdjustmentManagementService.AddStockAdjustmentAsync(validStockAdjustment);

            // Assert
            _stockAdjustmentRepositoryMock.VerifyAll();
            _stockAdjustmentUnitOfWorkMock.VerifyAll();
        }

        [Test]
        public async Task GetAllStockAdjustmentAsync_ShouldReturnPagedStockAdjustments_WhenCalledWithValidParameters()
        {
            // Arrange
            var pageIndex = 1;
            var pageSize = 10;
            var search = new DataTablesSearch
            {
                Regex = false,
                Value = "AdjustmentReason"
            };
            var order = "LocationName";

            var expectedStockAdjustments = new List<StockAdjustment>
            {
                new StockAdjustment { Id = Guid.NewGuid(), ProductId = Guid.NewGuid(), AdjustmentQuantity = 5, Reason = "Correction"},
                new StockAdjustment { Id = Guid.NewGuid(), ProductId = Guid.NewGuid(), AdjustmentQuantity = 10, Reason = "Damaged goods" }
            };

            var total = 20;
            var totalDisplay = 15;

            // Mock the repository to return paged data
            _stockAdjustmentRepositoryMock.Setup(repo => repo.GetPagedStockAdjustmentsAsync(pageIndex, pageSize, search, order))
                .ReturnsAsync((expectedStockAdjustments, total, totalDisplay)).Verifiable();

            // Act
            var result = await _stockAdjustmentManagementService.GetAllStockAdjustmentAsync(pageIndex, pageSize, search, order);

            // Assert  
            _stockAdjustmentRepositoryMock.VerifyAll();
            _stockAdjustmentUnitOfWorkMock.VerifyAll();
        }

        [Test]
        public async Task GetAllStockAdjustmentAsync_ShouldReturnEmptyList_WhenNoStockAdjustmentsFound()
        {
            // Arrange
            var pageIndex = 1;
            var pageSize = 10;
            var search = new DataTablesSearch { Regex = false, Value = "NonExistingReason" };
            var order = "LocationName";

            var expectedStockAdjustments = new List<StockAdjustment>();
            var total = 0;
            var totalDisplay = 0;

            _stockAdjustmentRepositoryMock.Setup(repo => repo.GetPagedStockAdjustmentsAsync(pageIndex, pageSize, search, order))
                .ReturnsAsync((expectedStockAdjustments, total, totalDisplay)).Verifiable();

            // Act
            var result = await _stockAdjustmentManagementService.GetAllStockAdjustmentAsync(pageIndex, pageSize, search, order);

            // Assert
            _stockAdjustmentRepositoryMock.VerifyAll();
            _stockAdjustmentUnitOfWorkMock.VerifyAll();
        }


        [Test]
        public async Task GetStockAdjustmentyByIdAsync_ShouldReturnStockAdjustment_WhenValidIdIsProvided()
        {
            // Arrange
            var stockAdjustmentId = Guid.NewGuid();
            var expectedStockAdjustment = new StockAdjustment
            {
                Id = stockAdjustmentId,
                ProductId = Guid.NewGuid(),
                AdjustmentQuantity = 10,
                Reason = "Correction"
            };

            // Mock the repository to return the expected stock adjustment
            _stockAdjustmentRepositoryMock.Setup(repo => repo.GetStockAdjustmentyByIdAsync(stockAdjustmentId))
                .ReturnsAsync(expectedStockAdjustment).Verifiable();

            // Act
            var result = await _stockAdjustmentManagementService.GetStockAdjustmentByIdAsync(stockAdjustmentId);

            // Assert
            _stockAdjustmentRepositoryMock.VerifyAll();
            _stockAdjustmentUnitOfWorkMock.VerifyAll();
        }

        [Test]
        public async Task GetStockAdjustmentyByIdAsync_ShouldReturnNull_WhenStockAdjustmentNotFound()
        {
            // Arrange
            var stockAdjustmentId = Guid.NewGuid();

            // Mock the repository to return null
            _stockAdjustmentRepositoryMock.Setup(repo => repo.GetStockAdjustmentyByIdAsync(stockAdjustmentId))
                .ReturnsAsync((StockAdjustment)null).Verifiable();

            // Act
            var result = await _stockAdjustmentManagementService.GetStockAdjustmentByIdAsync(stockAdjustmentId);

            // Assert
            _stockAdjustmentRepositoryMock.VerifyAll();
            _stockAdjustmentUnitOfWorkMock.VerifyAll();
        }

        [Test]
        public async Task DeleteStockAdjustmentAsync_ShouldCallRemoveAndSave_WhenValidIdIsProvided()
        {
            // Arrange
            var stockAdjustmentId = Guid.NewGuid();

            _stockAdjustmentRepositoryMock.Setup(repo => repo.RemoveAsync(stockAdjustmentId)).Verifiable();
            _stockAdjustmentUnitOfWorkMock.Setup(uow => uow.SaveAsync()).Verifiable();

            // Act
            await _stockAdjustmentManagementService.DeleteStockAdjustmentAsync(stockAdjustmentId);

            // Assert
            _stockAdjustmentRepositoryMock.VerifyAll();
            _stockAdjustmentUnitOfWorkMock.VerifyAll();
        }


        [Test]
        public async Task DeleteStockAdjustmentAsync_ShouldThrowException_WhenIdIsInvalid()
        {
            // Arrange
            var invalidStockAdjustmentId = Guid.NewGuid();

            // Mock the repository's RemoveAsync method to throw a KeyNotFoundException for invalid ID
            _stockAdjustmentRepositoryMock.Setup(repo => repo.RemoveAsync(invalidStockAdjustmentId))
                .Throws(new KeyNotFoundException($"Stock adjustment with ID {invalidStockAdjustmentId} not found."))
                .Verifiable();

            // Act & Assert
            var exception = Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _stockAdjustmentManagementService.DeleteStockAdjustmentAsync(invalidStockAdjustmentId));

            Assert.AreEqual($"Stock adjustment with ID {invalidStockAdjustmentId} not found.", exception.Message);

            _stockAdjustmentRepositoryMock.VerifyAll();
            _stockAdjustmentUnitOfWorkMock.VerifyAll();
        }

    }
}
