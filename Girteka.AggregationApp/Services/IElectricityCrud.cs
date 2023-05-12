using Girteka.AggregationApp.Models.Entity;

namespace Girteka.AggregationApp.Services;

public interface IElectricityCrud
{
    void Create(List<ElectricityEntity> records);
}