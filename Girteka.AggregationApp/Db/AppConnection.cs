using Microsoft.EntityFrameworkCore;

namespace Girteka.AggregationApp.Db;

public class AppConnection : DbContext
{
    public AppConnection(DbContextOptions<AppConnection> options)
        : base(options)
    {
    }
}