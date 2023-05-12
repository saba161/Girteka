using Girteka.AggregationApp.Content;
using Microsoft.AspNetCore.Mvc;

namespace Girteka.AggregationApp.Controllers;

[ApiController]
[Route("[controller]")]
public class ElectricityController : ControllerBase
{
    private readonly ILogger<ElectricityController> _logger;
    private readonly IContent _content;

    public ElectricityController(ILogger<ElectricityController> logger, IContent content)
    {
        _logger = logger;
        _content = content;
    }

    [HttpGet]
    [Route("api/download")]
    public async Task<IActionResult> DownloadFileAsync(string fileName)
    {
        try
        {
            var content = await _content.GetCsvContent(fileName);
            return File(content, "text/csv", fileName);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}