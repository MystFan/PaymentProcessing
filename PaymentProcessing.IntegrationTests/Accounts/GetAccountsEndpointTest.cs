using PaymentProcessing.Domain;
using PaymentProcessing.IntegrationTests.ResponseModels;
using System.Text.Json;

namespace PaymentProcessing.IntegrationTests.Accounts
{
    public class GetAccountsEndpointTest : EndpointTest
    {
        [Fact]
        public async Task ShouldGetAllAccounts()
        {
            var database = new DatabaseHelper(Context);

            var customer = new Customer
            {
                Name = "Alice",
                Email = "alice@example.com"
            };

            await database.CreateCustomerAsync(customer);

            var account = new Account
            {
                CustomerId = customer.Id,
                Balance = 500.0m,
                Currency = "USD"
            };

            await database.CreateAccountAsync(account);

            var accountsResponse = await Request.GetAsync("/accounts");

            await Expect(accountsResponse).ToBeOKAsync();

            var accountsElement = await accountsResponse.JsonAsync();

            var accounts = new List<AccountResponse>();
            foreach (JsonElement accountElement in accountsElement?.EnumerateArray() ?? [])
            {
                var accountResponse = new AccountResponse();

                if (accountElement.TryGetProperty("id", out var id))
                {
                    accountResponse.Id = id.GetInt64();
                }

                if (accountElement.TryGetProperty("customerId", out var customerId))
                {
                    accountResponse.CustomerId = customerId.GetInt64();
                }

                if (accountElement.TryGetProperty("balance", out var balance))
                {
                    accountResponse.Balance = balance.GetDecimal();
                }

                if (accountElement.TryGetProperty("currency", out var currency))
                {
                    accountResponse.Currency = currency.GetString();
                }

                accounts.Add(accountResponse);
            }

            var dbAccounts = await database.AccountsAsync();

            accounts = accounts.OrderBy(c => c.Id).ToList();

            Assert.Multiple(() =>
            {
                Assert.Equal(dbAccounts.Count(), accounts.Count);

                for (int i = 0; i < dbAccounts.Count(); i++)
                {
                    Assert.Equal(dbAccounts[i].Id, accounts[i].Id);
                    Assert.Equal(dbAccounts[i].CustomerId, accounts[i].CustomerId);
                    Assert.Equal(dbAccounts[i].Balance, accounts[i].Balance);
                    Assert.Equal(dbAccounts[i].Currency, accounts[i].Currency);
                }
            });
        }

        [Fact]
        public async Task ShouldGetAccountsForSpecificCustomer()
        {
            var database = new DatabaseHelper(Context);

            // Create customer A
            var customerA = new Customer
            {
                Name = "Alice",
                Email = "alice@example.com"
            };
            await database.CreateCustomerAsync(customerA);

            var accountA = new Account
            {
                CustomerId = customerA.Id,
                Balance = 100.0m,
                Currency = "USD"
            };
            await database.CreateAccountAsync(accountA);

            // Create customer B
            var customerB = new Customer
            {
                Name = "Bob",
                Email = "bob@example.com"
            };
            await database.CreateCustomerAsync(customerB);

            var accountB = new Account
            {
                CustomerId = customerB.Id,
                Balance = 200.0m,
                Currency = "USD"
            };
            await database.CreateAccountAsync(accountB);

            string urlWithCustomerA = $"/accounts?customerid={customerA.Id}";
            string urlWithCustomerB = $"/accounts?customerid={customerB.Id}";

            // Get accounts for specific customer A - should only return 1 account
            var accountsForCustomerAResponse = await Request.GetAsync(urlWithCustomerA);
            await Expect(accountsForCustomerAResponse).ToBeOKAsync();

            var accountsForCustomerAElement = await accountsForCustomerAResponse.JsonAsync();
            Assert.NotNull(accountsForCustomerAElement?.EnumerateArray());

            int countA = 0;
            foreach (JsonElement accountElement in accountsForCustomerAElement?.EnumerateArray() ?? [])
            {
                if (accountElement.TryGetProperty("customerId", out var customerId))
                {
                    Assert.Equal(customerA.Id, customerId.GetInt64());
                    countA++;
                }
            }

            // Should only have 1 account for customer A
            Assert.Equal(1, countA);

            // Get accounts for specific customer B - should only return 1 account
            var accountsForCustomerBResponse = await Request.GetAsync(urlWithCustomerB);
            await Expect(accountsForCustomerBResponse).ToBeOKAsync();

            var accountsForCustomerBElement = await accountsForCustomerBResponse.JsonAsync();
            Assert.NotNull(accountsForCustomerBElement?.EnumerateArray());

            int countB = 0;
            foreach (JsonElement accountElement in accountsForCustomerBElement?.EnumerateArray() ?? [])
            {
                if (accountElement.TryGetProperty("customerId", out var customerId))
                {
                    Assert.Equal(customerB.Id, customerId.GetInt64());
                    countB++;
                }
            }

            // Should only have 1 account for customer B
            Assert.Equal(1, countB);
        }
    }
}
