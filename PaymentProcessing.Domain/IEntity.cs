namespace PaymentProcessing.Domain
{
    public interface IEntity<TKey>
    {
        TKey Id { get; }
    }
}
