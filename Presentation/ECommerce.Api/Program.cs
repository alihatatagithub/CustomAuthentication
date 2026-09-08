using ECommerce.Contract;
using ECommerce.Ground;
using ECommerce.Presistance;
using ECommerce.Presistance.DependencyResolution;
using ECommerce.Service.DependencyResolution;
using Microsoft.OpenApi;
using Serilog;

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

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token"
    });

    options.AddSecurityRequirement(document =>
        new OpenApiSecurityRequirement
        {
            [new OpenApiSecuritySchemeReference("Bearer", document)] = []
        });
});
builder.Services.AddOpenApi();

builder.Services.AddPostgresDbContext();
builder.Services.AddCustomAuthentication();
builder.Services.AddInfrastructure();
builder.AddSerilog();
builder.Services.AddCoreService();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider
        .GetRequiredService<AppDbContext>();

    var jwtToken = scope.ServiceProvider
    .GetRequiredService<IJwtToken>();

    var passwordHasher = scope.ServiceProvider
    .GetRequiredService<IPasswordHasher>();

    //await context.Database.MigrateAsync();

    await AppDbContextSeed.SeedAsync(context, jwtToken, passwordHasher);
}

var supportedCultures = new[] { "en", "ar" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture("en")
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

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
app.UseSerilogRequestLogging();

app.MapControllers();

app.Run();
