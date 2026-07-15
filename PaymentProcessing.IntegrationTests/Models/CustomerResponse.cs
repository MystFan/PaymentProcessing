namespace PaymentProcessing.IntegrationTests.ResponseModels
{
    public record CustomerResponse
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;
    }
}
