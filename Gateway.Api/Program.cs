using Gateway.Api.Catalog;
using Gateway.Api.Persistence;
using Microsoft.EntityFrameworkCore;
using Rules.Abstractions.Catalog;
using Rules.Abstractions.Compilation;
using Rules.Abstractions.Validation;
using Rules.Engine.Catalog;

var builder = WebApplication.CreateBuilder(args);
var cs = builder.Configuration.GetConnectionString("Db");

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseNpgsql(cs);
    opt.UseSnakeCaseNamingConvention();
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Catalog cache & loader
builder.Services.AddSingleton<CatalogProvider>();
builder.Services.AddSingleton<ICatalogProvider>(sp => sp.GetRequiredService<CatalogProvider>());
builder.Services.AddSingleton<ICatalogUpdateNotifier>(sp => sp.GetRequiredService<CatalogProvider>());

// Раскоментить как будет реализовано
/*// Adapter for Rule Engine
builder.Services.AddScoped<IRuleCatalog, RuleCatalogAdapter>();

// Rule Engine services
builder.Services.AddScoped<IRuleValidator, RuleValidator>();
builder.Services.AddSingleton<IRuleCompiler, RuleCompiler>();*/

// Loader at startup
builder.Services.AddHostedService<CatalogLoaderHostedService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();