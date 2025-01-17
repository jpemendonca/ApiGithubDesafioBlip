using ApiGithubDesafioBlip.Interfaces;
using ApiGithubDesafioBlip.Models;
using ApiGithubDesafioBlip.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddHttpClient();

builder.Services.AddScoped<IGitHubService, GithubService>();
builder.Services.AddScoped<IRepositoryFilterService, RepositoryFilterService>();

builder.Services.Configure<GitHubSettings>(builder.Configuration.GetSection("GitHubSettings"));

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference("/");

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
