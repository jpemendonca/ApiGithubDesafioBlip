using ApiGithubDesafioBlip.Enums;

namespace ApiGithubDesafioBlip.Models;

public class Log
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public DateTime Date { get; private set; } = DateTime.Now;
    public string Message { get; set; }
    public EnumLogLevel Level { get; set; }
    public string Source { get; set; }
}