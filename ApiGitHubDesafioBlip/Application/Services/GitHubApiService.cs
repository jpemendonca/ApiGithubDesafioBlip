using System.Text.Json;
using ApiGithubDesafioBlip.Application.Interfaces;
using ApiGithubDesafioBlip.Domain.Models;
using Microsoft.Extensions.Options;

namespace ApiGithubDesafioBlip.Application.Services;

public class GitHubApiService : IGitHubApiService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;
    private readonly string _token;
    
    public GitHubApiService(HttpClient httpClient, IOptions<GitHubSettings> options)
    {
        _httpClient = httpClient;
        _baseUrl = options.Value.BaseUrl;
        _token = options.Value.Token;
    }
    
    public async Task<bool> VerifyIfUserExists(string username)
    {
        var request = new HttpRequestMessage(
            HttpMethod.Get, 
            $"{_baseUrl}/users/{username}"
        );
        
        request.Headers.Add("User-Agent", "HttpClient");
        request.Headers.Add("Authorization", $"Bearer {_token}");
        
        try
        {
            var response = await _httpClient.SendAsync(request);

            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException)
        {
            return false;
        }
    }

    public async Task<List<GitHubRepositoryResponse>> GetRepositories(string username, int amount)
    {
        var request = new HttpRequestMessage(
            HttpMethod.Get, 
            $"{_baseUrl}/users/{username}/repos?sort=created&direction=asc&type=owner&per_page={amount}"
        );
        
        request.Headers.Add("User-Agent", "HttpClient");
        request.Headers.Add("Authorization", $"Bearer {_token}");

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        
        return JsonSerializer.Deserialize<List<GitHubRepositoryResponse>>(content);
    }
}