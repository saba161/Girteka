using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Girteka.ElectricAggregate.Domain.Models;

namespace Girteka.ElectricAggregate.Domain;

public class ReadCsvFiles
{
    private readonly List<string> _fileNames;
    private readonly string _path;

    public ReadCsvFiles(List<string> fileNames, string path)
    {
        _fileNames = fileNames;
        _path = path;
    }

    public async Task<List<Electricity>> Do()
    {
        var records = new List<Electricity>();

        foreach (var fileName in _fileNames)
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
                        csv.Context.RegisterClassMap<ElectricityMapper>();
                        var fileRecords = csv.GetRecords<Electricity>().ToList();
                        records.AddRange(fileRecords);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                throw;
            }
        }

        return records;
    }
}