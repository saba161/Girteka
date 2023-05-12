using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace Girteka.AggregationApp.Converters;

public class CustomDecimalConverter : DecimalConverter
{
    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return 0m;
        }

        return base.ConvertFromString(text, row, memberMapData);
    }
}