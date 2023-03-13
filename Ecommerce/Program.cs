using System.Text.Json.Serialization;
using Ecommerce.DAL;
using Ecommerce.Models;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddScoped<AppDbContext>();

services.AddScoped<IValidator<CreateCategoryDto>, CreateCategoryDtoValidator>();
services.AddScoped<IValidator<AddNameInAnotherLanguageDto>, 
    AddNameInAnotherLanguageDtoValidator>();

services.AddLocalization();
services.AddApiVersioning();
services.AddControllers()
    .AddJsonOptions(x =>
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
;
services.AddRouting();


var app = builder.Build();

var supportedCultures = new[] { "uk", "ru" };
app.UseRequestLocalization(options =>
{
    options.AddSupportedCultures(supportedCultures)
        .AddSupportedUICultures(supportedCultures)
        .SetDefaultCulture("uk");
    options.FallBackToParentCultures = true;
});

app.UseStaticFiles();
app.UseApiVersioning();
app.UsePathBase("/api/");
app.UseRouting();
app.MapControllers();
app.MapGet("/", () => "Hello World!");

app.Run();