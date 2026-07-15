using PaymentProcessing.Domain;

namespace PaymentProcessing.DataAccess.Repositories
{
    public interface IRepository<TEntity>
        where TEntity : class, IEntity<long>
    {
        ValueTask CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

        ValueTask DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

        ValueTask UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        IQueryable<TEntity> GetAll();
    }
}
