namespace ApiGithubDesafioBlip.Domain.Exceptions;

public class RepositoryLanguageException : Exception
{
    public RepositoryLanguageException(string message) : base(message)
    {
    }
}