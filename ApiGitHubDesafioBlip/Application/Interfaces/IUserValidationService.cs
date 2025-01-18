namespace ApiGithubDesafioBlip.Application.Interfaces;

public interface IUserValidationService
{
    /// <summary>
    /// Validate if the user exists on github. If the status code from the api is not 200, it does not exists and returns false
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    Task<bool> UserExists(string username);
}