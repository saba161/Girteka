using Girteka.ElectricAggregate.Domain;
using Girteka.ElectricAggregate.Job;
using Girteka.ElectricAggregate.Persistence;
using Quartz;
using DonwloadCsvFiles = Girteka.ElectricAggregate.Job.DonwloadCsvFiles;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddScoped<IDonwloadCsvFiles, DonwloadCsvFiles>();
        services.AddScoped<ITransformCsvFiles, TransformCsvFiles>();
        services.AddScoped<IDbContext, ApplicationDbContext>();
        
        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionScopedJobFactory();
            // Just use the name of your job that you created in the Jobs folder.
            var jobKey = new JobKey("YearlyJob");
            q.AddJob<ElectricityDataDownloaderJob>(opts => opts.WithIdentity(jobKey));

            q.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity("YearlyJob-trigger")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithInterval(TimeSpan.FromDays(365))
                    .RepeatForever()));
        });
        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
    })
    .Build();

host.Run();