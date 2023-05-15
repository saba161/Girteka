using Girteka.ElectricAggregate.Domain;
using Girteka.ElectricAggregate.Domain.DownloadCsvFiles;
using Girteka.ElectricAggregate.Domain.Models;
using Girteka.ElectricAggregate.Domain.Services;
using Girteka.ElectricAggregate.Domain.TransforCsvFiles;
using Girteka.ElectricAggregate.Integrations;
using Girteka.ElectricAggregate.Job;
using Girteka.ElectricAggregate.Persistence;
using Girteka.ElectricAggregate.Persistence.Logger;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;
using DonwloadCsvFiles = Girteka.ElectricAggregate.Domain.DownloadCsvFiles.DownloadCsvFiles;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((ctx, services) =>
    {
        var connectionString = ctx.Configuration.GetConnectionString("DefaultConnection");

        services.AddScoped<IDownloadCsvFiles, DonwloadCsvFiles>();
        services.AddScoped<ILoadCsvFiles, LoadCsvFiles>();
        services.AddScoped<IDbContext, ApplicationDbContext>();
        services.AddScoped<IFileArchive, FileArchive>();
        services.AddScoped<IContext<string, Stream>, CSVFileFromHTTTP>();
        services.AddScoped<IContext<string, Stream>, CSVFileFromLocalDisk>();
        services.AddScoped<IFilesService, FilesService>();
        services.AddTransient<HttpClient>();

        // Add HttpClient service
        services.AddTransient<CSVFileFromHTTTP>();
        services.AddLogging(builder =>
        {
            builder.AddProvider(new DatabaseLoggerProvider(
                (category, level) => level >= LogLevel.Information, connectionString));
        });

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