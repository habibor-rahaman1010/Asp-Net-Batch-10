using Autofac.Extras.Moq;
using DevSkill.Inventory.Application.Services;
using DevSkill.Inventory.Application.ServicesContract;
using DevSkill.Inventory.Domain;
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
            _categoryUnitOfWorkMock.Setup(x => x.CategoryRepository).Returns(_categoryRepositoryMock.Object);
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
         
            _categoryRepositoryMock.Setup(x => x.AddAsync(category)).Verifiable();
            _categoryUnitOfWorkMock.Setup(x => x.SaveAsync()).Verifiable();

            //Act
            _categoryManagementService.AddCategoryAsync(category);

            //Assert
            _categoryRepositoryMock.VerifyAll();
            _categoryUnitOfWorkMock.VerifyAll();
        }

        [Test]
        public async Task UpdateCategoryAsync_ShouldUpdateCategory_WhenValidDataIsProvided()
        {
            // Arrange
            var category = new Category
            {
                Id = Guid.NewGuid(),
                CategoryName = "Updated Networking",
                CategoryCode = "CT256",
                Description = "Updated description for networking category"
            };
            
            _categoryRepositoryMock.Setup(x => x.EditAsync(category)).Verifiable();
            _categoryUnitOfWorkMock.Setup(x => x.SaveAsync()).Verifiable();

            // Act
            await _categoryManagementService.UpdateCategoryAsync(category);

            // Assert
            _categoryRepositoryMock.VerifyAll();
            _categoryUnitOfWorkMock.VerifyAll();
        }

        [Test]
        public async Task DeleteCategoryAsync_ShouldDeleteCategory_WhenValidIdIsProvided()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
          
            _categoryRepositoryMock.Setup(x => x.RemoveAsync(categoryId)).Verifiable();
            _categoryUnitOfWorkMock.Setup(x => x.SaveAsync()).Verifiable();

            // Act
            await _categoryManagementService.DeleteCategoryAsync(categoryId);

            // Assert
            _categoryRepositoryMock.VerifyAll();
            _categoryUnitOfWorkMock.VerifyAll();
        }

        [Test]
        public async Task GetCategoriesAsync_ShouldReturnAllCategories_WhenCalled()
        {
            // Arrange
            var mockCategories = new List<Category>
            {
                new Category { Id = Guid.NewGuid(), CategoryName = "Category1", CategoryCode = "C001" },
                new Category { Id = Guid.NewGuid(), CategoryName = "Category2", CategoryCode = "C002" }
            };

            _categoryRepositoryMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(mockCategories);

            // Act
            var result = await _categoryManagementService.GetCategoriesAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(mockCategories.Count, result.Count);
            Assert.AreEqual(mockCategories, result);

            _categoryRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
        }


        [Test]
        public async Task GetCategoriesAsync_ShouldReturnPagedCategories_WhenCalledWithValidParameters()
        {
            // Arrange
            var pageIndex = 1;
            var pageSize = 10;

            var search = new DataTablesSearch
            {
                Value = "test",
                Regex = false
            };
            var order = "CategoryName";

            var mockCategories = new List<Category>
            {
                new Category { Id = Guid.NewGuid(), CategoryName = "Category1", CategoryCode = "C001" },
                new Category { Id = Guid.NewGuid(), CategoryName = "Category2", CategoryCode = "C002" }
            };

            var expectedTotal = 50; 
            var expectedTotalDisplay = 2;

            _categoryRepositoryMock.Setup(x => x.GetPagedCategoriesAsync(pageIndex, pageSize, search, order))
                .ReturnsAsync((mockCategories, expectedTotal, expectedTotalDisplay));

            // Act
            var result = await _categoryManagementService.GetCategoriesAsync(pageIndex, pageSize, search, order);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(mockCategories, result.data);
            Assert.AreEqual(expectedTotal, result.total);
            Assert.AreEqual(expectedTotalDisplay, result.totalDisplay);

            _categoryRepositoryMock.VerifyAll();
        }

    }
}
