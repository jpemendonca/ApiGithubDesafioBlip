using ApiGithubDesafioBlip.Application.Services;
using ApiGithubDesafioBlip.Domain.Models;

namespace ApiGitHubDesafioBlip.Tests;

public class RepositoryFilterServiceTests
{
    private readonly RepositoryFilterService _repositoryFilterService;

    public RepositoryFilterServiceTests()
    {
        _repositoryFilterService = new RepositoryFilterService();
    }

    [Fact]
    public void FilterByLanguage_ValidLanguage_ReturnsFilteredRepositories()
    {
        // Arrange
        var repositories = new List<GitHubRepositoryResponse>
            {
                new GitHubRepositoryResponse { name = "Repo1", language = "C#", description = "Description1", owner = new Owner { avatar_url = "url1" }},
                new GitHubRepositoryResponse { name = "Repo2", language = "C#", description = "Description2", owner = new Owner { avatar_url = "url2" }},
                new GitHubRepositoryResponse { name = "Repo3", language = "Java", description = "Description3", owner = new Owner { avatar_url = "url3" }},
                new GitHubRepositoryResponse { name = "Repo4", language = "C#", description = "Description4", owner = new Owner { avatar_url = "url4" }},
                new GitHubRepositoryResponse { name = "Repo4", language = "C#", description = "Description43", owner = new Owner { avatar_url = "url9" }},
                new GitHubRepositoryResponse { name = "Repo5", language = "C#", description = "Description5", owner = new Owner { avatar_url = "url5" }},
                new GitHubRepositoryResponse { name = "Repo6", language = "Python", description = "Description6", owner = new Owner { avatar_url = "url6" }},
            };

        var language = "C#";
        // Act
        var result = _repositoryFilterService.FilterByLanguage(repositories, language);

        // Assert
        Assert.Equal(5, result.Count);
        Assert.All(result, repo => Assert.Equal(language, repo.language)); // Todos os repositórios retornados devem ser da linguagem C#
    }

    [Fact]
    public void FilterByLanguage_NoMatchingLanguage_ReturnsEmptyList()
    {
        // Arrange
        var repositories = new List<GitHubRepositoryResponse>
            {
                new GitHubRepositoryResponse { name = "Repo1", language = "Java", description = "Description1", owner = new Owner { avatar_url = "url1" }},
                new GitHubRepositoryResponse { name = "Repo2", language = "Java", description = "Description2", owner = new Owner { avatar_url = "url2" }},
                new GitHubRepositoryResponse { name = "Repo3", language = "Java", description = "Description3", owner = new Owner { avatar_url = "url3" }},
            };

        var language = "C#";

        // Act
        var result = _repositoryFilterService.FilterByLanguage(repositories, language);

        // Assert
        Assert.Empty(result);
    }
}