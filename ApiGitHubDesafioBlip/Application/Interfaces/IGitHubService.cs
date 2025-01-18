using ApiGithubDesafioBlip.Domain.Models;

namespace ApiGithubDesafioBlip.Application.Interfaces;

public interface IGitHubService
{
    /// <summary>
    /// Get a user's Github repositories, only the ones he owns
    /// </summary>
    /// <param name="owner"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    Task<List<GitHubRepositoryResponse>> GetRepositories(string owner, int amount);
    /// <summary>
    /// Validate if the user exists on github. If the status code from the api is not 200, it does not exists and returns false
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    Task<bool> UserExists(string username);
}