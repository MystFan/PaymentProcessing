namespace PaymentProcessing.Domain.Exceptions
{
    public enum ExceptionReasonCode
    {
        InvalidRequest = 10000,
        CannotTransferToSameAccount,
        AccountsMustUseTheSameCurrency,
        InsufficientFunds,
        CustomerNotFound,
        AccountNotFound
    }
}
