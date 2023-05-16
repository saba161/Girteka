using Girteka.ElectricAggregate.Domain.Services;
using Girteka.ElectricAggregate.Integrations;
using Girteka.ElectricAggregate.Persistence;
using Quartz;

namespace Girteka.ElectricAggregate.Job;

public class ElectricityDataDownloaderJob : IJob
{
    private readonly string _csvLocalpPath;
    private readonly ILogger<ElectricityDataDownloaderJob> _logger;

    public ElectricityDataDownloaderJob(IConfiguration configuration, ILogger<ElectricityDataDownloaderJob> logger,
        IFilesService filesService)
    {
        _logger = logger;
        _csvLocalpPath = configuration.GetValue<string>("CsvLocalpPath");
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var uris = new List<string>
        {
            "https://data.gov.lt/dataset/1975/download/10766/2022-05.csv",
            "https://data.gov.lt/dataset/1975/download/10765/2022-04.csv",
            "https://data.gov.lt/dataset/1975/download/10764/2022-03.csv",
            "https://data.gov.lt/dataset/1975/download/10763/2022-02.csv"
        };
        foreach (var uri in uris)
        {
            new DownloadFiles(new CSVFileFromHTTTP(new HttpClient())).Do(_csvLocalpPath,
                uri);
        }

        foreach (var uri in uris)
        {
            new StoreFiles(new CSVFileFromLocalDisk(), new ApplicationDbContext()).Do(_csvLocalpPath,
                uri.Split('/').Last());
        }
    }
}