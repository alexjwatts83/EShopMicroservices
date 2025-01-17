var builder = WebApplication.CreateBuilder(args);

/* Add Services to container and other DI */

/* Build the App */
var app = builder.Build();

/* Configure the HTTP request pipeline. */

app.Run();
