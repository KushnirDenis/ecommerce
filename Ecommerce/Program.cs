var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddControllers();
services.AddRouting();

var app = builder.Build();

app.UseRouting();
app.MapControllers();
app.MapGet("/", () => "Hello World!");

app.Run();