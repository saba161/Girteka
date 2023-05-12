using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Girteka.AggregationApp.Content;
using Girteka.AggregationApp.Maper;
using Girteka.AggregationApp.Models;

public class LocalCsvContent : ILocalCsvContent
{
    private readonly string _path;

    public LocalCsvContent(string path)
    {
        _path = path;
    }

    public async Task<List<Electricity>> GetLocalCsvContent(string fileName)
    {
        string filePath = Path.Combine(_path, fileName);

        try
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ",",
                    HasHeaderRecord = true,
                    HeaderValidated = null,
                    MissingFieldFound = null
                };
                using (var csv = new CsvReader(sr, csvConfig))
                {
                    csv.Context.RegisterClassMap<ElectricityMap>();
                    var records = csv.GetRecords<Electricity>().ToList();
                    return records;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
            throw;
        }
    }
}