using Discount.Grpc.Data;
using Discount.Grpc.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

/**********************************/
/* Add services to the container. */
/**********************************/

// Add Grpc
builder.Services.AddGrpc();

// Add Sql Lite
builder.Services.AddDbContext<DiscountContext>(opts =>
        opts.UseSqlite(builder.Configuration.GetConnectionString("Database")));

var app = builder.Build();

/****************************************/
/* Configure the HTTP request pipeline. */
/****************************************/

// Create/Seed/Migrate the SQL Lite Db
app.UseMigration();

// Add Grpc Services
app.MapGrpcService<DiscountService>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
