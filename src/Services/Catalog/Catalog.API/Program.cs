
using Catalog.API.Data;
using HealthChecks.UI.Client;
using Infrastructure.Exceptions.Handler;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

/* Add Services to container and other DI */
var assembly = typeof(Program).Assembly;

// Carter
builder.Services.AddCarter();

// Mediator
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

// Fluent Validation
builder.Services.AddValidatorsFromAssembly(assembly);

// Marten
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

// Marten seed data
if (builder.Environment.IsDevelopment())
    builder.Services.InitializeMartenWith<CatalogInitialData>();

// Exception Handlers
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

// Add Health Checks
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

/* Build the App */
var app = builder.Build();

/* Configure the HTTP request pipeline. */

// Carter
app.MapCarter();

// Use Exceptions
app.UseExceptionHandler(options => { });

// Use Health Checks
app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();
