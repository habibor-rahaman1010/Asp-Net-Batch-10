namespace DevSkill.Inventory.Domain.UnitOfWorkContracts
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        public void Save();
        public Task SaveAsync();

        public Task BeginTransactionAsync();
        public Task CommitTransactionAsync();
        public Task RollbackTransactionAsync();
    }
}