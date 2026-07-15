using Banking.Application.DependencyInjection;
using Banking.Infrastructure.DependencyInjection;
using Banking.Api.Middleware;
using Banking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Application & Infrastructure layers
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// Health check for deployment monitoring
builder.Services.AddHealthChecks();

// CORS - allow external consumption
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Swagger / OpenAPI available in all environments
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Banking API",
        Version = "v1",
        Description = "RESTful API for managing customers, bank accounts, and financial transactions. " +
                      "Supports deposits, withdrawals, and paginated transaction history with idempotency.",
        Contact = new OpenApiContact
        {
            Name = "Banking API Team"
        }
    });

    var apiXmlFile =
        $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";

    var apiXmlPath =
        Path.Combine(AppContext.BaseDirectory, apiXmlFile);

    if (File.Exists(apiXmlPath))
    {
        c.IncludeXmlComments(apiXmlPath);
    }


    var applicationXmlPath =
        Path.Combine(
            AppContext.BaseDirectory,
            "Banking.Application.xml");


    if (File.Exists(applicationXmlPath))
    {
        c.IncludeXmlComments(applicationXmlPath);
    }
});


var app = builder.Build();


// Apply migrations automatically on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider
        .GetRequiredService<BankingDbContext>();

    db.Database.Migrate();
}


// Global exception handling middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();


// Swagger available in all environments
app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint(
        "/swagger/v1/swagger.json",
        "BankingApi v1");

    c.RoutePrefix = "swagger";
});


// CORS
app.UseCors("AllowAll");


// Health check endpoint
app.MapHealthChecks("/health");


// Controllers
app.MapControllers();


app.Run();