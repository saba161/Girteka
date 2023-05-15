using Microsoft.Extensions.Logging;

namespace Girteka.ElectricAggregate.Domain.DownloadCsvFiles;

public class DownloadCsvFiles : IDownloadCsvFiles
{
    private readonly ILogger<DownloadCsvFiles> _logger;

    public DownloadCsvFiles(ILogger<DownloadCsvFiles> logger)
    {
        _logger = logger;
    }

    public async Task Do(string path, List<Uri> uris)
    {
        _logger.LogInformation("Files download started");

        using var httpClient = new HttpClient();
        httpClient.Timeout = TimeSpan.FromSeconds(500);
        var tasks = new List<Task>();

        foreach (var uri in uris)
        {
            tasks.Add(DownloadFile(httpClient, uri, path));
        }

        await Task.WhenAll(tasks);

        _logger.LogInformation("Files downloaded");
    }

    private async Task DownloadFile(HttpClient client, Uri uri, string path)
    {
        var response = await client.GetAsync(uri);

        response.EnsureSuccessStatusCode();

        var fileName = uri.Segments.Last();

        using var fileStream = new FileStream(path + fileName, FileMode.Create);
        using var contentStream = await response.Content.ReadAsStreamAsync();
        await contentStream.CopyToAsync(fileStream);
    }
}