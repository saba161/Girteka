namespace Girteka.ElectricAggregate.Domain.Services;

public interface IFilesService
{
    void Execute(List<string> fileNames);
}