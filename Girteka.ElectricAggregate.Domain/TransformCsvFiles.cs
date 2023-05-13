using Girteka.ElectricAggregate.Domain;
using Girteka.ElectricAggregate.Domain.Models;

namespace Girteka.ElectricAggregate.Job;

public class TransformCsvFiles
{
    private readonly List<Electricity> _records;
    private readonly IDbContext _dbContext;

    public TransformCsvFiles(List<Electricity> records)
    {
        _records = records;
    }

    public async Task Do()
    {
        //using var transaction = _dbContext.BeginTransaction();

        //foreach (var item in _records)
        //{
            _dbContext.Add<Electricity>(_records.FirstOrDefault());
        //}

        _dbContext.SaveChanges();
        //transaction.Commit();
    }
}