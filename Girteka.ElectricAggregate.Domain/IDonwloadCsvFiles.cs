namespace Girteka.ElectricAggregate.Domain;

public interface IDonwloadCsvFiles
{
    public Task Do(string path, List<Uri> uris);
}