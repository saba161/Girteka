using Girteka.ElectricAggregate.Domain;
using Girteka.ElectricAggregate.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Girteka.ElectricAggregate.Persistence;

public class ApplicationDbContext : DbContext, IDbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        System.Net.ServicePointManager.ServerCertificateValidationCallback +=
            (sender, cert, chain, sslPolicyErrors) => true;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Data Source=localhost;Initial Catalog=Electricity;User Id=sa; Password=YourStrongP@ssword; TrustServerCertificate=True;");
    }

    public virtual DbSet<Electricity> Electricities { get; set; }

    public void SaveChanges()
    {
        base.SaveChanges();
    }

    public void Add<T>(T obj)
        where T : class
    {
        base.Add(obj);
    }
}