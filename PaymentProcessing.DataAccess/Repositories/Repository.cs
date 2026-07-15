using Microsoft.EntityFrameworkCore;
using PaymentProcessing.Domain;
using System.Linq;

namespace PaymentProcessing.DataAccess.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity<long>
    {

        protected readonly EFContext _context;

        public Repository(EFContext context)
        {
            _context = context;
        }

        public IQueryable<TEntity> GetAll()
        {
            return this._context.Set<TEntity>();
        }

        public async ValueTask CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            switch (_context.Entry(entity).State)
            {
                case EntityState.Added:
                    return;

                case EntityState.Detached:
                    await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
                    break;

                default:
                    throw new InvalidOperationException("Entity has already been attached");
            }
        }

        public ValueTask DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _context.Set<TEntity>().Remove(entity);

            cancellationToken.ThrowIfCancellationRequested();
            return ValueTask.CompletedTask;
        }

        public ValueTask UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            switch (_context.Entry(entity).State)
            {
                case EntityState.Detached:
                    _context.Set<TEntity>().Attach(entity);
                    _context.Entry(entity).State = EntityState.Modified;
                    break;

                case EntityState.Unchanged:
                    _context.Entry(entity).State = EntityState.Modified;
                    break;

                case EntityState.Modified:
                    break;

                case EntityState.Added:
                    throw new InvalidOperationException("Added entity cannot be reattached as modified");

                case EntityState.Deleted:
                    throw new InvalidOperationException("Deleted entity cannot be reattached as modified");

                default:
                    throw new ArgumentOutOfRangeException();
            }

            cancellationToken.ThrowIfCancellationRequested();
            return ValueTask.CompletedTask;
        }
    }
}
