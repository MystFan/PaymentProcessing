using System.Collections.Generic;
using System.Threading.Tasks;
using PaymentProcessing.Domain.Models;

namespace PaymentProcessing.Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T?> GetByIdAsync(Guid id);
        Task<IList<T>> FindAllAsync();
        Task<T?> FindByAccountNumberAsync(string accountNumber);
    }

    public interface ICustomerRepository : IRepository<Customer> { }
    public interface IAccountRepository : IRepository<Account> { }
    public interface ITransactionRepository : IRepository<Transaction> { }
}
