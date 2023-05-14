namespace Girteka.ElectricAggregate.Domain.DownloadCsvFiles;

public class DownloadCsvFiles : IDownloadCsvFiles
{
    public async Task Do(string path, List<Uri> uris)
    {
        using var httpClient = new HttpClient();
        httpClient.Timeout = TimeSpan.FromSeconds(500);
        var tasks = new List<Task>();

        foreach (var uri in uris)
        {
            tasks.Add(DownloadFile(httpClient, uri, path));
        }

        await Task.WhenAll(tasks);
        //log
    }

    private async Task DownloadFile(HttpClient client, Uri uri, string path)
    {
        var response = await client.GetAsync(uri);

        response.EnsureSuccessStatusCode();

        var fileName = uri.Segments.Last();

        //TODO we should write this uri in config

        using var fileStream = new FileStream(path + fileName, FileMode.Create);
        using var contentStream = await response.Content.ReadAsStreamAsync();
        await contentStream.CopyToAsync(fileStream);
    }
}