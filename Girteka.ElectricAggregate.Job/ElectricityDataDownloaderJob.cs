using Girteka.ElectricAggregate.Domain;
using Girteka.ElectricAggregate.Persistence;
using Quartz;

namespace Girteka.ElectricAggregate.Job;

public class ElectricityDataDownloaderJob : IJob
{
    private readonly string _csvHttpUrl;
    private readonly string _csvLocalpPath;

    public ElectricityDataDownloaderJob(IConfiguration configuration)
    {
        _csvHttpUrl = configuration.GetValue<string>("CsvHttpUrl");
        _csvLocalpPath = configuration.GetValue<string>("CsvLocalpPath");
    }

    public async Task Execute(IJobExecutionContext context)
    {
        List<Uri> uris = new List<Uri>();

        var fileNames = new List<string>
        {
            "2022-05.csv",
            "2022-04.csv",
            "2022-03.csv",
            "2022-02.csv"
        };

        uris = fileNames
            .Select(x => new Uri(_csvHttpUrl + x))
            .ToList();

        await new DonwloadCsvFiles(_csvLocalpPath, uris).Do();

        await new TransformCsvFiles(_csvLocalpPath, fileNames,
            new ApplicationDbContext()).Do();
    }
}