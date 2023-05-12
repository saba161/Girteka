using Girteka.AggregationApp.Content;
using Girteka.AggregationApp.Content.Http;
using Girteka.AggregationApp.Db;
using Girteka.AggregationApp.Job;
using Girteka.AggregationApp.Services;
using Microsoft.EntityFrameworkCore;
using Quartz;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add configuration
builder.Configuration.AddJsonFile("appsettings.json", optional: true);

// Get the CsvUrl setting value
var csvHttpUrl = builder.Configuration.GetValue<string>("CsvHttpUrl");
var csvLocalpPath = builder.Configuration.GetValue<string>("CsvLocalpPath");

// Add services to the container.
builder.Services.AddScoped<IHttpCsvContent>(s => new HttpCsvContent(csvHttpUrl));
builder.Services.AddScoped<ILocalCsvContent>(s => new LocalCsvContent(csvLocalpPath));
builder.Services.AddScoped<IElectricityCrud, ElectricityCrud>();

builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionScopedJobFactory();
    // Just use the name of your job that you created in the Jobs folder.
    var jobKey = new JobKey("YearlyJob");
    q.AddJob<YearlyJob>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("YearlyJob-trigger")
        .StartNow()
        .WithSimpleSchedule(x => x
            .WithInterval(TimeSpan.FromDays(365))
            .RepeatForever()));
});
builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

builder.Services.AddDbContext<AppConnection>(options =>
    options.UseSqlServer(configuration.GetConnectionString("ConnStr")));


builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();