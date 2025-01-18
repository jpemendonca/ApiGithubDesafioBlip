namespace ApiGithubDesafioBlip.Application.Interfaces;

public interface IUserValidationService
{
    Task<bool> UserExists(string username);
}