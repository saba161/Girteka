namespace Girteka.AggregationApp.Content;

public interface IContent
{
    Task<Stream> GetCsvContent(string fileName);
}