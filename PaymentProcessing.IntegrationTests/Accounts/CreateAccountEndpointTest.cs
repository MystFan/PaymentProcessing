using Microsoft.Playwright;
using PaymentProcessing.Domain;
using PaymentProcessing.Domain.Exceptions;
using PaymentProcessing.IntegrationTests.Models;

namespace PaymentProcessing.IntegrationTests.Accounts
{
    public class CreateAccountEndpointTest : EndpointTest
    {
        [Fact]
        public async Task ShouldCreateAccount()
        {
            var database = new DatabaseHelper(Context);

            var customer = new Customer
            {
                Name = "Alice",
                Email = $"{Guid.NewGuid()}@example.com"
            };

            await database.CreateCustomerAsync(customer);

            var newAccount = new CreateAccountRequest
            {
                CustomerId = customer.Id,
                Balance = 100.5m,
                Currency = "USD"
            };

            var accountResponse = await Request.PostAsync("/accounts", new APIRequestContextOptions { DataObject = newAccount });

            await Expect(accountResponse).ToBeOKAsync();

            var dbCustomer = await database.CustomerByEmailAsync(customer.Email);
            var dbAccount = await database.AccountByCustomerIdAsync(dbCustomer!.Id);

            Assert.NotNull(dbAccount);
            Assert.Equal(customer.Id, dbAccount.CustomerId);
            Assert.Equal(100.5m, dbAccount.Balance);
            Assert.Equal("USD", dbAccount.Currency);
        }

        [Fact]
        public async Task ShouldFailWithoutCustomerId()
        {
            var database = new DatabaseHelper(Context);

            var customer = new Customer
            {
                Name = "Alice",
                Email = "alice@example.com"
            };

            await database.CreateCustomerAsync(customer);

            var newAccount = new CreateAccountRequest
            {
                Balance = 100.5m,
                Currency = "USD"
            };

            var accountResponse = await Request.PostAsync("/accounts", new APIRequestContextOptions { DataObject = newAccount });

            await Expect(accountResponse).Not.ToBeOKAsync();

            var errorsElement = await accountResponse.JsonAsync();

            string? errorMessage = await accountResponse.GetValidationErrorMessageAsync("customerId");
            ExceptionReasonCode? errorCode = await accountResponse.GetValidationErrorCodeAsync();

            Assert.Multiple(() =>
            {
                Assert.Equal(400, accountResponse.Status);
                Assert.Equal($"Invalid customer id.", errorMessage);
                Assert.Equal(ExceptionReasonCode.InvalidRequest, errorCode);
            });
        }

        [Fact]
        public async Task ShouldFailWithNegativeBalance()
        {
            var database = new DatabaseHelper(Context);

            var customer = new Customer
            {
                Name = "Alice",
                Email = "alice@example.com"
            };

            await database.CreateCustomerAsync(customer);

            var newAccount = new CreateAccountRequest
            {
                CustomerId = customer.Id,
                Balance = -1,
                Currency = "USD"
            };

            var accountResponse = await Request.PostAsync("/accounts", new APIRequestContextOptions { DataObject = newAccount });

            await Expect(accountResponse).Not.ToBeOKAsync();

            var errorsElement = await accountResponse.JsonAsync();

            string? errorMessage = await accountResponse.GetValidationErrorMessageAsync("balance");
            ExceptionReasonCode? errorCode = await accountResponse.GetValidationErrorCodeAsync();

            Assert.Multiple(() =>
            {
                Assert.Equal(400, accountResponse.Status);
                Assert.Equal("'Balance' must be greater than or equal to '0'.", errorMessage);
                Assert.Equal(ExceptionReasonCode.InvalidRequest, errorCode);
            });
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("ZYW")]
        public async Task ShouldFailWithInvalidCurrency(string? currency)
        {
            var database = new DatabaseHelper(Context);

            var customer = new Customer
            {
                Name = "Alice",
                Email = "alice@example.com"
            };

            await database.CreateCustomerAsync(customer);

            var newAccount = new CreateAccountRequest
            {
                CustomerId = customer.Id,
                Balance = 100.5m,
                Currency = currency!
            };

            var accountResponse = await Request.PostAsync("/accounts", new APIRequestContextOptions { DataObject = newAccount });

            var errorsElement = await accountResponse.JsonAsync();

            string? errorMessage = await accountResponse.GetValidationErrorMessageAsync("currency");
            ExceptionReasonCode? errorCode = await accountResponse.GetValidationErrorCodeAsync();

            Assert.Multiple(() =>
            {
                Assert.Equal(400, accountResponse.Status);
                Assert.Equal("Currency must be a valid currency code.", errorMessage);
                Assert.Equal(ExceptionReasonCode.InvalidRequest, errorCode);
            });
        }

        [Fact]
        public async Task ShouldFailWithInvalidCustomerId()
        {
            var newAccount = new CreateAccountRequest
            {
                CustomerId = 999999999999,
                Balance = 100.5m,
                Currency = "EUR"
            };

            var accountResponse = await Request.PostAsync("/accounts", new APIRequestContextOptions { DataObject = newAccount });

            await Expect(accountResponse).Not.ToBeOKAsync();

            var errorsElement = await accountResponse.JsonAsync();

            string? errorMessage = await accountResponse.GetErrorMessageAsync();
            ExceptionReasonCode? errorCode = await accountResponse.GetValidationErrorCodeAsync();

            Assert.Multiple(() =>
            {
                Assert.Equal(400, accountResponse.Status);
                Assert.Equal("Customer not found.", errorMessage);
                Assert.Equal(ExceptionReasonCode.CustomerNotFound, errorCode);
            });
        }
    }
}
