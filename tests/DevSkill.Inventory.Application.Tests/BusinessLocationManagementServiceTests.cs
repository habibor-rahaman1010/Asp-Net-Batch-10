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
    public class BusinessLocationManagementServiceTests
    {
        private AutoMock _moq;
        private IBusinessLocationManagementService _businessLocationManagementService;
        private Mock<IBusinessLocationRepository> _businessLocationRepositoryMock;
        private Mock<IInventoryUnitOfWork> _businessLocationUnitOfWorkMock;

        [SetUp]
        public void Setup()
        {
            _businessLocationManagementService = _moq.Create<BusinessLocationManagementService>();
            _businessLocationRepositoryMock = _moq.Mock<IBusinessLocationRepository>();
            _businessLocationUnitOfWorkMock = _moq.Mock<IInventoryUnitOfWork>();
            _businessLocationUnitOfWorkMock.Setup(x => x.BusinessLocationRepository).Returns(_businessLocationRepositoryMock.Object);
        }

        [TearDown]
        public void Teardown()
        {
            _businessLocationRepositoryMock?.Reset();
            _businessLocationUnitOfWorkMock?.Reset();
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
        public async Task AddBusinessLocationAsync_ShouldCallAddAndSave_WhenValidBusinessLocationIsProvided()
        {
            // Arrange
            var businessLocation = new BusinessLocation
            {
                Id = Guid.NewGuid(),
                LocationName = "Main Warehouse",
                Address = "123 Business St, City, Country"
            };

            _businessLocationRepositoryMock.Setup(repo => repo.AddAsync(businessLocation)).Verifiable();
            _businessLocationUnitOfWorkMock.Setup(uow => uow.SaveAsync()).Verifiable();

            // Act
            await _businessLocationManagementService.AddBusinessLocationAsync(businessLocation);

            // Assert
            _businessLocationUnitOfWorkMock.VerifyAll();
            _businessLocationUnitOfWorkMock.VerifyAll();
        }

        [Test]
        public async Task GetAllBusinessLocationAsync_ShouldReturnPagedBusinessLocations_WhenValidParametersAreProvided()
        {
            // Arrange
            var pageIndex = 1;
            var pageSize = 10;
            var search = new DataTablesSearch
            {
                Value = "Warehouse",
                Regex = false
            };
            var order = "LocationName";

            var expectedBusinessLocations = new List<BusinessLocation>
            {
                new BusinessLocation { Id = Guid.NewGuid(), LocationName = "Main Warehouse", Address = "123 Business St, City" },
                new BusinessLocation { Id = Guid.NewGuid(), LocationName = "Secondary Warehouse", Address = "456 Market Ave, City" }
            };

            var total = 20;
            var totalDisplay = 15;

            _businessLocationRepositoryMock.Setup(repo => repo.GetPagedBusinessLocationAsync(pageIndex, pageSize, search, order))
                .ReturnsAsync((expectedBusinessLocations, total, totalDisplay)).Verifiable();

            // Act
            var result = await _businessLocationManagementService.GetAllBusinessLocationAsync(pageIndex, pageSize, search, order);

            // Assert
            _businessLocationRepositoryMock.VerifyAll();
            _businessLocationUnitOfWorkMock.VerifyAll();
        }


        [Test]
        public async Task GetAllBusinessLocationAsync_ShouldReturnEmptyList_WhenNoMatchesAreFound()
        {
            // Arrange
            var pageIndex = 1;
            var pageSize = 10;
            var search = new DataTablesSearch { Value = "NonExistentLocation", Regex = false };
            var order = "LocationName";

            var expectedBusinessLocations = new List<BusinessLocation>();
            var total = 0;
            var totalDisplay = 0;

            _businessLocationRepositoryMock.Setup(repo => repo.GetPagedBusinessLocationAsync(pageIndex, pageSize, search, order))
                .ReturnsAsync((expectedBusinessLocations, total, totalDisplay)).Verifiable();

            // Act
            var result = await _businessLocationManagementService.GetAllBusinessLocationAsync(pageIndex, pageSize, search, order);

            // Assert

            _businessLocationRepositoryMock.VerifyAll();
            _businessLocationUnitOfWorkMock.VerifyAll();
        }


        [Test]
        public async Task GetAllBusinessLocationAsync_ShouldReturnAllBusinessLocations_WhenCalled()
        {
            // Arrange
            var expectedBusinessLocations = new List<BusinessLocation>
            {
                new BusinessLocation { Id = Guid.NewGuid(), LocationName = "Main Warehouse", Address = "123 Business St, City" },
                new BusinessLocation { Id = Guid.NewGuid(), LocationName = "Secondary Warehouse", Address = "456 Market Ave, City" }
            };

            _businessLocationRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(expectedBusinessLocations).Verifiable();

            // Act
            var result = await _businessLocationManagementService.GetAllBusinessLocationAsync();

            // Assert
            _businessLocationRepositoryMock.VerifyAll();
            _businessLocationUnitOfWorkMock.VerifyAll();
        }

        [Test]
        public void GetAllBusinessLocationAsync_ShouldThrowException_WhenRepositoryFails()
        {
            // Arrange
            _businessLocationUnitOfWorkMock.Setup(uow => uow.BusinessLocationRepository.GetAllAsync())
                .ThrowsAsync(new Exception("Database failure"));

            // Act & Assert
            var exception = Assert.ThrowsAsync<Exception>(async () =>
                await _businessLocationManagementService.GetAllBusinessLocationAsync());

            Assert.AreEqual("Database failure", exception.Message);
        }


        [Test]
        public async Task GetBusinessLocationByIdAsync_ShouldReturnBusinessLocation_WhenValidIdIsProvided()
        {
            // Arrange
            var businessLocationId = Guid.NewGuid();
            var expectedBusinessLocation = new BusinessLocation
            {
                Id = businessLocationId,
                LocationName = "Main Warehouse",
                Address = "123 Business St, City"
            };

            _businessLocationRepositoryMock.Setup(repo => repo.GetByIdAsync(businessLocationId))
                .ReturnsAsync(expectedBusinessLocation)
                .Verifiable();

            // Act
            var result = await _businessLocationManagementService.GetBusinessLocationByIdAsync(businessLocationId);

            // Assert
            _businessLocationRepositoryMock.VerifyAll();
            _businessLocationUnitOfWorkMock.VerifyAll();
        }


        [Test]
        public async Task GetBusinessLocationByIdAsync_ShouldReturnNull_WhenInvalidIdIsProvided()
        {
            // Arrange
            var invalidBusinessLocationId = Guid.NewGuid();

            _businessLocationRepositoryMock.Setup(repo => repo.GetByIdAsync(invalidBusinessLocationId))
                .ReturnsAsync((BusinessLocation)null)
                .Verifiable();

            // Act
            var result = await _businessLocationManagementService.GetBusinessLocationByIdAsync(invalidBusinessLocationId);

            // Assert
            _businessLocationRepositoryMock.VerifyAll();
            _businessLocationUnitOfWorkMock.VerifyAll();
        }


        [Test]
        public async Task UpdateBusinessLocationAsync_ShouldUpdateBusinessLocation_WhenValidBusinessLocationIsProvided()
        {
            // Arrange
            var businessLocationId = Guid.NewGuid();
            var updatedBusinessLocation = new BusinessLocation
            {
                Id = businessLocationId,
                LocationName = "New Warehouse",
                Address = "456 Updated St, New City"
            };

            _businessLocationRepositoryMock.Setup(repo => repo.EditAsync(updatedBusinessLocation)).Verifiable();
            _businessLocationUnitOfWorkMock.Setup(uow => uow.SaveAsync()).Verifiable();

            // Act
            await _businessLocationManagementService.UpdateBusinessLocationAsync(updatedBusinessLocation);

            // Assert
            _businessLocationRepositoryMock.VerifyAll();
            _businessLocationUnitOfWorkMock.VerifyAll();
        }

        [Test]
        public void UpdateBusinessLocationAsync_ShouldThrowKeyNotFoundException_WhenBusinessLocationIsNotFound()
        {
            // Arrange
            var businessLocationId = Guid.NewGuid();
            var nonExistentBusinessLocation = new BusinessLocation
            {
                Id = businessLocationId,
                LocationName = "Non-existent Warehouse",
                Address = "123 Fake St"
            };

            _businessLocationRepositoryMock.Setup(repo => repo.EditAsync(nonExistentBusinessLocation))
                .ThrowsAsync(new KeyNotFoundException("Business location not found"));

            // Act & Assert
            var exception = Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _businessLocationManagementService.UpdateBusinessLocationAsync(nonExistentBusinessLocation));

            Assert.AreEqual("Business location not found", exception.Message);
        }


        [Test]
        public async Task DeleteBusinessLocationAsync_ShouldDeleteBusinessLocation_WhenValidIdIsProvided()
        {
            // Arrange
            var businessLocationId = Guid.NewGuid();

            _businessLocationRepositoryMock.Setup(repo => repo.RemoveAsync(businessLocationId)).Verifiable();
            _businessLocationUnitOfWorkMock.Setup(uow => uow.SaveAsync()).Verifiable();

            // Act
            await _businessLocationManagementService.DeleteBusinessLocationAsync(businessLocationId);

            // Assert
            _businessLocationUnitOfWorkMock.VerifyAll();
            _businessLocationUnitOfWorkMock.VerifyAll();
        }

        [Test]
        public void DeleteBusinessLocationAsync_ShouldThrowKeyNotFoundException_WhenBusinessLocationIsNotFound()
        {
            // Arrange
            var businessLocationId = Guid.NewGuid();

            _businessLocationRepositoryMock.Setup(repo => repo.RemoveAsync(businessLocationId))
                .ThrowsAsync(new KeyNotFoundException("Business location not found"));

            // Act & Assert
            var exception = Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _businessLocationManagementService.DeleteBusinessLocationAsync(businessLocationId));

            Assert.AreEqual("Business location not found", exception.Message);
        }

    }
}
