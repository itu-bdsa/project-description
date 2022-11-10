using Microsoft.EntityFrameworkCore;
using GitInsight.Entities;
using GitInsight;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
/*builder.Services.AddDbContext<GitInsightContext>(opt =>
    opt.UseInMemoryDatabase("GetInsight"));*/


//--------------Real database setup---------------------
//Naviger til GitInsight folder og kør disse to commands i terminal.
//Husk at udskift database, username og password med dit eget

//$CONNECTION_STRING="Host=localhost;Database=GitDB;Username=postgres;Password=bianca3";
//dotnet user-secrets set "ConnectionStrings:GitIn" "$CONNECTION_STRING"

var configuration = new ConfigurationBuilder().AddUserSecrets<GitInsightClass>().Build();
var connectionString = configuration.GetConnectionString("GitIn");

//hvis ovenstående ikke fungerer, brug denne i stedet. Husk at udskifte info og udkommentere ovenstående
//var connectionString = @"Host=localhost;Database=GitDB;Username=postgres;Password=bianca3";


builder.Services.AddDbContext<GitInsightContext>(options =>
     options.UseNpgsql(connectionString)); //options.UseSqlServer(connectionString));
     //lortet funker kun med Npgsql server, dunno why but it works

//-------------------------------------

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "GetInsight", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GetInsight v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();