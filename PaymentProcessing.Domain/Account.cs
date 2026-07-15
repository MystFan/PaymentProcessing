using PaymentProcessing.Domain.Exceptions;

namespace PaymentProcessing.Domain
{
    public class Account : EntityBase
    {
        public Customer? Customer { get; set; }

        public long CustomerId { get; set; }

        public decimal Balance { get; set; }

        public string Currency { get; set; } = null!;

        public Transaction TransferTo(Account destination, decimal amount, DateTime date)
        {
            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            if (destination.Id == Id)
            {
                throw new DomainException(ExceptionReasonCode.CannotTransferToSameAccount, "Cannot transfer to the same account.");
            }

            if (Currency != destination.Currency)
            {
                throw new DomainException(ExceptionReasonCode.AccountsMustUseTheSameCurrency, "Accounts must use the same currency.");
            }

            Withdraw(amount);
            destination.Deposit(amount);

            return new Transaction
            {
                FromAccount = this,
                FromAccountId = this.Id,
                ToAccount = destination,
                ToAccountId = destination.Id,
                Amount = amount,
                CreatedAt = date
            };
        }

        private void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount));
            }

            Balance += amount;
        }

        private void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount));
            }

            if (Balance < amount)
            {
                throw new DomainException(ExceptionReasonCode.InsufficientFunds, "Insufficient funds.");
            }

            Balance -= amount;
        }
    }
}
