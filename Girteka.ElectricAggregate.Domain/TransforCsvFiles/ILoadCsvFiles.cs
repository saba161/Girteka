namespace Girteka.ElectricAggregate.Domain.TransforCsvFiles;

public interface ILoadCsvFiles
{
    Task Do(List<string> fileNames, string path);
}