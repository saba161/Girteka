using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Girteka.ElectricAggregate.Domain.Mappers;
using Girteka.ElectricAggregate.Domain.Models;

namespace Girteka.ElectricAggregate.Domain.Services;

public class FilesService : IFilesService
{
    private readonly IContext<string, Stream> _httpContext;
    private readonly IContext<string, Stream> _localContext;
    private readonly IDbContext _dbContext;

    public FilesService(IEnumerable<IContext<string, Stream>> contexts, IDbContext dbContext)
    {
        _dbContext = dbContext;
        _httpContext = contexts.First();
        _localContext = contexts.Skip(1).First();
    }

    public void Execute(List<string> fileNames)
    {
        var streams = new List<Stream>();
        foreach (var name in fileNames)
        {
            _httpContext.Do(name);
        }

        foreach (var names in fileNames)
        {
            streams.Add(_localContext.Do(names));
        }

        var electricities = ConvertStreamToModel(streams);

        var filteredData = electricities
            .Where(x => x.Pavadinimas == "Butas")
            .GroupBy(x => x.Tinklas)
            .Select(s => new Electricity()
            {
                Tinklas = s.Key,
                Pavadinimas = "Butas",
                PPlus = s.Sum(x => x.PPlus),
                PMinus = s.Sum(x => x.PMinus),
            });

        _dbContext.Electricities.AddRangeAsync(filteredData);

        _dbContext.SaveChangesAsync();
    }

    private List<Electricity> ConvertStreamToModel(List<Stream> streams)
    {
        var response = new List<Electricity>();
        foreach (var stream in streams)
        {
            using (StreamReader sr = new StreamReader(stream))
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
                    response.AddRange(fileRecords);
                }
            }
        }

        return response;
    }
}