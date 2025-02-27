using Microsoft.EntityFrameworkCore;
using AddressService.Database;
using AddressService.Services;

var builder = WebApplication.CreateBuilder(args);

// Configura il DbContext per Indirizzi con SQLite (address.db)
builder.Services.AddDbContext<AddressDB>(options =>
    options.UseSqlite("Data Source=address.db"));

builder.Services.AddScoped<ServiceAddress>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

builder.Services.AddControllers();
var app = builder.Build();

// Porta HTTP per il microservizio Indirizzi (5004)
app.Urls.Add("http://localhost:5004");

app.UseCors("AllowAll");
app.MapControllers();

// Crea il database/tabella Indirizzi se non esiste
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AddressDB>();
    db.Database.EnsureCreated();
}

app.Run();
