namespace Girteka.AggregationApp.Content.Http;

public class HttpCsvContent : IHttpCsvContent
{
    private readonly string _url;

    public HttpCsvContent(string url)
    {
        _url = url;
    }

    public async Task<Stream> GetCsvContent(string fileName)
    {
        using (var client = new HttpClient())
        {
            client.Timeout = TimeSpan.FromSeconds(300);
            var response = await client.GetAsync(_url + fileName);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStreamAsync();
            }
            else
            {
                throw new Exception("Failed to download CSV file.");
            }
        }
    }

    public async Task DownloadFilesAsync(List<Uri> uris)
    {
        using var httpClient = new HttpClient();
        httpClient.Timeout = TimeSpan.FromSeconds(500);
        var tasks = new List<Task>();

        foreach (var uri in uris)
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

        using var fileStream = new FileStream(fileName, FileMode.Create);
        using var contentStream = await response.Content.ReadAsStreamAsync();
        await contentStream.CopyToAsync(fileStream);
    }
}