using ApiGithubDesafioBlip.Domain.Models;

namespace ApiGithubDesafioBlip.Application.Interfaces;

public interface IGitHubService
{
    /// <summary>
    /// Get a user's Github repositories specified by language
    /// </summary>
    /// <param name="owner"></param>
    /// <returns></returns>
    Task<List<RepositoryDetails>> GetRepositoryDetails(string username, string language, int minCount);
}