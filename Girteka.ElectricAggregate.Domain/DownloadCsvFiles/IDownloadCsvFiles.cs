namespace Girteka.ElectricAggregate.Domain.DownloadCsvFiles;

public interface IDownloadCsvFiles
{
    public Task Do(string path, List<Uri> uris);
}