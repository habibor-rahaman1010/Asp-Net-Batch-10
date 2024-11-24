using Autofac.Extras.Moq;
using DevSkill.Inventory.Application.Services;
using DevSkill.Inventory.Application.ServicesContract;
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
    }
}
