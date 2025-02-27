using Microsoft.EntityFrameworkCore;
using ProductService.Database;
using ProductService.Services;

var builder = WebApplication.CreateBuilder(args);

// Configura il DbContext per Prodotti con database SQLite (file products.db)
builder.Services.AddDbContext<ProductDB>(options =>
    options.UseSqlite("Data Source=products.db"));

// Registra il servizio di logica prodotti
builder.Services.AddScoped<ServiceProduct>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

builder.Services.AddControllers();
var app = builder.Build();

// Porta HTTP per il microservizio Prodotti (5002)
app.Urls.Add("http://localhost:5002");

app.UseCors("AllowAll");
app.MapControllers();

// Crea il database e tabella Prodotti se non esistono
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ProductDB>();
    db.Database.EnsureCreated();
}

app.Run();
