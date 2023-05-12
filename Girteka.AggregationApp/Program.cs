using Girteka.AggregationApp.Content;
using Girteka.AggregationApp.Content.Http;

var builder = WebApplication.CreateBuilder(args);

// Add configuration
builder.Configuration.AddJsonFile("appsettings.json", optional: true);

// Get the CsvUrl setting value
var csvHttpUrl = builder.Configuration.GetValue<string>("CsvHttpUrl");
var csvLocalpPath = builder.Configuration.GetValue<string>("CsvLocalpPath");

// Add services to the container.
builder.Services.AddScoped<IHttpCsvContent>(s => new HttpCsvContent(csvHttpUrl));
builder.Services.AddScoped<ILocalCsvContent>(s => new LocalCsvContent(csvLocalpPath));

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