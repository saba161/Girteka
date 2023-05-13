using Girteka.ElectricAggregate.Domain;
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
        var uris = new List<Uri>
        {
            //TODO we should write this uri in config
            new Uri("https://data.gov.lt/dataset/1975/download/10766/2022-05.csv"),
            new Uri("https://data.gov.lt/dataset/1975/download/10765/2022-04.csv"),
            new Uri("https://data.gov.lt/dataset/1975/download/10764/2022-03.csv"),
            new Uri("https://data.gov.lt/dataset/1975/download/10763/2022-02.csv")
        };

        var fileNames = new List<string>
        {
            "2022-05.csv",
            // "2022-04.csv",
            // "2022-03.csv",
            // "2022-02.csv"
        };

        //await new DonwloadCsvFiles(uris).Do();

        var readedFiles = await new ReadCsvFiles(fileNames, "/Users/sabakoghuashvili/Desktop/Data/").Do();

        await new TransformCsvFiles(readedFiles).Do();
    }
}