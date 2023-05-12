using Girteka.AggregationApp.Content;
using Microsoft.AspNetCore.Mvc;

namespace Girteka.AggregationApp.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetElectricityData")]
    public List<ElectricityData> Get()
    {
        try
        {
            // var localCsvContent = new LocalCsvContent("2022-02.csv");
            // var csvDataFromLocalFile = localCsvContent.GetCsvContent();

            var httpCvsContent = new HttpCsvContent("https://data.gov.lt/dataset/1975/download/10766/2022-05.csv");
            var csvDataFromHttpClient = httpCvsContent.GetCsvContent();
            return new List<ElectricityData>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}

// 1) Download CVS files
// Write class, which download one file, failis saxelis mixedvit
// write another class which download all files
// 2) files aggregations 
// 3) save in Data base
// 4) expose GET API from Data base