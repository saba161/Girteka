using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Girteka.ElectricAggregate.Domain;
using Girteka.ElectricAggregate.Domain.Models;

namespace Girteka.ElectricAggregate.Job;

public class TransformCsvFiles
{
    private readonly List<string> _fileNames;
    private readonly string _path;
    private readonly IDbContext _dbContext;

    public TransformCsvFiles(string path,
        List<string> fileNames, IDbContext dbContext)
    {
        _path = path;
        _fileNames = fileNames;
        _dbContext = dbContext;
    }

    public async Task Do()
    {
        try
        {
            var records = await ReadCsvFilesAsync();

            var filteredData = records
                .Where(x => x.Pavadinimas == "Butas")
                .GroupBy(x => x.Tinklas)
                .Select(s => new Electricity()
                {
                    Tinklas = s.Key,
                    Pavadinimas = "Butas",
                    PPlus = s.Sum(x => x.PPlus),
                    PMinus = s.Sum(x => x.PMinus),
                    //Date
                });

            await _dbContext.Electricities.AddRangeAsync(filteredData);

            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            //log
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task<List<Electricity>> ReadCsvFilesAsync()
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
                //log
            }
            catch (Exception e)
            {
                //log
                Console.WriteLine("Error: " + e.Message);
                throw;
            }
        }

        return await Task.FromResult(records);
    }
}