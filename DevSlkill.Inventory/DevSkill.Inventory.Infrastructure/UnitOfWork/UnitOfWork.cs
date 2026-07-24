using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.UnitOfWorkContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace DevSkill.Inventory.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;
        private IDbContextTransaction? _transaction;
        protected ISqlUtility SqlUtility { get; private set; }

        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
            SqlUtility = new SqlUtility(_dbContext.Database.GetDbConnection());
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            if (_transaction != null)
            {
                return;
            }

            _transaction = await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("No active transaction.");
            }

            try
            {
                await _dbContext.SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            finally
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction == null)
            {
                return;
            }

            try
            {
                await _transaction.RollbackAsync();
            }
            finally
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }


        public void Dispose()
        {
            _transaction?.Dispose();
            _dbContext.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
            }

            await _dbContext.DisposeAsync();
        }
    }
}