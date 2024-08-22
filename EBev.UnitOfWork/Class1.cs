namespace EBev.UnitOfWork
{
    public interface IUnitOfWorkWrite : IDisposable
    {
        void Begin();

        void Commit();

        void Rollback();

        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

        int SaveChanges(bool ensureAutoHistory = false);

        Task<int> SaveChangesAsync(bool ensureAutoHistory = false);

        int ExecuteSqlCommand(string sql, params object[] parameters);

        IQueryable<TEntity> FromSql<TEntity>(string sql, params object[] parameters) where TEntity : class;
    }
}
