using Girteka.AggregationApp.Content;
using Girteka.AggregationApp.Content.Http;
using Girteka.AggregationApp.Models.Entity;
using Girteka.AggregationApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace Girteka.AggregationApp.Controllers;

[ApiController]
[Route("[controller]")]
public class ElectricityController : ControllerBase
{
    private readonly ILogger<ElectricityController> _logger;
    private readonly IHttpCsvContent _httpCsvContent;
    private readonly ILocalCsvContent _localCsvContent;
    private readonly IElectricityCrud _electricityCrud;

    public ElectricityController(ILogger<ElectricityController> logger, IHttpCsvContent httpCsvContent,
        ILocalCsvContent localCsvContent, IElectricityCrud electricityCrud)
    {
        _logger = logger;
        _localCsvContent = localCsvContent;
        _electricityCrud = electricityCrud;
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

            var s = result.Select(x =>
                new ElectricityEntity
                {
                    Tinklas = x.Tinklas,
                    Pavadinimas = x.Pavadinimas,
                    Tipas = x.Tipas,
                    Numeris = x.Numeris,
                    PPlus = x.PPlus,
                    PlT = x.PlT,
                    PMinus = x.PMinus
                }
            ).ToList();

            _electricityCrud.Create(s);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("download")]
    public async Task<IActionResult> DownloadElectricityFiles()
    {
        var uris = new List<Uri>
        {
            new Uri("https://data.gov.lt/dataset/1975/download/10766/2022-05.csv"),
            new Uri("https://data.gov.lt/dataset/1975/download/10765/2022-04.csv"),
            new Uri("https://data.gov.lt/dataset/1975/download/10764/2022-03.csv"),
            new Uri("https://data.gov.lt/dataset/1975/download/10763/2022-02.csv")
        };

        try
        {
            await _httpCsvContent.DownloadFilesAsync(uris);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest($"An error occurred while downloading files: {ex.Message}");
        }
    }
}