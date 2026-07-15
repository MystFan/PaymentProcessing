namespace PaymentProcessing.IntegrationTests.ResponseModels
{
    internal class CustomerResponse
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;
    }
}
