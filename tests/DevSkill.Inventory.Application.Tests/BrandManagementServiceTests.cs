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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Application.Tests
{
    public class BrandManagementServiceTests
    {
        private AutoMock _moq;
        private IBrandManagementService _brandManagementService;
        private Mock<IBrandRepository> _brandRepositoryMock;
        private Mock<IInventoryUnitOfWork> _brandUnitOfWorkMock;

        [SetUp]
        public void Setup()
        {
            _brandManagementService = _moq.Create<BrandManagementService>();
            _brandRepositoryMock = _moq.Mock<IBrandRepository>();
            _brandUnitOfWorkMock = _moq.Mock<IInventoryUnitOfWork>();
            _brandUnitOfWorkMock.Setup(x => x.BrandRepository).Returns(_brandRepositoryMock.Object);
        }

        [TearDown]
        public void Teardown()
        {
            _brandRepositoryMock?.Reset();
            _brandUnitOfWorkMock?.Reset();
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
        public async Task AddBrandAsync_ShouldAddBrand_WhenValidDataIsProvided()
        {
            // Arrange
            var brand = new Brand
            {
                Id = Guid.NewGuid(),
                BrandName = "Samsung"
            };

            // Set up mocks
            _brandRepositoryMock.Setup(repo => repo.AddAsync(brand)).Verifiable();
            _brandUnitOfWorkMock.Setup(uow => uow.SaveAsync()).Verifiable();

            // Act
            await _brandManagementService.AddBrandAsync(brand);

            // Assert
            _brandRepositoryMock.VerifyAll();
            _brandUnitOfWorkMock.VerifyAll();
        }

        [Test]
        public async Task GetBrandsAsync_ShouldReturnPagedBrands_WhenValidParametersAreProvided()
        {
            // Arrange
            var pageIndex = 1;
            var pageSize = 10;
            var search = new DataTablesSearch
            {
                Value = "Samsung",
                Regex = false
            };
            var order = "BrandName";

            var expectedData = new List<Brand>
            {
                new Brand { Id = Guid.NewGuid(), BrandName = "Samsung" },
                new Brand { Id = Guid.NewGuid(), BrandName = "Apple" }
            };

            var total = 2;
            var totalDisplay = 2;

            // Set up the mock for GetPagedBrandsAsync
            _brandRepositoryMock.Setup(repo => repo.GetPagedBrandsAsync(pageIndex, pageSize, search, order))
                                .ReturnsAsync((expectedData, total, totalDisplay))
                                .Verifiable();

            // Act
            var result = await _brandManagementService.GetBrandsAsync(pageIndex, pageSize, search, order);

            // Assert
            _brandRepositoryMock.VerifyAll();
            _brandUnitOfWorkMock.VerifyAll();
        }


        [Test]
        public async Task GetAllBrandAsync_ShouldReturnAllBrands_WhenCalled()
        {
            // Arrange
            var expectedBrands = new List<Brand>
        {
            new Brand { Id = Guid.NewGuid(), BrandName = "Samsung" },
            new Brand { Id = Guid.NewGuid(), BrandName = "Apple" },
            new Brand { Id = Guid.NewGuid(), BrandName = "Huawei" }
        };

            // Set up the mock for GetAllAsync
            _brandRepositoryMock.Setup(repo => repo.GetAllAsync())
                                .ReturnsAsync(expectedBrands)
                                .Verifiable();

            // Act
            var result = await _brandManagementService.GetAllBrandAsync();

            // Assert
            _brandRepositoryMock.VerifyAll();
            _brandUnitOfWorkMock.VerifyAll();
        }

        [Test]
        public async Task GetBrandByIdAsync_ShouldReturnBrand_WhenValidIdIsProvided()
        {
            // Arrange
            var brandId = Guid.NewGuid();
            var expectedBrand = new Brand
            {
                Id = brandId,
                BrandName = "Samsung"
            };

            _brandRepositoryMock.Setup(repo => repo.GetByIdAsync(brandId))
                                .ReturnsAsync(expectedBrand)
                                .Verifiable();

            // Act
            var result = await _brandManagementService.GetBrandByIdAsync(brandId);

            // Assert
            _brandRepositoryMock.VerifyAll();
            _brandUnitOfWorkMock.VerifyAll();
        }


        [Test]
        public async Task DeleteBrandAsync_ShouldDeleteBrand_WhenValidIdIsProvided()
        {
            // Arrange
            var brandId = Guid.NewGuid();

            _brandRepositoryMock.Setup(repo => repo.RemoveAsync(brandId)).Verifiable();
            _brandUnitOfWorkMock.Setup(uow => uow.SaveAsync()).Verifiable();

            // Act
            await _brandManagementService.DeleteBrandAsync(brandId);

            // Assert
            _brandRepositoryMock.VerifyAll();
            _brandUnitOfWorkMock.VerifyAll();
        }

        [Test]
        public async Task UpdateBrandAsync_ShouldUpdateBrand_WhenValidBrandIsProvided()
        {
            // Arrange
            var brand = new Brand
            {
                Id = Guid.NewGuid(),
                BrandName = "Samsung"
            };

            _brandRepositoryMock.Setup(repo => repo.EditAsync(brand)).Verifiable();
            _brandUnitOfWorkMock.Setup(uow => uow.SaveAsync()).Verifiable();

            // Act
            await _brandManagementService.UpdateBrandAsync(brand);

            // Assert
            _brandUnitOfWorkMock.VerifyAll();
            _brandUnitOfWorkMock.VerifyAll();
        }

    }
}
