using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Girteka.ElectricAggregate.Domain;
using Girteka.ElectricAggregate.Domain.Mappers;
using Girteka.ElectricAggregate.Domain.Models;

namespace Girteka.ElectricAggregate.Integrations;

public class CSVFileFromLocalDisk : IContext<string, Stream>
{
    public Stream Do(string input)
    {
        string filePath = Path.Combine("/Users/sabakoghuashvili/Desktop/Temp/", input);
        var records = new List<Electricity>();

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
                    csv.Context.RegisterClassMap<ElectricityMapper>();
                    var fileRecords = csv.GetRecords<Electricity>().ToList();
                    records.AddRange(fileRecords);
                }
            }

            return File.Open(filePath, FileMode.Open);
        }
        catch (Exception e)
        {
            //_logger.LogError(e.Message);
            throw;
        }
    }
}