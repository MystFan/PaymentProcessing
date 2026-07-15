namespace PaymentProcessing.Application.Queries.GetCustomersQuery
{
    public record GetCustomersQueryResponse
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;
    }
}
