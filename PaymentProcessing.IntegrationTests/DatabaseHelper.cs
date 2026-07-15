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

        public async Task<Customer?> CustomerByEmailAsync(string email)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<Customer?> CustomerByNameAsync(string name)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Name == name);
        }

        public async ValueTask CreateCustomerAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }
    }
}
