using Girteka.AggregationApp.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace Girteka.AggregationApp.Db;

public class AppConnection : DbContext
{
    public AppConnection(DbContextOptions<AppConnection> options)
        : base(options)
    {
        System.Net.ServicePointManager.ServerCertificateValidationCallback +=
            (sender, cert, chain, sslPolicyErrors) => true;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ElectricityEntity>()
            .Property(e => e.PPlus)
            .HasColumnType("decimal(18, 2)");
        
        modelBuilder.Entity<ElectricityEntity>()
            .Property(e => e.PMinus)
            .HasColumnType("decimal(18, 2)");
    }

    public virtual DbSet<ElectricityEntity> Electricities { get; set; }
}