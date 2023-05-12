namespace Girteka.AggregationApp.Models;

public class ElectricityData
{
    public int Id { get; set; }
    public string Tinklas { get; set; }
    public string Butas { get; set; }
    public DateTime Data { get; set; }
    public double PPlus { get; set; }
    public double PMinus { get; set; }
}