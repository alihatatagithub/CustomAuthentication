using ECommerce.Presistance.DependencyResolution;
using ECommerce.Ground;
using ECommerce.Service.DependencyResolution;

var builder = WebApplication.CreateBuilder(args);

//builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
//                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
//                .AddEnvironmentVariables();
//// make configuration available to infrastructure via Configurations static
Configurations.ConfigurationManager = builder.Configuration;

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

builder.Services.AddPostgresDbContext();
builder.Services.AddCustomAuthentication();
builder.Services.AddInfrastructure();
builder.Services.AddCoreService();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

 app.UseCors("AllowAngular"); // Commented out to trigger CORS error for testing/demonstration

app.UseAuthorization();

app.MapControllers();

app.Run();
