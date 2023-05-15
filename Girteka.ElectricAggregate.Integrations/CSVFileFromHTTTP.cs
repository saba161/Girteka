using Microsoft.Extensions.Logging;

namespace Girteka.ElectricAggregate.Integrations;

public class CSVFileFromHTTTP
{
    private readonly string _csvLocalpPath = "/Users/sabakoghuashvili/Desktop/Temp/";
    private readonly ILogger<CSVFileFromHTTTP> _logger;
    private readonly HttpClient _client;

    public CSVFileFromHTTTP(ILogger<CSVFileFromHTTTP> logger, HttpClient client)
    {
        _logger = logger;
        _client = client;
    }

    public Stream Do(string fileName)
    {
        Uri uri = new Uri(_csvLocalpPath + "2022-05.csv");
        var response = _client.GetAsync(uri).Result;

        response.EnsureSuccessStatusCode();

        using var fileStream = new FileStream(_csvLocalpPath + fileName, FileMode.Create);
        var contentStream = response.Content.ReadAsStreamAsync().Result;
        return contentStream;
    }
}