
using Infrastructure.Exceptions.Handler;

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

// Exception Handlers
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

/* Build the App */
var app = builder.Build();

/* Configure the HTTP request pipeline. */

// Carter
app.MapCarter();

// Use Exceptions
app.UseExceptionHandler(options => { });

app.Run();
