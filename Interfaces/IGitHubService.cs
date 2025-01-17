using ApiGithubDesafioBlip.Models;

namespace ApiGithubDesafioBlip.Interfaces;

public interface IGitHubService
{
    /// <summary>
    /// Get a user's Github repositories, only the ones he owns
    /// </summary>
    /// <param name="owner"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    Task<List<GitHubRepositoryResponse>> GetRepositories(string owner, int amount);
}