using ApiGithubDesafioBlip.Models;

namespace ApiGithubDesafioBlip.Interfaces;

public interface IRepositoryFilterService
{
    /// <summary>
    /// Filter github repositories to get only C# repositories
    /// </summary>
    /// <param name="repositories"></param>
    /// <returns></returns>
    List<GitHubRepositoryResponse> FilterByLanguage(List<GitHubRepositoryResponse> repositories, string language);
    /// <summary>
    /// Extract the title, subtitle and avatar url from the list of repositories
    /// </summary>
    /// <param name="repositories"></param>
    /// <returns></returns>
    List<RepositoryDetails> ExtractDetails(List<GitHubRepositoryResponse> repositories);
}