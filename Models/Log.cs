using ApiGithubDesafioBlip.Enums;

namespace ApiGithubDesafioBlip.Models;

public class Log
{
    public int Id { get; private set; }
    public DateTime Date { get; private set; } = DateTime.Now;
    public string Message { get; set; }
    public EnumLogLevel Level { get; set; }
    public string Source { get; set; }
}