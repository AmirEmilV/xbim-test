using xBimApp.Api.Exceptions;
using XBimApp.Api;
using XBimApp.Api.Extentions;

/// <summary>
/// Initializes a new instance of the <see cref="$Program"/> class.
/// </summary>
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = GetConfiguration();

builder.Services.AddOptions();
builder.Services.Configure<AppSettings>(configuration);
var appSettings = configuration.Get<AppSettings>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddServices();

//builder.Services.AddServicesAuthentication(appSettings,builder);


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();

if (app.Environment.IsDevelopment())
{
  app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();


IConfiguration GetConfiguration(/*IWebHostEnvironment webHostEnvironment*/)
{
  string hostingEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

  var builder = new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
      .AddJsonFile($"appsettings.{hostingEnvironment}.json", optional: false, reloadOnChange: true)
      .AddEnvironmentVariables();
  var config = builder.Build();

  return config;
}