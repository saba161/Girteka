using Girteka.ElectricAggregate.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Girteka.ElectricAggregate.Domain;

public interface IDbContext
{
    public DbSet<Electricity> Electricities { get; set; }
    public void SaveChanges();

    public void Add<T>(T obj)
        where T : class;
}