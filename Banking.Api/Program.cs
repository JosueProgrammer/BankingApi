using Banking.Application.DependencyInjection;
using Banking.Infrastructure.DependencyInjection;
using Banking.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddApplication();

builder.Services.AddInfrastructure(
    builder.Configuration);


// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var apiXmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var apiXmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, apiXmlFile);
    if (System.IO.File.Exists(apiXmlPath))
    {
        c.IncludeXmlComments(apiXmlPath);
    }

    var appXmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, "Banking.Application.xml");
    if (System.IO.File.Exists(appXmlPath))
    {
        c.IncludeXmlComments(appXmlPath);
    }
});


var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.MapControllers();

app.Run();