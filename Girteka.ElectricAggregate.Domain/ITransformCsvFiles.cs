namespace Girteka.ElectricAggregate.Domain;

public interface ITransformCsvFiles
{
    Task Do(List<string> fileNames, string path);
}