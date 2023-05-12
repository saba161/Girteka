using Girteka.AggregationApp.Db;
using Girteka.AggregationApp.Models.Entity;

namespace Girteka.AggregationApp.Services;

public class ElectricityCrud : IElectricityCrud
{
    private readonly AppConnection _dbContext;

    public ElectricityCrud(AppConnection dbContext)
    {
        _dbContext = dbContext;
    }

    public void Create(List<ElectricityEntity> records)
    {
        using var transaction = _dbContext.Database.BeginTransaction();

        foreach (var item in records)
        {
            _dbContext.Add<ElectricityEntity>(item);
        }

        _dbContext.SaveChanges();
        transaction.Commit();
    }
}