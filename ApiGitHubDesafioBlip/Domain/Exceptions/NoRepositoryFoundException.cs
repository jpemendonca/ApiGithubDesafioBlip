namespace ApiGithubDesafioBlip.Domain.Exceptions;

public class NoRepositoryFoundException : Exception
{
    public NoRepositoryFoundException(string message) : base(message)
    {
    }
}