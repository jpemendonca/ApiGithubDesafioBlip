using ApiGithubDesafioBlip.Application.Interfaces;
using ApiGithubDesafioBlip.Domain.Enums;
using ApiGithubDesafioBlip.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ApiGithubDesafioBlip.Apis.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GitHubController : ControllerBase
{
    private readonly IGitHubService _githubService;
    private readonly ILogService _logService;

    public GitHubController(IGitHubService githubService, IRepositoryFilterService repositoryFilterService,
        ILogService logService)
    {
        _githubService = githubService;
        _logService = logService;
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(422)]
    [ProducesResponseType(404)]
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
            var repositories = await _githubService.GetRepositoryDetails(username, language, minCount);

            return Ok(repositories);
        }
        catch (UserNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (NoRepositoryFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (RepositoryLanguageException ex)
        {
            return UnprocessableEntity(ex.Message);
        }
        catch (Exception ex)
        {
            await _logService.LogError(ex.Message);
            return StatusCode(500, "Server Internal Error");
        }
    }
}