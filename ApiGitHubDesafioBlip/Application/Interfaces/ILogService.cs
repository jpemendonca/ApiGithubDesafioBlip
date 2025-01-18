using ApiGithubDesafioBlip.Domain.Enums;
using ApiGithubDesafioBlip.Domain.Models;

namespace ApiGithubDesafioBlip.Application.Interfaces;

public interface ILogService
{
    Task Log(string message, EnumLogLevel level, string source);
    List<Log> GetAll();
}