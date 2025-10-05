using Gateway.Api.Persistence;
using Microsoft.EntityFrameworkCore;

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