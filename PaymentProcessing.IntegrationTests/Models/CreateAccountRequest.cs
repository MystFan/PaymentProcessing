namespace PaymentProcessing.IntegrationTests.Models
{
    public record CreateAccountRequest
    {
        public long CustomerId { get; set; }

        public decimal Balance { get; set; }

        public string Currency { get; set; } = null!;
    }
}
