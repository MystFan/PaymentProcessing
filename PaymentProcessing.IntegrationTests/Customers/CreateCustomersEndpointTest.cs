using Microsoft.Playwright;
using PaymentProcessing.Domain;
using PaymentProcessing.Domain.Exceptions;
using PaymentProcessing.IntegrationTests.ResponseModels;

namespace PaymentProcessing.IntegrationTests.Customers
{
    public class CreateCustomersEndpointTest : EndpointTest
    {
        [Fact]
        public async Task ShouldCreateCustomerWithNameAndEmail()
        {
            var newCustomer = new CreateCustomerRequest
            {
                Name = $"Test-{Guid.NewGuid()}",
                Email = $"{Guid.NewGuid()}@test.com"
            };

            var createCustomerResponse = await Request.PostAsync("/customers", new APIRequestContextOptions { DataObject = newCustomer });

            var database = new DatabaseHelper(Context);
            var dbCustomer = await database.CustomerByEmailAsync(newCustomer.Email);

            await Expect(createCustomerResponse).ToBeOKAsync();

            Assert.Multiple(() =>
            {
                Assert.NotNull(dbCustomer);
                Assert.Equal(dbCustomer.Name, newCustomer.Name);
                Assert.Equal(dbCustomer.Email, newCustomer.Email);
            });
        }

        [Fact]
        public async Task ShouldReturnErrorWithoutName()
        {
            var newCustomer = new CreateCustomerRequest
            {
                Email = $"{Guid.NewGuid()}@test.com"
            };

            var createCustomerResponse = await Request.PostAsync("/customers", new APIRequestContextOptions { DataObject = newCustomer });

            var errorsElement = await createCustomerResponse.JsonAsync();

            string? errorMessage = await createCustomerResponse.GetValidationErrorMessageAsync("name");
            ExceptionReasonCode? errorCode = await createCustomerResponse.GetValidationErrorCodeAsync();

            var database = new DatabaseHelper(Context);
            var dbCustomer = await database.CustomerByEmailAsync(newCustomer.Email);

            Assert.Multiple(() =>
            {
                Assert.Equal(400, createCustomerResponse.Status);
                Assert.Equal("'Name' must not be empty.", errorMessage);
                Assert.Equal(ExceptionReasonCode.InvalidRequest, errorCode);
                Assert.Null(dbCustomer);
            });
        }

        [Fact]
        public async Task ShouldReturnErrorWithLongName()
        {
            var newCustomer = new CreateCustomerRequest
            {
                Name = new string('a', Constants.Customer.NameMaxLength + 1),
                Email = $"{Guid.NewGuid()}@test.com"
            };

            var createCustomerResponse = await Request.PostAsync("/customers", new APIRequestContextOptions { DataObject = newCustomer });

            var errorsElement = await createCustomerResponse.JsonAsync();

            string? errorMessage = await createCustomerResponse.GetValidationErrorMessageAsync("name");
            ExceptionReasonCode? errorCode = await createCustomerResponse.GetValidationErrorCodeAsync();

            var database = new DatabaseHelper(Context);
            var dbCustomer = await database.CustomerByEmailAsync(newCustomer.Email);

            Assert.Multiple(() =>
            {
                Assert.Equal(400, createCustomerResponse.Status);
                Assert.Equal($"The length of 'Name' must be {Constants.Customer.NameMaxLength} characters or fewer. You entered {newCustomer.Name.Length} characters.", errorMessage);
                Assert.Equal(ExceptionReasonCode.InvalidRequest, errorCode);
                Assert.Null(dbCustomer);
            });
        }

        [Fact]
        public async Task ShouldReturnErrorWithoutEmail()
        {
            var newCustomer = new CreateCustomerRequest
            {
                Name = $"{Guid.NewGuid()}"
            };

            var createCustomerResponse = await Request.PostAsync("/customers", new APIRequestContextOptions { DataObject = newCustomer });

            var errorsElement = await createCustomerResponse.JsonAsync();

            string? errorMessage = await createCustomerResponse.GetValidationErrorMessageAsync("email");
            ExceptionReasonCode? errorCode = await createCustomerResponse.GetValidationErrorCodeAsync();

            var database = new DatabaseHelper(Context);
            var dbCustomer = await database.CustomerByNameAsync(newCustomer.Name);

            Assert.Multiple(() =>
            {
                Assert.Equal(400, createCustomerResponse.Status);
                Assert.Equal("'Email' must not be empty.", errorMessage);
                Assert.Equal(ExceptionReasonCode.InvalidRequest, errorCode);
                Assert.Null(dbCustomer);
            });
        }

        [Fact]
        public async Task ShouldReturnErrorWithLongEmail()
        {
            var newCustomer = new CreateCustomerRequest
            {
                Name = Guid.NewGuid().ToString(),
                Email = $"{new string('a', Constants.Customer.EmailMaxLength)}@test.com"
            };

            var createCustomerResponse = await Request.PostAsync("/customers", new APIRequestContextOptions { DataObject = newCustomer });

            var errorsElement = await createCustomerResponse.JsonAsync();

            string? errorMessage = await createCustomerResponse.GetValidationErrorMessageAsync("email");
            ExceptionReasonCode? errorCode = await createCustomerResponse.GetValidationErrorCodeAsync();

            var database = new DatabaseHelper(Context);
            var dbCustomer = await database.CustomerByEmailAsync(newCustomer.Email);

            Assert.Multiple(() =>
            {
                Assert.Equal(400, createCustomerResponse.Status);
                Assert.Equal($"The length of 'Email' must be {Constants.Customer.EmailMaxLength} characters or fewer. You entered {newCustomer.Email.Length} characters.", errorMessage);
                Assert.Equal(ExceptionReasonCode.InvalidRequest, errorCode);
                Assert.Null(dbCustomer);
            });
        }

        [Fact]
        public async Task ShouldReturnErrorWithInvalidEmail()
        {
            var newCustomer = new CreateCustomerRequest
            {
                Name = Guid.NewGuid().ToString(),
                Email = $"{new string('o', Constants.Customer.EmailMaxLength)}"
            };

            var createCustomerResponse = await Request.PostAsync("/customers", new APIRequestContextOptions { DataObject = newCustomer });

            var errorsElement = await createCustomerResponse.JsonAsync();

            string? errorMessage = await createCustomerResponse.GetValidationErrorMessageAsync("email");
            ExceptionReasonCode? errorCode = await createCustomerResponse.GetValidationErrorCodeAsync();

            var database = new DatabaseHelper(Context);
            var dbCustomer = await database.CustomerByEmailAsync(newCustomer.Email);

            Assert.Multiple(() =>
            {
                Assert.Equal(400, createCustomerResponse.Status);
                Assert.Equal($"'Email' is not a valid email address.", errorMessage);
                Assert.Equal(ExceptionReasonCode.InvalidRequest, errorCode);
                Assert.Null(dbCustomer);
            });
        }
    }
}
