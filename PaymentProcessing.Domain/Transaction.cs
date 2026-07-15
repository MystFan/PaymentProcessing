namespace PaymentProcessing.Domain
{
    public class Transaction : EntityBase
    {
        public Account FromAccount { get; set; } = null!;

        public long FromAccountId { get; set; }

        public Account ToAccount { get; set; } = null!;

        public long ToAccountId { get; set; }

        public decimal Amount { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
