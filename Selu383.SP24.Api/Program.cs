using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Selu383.SP24.Api.Data;
using Selu383.SP24.Api.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DataContext")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

 using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DataContext>();
    db.Database.Migrate();

    if (!db.Hotels.Any())
    {
        db.Hotels.Add(new Hotel
        {
            Name = "hotel",
            Address = "123 hotel st"
        });

        db.Hotels.Add(new Hotel
        {
            Name = "hotel2",
            Address = "456 hotel st"
        });

        db.Hotels.Add(new Hotel
        {
            Name = "hotel3",
            Address = "789 hotel st"
        });

        db.SaveChanges();
    }

    
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

//see: https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-8.0
// Hi 383 - this is added so we can test our web project automatically
public partial class Program { }