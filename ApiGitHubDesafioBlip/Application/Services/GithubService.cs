using System.Text.Json;
using ApiGithubDesafioBlip.Application.Interfaces;
using ApiGithubDesafioBlip.Domain.Exceptions;
using ApiGithubDesafioBlip.Domain.Models;
using Microsoft.Extensions.Options;

namespace ApiGithubDesafioBlip.Application.Services;

public class GithubService : IGitHubService
{
    private readonly IGitHubVerificationService _verificationService;
    private readonly IGitHubApiService _gitHubApiService;
    private readonly IRepositoryFilterService _repositoryFilterService;
    private readonly ILogService _logService;

    public GithubService(ILogService logService, IRepositoryFilterService repositoryFilterService,
        IGitHubApiService gitHubApiService,
        IGitHubVerificationService verificationService)
    {
        _logService = logService;
        _repositoryFilterService = repositoryFilterService;
        _gitHubApiService = gitHubApiService;
        _verificationService = verificationService;
    }

    public async Task<List<RepositoryDetails>> GetRepositoryDetails(string username, string language, int minCount)
    {
        // Verifica se o usuario existe, se não, lança um erro e gera um log
        await _verificationService.EnsureUserExists(username);

        await _logService.LogInfo($"Fetching at least {minCount} {language} repositories for github user {username}");

        // Obtem os 30 primeiros repositorios, do mais antigo ao mais novo, como vêm da api do github
        var repositories = await _gitHubApiService.GetRepositories(username, 30);

        // Verifica se o usuario tem pelo menos um repositorio, se não lança um erro
        await _verificationService.VerifyIfUserHasRepositories(repositories, username);
        
        // Filtra os repositorios por linguagem
        var repositoryFilteredByLanguage = _repositoryFilterService.FilterByLanguage(repositories, language);
        
        // Se o usuario tiver menos repositorios da linguagem do que o minimo solicitado, lança um erro e gera o log
        await _verificationService.VerifyIfUserHasMinimumCountOfRepositoriesByLanguage(repositoryFilteredByLanguage, language, username, minCount);

        // Formata os repositorios para extrair apenas avatarurl, titulo e descricao
        var repositoryDetails = _repositoryFilterService.ExtractDetails(repositoryFilteredByLanguage);

        await _logService.LogInfo(
            $"Returning {repositoryDetails.Count} repositories for user {username} filtered by language {language}.");

        return repositoryDetails;
    }
}