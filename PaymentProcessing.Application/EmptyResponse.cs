namespace PaymentProcessing.Application.Abstract.Implementation;

public class EmptyResponse
{
    public static EmptyResponse Instance { get; } = new();

    private EmptyResponse()
    {
        //
    }
}