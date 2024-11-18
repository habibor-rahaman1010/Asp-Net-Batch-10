using Autofac.Extras.Moq;
using DevSkill.Inventory.Application.Services;
using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.RepositoryContracts;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace DevSkill.Inventory.Application.Tests
{
    [ExcludeFromCodeCoverage]
    public class CategoryManagementServiceTests
    {
        private AutoMock _moq;
        private ICategoryManagementService _categoryManagementService;
        private Mock<IInventoryUnitOfWork> _categoryUnitOfWorkMock;
        private Mock<ICategoryRepository> _categoryRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _categoryManagementService = _moq.Create<CategoryManagementService>();
            _categoryUnitOfWorkMock = _moq.Mock<IInventoryUnitOfWork>();
            _categoryRepositoryMock = _moq.Mock<ICategoryRepository>();
        }

        [TearDown] 
        public void Teardown() 
        { 
            _categoryUnitOfWorkMock?.Reset();
            _categoryRepositoryMock?.Reset();
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
        public void AddCategoryAsync_ShouldAddCategory_WhenValidDataIsProvided()
        {
            //Arrange
            var category = new Category()
            {
                Id = Guid.NewGuid(),
                CategoryName = "Networking",
                CategoryCode = "CT256",
                Description = "This is my networking category"
            };

            _categoryUnitOfWorkMock.Setup(x => x.CategoryRepository).Returns(_categoryRepositoryMock.Object);
            _categoryRepositoryMock.Setup(x => x.AddAsync(category)).Verifiable();
            _categoryUnitOfWorkMock.Setup(x => x.SaveAsync()).Verifiable();

            //Act
            _categoryManagementService.AddCategoryAsync(category);

            //Assert
            _categoryRepositoryMock.VerifyAll();
            _categoryUnitOfWorkMock.VerifyAll();
        }

    }
}