namespace Girteka.ElectricAggregate.Domain.Services;

public interface IFilesService
{
    void Execute(List<string> fileNames, string csvLocalpPath, List<string> httpPath);
}