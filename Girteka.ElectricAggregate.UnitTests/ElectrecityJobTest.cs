namespace Girteka.ElectricAggregate.UnitTests;

public class ElectrecityJobTest
{
    private HttpClient _httpClient;

    [SetUp]
    public void Setup()
    {
        _httpClient = new HttpClient();
    }


    [Test]
    public async Task CanDownloadCsvFiles()
    {
        var csvFileUrls = new[]
        {
            "https://data.gov.lt/dataset/1975/download/10766/2022-05.csv",
            "https://data.gov.lt/dataset/1975/download/10765/2022-04.csv",
            "https://data.gov.lt/dataset/1975/download/10764/2022-03.csv",
            "https://data.gov.lt/dataset/1975/download/10763/2022-02.csv"
        };

        foreach (var csvFileUrl in csvFileUrls)
        {
            var response = await _httpClient.GetAsync(csvFileUrl);

            response.EnsureSuccessStatusCode();

            var csvFileContent = await response.Content.ReadAsStringAsync();
            Assert.IsNotEmpty(csvFileContent);
        }
    }

    [Test]
    public async Task CanReadCsvFiles()
    {
        FileStream result = null;
        var localFiles = new[]
        {
            "/Users/sabakoghuashvili/Desktop/Temp/2022-05.csv",
            "/Users/sabakoghuashvili/Desktop/Temp/2022-04.csv"
        };

        foreach (var path in localFiles)
        {
            string fileName = path.Split('/').Last();

            result = File.Open(path, FileMode.Open);
            
            Assert.IsTrue(result.Length > 0);
            result.Close();
        }
    }

    [TearDown]
    public void Teardown()
    {
        _httpClient.Dispose();
    }
}