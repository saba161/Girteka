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
}