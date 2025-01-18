using Infrastructure.Exceptions.Handler;

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

/*****************/
/* Build the App */
/*****************/
var app = builder.Build();

/****************************************/
/* Configure the HTTP request pipeline. */
/****************************************/

// Carter
app.MapCarter();

// Use Exceptions
app.UseExceptionHandler(options => { });

app.Run();
