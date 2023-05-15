using Girteka.ElectricAggregate.Domain;
using Microsoft.Extensions.Logging;

namespace Girteka.ElectricAggregate.Integrations;

public class CSVFileFromHTTTP : IContext<string, Stream>
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
        Uri uri = new Uri("https://data.gov.lt/dataset/1975/download/10766/2022-05.csv");
        var response = _client.GetAsync(uri).Result;

        response.EnsureSuccessStatusCode();

        using var fileStream = new FileStream(_csvLocalpPath + fileName, FileMode.Create);
        var contentStream = response.Content.ReadAsStreamAsync().Result;
        contentStream.CopyToAsync(fileStream);
        return contentStream;
    }
}