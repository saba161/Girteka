namespace Girteka.AggregationApp.Content;

public class LocalCsvContent : IContent
{
    private readonly string _filePath;

    public LocalCsvContent(string filePath)
    {
        _filePath = filePath;
    }

    public Task<string> GetCsvContent()
    {
        var csvContent = File.ReadAllText(_filePath);
        return null;
    }
}