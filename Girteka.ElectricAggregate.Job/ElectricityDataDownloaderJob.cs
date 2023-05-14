using Girteka.ElectricAggregate.Domain.DownloadCsvFiles;
using Girteka.ElectricAggregate.Domain.TransforCsvFiles;
using Quartz;

namespace Girteka.ElectricAggregate.Job;

public class ElectricityDataDownloaderJob : IJob
{
    private readonly string _csvLocalpPath;
    private readonly IDownloadCsvFiles _donwloadCsvFiles;
    private readonly ILogger<ElectricityDataDownloaderJob> _logger;
    private readonly ILoadCsvFiles _loadCsvFiles;

    public ElectricityDataDownloaderJob(IConfiguration configuration, IDownloadCsvFiles donwloadCsvFiles,
        ILoadCsvFiles loadCsvFiles, ILogger<ElectricityDataDownloaderJob> logger)
    {
        _donwloadCsvFiles = donwloadCsvFiles;
        _loadCsvFiles = loadCsvFiles;
        _logger = logger;
        _csvLocalpPath = configuration.GetValue<string>("CsvLocalpPath");
    }

    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            _logger.LogInformation("HI");
            var uris = new List<Uri>
            {
                new Uri("https://data.gov.lt/dataset/1975/download/10766/2022-05.csv"),
                new Uri("https://data.gov.lt/dataset/1975/download/10765/2022-04.csv"),
                new Uri("https://data.gov.lt/dataset/1975/download/10764/2022-03.csv"),
                new Uri("https://data.gov.lt/dataset/1975/download/10763/2022-02.csv")
            };

            var fileNames = new List<string>()
            {
                "2022-05.csv",
                //"2022-04.csv",
                //"2022-03.csv",
                //"2022-02.csv"
            };

            //await _donwloadCsvFiles.Do(_csvLocalpPath, uris);
            await _loadCsvFiles.Do(fileNames, _csvLocalpPath);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}