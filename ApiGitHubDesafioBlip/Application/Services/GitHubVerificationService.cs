using ApiGithubDesafioBlip.Application.Interfaces;
using ApiGithubDesafioBlip.Domain.Exceptions;
using ApiGithubDesafioBlip.Domain.Models;

namespace ApiGithubDesafioBlip.Application.Services;

public class GitHubVerificationService : IGitHubVerificationService
{
    private readonly IUserValidationService _userValidationService;
    private readonly ILogService _logService;

    public GitHubVerificationService(IUserValidationService userValidationService, ILogService logService)
    {
        _userValidationService = userValidationService;
        _logService = logService;
    }

    public async Task EnsureUserExists(string username)
    {
        bool userExists = await _userValidationService.UserExists(username);

        if (!userExists)
        {
            await _logService.LogError($"GitHub user '{username}' does not exist.");
            throw new UserNotFoundException($"GitHub user '{username}' does not exist.");
        }
    }

    public async Task VerifyIfUserHasRepositories(List<GitHubRepositoryResponse> repositories, string username)
    {
        if (!repositories.Any())
        {
            await _logService.LogError($"No repositories found for user {username}");
            throw new NoRepositoryFoundException($"No repositories found for user {username}");
        }
    }

    public async Task VerifyIfUserHasMinimumCountOfRepositoriesByLanguage(List<GitHubRepositoryResponse> repositories,
        string language, string username, int minCount)
    {

        if (repositories.Count < minCount)
        {
            await _logService.LogWarning(
                $"Only {repositories.Count} repositories found in {language} for user {username}");
            throw new RepositoryLanguageException(
                $"{repositories.Count} repositories found in {language} for user {username}, which " +
                $"is less than the minimum required ({minCount}).");
        }

    }
}