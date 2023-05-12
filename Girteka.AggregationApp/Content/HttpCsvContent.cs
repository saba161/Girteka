using System.Net;

namespace Girteka.AggregationApp.Content;

public class HttpCsvContent : IContent
{
    private readonly string _url;

    public HttpCsvContent(string url)
    {
        _url = url;
    }

    public async Task<string> GetCsvContent()
    {
        using (var client = new HttpClient())
        {
            using (var result = await client.GetAsync(_url))
            {
                if (result.IsSuccessStatusCode)
                {
                    var s = result.Content.ReadAsByteArrayAsync();
                }
            }
        }

        return null;
    }
}