using Girteka.AggregationApp.Models;

namespace Girteka.AggregationApp.Content;

public interface ILocalCsvContent
{
    Task<List<Electricity>> GetLocalCsvContent(string fileName);
}