using System.Text.Json;
using ApiGithubDesafioBlip.Application.Interfaces;
using ApiGithubDesafioBlip.Domain.Models;
using Microsoft.Extensions.Options;

namespace ApiGithubDesafioBlip.Application.Services;

public class GithubService : IGitHubService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;
    private readonly string _token;
    
    public GithubService(HttpClient httpClient, IOptions<GitHubSettings> options)
    {
        _httpClient = httpClient;
        _baseUrl = options.Value.BaseUrl;
        _token = options.Value.Token;
    }
    
    public async Task<List<GitHubRepositoryResponse>> GetRepositories(string userName, int amount)
    {
        var request = new HttpRequestMessage(
            HttpMethod.Get, 
            $"{_baseUrl}/users/{userName}/repos?sort=created&direction=asc&type=owner&per_page={amount}"
        );
        
        request.Headers.Add("User-Agent", "HttpClient");
        request.Headers.Add("Authorization", $"Bearer {_token}");

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var repositories = JsonSerializer.Deserialize<List<GitHubRepositoryResponse>>(content);
        
        return repositories;
    }

    public async Task<bool> UserExists(string username)
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
}