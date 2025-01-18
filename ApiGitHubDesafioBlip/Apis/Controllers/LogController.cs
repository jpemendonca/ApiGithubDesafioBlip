using ApiGithubDesafioBlip.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiGithubDesafioBlip.Apis.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LogController : ControllerBase
{
    private readonly ILogService _logService;

    public LogController(ILogService logService)
    {
        _logService = logService;
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [EndpointSummary("Get the logs for the api application.")]
    [EndpointDescription(
        "All the logs are storaged in the application, on a SQLite database.")]
    [HttpGet()]
    public async Task<IActionResult> Get()
    {
        try
        {
            var logs = _logService.GetAll();
        
            return Ok(logs);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}