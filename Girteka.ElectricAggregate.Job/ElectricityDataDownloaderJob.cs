using Girteka.ElectricAggregate.Domain.DownloadCsvFiles;
using Girteka.ElectricAggregate.Domain.Services;
using Girteka.ElectricAggregate.Domain.TransforCsvFiles;
using Girteka.ElectricAggregate.Integrations;
using Quartz;

namespace Girteka.ElectricAggregate.Job;

public class ElectricityDataDownloaderJob : IJob
{
    private readonly string _csvLocalpPath;
    private readonly IDownloadCsvFiles _donwloadCsvFiles;
    private readonly ILogger<ElectricityDataDownloaderJob> _logger;
    private readonly ILoadCsvFiles _loadCsvFiles;
    private readonly IFilesService _filesService;

    public ElectricityDataDownloaderJob(IConfiguration configuration, IDownloadCsvFiles donwloadCsvFiles,
        ILoadCsvFiles loadCsvFiles, ILogger<ElectricityDataDownloaderJob> logger, IFilesService filesService)
    {
        _donwloadCsvFiles = donwloadCsvFiles;
        _loadCsvFiles = loadCsvFiles;
        _logger = logger;
        _filesService = filesService;
        _csvLocalpPath = configuration.GetValue<string>("CsvLocalpPath");
    }

    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            // new CSVFileFromHTTTP(null, new HttpClient()).Do("2022-04.csv");
            //
            // new CSVFileFromLocalDisk().Do("2022-05.csv");


            _logger.LogInformation("Start Execute Job");
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
                "2022-04.csv",
                "2022-03.csv",
                "2022-02.csv"
            };

            _filesService.Execute(fileNames);


            await _donwloadCsvFiles.Do(_csvLocalpPath, uris);
            await _loadCsvFiles.Do(fileNames, _csvLocalpPath);

            _logger.LogInformation("Job complete work Successfully");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }
}