using Girteka.ElectricAggregate.Domain;
using Girteka.ElectricAggregate.Domain.Models;

namespace Girteka.ElectricAggregate.Persistence;

public class GetElectricity : IElectricity
{
    private readonly IDbContext _dbContext;

    public GetElectricity(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Electricity>> Get()
    {
        var result = _dbContext.Electricities.ToList();
        return result;
    }
}