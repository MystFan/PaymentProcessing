using PaymentProcessing.IntegrationTests.ResponseModels;
using System.Text.Json;

namespace PaymentProcessing.IntegrationTests.Customers
{
    public class GetCustomersEndpointTest : EndpointTest
    {
        [Fact]
        public async Task ShouldGetAllCustomers()
        {
            var database = new DatabaseHelper(Context);
            var dbCustomers = await database.CustomersAsync();

            var customersResponse = await Request.GetAsync("/customers");

            await Expect(customersResponse).ToBeOKAsync();

            var customersElement = await customersResponse.JsonAsync();

            var customers = new List<CustomerResponse>();
            foreach (JsonElement customerElement in customersElement?.EnumerateArray() ?? [])
            {
                var customer = new CustomerResponse();

                if (customerElement.TryGetProperty("id", out var id))
                {
                    customer.Id = id.GetInt64();
                }

                if (customerElement.TryGetProperty("name", out var name))
                {
                    customer.Name = name.GetString();
                }

                if (customerElement.TryGetProperty("email", out var email))
                {
                    customer.Email = email.GetString();
                }

                customers.Add(customer);
            }

            customers = customers.OrderBy(c => c.Id).ToList();

            Assert.Multiple(() =>
            {
                Assert.Equal(dbCustomers.Count(), customers.Count);

                for (int i = 0; i < dbCustomers.Count(); i++) 
                {
                    Assert.Equal(dbCustomers[i].Id, customers[i].Id);
                    Assert.Equal(dbCustomers[i].Name, customers[i].Name);
                    Assert.Equal(dbCustomers[i].Email, customers[i].Email);
                }
            });
        }
    }
}
