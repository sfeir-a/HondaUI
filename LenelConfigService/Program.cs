using LenelConfigService.Data;
using LenelConfigService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ----- SQL Server connection (Docker container on localhost) -----
builder.Services.AddDbContext<ConfigContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("LenelExtractDB")));

// ----- DI registrations -----
builder.Services.AddScoped<IConfigService, ConfigService>();

builder.Services.AddControllers();

// Swagger for quick testing
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.MapControllers();

app.Run();
