using ApiGithubDesafioBlip.Domain.Models;

namespace ApiGithubDesafioBlip.Application.Interfaces;

public interface IGitHubApiService
{
    Task<bool> VerifyIfUserExists(string username);
    Task<List<GitHubRepositoryResponse>> GetRepositories(string username, int amount);
}