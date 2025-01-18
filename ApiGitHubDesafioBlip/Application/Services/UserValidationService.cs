using ApiGithubDesafioBlip.Application.Interfaces;

namespace ApiGithubDesafioBlip.Application.Services;

public class UserValidationService : IUserValidationService
{
    private readonly IGitHubService _githubService;

    public UserValidationService(IGitHubService githubService)
    {
        _githubService = githubService;
    }

    public async Task<bool> UserExists(string username)
    {
        return await _githubService.UserExists(username);
    }
}