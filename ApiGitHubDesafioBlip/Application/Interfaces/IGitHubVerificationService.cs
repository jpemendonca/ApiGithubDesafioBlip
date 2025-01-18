using ApiGithubDesafioBlip.Domain.Models;

namespace ApiGithubDesafioBlip.Application.Interfaces;

public interface IGitHubVerificationService
{
    Task EnsureUserExists(string username);
    Task VerifyIfUserHasRepositories(List<GitHubRepositoryResponse> repositories, string username);

    Task VerifyIfUserHasMinimumCountOfRepositoriesByLanguage(List<GitHubRepositoryResponse> repositories,
        string language, string username, int minCount);
}