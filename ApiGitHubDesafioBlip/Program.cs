using ApiGithubDesafioBlip.Application.Interfaces;
using ApiGithubDesafioBlip.Application.Services;
using ApiGithubDesafioBlip.Domain.Models;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi(opt =>
{
    opt.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info.Version = "v1";
        document.Info.Title = "API Github Desafio Blip";
        document.Info.Description = "Conjunto de apis desenvolvidas para o desafio Blip";
        return Task.CompletedTask;
    });
});

builder.Services.AddControllers();

builder.Services.AddHttpClient();

builder.Services.AddScoped<IGitHubService, GithubService>();
builder.Services.AddScoped<IRepositoryFilterService, RepositoryFilterService>();
builder.Services.AddScoped<ILogService, LogService>();

builder.Services.Configure<GitHubSettings>(builder.Configuration.GetSection("GitHubSettings"));

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference("/");

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
