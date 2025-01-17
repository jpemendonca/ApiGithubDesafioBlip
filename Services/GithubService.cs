﻿using System.Text.Json;
using ApiGithubDesafioBlip.Interfaces;
using ApiGithubDesafioBlip.Models;
using Microsoft.Extensions.Options;

namespace ApiGithubDesafioBlip.Services;

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
}