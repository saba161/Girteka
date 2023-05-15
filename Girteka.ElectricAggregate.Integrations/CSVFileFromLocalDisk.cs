using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Girteka.ElectricAggregate.Domain;
using Girteka.ElectricAggregate.Domain.Mappers;
using Girteka.ElectricAggregate.Domain.Models;

namespace Girteka.ElectricAggregate.Integrations;

public class CSVFileFromLocalDisk : IContext<string, Stream>
{
    public Stream Do(string input)
    {
        string filePath = Path.Combine("/Users/sabakoghuashvili/Desktop/Temp/", input);

        return File.Open(filePath, FileMode.Open);
    }
}