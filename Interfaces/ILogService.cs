using ApiGithubDesafioBlip.Enums;
using ApiGithubDesafioBlip.Models;

namespace ApiGithubDesafioBlip.Interfaces;

public interface ILogService
{
    Task Log(string message, EnumLogLevel level, string source);
    List<Log> GetAll();
}