using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

public class ElectricityData
{
    [Name("TINKLAS")] 
    public string Region { get; set; }

    [Name("OBT_PAVADINIMAS")] 
    public string Type { get; set; }

    [Name("P+")] 
    public decimal? PPlus { get; set; }

    [Name("P-")] 
    public decimal? PMinus { get; set; }
}

public static class ElectricityDataClient
{
    private static readonly HttpClient _httpClient = new HttpClient();

    private static readonly CsvConfiguration _csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
    {
        HasHeaderRecord = true,
        HeaderValidated = null,
        MissingFieldFound = null
    };

    public static async Task<IEnumerable<ElectricityData>> GetElectricityData()
    {
        var urls = new[]
        {
            "https://data.gov.lt/dataset/1975/download/10766/2022-05.csv",
            "https://data.gov.lt/dataset/1975/download/10765/2022-04.csv",
            "https://data.gov.lt/dataset/1975/download/10764/2022-03.csv",
            "https://data.gov.lt/dataset/1975/download/10763/2022-02.csv"
        };

        var allData = new List<ElectricityData>();

        foreach (var url in urls)
        {
            var httpClientHandler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            var httpClient = new HttpClient(httpClientHandler);

            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var csvString = await response.Content.ReadAsStringAsync();

            using var csv = new CsvReader(new StringReader(csvString),
                new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    HeaderValidated = null,
                    MissingFieldFound = null
                });
            var data = csv.GetRecords<ElectricityData>().Where(d => d.Type == "Butas");
            allData.AddRange(data);
        }

        return allData;
    }
}