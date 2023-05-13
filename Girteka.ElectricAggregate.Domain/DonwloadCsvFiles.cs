namespace Girteka.ElectricAggregate.Domain;

public class DonwloadCsvFiles
{
    private readonly List<Uri> _uris;

    public DonwloadCsvFiles(List<Uri> uris)
    {
        _uris = uris;
    }

    public async Task Do()
    {
        using var httpClient = new HttpClient();
        httpClient.Timeout = TimeSpan.FromSeconds(500);
        var tasks = new List<Task>();

        foreach (var uri in _uris)
        {
            tasks.Add(DownloadFile(httpClient, uri));
        }

        await Task.WhenAll(tasks);
    }

    private async Task DownloadFile(HttpClient client, Uri uri)
    {
        var response = await client.GetAsync(uri);

        response.EnsureSuccessStatusCode();

        var fileName = uri.Segments.Last();

        //TODO we should write this uri in config
        using var fileStream = new FileStream("/Users/sabakoghuashvili/Desktop/Temp/" + fileName, FileMode.Create);
        using var contentStream = await response.Content.ReadAsStreamAsync();
        await contentStream.CopyToAsync(fileStream);
    }
}