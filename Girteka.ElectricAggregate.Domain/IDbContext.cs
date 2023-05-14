using Girteka.ElectricAggregate.Domain.Logger;
using Girteka.ElectricAggregate.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Girteka.ElectricAggregate.Domain;

public interface IDbContext
{
    public DbSet<Electricity> Electricities { get; set; }

    public DbSet<Log> Logs { get; set; }

    public Task SaveChangesAsync();
}