namespace Girteka.ElectricAggregate.UnitTests;

public class HttpDownloadTests
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

    [TearDown]
    public void Teardown()
    {
        _httpClient.Dispose();
    }

    [Test]
    public void Sum()
    {
        // Arrange
        var a = 1;
        var b = 2;
        
        var expected = 3;
        
        // Act
        var sum = a + b;
        
        // Assert
        Assert.AreEqual(sum, expected);
    }
}