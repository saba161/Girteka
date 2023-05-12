using Girteka.AggregationApp.Content;
using Girteka.AggregationApp.Content.Http;
using Microsoft.AspNetCore.Mvc;

namespace Girteka.AggregationApp.Controllers;

[ApiController]
[Route("[controller]")]
public class ElectricityController : ControllerBase
{
    private readonly ILogger<ElectricityController> _logger;
    private readonly IHttpCsvContent _httpCsvContent;
    private readonly ILocalCsvContent _localCsvContent;

    public ElectricityController(ILogger<ElectricityController> logger, IHttpCsvContent httpCsvContent,
        ILocalCsvContent localCsvContent)
    {
        _logger = logger;
        _localCsvContent = localCsvContent;
        _httpCsvContent = httpCsvContent;
    }

    [HttpGet]
    [Route("api/download")]
    public async Task<IActionResult> DownloadFile(string fileName)
    {
        try
        {
            var content = await _httpCsvContent.GetCsvContent(fileName);
            return File(content, "text/csv", fileName);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [Route("api/read")]
    public async Task<IActionResult> ReadLocalFile(string fileName)
    {
        try
        {
            var result = await _localCsvContent.GetLocalCsvContent(fileName);
            return Ok(result.FirstOrDefault());
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}