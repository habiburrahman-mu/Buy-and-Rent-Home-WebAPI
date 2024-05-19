namespace Domain.Exceptions;

public class InvalidDomainRequestException : Exception
{
    public InvalidDomainRequestException(string message) : base(message)
    {
        
    }
}
