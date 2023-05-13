using Quartz;

namespace Girteka.ElectricAggregate.Job;

public class ElectricityDataDownloaderJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        await DownloadFilesAsync();
    }

    private async Task DownloadFilesAsync()
    {
        var uris = new List<Uri>
        {
            //TODO we should write this uri in config
            new Uri("https://data.gov.lt/dataset/1975/download/10766/2022-05.csv"),
            new Uri("https://data.gov.lt/dataset/1975/download/10765/2022-04.csv"),
            new Uri("https://data.gov.lt/dataset/1975/download/10764/2022-03.csv"),
            new Uri("https://data.gov.lt/dataset/1975/download/10763/2022-02.csv")
        };

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

        //TODO we should write this uri in config
        using var fileStream = new FileStream("/Users/sabakoghuashvili/Desktop/Temp/" + fileName, FileMode.Create);
        using var contentStream = await response.Content.ReadAsStreamAsync();
        await contentStream.CopyToAsync(fileStream);
    }
}