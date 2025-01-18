using ApiGithubDesafioBlip.Application.Interfaces;

namespace ApiGithubDesafioBlip.Application.Services;

public class UserValidationService : IUserValidationService
{
    private readonly IGitHubApiService _gitHubApiService;

    public UserValidationService(IGitHubApiService gitHubApiService)
    {
        _gitHubApiService = gitHubApiService;
    }

    public async Task<bool> UserExists(string username)
    {
        return await _gitHubApiService.VerifyIfUserExists(username);
    }
}