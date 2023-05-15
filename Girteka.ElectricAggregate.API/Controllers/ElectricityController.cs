using Girteka.ElectricAggregate.Domain;
using Girteka.ElectricAggregate.Domain.DownloadCsvFiles;
using Girteka.ElectricAggregate.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Girteka.ElectricAggregate.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ElectricityController : ControllerBase
{
    private readonly IElectricity _electricity;
    private readonly ILogger<ElectricityController> _logger;

    public ElectricityController(IElectricity electricity, ILogger<ElectricityController> logger,
        IDownloadCsvFiles csv)
    {
        _electricity = electricity;
        _logger = logger;
    }

    [HttpGet("get/electricity")]
    public async Task<IActionResult> Get()
    {
        try
        {
            return Ok(await _electricity.Get());
        }
        catch (Exception e)
        {
            _logger.LogInformation(e.Message);
            throw;
        }
    }
}