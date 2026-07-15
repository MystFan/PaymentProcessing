
namespace PaymentProcessing.IntegrationTests.ResponseModels
{
    public record AccountResponse
    {
        public long Id { get; set; }
        
        public long CustomerId { get; set; }

        public decimal Balance { get; set; }

        public string Currency { get; set; } = null!;
    }
}
