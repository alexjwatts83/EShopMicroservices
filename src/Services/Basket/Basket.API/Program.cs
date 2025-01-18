using HealthChecks.UI.Client;
using Infrastructure.Exceptions.Handler;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

/******************************************/
/* Add Services to container and other DI */
/******************************************/
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

    opts.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();

// TODO: maybe consider creating an extension method to build up all the data related services
// Add Services
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();

// Add Redis
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    //options.InstanceName = "Basket";
});

// Exception Handlers
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

// Health Checks
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!);

/*****************/
/* Build the App */
/*****************/
var app = builder.Build();

/****************************************/
/* Configure the HTTP request pipeline. */
/****************************************/

// Carter
app.MapCarter();

// Exceptions
app.UseExceptionHandler(options => { });

// Health Checks
app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();
