using ApiGithubDesafioBlip.Application.Interfaces;
using ApiGithubDesafioBlip.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace ApiGithubDesafioBlip.Apis.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GitHubController : ControllerBase
{
    private readonly IGitHubService _githubService;
    private readonly IRepositoryFilterService _repositoryFilterService;
    private readonly ILogService _logService;

    public GitHubController(IGitHubService githubService, IRepositoryFilterService repositoryFilterService,
        ILogService logService)
    {
        _githubService = githubService;
        _repositoryFilterService = repositoryFilterService;
        _logService = logService;
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(422)]
    [ProducesResponseType(500)]
    [EndpointSummary("Get repositories for a specific GitHub user, filtered by language.")]
    [EndpointDescription(
        "If you don't inform the language and the minimum of repositories, you will get C# and 5 repositories as default")]
    [HttpGet("{username}/repositories")]
    public async Task<IActionResult> GetRepositories(string username, [FromQuery] string language = "C#",
        [FromQuery] int minCount = 5)
    {
        try
        {
            bool userExists = await _githubService.UserExists(username);

            if (!userExists)
            {
                await _logService.Log(
                    $"Failed to fetch {minCount} {language} repositories for non-existent GitHub user '{username}'.",
                    EnumLogLevel.Warning,
                    "GitHubController.GetRepositories"
                );

                return NotFound($"The GitHub user '{username}' does not exist.");
            }

            await _logService.Log($"Fetching at least {minCount} {language} repositories for github user {username}",
                EnumLogLevel.Info,
                "GitHubController.GetRepositories");

            var repositories = await _githubService.GetRepositories(username, 30);

            if (!repositories.Any())
            {
                await _logService.Log($"No repositories found for user {username}.", EnumLogLevel.Warning,
                    "GitHubController.GetRepositories");

                return NotFound("No repositories found.");
            }

            var filteredRepositories = _repositoryFilterService.FilterByLanguage(repositories, language);

            if (!filteredRepositories.Any())
            {
                await _logService.Log($"No repositories found in {language} for user {username}.", EnumLogLevel.Warning,
                    "GitHubController.GetRepositories");

                return NotFound($"No repositories found in {language} for user {username}.");
            }

            if (filteredRepositories.Count < minCount)
            {
                await _logService.Log(
                    $"Only {filteredRepositories.Count} repositories found in {language} for user {username}, which is less than the minimum required ({minCount}).",
                    EnumLogLevel.Warning, "GitHubController.GetRepositories");

                return UnprocessableEntity(
                    $"Only {filteredRepositories.Count} repositories found in {language} for user {username}.");
            }

            var repositoryDetails = _repositoryFilterService.ExtractDetails(filteredRepositories);

            await _logService.Log(
                $"Returning {repositoryDetails.Count} repositories for user {username} filtered by language {language}.",
                EnumLogLevel.Info, "GitHubController.GetRepositories");

            return Ok(repositoryDetails);
        }
        catch (Exception ex)
        {
            await _logService.Log($"Error while fetching repositories for user {username}: {ex.Message}",
                EnumLogLevel.Error, "GitHubController.GetRepositories");

            return StatusCode(500, $"Error while fetching repositories for user {username}: {ex.Message}");
        }
    }
}