using Girteka.ElectricAggregate.Domain;
using Quartz;

namespace Girteka.ElectricAggregate.Job;

public class ElectricityDataDownloaderJob : IJob
{
    private readonly string _csvHttpUrl;
    private readonly string _csvLocalpPath;
    private readonly IDonwloadCsvFiles _donwloadCsvFiles;
    private readonly ITransformCsvFiles _transformCsvFiles;

    public ElectricityDataDownloaderJob(IConfiguration configuration, IDonwloadCsvFiles donwloadCsvFiles,
        ITransformCsvFiles transformCsvFiles)
    {
        _donwloadCsvFiles = donwloadCsvFiles;
        _transformCsvFiles = transformCsvFiles;
        _csvHttpUrl = configuration.GetValue<string>("CsvHttpUrl");
        _csvLocalpPath = configuration.GetValue<string>("CsvLocalpPath");
    }

    public async Task Execute(IJobExecutionContext context)
    {
        try
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

            await _donwloadCsvFiles.Do(_csvLocalpPath, uris);
            await _transformCsvFiles.Do(fileNames, _csvLocalpPath);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}