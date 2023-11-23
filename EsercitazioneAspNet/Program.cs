using Microsoft.EntityFrameworkCore;
using EsercitazioneAspNet.Models;
using EsercitazioneAspNet.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<SoluzioneBankomatContext>(cfg =>
{
    var connectionString = "Server=B80MI-SCM120\\SQLEXPRESS;Database=SoluzioneBankomat;User Id=sa;Password=password123;";

    //ServerVersion sv = ServerVersion.AutoDetect(connectionString);
    //var serverVersion = new MySqlServerVersion(sv.Version);

    cfg.UseSqlServer(connectionString /*serverVersion*/)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors();

});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUtentiDbRepository, UtentiDbRepository>();
builder.Services.AddScoped<IBancaDbRepository, BancaDbRepository>();
builder.Services.AddScoped<IFunzionalitaDbRepository, FunzionalitaDbRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
