using ApiGithubDesafioBlip.Domain.Enums;
using ApiGithubDesafioBlip.Domain.Models;

namespace ApiGithubDesafioBlip.Application.Interfaces;

public interface ILogService
{
    Task Log(string message, EnumLogLevel level, string source);
    Task LogInfo(string message);
    Task LogWarning(string message);
    Task LogError(string message);
    List<Log> GetAll();
}