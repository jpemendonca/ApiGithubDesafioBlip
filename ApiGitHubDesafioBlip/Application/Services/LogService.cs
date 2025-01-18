using ApiGithubDesafioBlip.Application.Interfaces;
using ApiGithubDesafioBlip.Domain.Enums;
using ApiGithubDesafioBlip.Domain.Models;
using Dapper;
using Microsoft.Data.Sqlite;

namespace ApiGithubDesafioBlip.Application.Services;

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
            Message = message
        };
        
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();

            var query = "INSERT INTO Logs (Date, Message, Level, Source) VALUES (@Date, @Message, @Level)";
            await connection.ExecuteAsync(query, log);
        }
    }

    public async Task LogInfo(string message)
    {
        var log = new Log()
        {
            Level = EnumLogLevel.Info,
            Message = message,
        };
        
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();

            var query = "INSERT INTO Logs (Date, Message, Level) VALUES (@Date, @Message, @Level)";
            await connection.ExecuteAsync(query, log);
        }
    }

    public async Task LogWarning(string message)
    {
        var log = new Log()
        {
            Level = EnumLogLevel.Warning,
            Message = message,
        };
        
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();

            var query = "INSERT INTO Logs (Date, Message, Level) VALUES (@Date, @Message, @Level)";
            await connection.ExecuteAsync(query, log);
        }
    }

    public async Task LogError(string message)
    {
        var log = new Log()
        {
            Level = EnumLogLevel.Error,
            Message = message,
        };
        
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();

            var query = "INSERT INTO Logs (Date, Message, Level) VALUES (@Date, @Message, @Level)";
            await connection.ExecuteAsync(query, log);
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