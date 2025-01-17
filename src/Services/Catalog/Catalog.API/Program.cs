using FluentValidation;
using Infrastructure.Behaviors;

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
});

// Fluent Validation
builder.Services.AddValidatorsFromAssembly(assembly);

// Marten
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

/* Build the App */
var app = builder.Build();

/* Configure the HTTP request pipeline. */

// Carter
app.MapCarter();

app.Run();
