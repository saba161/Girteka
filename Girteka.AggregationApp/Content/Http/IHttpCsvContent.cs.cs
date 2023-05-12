namespace Girteka.AggregationApp.Content.Http;

public interface IHttpCsvContent
{
    Task<Stream> GetCsvContent(string fileName);
    Task DownloadFilesAsync(List<Uri> uris);
}