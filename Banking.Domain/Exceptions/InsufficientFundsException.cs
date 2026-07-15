namespace Banking.Domain.Exceptions;

public class InsufficientFundsException : BusinessException
{
    public InsufficientFundsException(string message)
        : base(message)
    {
    }
}