using ApiGithubDesafioBlip.Enums;
using ApiGithubDesafioBlip.Interfaces;
using ApiGithubDesafioBlip.Models;
using Dapper;
using Microsoft.Data.Sqlite;

namespace ApiGithubDesafioBlip.Services;

public class LogService : ILogService
{
    private readonly string _connectionString;
    
    public LogService(IConfiguration configuration)
    {
        if (configuration is not null)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
    }
    
    public async Task Log(string message, EnumLogLevel level, string source)
    {
        var log = new Log()
        {
            Level = level,
            Message = message,
            Source = source
        };
        
        using (var connection = new SqliteConnection(_connectionString))
        {
            await connection.OpenAsync();

            var query = @"
            INSERT INTO Logs (Id, Date, Message, Level, Source)
            VALUES (@Id, @Date, @Message, @Level, @Source)";

            await connection.ExecuteAsync(query, new
            {
                log.Id,
                log.Date,
                log.Message,
                log.Level,
                log.Source
            });
        }
    }

    public  List<Log> GetAll()
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();

            var query = "SELECT * FROM Logs";
            var dados = connection.Query<Log>(query).ToList();

            return dados.OrderByDescending(x => x.Date).ToList();
        }
    }
}