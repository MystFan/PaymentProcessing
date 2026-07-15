using Microsoft.EntityFrameworkCore;
using PaymentProcessing.DataAccess;
using PaymentProcessing.Domain;

namespace PaymentProcessing.IntegrationTests
{
    internal class DatabaseHelper
    {
        private readonly EFContext _context;

        public DatabaseHelper(EFContext context)
        {
            _context = context;
        }

        public async Task<Customer[]> CustomersAsync()
        {
            return await _context.Customers
                .OrderBy(c => c.Id)
                .Select(c => new Customer
                {
                    Id = c.Id,
                    Name = c.Name,
                    Email = c.Email
                })
                .ToArrayAsync();
        }

        public async Task<Account[]> AccountsAsync()
        {
            return await _context.Accounts
                .OrderBy(a => a.Id)
                .Select(a => new Account
                {
                    Id = a.Id,
                    CustomerId = a.CustomerId,
                    Balance = a.Balance,
                    Currency = a.Currency
                })
                .ToArrayAsync();
        }

        public async Task<Customer?> CustomerByEmailAsync(string email)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<Customer?> CustomerByNameAsync(string name)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task<Account?> AccountByCustomerIdAsync(long id)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.CustomerId == id);
        }

        public async ValueTask CreateCustomerAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async ValueTask CreateAccountAsync(Account account)
        {
            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
        }
    }
}
