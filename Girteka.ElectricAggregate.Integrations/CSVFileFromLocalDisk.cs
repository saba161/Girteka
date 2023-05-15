namespace Girteka.ElectricAggregate.Integrations;

public class CSVFileFromLocalDisk
{
    public Stream Do(string input)
    {
        var filePath = input;
        return File.Open(filePath, FileMode.Open);
    }
}