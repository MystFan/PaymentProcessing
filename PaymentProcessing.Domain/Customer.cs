namespace PaymentProcessing.Domain
{
    public class Customer : EntityBase
    {
        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public ICollection<Account> Accounts { get; set; } = [];
    }
}
