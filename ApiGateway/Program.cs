using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile(path: "ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot();
var app = builder.Build();

await app.UseOcelot();

app.Run();
