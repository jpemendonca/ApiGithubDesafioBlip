using System.Text.Json;
using ApiGithubDesafioBlip.Interfaces;
using ApiGithubDesafioBlip.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiGithubDesafioBlip.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GitHubController : ControllerBase
{
    private readonly IGitHubService _githubService;
    private readonly IRepositoryFilterService _repositoryFilterService;

    public GitHubController(IGitHubService githubService, IRepositoryFilterService repositoryFilterService)
    {
        _githubService = githubService;
        _repositoryFilterService = repositoryFilterService;
    }
    
    [HttpGet("{username}/repositories")]
    public async Task<IActionResult> GetRepositories(string username, [FromQuery] string language = "C#", [FromQuery] int minCount = 5)
    {
        try
        {
            var repositories = await _githubService.GetRepositories(username, 30);

            if (!repositories.Any())
            {
                return NotFound("No repositories found.");
            }

            var filteredRepositories = _repositoryFilterService.FilterByLanguage(repositories, language);

            if (!filteredRepositories.Any())
            {
                return NotFound($"No repositories found in {language} for user {username}.");
            }

            if (filteredRepositories.Count < minCount)
            {
                return UnprocessableEntity($"Only {filteredRepositories.Count} repositories found in {language} for user {username}.");
            }

            var repositoryDetails = _repositoryFilterService.ExtractDetails(filteredRepositories);

            return Ok(repositoryDetails);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao buscar projetos do GitHub: {ex.Message}");
        }
    }

}