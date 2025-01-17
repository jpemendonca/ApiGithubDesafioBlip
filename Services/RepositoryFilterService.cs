using ApiGithubDesafioBlip.Interfaces;
using ApiGithubDesafioBlip.Models;

namespace ApiGithubDesafioBlip.Services;

public class RepositoryFilterService : IRepositoryFilterService
{
    public List<GitHubRepositoryResponse> FilterByLanguage(List<GitHubRepositoryResponse> repositories, string language)
    {
        return repositories.Where(x => x.language == language).Take(5).ToList();
    }

    public List<RepositoryDetails> ExtractDetails(List<GitHubRepositoryResponse> repositories)
    {
        var repositoryDetails = new List<RepositoryDetails>();

        foreach (var repository in repositories)
        {
            var repositoryDetail = new RepositoryDetails()
            {
                Subtitle = repository.description,
                Title = repository.name,
                AvatarUrl = repository.owner.avatar_url,
            };
            
            repositoryDetails.Add(repositoryDetail);
        }
        
        return repositoryDetails;
    }
}