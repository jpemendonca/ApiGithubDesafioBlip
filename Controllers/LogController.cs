using ApiGithubDesafioBlip.Enums;
using ApiGithubDesafioBlip.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiGithubDesafioBlip.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LogController : ControllerBase
{
    private readonly ILogService _logService;

    public LogController(ILogService logService)
    {
        _logService = logService;
    }

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